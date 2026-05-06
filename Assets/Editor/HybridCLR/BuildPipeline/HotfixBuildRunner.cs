using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Framework.Assemblies;
using HybridCLR.Editor.Commands;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace HybridCLR.Editor
{
    public static class HotfixBuildRunner
    {
        public static HotfixBuildReport ValidateOnly(HotfixBuildMode mode)
        {
            var context = HotfixBuildContext.Create(mode);
            if (context.ReleaseProfile != null)
            {
                context.ReleaseProfile.ApplyToEditorSettings();
                context = HotfixBuildContext.Create(mode);
            }

            return HotfixBuildValidator.Validate(context);
        }

        public static HotfixBuildReport FixAll(HotfixBuildMode mode)
        {
            var context = HotfixBuildContext.Create(mode);
            var releaseProfile = context.ReleaseProfile ?? HotfixReleaseProfile.GetOrCreateDefault();
            HotfixReleaseProfile.SaveSelectedProfile(releaseProfile);
            releaseProfile.ApplyToEditorSettings();
            HotfixBuildProfileUtility.ApplyPlayModeToRuntimeSettings(context.BuildTarget);
            HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return ValidateOnly(mode);
        }

        public static HotfixBuildReport Build(HotfixBuildMode mode)
        {
            var context = HotfixBuildContext.Create(mode);
            if (context.ReleaseProfile == null)
            {
                throw new InvalidOperationException(
                    $"缺少 ReleaseProfile：{HotfixReleaseProfile.DefaultAssetPath}。请在构建中心执行“一键修复”创建并绑定。");
            }

            context.ReleaseProfile.ApplyToEditorSettings();
            context = HotfixBuildContext.Create(mode);
            var report = HotfixBuildValidator.Validate(context);
            if (report.HasErrors)
            {
                throw new InvalidOperationException(
                    "热更新构建被校验错误阻断：\n" + report.BuildErrorSummary());
            }

            HotfixBuildExecutionResult executionResult;
            switch (mode)
            {
                case HotfixBuildMode.InitialPackage:
                    executionResult = BuildInitialPackage(context);
                    break;
                case HotfixBuildMode.HotfixPackage:
                    executionResult = BuildHotfixPackage(context);
                    break;
                case HotfixBuildMode.AOTMetadataPatch:
                    executionResult = BuildAOTMetadataPatch(context);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            report = ValidateOnly(executionResult.Mode);
            executionResult.AppendTo(report);
            WriteBuildReport(executionResult, report);
            report.AddInfo("构建", "完成", HotfixBuildModeUtility.GetDisplayName(mode));
            return report;
        }

        public static HotfixBuildExecutionResult BuildInitialPackage(HotfixBuildContext context)
        {
            Debug.Log("[HotfixBuild] 开始构建首包。");
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            HybridCLRGenerateAllSafe.Run();
            CompileDllCommand.CompileDll(context.BuildTarget);

            var aotAssemblies = BuildAssetsCommand.CopyAOTAssembliesToTargetPath(context.BuildTarget);
            var hotfixAssemblies = BuildAssetsCommand.CopyHotUpdateAssembliesToTargetPath(context.BuildTarget);
            var aotManifest = BuildAssetsCommand.CreateOrUpdateAOTAssemblyManifest(context.BuildTarget, aotAssemblies);
            var hotfixManifest = BuildAssetsCommand.CreateOrUpdateHotfixAssemblyManifest(
                context.BuildTarget,
                hotfixAssemblies,
                aotManifest.AotVersion);
            BuildAssetsCommand.ValidateHotfixAppVersionRange(hotfixManifest);
            BuildAssetsCommand.CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
            BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
            BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget);

            bool copyToStreamingAssets = context.StartupPackageMode != Framework.StartupPackageMode.EmptyPackage;
            var buildResult = BuildAssetsCommand.BuildYooAssetPackage(
                packageConfig.MainPackageName,
                context.BuildTarget,
                copyToStreamingAssets);
            Debug.Log("[HotfixBuild] 首包构建完成。");

            return HotfixBuildExecutionResult.Create(
                context,
                packageConfig.MainPackageName,
                buildResult,
                copyToStreamingAssets,
                aotManifest,
                hotfixManifest,
                false);
        }

        public static HotfixBuildExecutionResult BuildHotfixPackage(HotfixBuildContext context)
        {
            Debug.Log("[HotfixBuild] 开始构建热更包。");
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            var aotManifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(BuildAssetsCommand.AOTAssemblyManifestAssetPath);
            try
            {
                BuildAssetsCommand.ValidateAOTManifestNotExpired(context.BuildTarget, aotManifest);
            }
            catch (Exception exception)
            {
                return ResolveExpiredAOTManifest(context, exception);
            }

            CompileDllCommand.CompileDll(context.BuildTarget);
            var hotfixAssemblies = BuildAssetsCommand.CopyHotUpdateAssembliesToTargetPath(context.BuildTarget);
            var hotfixManifest = BuildAssetsCommand.CreateOrUpdateHotfixAssemblyManifest(
                context.BuildTarget,
                hotfixAssemblies,
                aotManifest.AotVersion);
            BuildAssetsCommand.ValidateHotfixAppVersionRange(hotfixManifest);
            BuildAssetsCommand.CreateOrUpdateAssemblyManifest(aotManifest.AotMetadataAssemblies, hotfixManifest.HotUpdateAssemblies);
            BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
            BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget);

            var buildResult = BuildAssetsCommand.BuildYooAssetPackage(
                packageConfig.MainPackageName,
                context.BuildTarget,
                false);
            Debug.Log("[HotfixBuild] 热更包构建完成。");

            return HotfixBuildExecutionResult.Create(
                context,
                packageConfig.MainPackageName,
                buildResult,
                false,
                aotManifest,
                hotfixManifest,
                false);
        }

        public static HotfixBuildExecutionResult BuildAOTMetadataPatch(HotfixBuildContext context)
        {
            const string message =
                "AOT 元数据补丁只能补充同一 App 基线下的泛型元数据。\n\n" +
                "如果修改了主工程 AOT 代码逻辑、公共接口、原生 SDK 或 PlayerSettings，应发布新 App，而不是发布 AOT 元数据补丁。";

            if (!EditorUtility.DisplayDialog("AOT 元数据补丁", message, "继续构建", "取消"))
            {
                throw new OperationCanceledException("已取消 AOT 元数据补丁构建。");
            }

            var previousAotManifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(BuildAssetsCommand.AOTAssemblyManifestAssetPath);
            if (previousAotManifest == null)
            {
                throw new InvalidOperationException("构建 AOT 元数据补丁前必须存在旧的 AOT 清单。请先构建首包建立 App 基线。");
            }

            if (!string.Equals(previousAotManifest.AppVersion, context.AppVersion, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"AOT 元数据补丁必须绑定同一 AppVersion。Manifest={previousAotManifest.AppVersion}, PlayerSettings={context.AppVersion}");
            }

            if (!string.Equals(previousAotManifest.BuildTarget, context.BuildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"AOT 元数据补丁必须绑定同一构建目标。Manifest={previousAotManifest.BuildTarget}, Build={context.BuildTargetName}");
            }

            Debug.Log("[HotfixBuild] 开始构建 AOT 元数据补丁。");
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            HybridCLRGenerateAllSafe.Run();
            CompileDllCommand.CompileDll(context.BuildTarget);

            var aotAssemblies = BuildAssetsCommand.CopyAOTAssembliesToTargetPath(context.BuildTarget);
            var hotfixAssemblies = BuildAssetsCommand.CopyHotUpdateAssembliesToTargetPath(context.BuildTarget);
            var aotManifest = BuildAssetsCommand.CreateOrUpdateAOTAssemblyManifest(context.BuildTarget, aotAssemblies);
            var hotfixManifest = BuildAssetsCommand.CreateOrUpdateHotfixAssemblyManifest(
                context.BuildTarget,
                hotfixAssemblies,
                aotManifest.AotVersion);
            BuildAssetsCommand.ValidateHotfixAppVersionRange(hotfixManifest);
            BuildAssetsCommand.CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
            BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
            BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget);

            var buildResult = BuildAssetsCommand.BuildYooAssetPackage(
                packageConfig.MainPackageName,
                context.BuildTarget,
                false);
            Debug.Log("[HotfixBuild] AOT 元数据补丁构建完成。");

            return HotfixBuildExecutionResult.Create(
                context,
                packageConfig.MainPackageName,
                buildResult,
                false,
                aotManifest,
                hotfixManifest,
                true);
        }

        private static HotfixBuildExecutionResult ResolveExpiredAOTManifest(
            HotfixBuildContext context,
            Exception exception)
        {
            int option = EditorUtility.DisplayDialogComplex(
                "AOT 基线变化",
                exception.Message + "\n\n普通热更包已阻断。请选择下一步：",
                "构建首包",
                "取消",
                "构建 AOT 元数据补丁");

            if (option == 0)
            {
                return BuildInitialPackage(HotfixBuildContext.Create(HotfixBuildMode.InitialPackage));
            }

            if (option == 2)
            {
                return BuildAOTMetadataPatch(HotfixBuildContext.Create(HotfixBuildMode.AOTMetadataPatch));
            }

            throw new OperationCanceledException("已取消热更包构建。", exception);
        }

        private static void WriteBuildReport(HotfixBuildExecutionResult result, HotfixBuildReport report)
        {
            string reportDirectory = Path.Combine(Application.dataPath, "..", "BuildReports", "Hotfix");
            Directory.CreateDirectory(reportDirectory);
            string reportPath = Path.Combine(
                reportDirectory,
                $"{DateTime.Now:yyyyMMdd-HHmmss}-{result.ModeName}.txt");

            var builder = new StringBuilder();
            builder.AppendLine($"构建模式: {result.ModeDisplayName}");
            builder.AppendLine($"构建目标: {result.BuildTarget}");
            builder.AppendLine($"AppVersion: {result.AppVersion}");
            builder.AppendLine($"PackageName: {result.PackageName}");
            builder.AppendLine($"ResourceVersion: {result.ResourceVersion}");
            builder.AppendLine($"PackageVersion: {result.PackageVersion}");
            builder.AppendLine($"AotVersion: {result.AotVersion}");
            builder.AppendLine($"HotfixVersion: {result.HotfixVersion}");
            builder.AppendLine($"RequiredAotVersion: {result.RequiredAotVersion}");
            builder.AppendLine($"StreamingAssets: {(result.CopyToStreamingAssets ? "复制" : "不复制")}");
            builder.AppendLine($"输出目录: {result.OutputPackageDirectory}");
            builder.AppendLine($"CDN 上传目录: {result.CdnUploadDirectory}");
            builder.AppendLine();
            builder.AppendLine("校验与构建状态:");
            foreach (var item in report.Items)
            {
                builder.Append("- ");
                builder.Append(item.Severity);
                builder.Append(" | ");
                builder.Append(item.Label);
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    builder.Append(": ");
                    builder.Append(item.Value);
                }

                if (!string.IsNullOrWhiteSpace(item.Message))
                {
                    builder.Append(" | ");
                    builder.Append(item.Message);
                }

                builder.AppendLine();
            }

            HotfixBuildExecutionResult.AppendAssemblyFileRecords(builder, "AOT Metadata Hashes", result.AotMetadataFiles);
            HotfixBuildExecutionResult.AppendDependencyRecords(builder, "Hotfix DLL Dependencies", result.HotUpdateDependencies);
            HotfixBuildExecutionResult.AppendAssemblyFileRecords(builder, "Hotfix DLL Hashes", result.HotUpdateFiles);

            File.WriteAllText(reportPath, builder.ToString(), Encoding.UTF8);
            result.ReportPath = reportPath;
            report.AddInfo("构建报告", reportPath);
        }
    }

    public sealed class HotfixBuildExecutionResult
    {
        public HotfixBuildMode Mode;
        public string ModeName;
        public string ModeDisplayName;
        public string BuildTarget;
        public string AppVersion;
        public string PackageName;
        public string ResourceVersion;
        public string PackageVersion;
        public string OutputPackageDirectory;
        public string CdnUploadDirectory;
        public string AotVersion;
        public string HotfixVersion;
        public string RequiredAotVersion;
        public List<AssemblyFileRecord> AotMetadataFiles = new List<AssemblyFileRecord>();
        public List<AssemblyFileRecord> HotUpdateFiles = new List<AssemblyFileRecord>();
        public List<AssemblyDependencyRecord> HotUpdateDependencies = new List<AssemblyDependencyRecord>();
        public bool CopyToStreamingAssets;
        public bool IsAOTMetadataPatch;
        public string ReportPath;

        public static HotfixBuildExecutionResult Create(
            HotfixBuildContext context,
            string packageName,
            BuildResult buildResult,
            bool copyToStreamingAssets,
            AOTAssemblyManifest aotManifest,
            HotfixAssemblyManifest hotfixManifest,
            bool isAOTMetadataPatch)
        {
            string outputDirectory = buildResult == null ? string.Empty : buildResult.OutputPackageDirectory;
            return new HotfixBuildExecutionResult
            {
                Mode = context.Mode,
                ModeName = context.Mode.ToString(),
                ModeDisplayName = HotfixBuildModeUtility.GetDisplayName(context.Mode),
                BuildTarget = context.BuildTargetName,
                AppVersion = context.AppVersion,
                PackageName = packageName,
                ResourceVersion = context.ReleaseProfile == null ? string.Empty : context.ReleaseProfile.ResourceVersion,
                PackageVersion = GetPackageVersion(outputDirectory),
                OutputPackageDirectory = outputDirectory,
                CdnUploadDirectory = outputDirectory,
                AotVersion = aotManifest == null ? string.Empty : aotManifest.AotVersion,
                HotfixVersion = hotfixManifest == null ? string.Empty : hotfixManifest.HotfixVersion,
                RequiredAotVersion = hotfixManifest == null ? string.Empty : hotfixManifest.RequiredAotVersion,
                AotMetadataFiles = CloneFileRecords(aotManifest == null ? null : aotManifest.AotMetadataFiles),
                HotUpdateFiles = CloneFileRecords(hotfixManifest == null ? null : hotfixManifest.HotUpdateFiles),
                HotUpdateDependencies = CloneDependencyRecords(hotfixManifest == null ? null : hotfixManifest.HotUpdateDependencies),
                CopyToStreamingAssets = copyToStreamingAssets,
                IsAOTMetadataPatch = isAOTMetadataPatch
            };
        }

        public void AppendTo(HotfixBuildReport report)
        {
            report.AddInfo("资源包名", PackageName);
            if (!string.IsNullOrWhiteSpace(ResourceVersion))
            {
                report.AddInfo("ResourceVersion", ResourceVersion);
            }

            report.AddInfo("资源包版本", PackageVersion);
            report.AddInfo("AotVersion", AotVersion);
            report.AddInfo("HotfixVersion", HotfixVersion);
            report.AddInfo("RequiredAotVersion", RequiredAotVersion);
            report.AddInfo("YooAsset 输出目录", OutputPackageDirectory);
            report.AddInfo("CDN 上传目录", CdnUploadDirectory);
            report.AddInfo("StreamingAssets", CopyToStreamingAssets ? "已复制" : "不复制");
            report.AddInfo("Hotfix DLL 加载顺序", HotfixAssemblyDependencySorter.FormatLoadingOrder(HotUpdateFiles.Select(GetRecordFileName)));
            report.AddInfo("Hotfix DLL 依赖关系", HotfixAssemblyDependencySorter.FormatDependencies(HotUpdateDependencies));
            report.AddInfo("AOT Metadata Hash", FormatHashSummary(AotMetadataFiles));
            report.AddInfo("Hotfix DLL Hash", FormatHashSummary(HotUpdateFiles));
            if (IsAOTMetadataPatch)
            {
                report.AddWarning("高级构建模式", "AOT 元数据补丁", "该产物只能用于同一 App 基线下的元数据补充。");
            }
        }

        public static void AppendAssemblyFileRecords(
            StringBuilder builder,
            string title,
            IEnumerable<AssemblyFileRecord> records)
        {
            builder.AppendLine();
            builder.AppendLine(title + ":");
            foreach (var record in records ?? Enumerable.Empty<AssemblyFileRecord>())
            {
                builder.Append("- ");
                builder.Append(GetRecordFileName(record));
                builder.Append(" | size=");
                builder.Append(record.Size);
                builder.Append(" | sha256=");
                builder.AppendLine(record.Sha256 ?? string.Empty);
            }
        }

        public static void AppendDependencyRecords(
            StringBuilder builder,
            string title,
            IEnumerable<AssemblyDependencyRecord> records)
        {
            builder.AppendLine();
            builder.AppendLine(title + ":");
            foreach (var record in records ?? Enumerable.Empty<AssemblyDependencyRecord>())
            {
                builder.Append("- ");
                builder.Append(record.DllName);
                builder.Append(" (");
                builder.Append(record.AssemblyName);
                builder.Append(") depends on ");
                builder.AppendLine(record.DependsOn == null || record.DependsOn.Count == 0
                    ? "none"
                    : string.Join(", ", record.DependsOn));
            }
        }

        private static string FormatHashSummary(IEnumerable<AssemblyFileRecord> records)
        {
            var lines = (records ?? Enumerable.Empty<AssemblyFileRecord>())
                .Select(record => $"{GetRecordFileName(record)}:{record.Size}:{record.Sha256}")
                .ToList();
            return lines.Count == 0 ? "empty" : string.Join("; ", lines);
        }

        private static List<AssemblyFileRecord> CloneFileRecords(IEnumerable<AssemblyFileRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyFileRecord>())
                .Where(record => record != null)
                .Select(record => new AssemblyFileRecord
                {
                    FileName = record.FileName ?? string.Empty,
                    AssemblyName = record.AssemblyName ?? string.Empty,
                    Size = record.Size,
                    Sha256 = record.Sha256 ?? string.Empty
                })
                .ToList();
        }

        private static List<AssemblyDependencyRecord> CloneDependencyRecords(IEnumerable<AssemblyDependencyRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyDependencyRecord>())
                .Where(record => record != null)
                .Select(record => new AssemblyDependencyRecord
                {
                    AssemblyName = record.AssemblyName ?? string.Empty,
                    DllName = record.DllName ?? string.Empty,
                    DependsOn = record.DependsOn == null ? new List<string>() : new List<string>(record.DependsOn)
                })
                .ToList();
        }

        private static string GetRecordFileName(AssemblyFileRecord record)
        {
            if (record == null)
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(record.FileName) ? record.AssemblyName : record.FileName;
        }

        private static string GetPackageVersion(string outputDirectory)
        {
            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                return string.Empty;
            }

            return Path.GetFileName(outputDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        }
    }
}
