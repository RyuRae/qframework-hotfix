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
    /// <summary>
    /// 构建中心和 CI 共用的统一执行器，按模式编排配置应用、校验、HybridCLR、Manifest 与 YooAsset 构建。
    /// </summary>
    public static class HotfixBuildRunner
    {
        public static Action<string, float, string> ProgressChanged;
        public static HotfixBuildExecutionResult LastExecutionResult { get; private set; }
        private static HotfixBuildExecutionOptions sExecutionOptions;

        /// <summary>只读取并校验当前构建配置，不生成任何资源产物。</summary>
        public static HotfixBuildReport ValidateOnly(HotfixBuildMode mode)
        {
            var context = HotfixBuildContext.Create(mode);
            return HotfixBuildValidator.Validate(context);
        }

        /// <summary>把 ReleaseProfile 应用到编辑器资产并执行可自动完成的配置修复。</summary>
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

        /// <summary>以交互模式执行指定资源构建任务。</summary>
        public static HotfixBuildReport Build(HotfixBuildMode mode)
        {
            return Build(mode, HotfixBuildExecutionOptions.Interactive);
        }

        /// <summary>使用指定交互策略执行构建，供 batchmode CI 禁止弹窗并显式确认高风险任务。</summary>
        public static HotfixBuildReport Build(
            HotfixBuildMode mode,
            HotfixBuildExecutionOptions executionOptions)
        {
            var previousOptions = sExecutionOptions;
            sExecutionOptions = executionOptions ?? HotfixBuildExecutionOptions.Interactive;
            try
            {
                return BuildInternal(mode);
            }
            finally
            {
                sExecutionOptions = previousOptions;
            }
        }

        private static HotfixBuildReport BuildInternal(HotfixBuildMode mode)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            LastExecutionResult = null;
            ReportProgress("准备构建", 0f, string.Empty);
            var context = HotfixBuildContext.Create(mode);
            if (context.ReleaseProfile == null)
            {
                throw new InvalidOperationException(
                    $"缺少 ReleaseProfile：{HotfixReleaseProfile.DefaultAssetPath}。请在构建中心创建并绑定默认发布配置。");
            }

            ReportProgress("应用 ReleaseProfile", 0.05f, string.Empty);
            context.ReleaseProfile.ApplyToEditorSettings();
            context = HotfixBuildContext.Create(mode);
            ReportProgress("校验构建配置", 0.1f, string.Empty);
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
                    ReportProgress("构建首包资源", 0.15f, string.Empty);
                    executionResult = BuildInitialPackage(context);
                    break;
                case HotfixBuildMode.HotfixPackage:
                    ReportProgress("构建热更资源", 0.15f, string.Empty);
                    executionResult = BuildHotfixPackage(context);
                    break;
                case HotfixBuildMode.AOTMetadataPatch:
                    ReportProgress("构建 AOT 元数据补丁", 0.15f, string.Empty);
                    executionResult = BuildAOTMetadataPatch(context);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            ReportProgress(
                "构建产物已生成",
                0.92f,
                executionResult.OutputPackageDirectory);
            stopwatch.Stop();
            executionResult.DurationSeconds = stopwatch.Elapsed.TotalSeconds;
            LastExecutionResult = executionResult;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            report = ValidateOnly(executionResult.Mode);
            executionResult.AppendTo(report);
            WriteBuildReport(executionResult, report);
            report.AddInfo("构建", "完成", HotfixBuildModeUtility.GetDisplayName(mode));
            ReportProgress("构建完成", 1f, executionResult.OutputPackageDirectory);
            return report;
        }

        /// <summary>构建首包资源，在完整成功后建立独立 Player AOT 基线。</summary>
        public static HotfixBuildExecutionResult BuildInitialPackage(HotfixBuildContext context)
        {
            Debug.Log("[HotfixBuild] 开始构建首包。");
            ReportProgress("同步 YooAsset Collector", 0.2f, string.Empty);
            LocalizationContentSynchronizer.SyncOrThrow();
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            ReportProgress("生成 HybridCLR 文件", 0.28f, string.Empty);
            HybridCLRGenerateAllSafe.Run();
            ReportProgress("编译 AOT / 热更 DLL", 0.38f, string.Empty);
            CompileDllCommand.CompileDll(context.BuildTarget);

            string packageVersion = BuildAssetsCommand.CreatePackageVersion(context.BuildTarget);

            var aotAssemblies = BuildAssetsCommand.CopyAOTAssembliesToTargetPath(context.BuildTarget);
            var hotfixAssemblies = BuildAssetsCommand.CopyHotUpdateAssembliesToTargetPath(context.BuildTarget);
            ReportProgress("生成 AOT / Hotfix Manifest", 0.5f, string.Empty);
            var aotManifest = BuildAssetsCommand.CreateOrUpdateAOTAssemblyManifest(
                context.BuildTarget,
                aotAssemblies,
                packageVersion);
            var hotfixManifest = BuildAssetsCommand.CreateOrUpdateHotfixAssemblyManifest(
                context.BuildTarget,
                hotfixAssemblies,
                aotManifest.AotVersion,
                packageVersion);
            ReportProgress("构建 RawFile 包", 0.56f, string.Empty);
            BuildResult rawFileBuildResult = BuildRawFilePackage(
                context,
                packageConfig,
                packageVersion);
            ReportProgress(
                "RawFile 包构建完成",
                0.64f,
                rawFileBuildResult == null ? string.Empty : rawFileBuildResult.OutputPackageDirectory);
            ApplyRawFileManifestBinding(packageConfig, packageVersion, rawFileBuildResult, hotfixManifest);
            BuildAssetsCommand.ValidateHotfixAppVersionRange(hotfixManifest);
            BuildAssetsCommand.CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
            BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
            BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget, false);

            bool copyToStreamingAssets = context.StartupPackageMode != Framework.StartupPackageMode.EmptyPackage;
            ReportProgress("构建主资源包", 0.72f, string.Empty);
            var buildResult = BuildAssetsCommand.BuildYooAssetPackage(
                packageConfig.MainPackageName,
                context.BuildTarget,
                copyToStreamingAssets
                    ? EBuildinFileCopyOption.ClearAndCopyAll
                    : EBuildinFileCopyOption.None,
                packageVersion);
            ReportProgress("主资源包构建完成", 0.84f, buildResult == null ? string.Empty : buildResult.OutputPackageDirectory);
            if (copyToStreamingAssets && rawFileBuildResult != null)
            {
                BuildAssetsCommand.AppendRawFilePackageToStreamingAssets(
                    packageConfig.RawFilePackageName,
                    packageVersion,
                    rawFileBuildResult);
            }
            ReportProgress("建立 Player AOT 基线", 0.88f, buildResult == null ? string.Empty : buildResult.OutputPackageDirectory);
            var playerBaseline = HotfixPlayerAOTBaselineUtility.CaptureAfterInitialPackage(
                context.BuildTarget,
                context.AppVersion,
                aotManifest);
            Debug.Log("[HotfixBuild] 首包构建完成。");

            var result = HotfixBuildExecutionResult.Create(
                context,
                packageConfig.MainPackageName,
                buildResult,
                packageConfig.IncludeRawFilePackage ? packageConfig.RawFilePackageName : string.Empty,
                rawFileBuildResult,
                copyToStreamingAssets,
                aotManifest,
                hotfixManifest,
                false);
            result.SetPlayerBaseline(playerBaseline, true);
            return result;
        }

        /// <summary>复用现有 AOT 基线，仅更新 Hotfix DLL、资源和远端 YooAsset 包。</summary>
        public static HotfixBuildExecutionResult BuildHotfixPackage(HotfixBuildContext context)
        {
            Debug.Log("[HotfixBuild] 开始构建热更包。");
            ReportProgress("同步 YooAsset Collector", 0.2f, string.Empty);
            LocalizationContentSynchronizer.SyncOrThrow();
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

            ReportProgress("编译热更 DLL", 0.38f, string.Empty);
            CompileDllCommand.CompileDll(context.BuildTarget);
            string packageVersion = BuildAssetsCommand.CreatePackageVersion(context.BuildTarget);
            var hotfixAssemblies = BuildAssetsCommand.CopyHotUpdateAssembliesToTargetPath(context.BuildTarget);
            var hotfixManifest = BuildAssetsCommand.CreateOrUpdateHotfixAssemblyManifest(
                context.BuildTarget,
                hotfixAssemblies,
                aotManifest.AotVersion,
                packageVersion);
            ReportProgress("生成 Hotfix Manifest", 0.5f, string.Empty);
            ReportProgress("构建 RawFile 包", 0.56f, string.Empty);
            BuildResult rawFileBuildResult = BuildRawFilePackage(
                context,
                packageConfig,
                packageVersion);
            ReportProgress(
                "RawFile 包构建完成",
                0.64f,
                rawFileBuildResult == null ? string.Empty : rawFileBuildResult.OutputPackageDirectory);
            ApplyRawFileManifestBinding(packageConfig, packageVersion, rawFileBuildResult, hotfixManifest);
            BuildAssetsCommand.ValidateHotfixAppVersionRange(hotfixManifest);
            BuildAssetsCommand.CreateOrUpdateAssemblyManifest(aotManifest.AotMetadataAssemblies, hotfixManifest.HotUpdateAssemblies);
            BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
            BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget, false);

            ReportProgress("构建主资源包", 0.72f, string.Empty);
            var buildResult = BuildAssetsCommand.BuildYooAssetPackage(
                packageConfig.MainPackageName,
                context.BuildTarget,
                EBuildinFileCopyOption.None,
                packageVersion);
            ReportProgress("主资源包构建完成", 0.84f, buildResult == null ? string.Empty : buildResult.OutputPackageDirectory);
            Debug.Log("[HotfixBuild] 热更包构建完成。");

            return HotfixBuildExecutionResult.Create(
                context,
                packageConfig.MainPackageName,
                buildResult,
                packageConfig.IncludeRawFilePackage ? packageConfig.RawFilePackageName : string.Empty,
                rawFileBuildResult,
                false,
                aotManifest,
                hotfixManifest,
                false);
        }

        /// <summary>严格绑定同一 Player AOT 基线，构建显式确认的 AOT 元数据补丁。</summary>
        public static HotfixBuildExecutionResult BuildAOTMetadataPatch(HotfixBuildContext context)
        {
            const string message =
                "AOT 元数据补丁只能补充同一 App 基线下的泛型元数据。\n\n" +
                "如果修改了主工程 AOT 代码逻辑、公共接口、原生 SDK 或 PlayerSettings，应发布新 App，而不是发布 AOT 元数据补丁。";

            if (!IsAOTMetadataPatchConfirmed(message))
            {
                throw new OperationCanceledException("已取消 AOT 元数据补丁构建。");
            }

            var previousAotManifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(BuildAssetsCommand.AOTAssemblyManifestAssetPath);
            if (previousAotManifest == null)
            {
                throw new InvalidOperationException("构建 AOT 元数据补丁前必须存在旧的 AOT 清单。请先构建首包建立 App 基线。");
            }

            var playerBaseline = HotfixPlayerAOTBaselineUtility.Load();
            HotfixPlayerAOTBaselineUtility.ValidateIdentityOrThrow(
                playerBaseline,
                context.BuildTarget,
                context.AppVersion);

            string previousAotVersion = previousAotManifest.AotVersion ?? string.Empty;
            string previousManifestFingerprint = previousAotManifest.BaselineFingerprint ?? string.Empty;
            var previousAotFiles = CloneFileRecords(previousAotManifest.AotMetadataFiles);

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
            ReportProgress("同步 YooAsset Collector", 0.2f, string.Empty);
            LocalizationContentSynchronizer.SyncOrThrow();
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            ReportProgress("生成 HybridCLR 文件", 0.28f, string.Empty);
            HybridCLRGenerateAllSafe.Run();
            ReportProgress("编译 AOT / 热更 DLL", 0.38f, string.Empty);
            CompileDllCommand.CompileDll(context.BuildTarget);
            ReportProgress("验证 Player AOT 基线", 0.44f, string.Empty);
            HotfixPlayerAOTBaselineUtility.ValidateGeneratedAssembliesOrThrow(
                playerBaseline,
                context.BuildTarget);

            string packageVersion = BuildAssetsCommand.CreatePackageVersion(context.BuildTarget);

            var aotAssemblies = BuildAssetsCommand.CopyAOTAssembliesToTargetPath(context.BuildTarget);
            var hotfixAssemblies = BuildAssetsCommand.CopyHotUpdateAssembliesToTargetPath(context.BuildTarget);
            ReportProgress("生成 AOT / Hotfix Manifest", 0.5f, string.Empty);
            var aotManifest = BuildAssetsCommand.CreateOrUpdateAOTAssemblyManifest(
                context.BuildTarget,
                aotAssemblies,
                packageVersion);
            var hotfixManifest = BuildAssetsCommand.CreateOrUpdateHotfixAssemblyManifest(
                context.BuildTarget,
                hotfixAssemblies,
                aotManifest.AotVersion,
                packageVersion);
            ReportProgress("构建 RawFile 包", 0.56f, string.Empty);
            BuildResult rawFileBuildResult = BuildRawFilePackage(
                context,
                packageConfig,
                packageVersion);
            ReportProgress(
                "RawFile 包构建完成",
                0.64f,
                rawFileBuildResult == null ? string.Empty : rawFileBuildResult.OutputPackageDirectory);
            ApplyRawFileManifestBinding(packageConfig, packageVersion, rawFileBuildResult, hotfixManifest);
            BuildAssetsCommand.ValidateHotfixAppVersionRange(hotfixManifest);
            BuildAssetsCommand.CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
            BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
            BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget, false);

            ReportProgress("构建主资源包", 0.72f, string.Empty);
            var buildResult = BuildAssetsCommand.BuildYooAssetPackage(
                packageConfig.MainPackageName,
                context.BuildTarget,
                EBuildinFileCopyOption.None,
                packageVersion);
            ReportProgress("主资源包构建完成", 0.84f, buildResult == null ? string.Empty : buildResult.OutputPackageDirectory);
            Debug.Log("[HotfixBuild] AOT 元数据补丁构建完成。");

            var result = HotfixBuildExecutionResult.Create(
                context,
                packageConfig.MainPackageName,
                buildResult,
                packageConfig.IncludeRawFilePackage ? packageConfig.RawFilePackageName : string.Empty,
                rawFileBuildResult,
                false,
                aotManifest,
                hotfixManifest,
                true);
            result.SetPlayerBaseline(playerBaseline, true);
            result.SetAOTMetadataPatchChanges(
                previousAotVersion,
                previousManifestFingerprint,
                previousAotFiles,
                aotManifest);
            return result;
        }

        private static BuildResult BuildRawFilePackage(
            HotfixBuildContext context,
            BuildAssetsCommand.RuntimePackageConfig packageConfig,
            string packageVersion)
        {
            if (!packageConfig.IncludeRawFilePackage)
            {
                return null;
            }

            return BuildAssetsCommand.BuildRawFilePackage(
                packageConfig.RawFilePackageName,
                context.BuildTarget,
                EBuildinFileCopyOption.None,
                packageVersion);
        }

        private static void ApplyRawFileManifestBinding(
            BuildAssetsCommand.RuntimePackageConfig packageConfig,
            string packageVersion,
            BuildResult rawFileBuildResult,
            HotfixAssemblyManifest hotfixManifest)
        {
            if (packageConfig.IncludeRawFilePackage)
            {
                BuildAssetsCommand.BindRawFileManifestToHotfixManifest(
                    hotfixManifest,
                    packageConfig.RawFilePackageName,
                    packageVersion,
                    rawFileBuildResult);
                return;
            }

            BuildAssetsCommand.ClearRawFileManifestBinding(hotfixManifest);
        }

        private static HotfixBuildExecutionResult ResolveExpiredAOTManifest(
            HotfixBuildContext context,
            Exception exception)
        {
            if (IsNonInteractiveBuild)
            {
                throw new InvalidOperationException(
                    "非交互构建检测到 AOT 基线变化，已停止普通热更构建。" +
                    "CI 不会自动切换为首包或 AOT 元数据补丁；请显式选择正确任务后重试。",
                    exception);
            }

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
            if (!string.IsNullOrWhiteSpace(result.RawFilePackageName))
            {
                builder.AppendLine($"RawFilePackageName: {result.RawFilePackageName}");
                builder.AppendLine($"RawFilePackageVersion: {result.RawFilePackageVersion}");
                builder.AppendLine($"RawFile 输出目录: {result.RawFileOutputPackageDirectory}");
                builder.AppendLine($"RawFile CDN 上传目录: {result.RawFileCdnUploadDirectory}");
            }
            builder.AppendLine($"AotVersion: {result.AotVersion}");
            if (!string.IsNullOrWhiteSpace(result.PlayerBaselineFingerprint))
            {
                builder.AppendLine($"Player AOT 基线路径: {result.PlayerBaselinePath}");
                builder.AppendLine($"Player AOT 基线校验: {(result.PlayerBaselineVerified ? "通过" : "未校验")}");
                builder.AppendLine($"Player AOT 基线指纹: {result.PlayerBaselineFingerprint}");
            }
            if (result.IsAOTMetadataPatch)
            {
                builder.AppendLine($"旧 AotVersion: {result.PreviousAotVersion}");
                builder.AppendLine($"新 AotVersion: {result.AotVersion}");
                builder.AppendLine($"旧 Manifest 基线指纹: {result.PreviousManifestBaselineFingerprint}");
                builder.AppendLine($"新 Manifest 基线指纹: {result.CurrentManifestBaselineFingerprint}");
                builder.AppendLine($"AOT Metadata 新增 DLL: {result.FormatAOTChanges(result.AotAddedFiles)}");
                builder.AppendLine($"AOT Metadata 变化 DLL: {result.FormatAOTChanges(result.AotChangedFiles)}");
                builder.AppendLine($"AOT Metadata 移除 DLL: {result.FormatAOTChanges(result.AotRemovedFiles)}");
            }
            builder.AppendLine($"HotfixVersion: {result.HotfixVersion}");
            builder.AppendLine($"RequiredAotVersion: {result.RequiredAotVersion}");
            builder.AppendLine($"StreamingAssets: {(result.CopyToStreamingAssets ? "复制" : "不复制")}");
            builder.AppendLine($"输出目录: {result.OutputPackageDirectory}");
            builder.AppendLine($"CDN 上传目录: {result.CdnUploadDirectory}");
            builder.AppendLine($"构建耗时: {result.DurationSeconds:F1}s");
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

        private static void ReportProgress(string stage, float progress, string outputDirectory)
        {
            ProgressChanged?.Invoke(stage ?? string.Empty, Mathf.Clamp01(progress), outputDirectory ?? string.Empty);
        }

        private static bool IsNonInteractiveBuild =>
            Application.isBatchMode || (sExecutionOptions != null && sExecutionOptions.NonInteractive);

        private static bool IsAOTMetadataPatchConfirmed(string message)
        {
            if (sExecutionOptions != null && sExecutionOptions.ConfirmAOTMetadataPatch)
            {
                return true;
            }

            if (IsNonInteractiveBuild)
            {
                throw new InvalidOperationException(
                    "非交互 AOT 元数据补丁必须显式确认。" +
                    "使用专用 CI 入口时请传入 -hotfixConfirmAotPatch true。");
            }

            return EditorUtility.DisplayDialog("AOT 元数据补丁", message, "继续构建", "取消");
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
    }

    /// <summary>控制构建是否允许交互，以及是否已显式确认 AOT 补丁风险。</summary>
    public sealed class HotfixBuildExecutionOptions
    {
        public static readonly HotfixBuildExecutionOptions Interactive = new HotfixBuildExecutionOptions();

        public bool NonInteractive;
        public bool ConfirmAOTMetadataPatch;
    }

    /// <summary>一次构建的结构化结果，供构建中心、文本报告和 CI JSON 复用。</summary>
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
        public string RawFilePackageName;
        public string RawFilePackageVersion;
        public string RawFileOutputPackageDirectory;
        public string RawFileCdnUploadDirectory;
        public string AotVersion;
        public string HotfixVersion;
        public string RequiredAotVersion;
        public string PlayerBaselinePath;
        public string PlayerBaselineFingerprint;
        public bool PlayerBaselineVerified;
        public string PreviousAotVersion;
        public string PreviousManifestBaselineFingerprint;
        public string CurrentManifestBaselineFingerprint;
        public List<string> AotAddedFiles = new List<string>();
        public List<string> AotChangedFiles = new List<string>();
        public List<string> AotRemovedFiles = new List<string>();
        public List<AssemblyFileRecord> AotMetadataFiles = new List<AssemblyFileRecord>();
        public List<AssemblyFileRecord> HotUpdateFiles = new List<AssemblyFileRecord>();
        public List<AssemblyDependencyRecord> HotUpdateDependencies = new List<AssemblyDependencyRecord>();
        public bool CopyToStreamingAssets;
        public bool IsAOTMetadataPatch;
        public string ReportPath;
        public double DurationSeconds;

        /// <summary>从构建上下文、YooAsset 结果和双 Manifest 创建结果快照。</summary>
        public static HotfixBuildExecutionResult Create(
            HotfixBuildContext context,
            string packageName,
            BuildResult buildResult,
            string rawFilePackageName,
            BuildResult rawFileBuildResult,
            bool copyToStreamingAssets,
            AOTAssemblyManifest aotManifest,
            HotfixAssemblyManifest hotfixManifest,
            bool isAOTMetadataPatch)
        {
            string outputDirectory = buildResult == null ? string.Empty : buildResult.OutputPackageDirectory;
            string rawFileOutputDirectory = rawFileBuildResult == null
                ? string.Empty
                : rawFileBuildResult.OutputPackageDirectory;
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
                RawFilePackageName = rawFilePackageName ?? string.Empty,
                RawFilePackageVersion = GetPackageVersion(rawFileOutputDirectory),
                RawFileOutputPackageDirectory = rawFileOutputDirectory,
                RawFileCdnUploadDirectory = rawFileOutputDirectory,
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

        /// <summary>记录本次首包建立或 AOT 补丁验证使用的 Player 基线。</summary>
        public void SetPlayerBaseline(HotfixPlayerAOTBaseline baseline, bool verified)
        {
            PlayerBaselinePath = baseline == null ? string.Empty : HotfixPlayerAOTBaseline.AssetPath;
            PlayerBaselineFingerprint = baseline == null ? string.Empty : baseline.BaselineFingerprint ?? string.Empty;
            PlayerBaselineVerified = baseline != null && verified;
        }

        /// <summary>比较补丁前后 AOT Metadata 文件并记录新增、变化和移除项。</summary>
        public void SetAOTMetadataPatchChanges(
            string previousAotVersion,
            string previousManifestFingerprint,
            IEnumerable<AssemblyFileRecord> previousFiles,
            AOTAssemblyManifest currentManifest)
        {
            PreviousAotVersion = previousAotVersion ?? string.Empty;
            PreviousManifestBaselineFingerprint = previousManifestFingerprint ?? string.Empty;
            CurrentManifestBaselineFingerprint = currentManifest == null
                ? string.Empty
                : currentManifest.BaselineFingerprint ?? string.Empty;
            HotfixPlayerAOTBaselineUtility.BuildAssemblyDiff(
                previousFiles,
                currentManifest == null ? null : currentManifest.AotMetadataFiles,
                out var added,
                out var changed,
                out var removed);
            AotAddedFiles = added;
            AotChangedFiles = changed;
            AotRemovedFiles = removed;
        }

        /// <summary>将构建产物、版本、基线和 DLL 摘要追加到校验报告。</summary>
        public void AppendTo(HotfixBuildReport report)
        {
            report.AddInfo("资源包名", PackageName);
            if (!string.IsNullOrWhiteSpace(ResourceVersion))
            {
                report.AddInfo("ResourceVersion", ResourceVersion);
            }

            report.AddInfo("资源包版本", PackageVersion);
            report.AddInfo("AotVersion", AotVersion);
            if (!string.IsNullOrWhiteSpace(PlayerBaselineFingerprint))
            {
                report.AddInfo("Player AOT 基线路径", PlayerBaselinePath);
                report.AddInfo("Player AOT 基线校验", PlayerBaselineVerified ? "通过" : "未校验");
                report.AddInfo("Player AOT 基线指纹", PlayerBaselineFingerprint);
            }
            report.AddInfo("HotfixVersion", HotfixVersion);
            report.AddInfo("RequiredAotVersion", RequiredAotVersion);
            report.AddInfo("YooAsset 输出目录", OutputPackageDirectory);
            report.AddInfo("CDN 上传目录", CdnUploadDirectory);
            if (!string.IsNullOrWhiteSpace(RawFilePackageName))
            {
                report.AddInfo("RawFile 资源包名", RawFilePackageName);
                report.AddInfo("RawFile 资源包版本", RawFilePackageVersion);
                report.AddInfo("RawFile YooAsset 输出目录", RawFileOutputPackageDirectory);
                report.AddInfo("RawFile CDN 上传目录", RawFileCdnUploadDirectory);
            }
            report.AddInfo("StreamingAssets", CopyToStreamingAssets ? "已复制" : "不复制");
            report.AddInfo("构建耗时", $"{DurationSeconds:F1}s");
            report.AddInfo("Hotfix DLL 加载顺序", HotfixAssemblyDependencySorter.FormatLoadingOrder(HotUpdateFiles.Select(GetRecordFileName)));
            report.AddInfo("Hotfix DLL 依赖关系", HotfixAssemblyDependencySorter.FormatDependencies(HotUpdateDependencies));
            report.AddInfo("AOT Metadata Hash", FormatHashSummary(AotMetadataFiles));
            report.AddInfo("Hotfix DLL Hash", FormatHashSummary(HotUpdateFiles));
            if (IsAOTMetadataPatch)
            {
                report.AddWarning("高级构建模式", "AOT 元数据补丁", "该产物只能用于同一 App 基线下的元数据补充。");
                report.AddInfo("AotVersion 变化", $"{PreviousAotVersion} -> {AotVersion}");
                report.AddInfo(
                    "Manifest 基线指纹变化",
                    $"{PreviousManifestBaselineFingerprint} -> {CurrentManifestBaselineFingerprint}");
                report.AddInfo("AOT Metadata 新增 DLL", FormatAOTChanges(AotAddedFiles));
                report.AddInfo("AOT Metadata 变化 DLL", FormatAOTChanges(AotChangedFiles));
                report.AddInfo("AOT Metadata 移除 DLL", FormatAOTChanges(AotRemovedFiles));
            }
        }

        public string FormatAOTChanges(IEnumerable<string> files)
        {
            var values = (files ?? Enumerable.Empty<string>()).ToList();
            return values.Count == 0 ? "无" : string.Join(", ", values);
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
