using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Framework;
using Framework.Assemblies;
using Framework.Utils;
using Framework.YooAssetBridge;
using HybridCLR.Editor.Commands;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;
using YooAsset;

namespace HybridCLR.Editor
{
    public static class BuildAssetsCommand
    {
        public const string DefaultPackageName = HotfixRuntimeSettings.DefaultMainPackageName;
        public const string DefaultEntrySceneAddress = HotfixUtility.DefaultEntrySceneAddress;
        public const string AOTCodesPath = "Assets/AssetsPackage/AssetsHotFix/AOTCodes";
        public const string HotfixCodesPath = "Assets/AssetsPackage/AssetsHotFix/HotfixCodes";
        public const string ConfigsPath = "Assets/AssetsPackage/AssetsHotFix/Configs";
        public const string DatasPath = "Assets/AssetsPackage/AssetsHotFix/Datas";
        public const string AssemblyManifestAssetPath = ConfigsPath + "/AssemblyManifest.asset";
        public const string AOTAssemblyManifestAssetPath = ConfigsPath + "/AOTAssemblyManifest.asset";
        public const string HotfixAssemblyManifestAssetPath = ConfigsPath + "/HotfixAssemblyManifest.asset";
        public const string RequiredStartupTag = HotfixRuntimeSettings.DefaultStartupTag;
        // Mirrors YooAsset's internal EBuildBundleType.AssetBundle without referencing an internal enum.
        private const int YooAssetBuildBundleTypeAssetBundle = 2;
        // Mirrors YooAsset's internal EBuildBundleType.RawBundle without referencing an internal enum.
        private const int YooAssetBuildBundleTypeRawBundle = 3;

        public sealed class RuntimePackageConfig
        {
            public readonly string MainPackageName;
            public readonly bool IncludeRawFilePackage;
            public readonly string RawFilePackageName;

            public RuntimePackageConfig(string mainPackageName, bool includeRawFilePackage, string rawFilePackageName)
            {
                MainPackageName = mainPackageName;
                IncludeRawFilePackage = includeRawFilePackage;
                RawFilePackageName = rawFilePackageName;
            }
        }

        [MenuItem("Build/热更新/内部工具/仅构建首包 YooAsset", false, HotfixBuildMenuPriority.InternalYooAssetInitialOnly)]
        public static void BuildInitialYooAssetPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            CompileDllCommand.CompileDll(target);
            string packageVersion = CreatePackageVersion(target);

            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies, packageVersion);
            var hotfixManifest = CreateOrUpdateHotfixAssemblyManifest(
                target,
                hotfixAssemblies,
                aotManifest.AotVersion,
                packageVersion);

            bool copyToStreamingAssets = ShouldCopyPackageToStreamingAssets();
            BuildResult rawFileBuildResult = null;
            if (packageConfig.IncludeRawFilePackage)
            {
                rawFileBuildResult = BuildRawFilePackage(
                    packageConfig.RawFilePackageName,
                    target,
                    EBuildinFileCopyOption.None,
                    packageVersion);
            }
            ApplyRawFileManifestBinding(packageConfig, packageVersion, rawFileBuildResult, hotfixManifest);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
            ValidateStartupPackageForBuild(target, false);
            BuildYooAssetPackage(
                packageConfig.MainPackageName,
                target,
                copyToStreamingAssets ? EBuildinFileCopyOption.ClearAndCopyAll : EBuildinFileCopyOption.None,
                packageVersion);
            if (copyToStreamingAssets && rawFileBuildResult != null)
            {
                AppendRawFilePackageToStreamingAssets(
                    packageConfig.RawFilePackageName,
                    packageVersion,
                    rawFileBuildResult);
            }
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/热更新/内部工具/仅构建热更 YooAsset", false, HotfixBuildMenuPriority.InternalYooAssetHotfixOnly)]
        public static void BuildHotfixYooAssetPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            var aotManifest = LoadValidatedAOTManifestForHotfixBuild(target);
            CompileDllCommand.CompileDll(target);
            string packageVersion = CreatePackageVersion(target);

            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var hotfixManifest = CreateOrUpdateHotfixAssemblyManifest(
                target,
                hotfixAssemblies,
                aotManifest.AotVersion,
                packageVersion);
            BuildResult rawFileBuildResult = null;
            if (packageConfig.IncludeRawFilePackage)
            {
                rawFileBuildResult = BuildRawFilePackage(
                    packageConfig.RawFilePackageName,
                    target,
                    EBuildinFileCopyOption.None,
                    packageVersion);
            }
            ApplyRawFileManifestBinding(packageConfig, packageVersion, rawFileBuildResult, hotfixManifest);
            CreateOrUpdateAssemblyManifest(aotManifest.AotMetadataAssemblies, hotfixManifest.HotUpdateAssemblies);
            ValidateSplitAssemblyManifestsForBuild(target);
            ValidateStartupPackageForBuild(target, false);
            BuildYooAssetPackage(packageConfig.MainPackageName, target, EBuildinFileCopyOption.None, packageVersion);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/热更新/内部工具/旧命令/构建并复制到 AssetsPackage", false, HotfixBuildMenuPriority.LegacyCommands)]
        public static void BuildAndCopyToAssetsPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);

            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
            var hotfixManifest = CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
            ValidateStartupPackageForBuild(target);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/热更新/内部工具/旧命令/构建并复制到 StreamingAssets", false, HotfixBuildMenuPriority.LegacyCommands + 1)]
        public static void BuildAndCopyABAOTHotUpdateDlls()
        {
            BuildInitialYooAssetPackage();
        }

        [MenuItem("Build/热更新/内部工具/复制 AOT 元数据 DLL", false, HotfixBuildMenuPriority.InternalCopyAOTMetadata)]
        public static void BuildAndCopyAOTHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);
            CopyAotMetaDataDlls(target);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/热更新/内部工具/复制热更 DLL", false, HotfixBuildMenuPriority.InternalCopyHotfixDlls)]
        public static void BuildAndCopyHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var aotManifest = LoadValidatedAOTManifestForHotfixBuild(target);
            CompileDllCommand.CompileDll(target);
            CopyHotUpdateDlls(aotManifest);
            AssetDatabase.Refresh();
        }

        public static void CopyHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var aotManifest = LoadValidatedAOTManifestForHotfixBuild(target);
            CopyHotUpdateDlls(aotManifest);
        }

        private static void CopyHotUpdateDlls(AOTAssemblyManifest aotManifest)
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var hotfixManifest = CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotManifest.AotMetadataAssemblies, hotfixManifest.HotUpdateAssemblies);
        }

        public static void CopyAotMetaDataDlls(BuildTarget target)
        {
            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
            var hotfixAssemblies = GetManifestOrConfiguredHotfixAssemblies();
            var hotfixManifest = CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
        }

        public static List<string> CopyAOTAssembliesToTargetPath()
        {
            return CopyAOTAssembliesToTargetPath(EditorUserBuildSettings.activeBuildTarget);
        }

        public static List<string> CopyAOTAssembliesToTargetPath(BuildTarget target)
        {
            string aotAssembliesSrcDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            var aotAssemblies = FindAllAOTMetaAssemblies(target);
            var copiedAssemblies = new List<string>();

            PrepareDirectory(AOTCodesPath);
            foreach (var dll in aotAssemblies)
            {
                string srcDllPath = Path.Combine(aotAssembliesSrcDir, dll);
                if (!File.Exists(srcDllPath))
                {
                    throw new FileNotFoundException($"AOT metadata assembly not found: {srcDllPath}", srcDllPath);
                }

                string dllBytesPath = Path.Combine(AOTCodesPath, $"{dll}.bytes");
                File.Copy(srcDllPath, dllBytesPath, true);
                copiedAssemblies.Add(dll);
                Debug.Log($"[CopyAOTAssemblies] {srcDllPath} -> {dllBytesPath}");
            }

            return copiedAssemblies;
        }

        public static List<string> CopyHotUpdateAssembliesToTargetPath()
        {
            return CopyHotUpdateAssembliesToTargetPath(EditorUserBuildSettings.activeBuildTarget);
        }

        public static List<string> CopyHotUpdateAssembliesToTargetPath(BuildTarget target)
        {
            string hotfixDllSrcDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            var hotfixAssemblies = GetAllHotfixAssemblies();
            var copiedAssemblies = new List<string>();

            PrepareDirectory(HotfixCodesPath);
            foreach (var dll in hotfixAssemblies)
            {
                string dllPath = Path.Combine(hotfixDllSrcDir, dll);
                if (!File.Exists(dllPath))
                {
                    throw new FileNotFoundException($"Hot update assembly not found: {dllPath}", dllPath);
                }

                string dllBytesPath = Path.Combine(HotfixCodesPath, $"{dll}.bytes");
                File.Copy(dllPath, dllBytesPath, true);
                copiedAssemblies.Add(dll);
                Debug.Log($"[CopyHotUpdateAssemblies] {dllPath} -> {dllBytesPath}");
            }

            return copiedAssemblies;
        }

        public static void CopyAssembiesSettingToTargetPath()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var aotAssemblies = FindAllAOTMetaAssemblies(target);
            var hotfixAssemblies = GetAllHotfixAssemblies();
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
            var hotfixManifest = CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixManifest.HotUpdateAssemblies);
        }

        public static BuildResult BuildYooAssetPackage(
            string packageName,
            BuildTarget target,
            bool copyToStreamingAssets,
            string packageVersion = null)
        {
            return BuildYooAssetPackage(
                packageName,
                target,
                copyToStreamingAssets
                    ? EBuildinFileCopyOption.ClearAndCopyAll
                    : EBuildinFileCopyOption.None,
                packageVersion);
        }

        public static BuildResult BuildYooAssetPackage(
            string packageName,
            BuildTarget target,
            EBuildinFileCopyOption buildinFileCopyOption,
            string packageVersion = null)
        {
            packageName = NormalizePackageName(packageName, GetConfiguredMainPackageName());
            var buildParameters = new BuiltinBuildParameters
            {
                BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(),
                BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot(),
                BuildPipeline = EBuildPipeline.BuiltinBuildPipeline.ToString(),
                BuildBundleType = YooAssetBuildBundleTypeAssetBundle,
                BuildTarget = target,
                PackageName = packageName,
                PackageVersion = string.IsNullOrWhiteSpace(packageVersion)
                    ? CreatePackageVersion(target)
                    : packageVersion.Trim(),
                EnableSharePackRule = true,
                VerifyBuildingResult = true,
                FileNameStyle = EFileNameStyle.HashName,
                BuildinFileCopyOption = buildinFileCopyOption,
                BuildinFileCopyParams = string.Empty,
                CompressOption = ECompressOption.LZ4,
                ClearBuildCacheFiles = true,
                UseAssetDependencyDB = true
            };

            var pipeline = new BuiltinBuildPipeline();
            BuildResult result = pipeline.Run(buildParameters, true);
            if (!result.Success)
            {
                throw new Exception($"YooAsset package build failed: {result.ErrorInfo}");
            }

            Debug.Log($"[BuildYooAssetPackage] Output: {result.OutputPackageDirectory}");
            return result;
        }

        public static BuildResult BuildRawFilePackage(
            string packageName,
            BuildTarget target,
            EBuildinFileCopyOption buildinFileCopyOption,
            string packageVersion = null)
        {
            packageName = NormalizePackageName(packageName, HotfixRuntimeSettings.DefaultRawFilePackageName);
            var buildParameters = new RawFileBuildParameters
            {
                BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(),
                BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot(),
                BuildPipeline = EBuildPipeline.RawFileBuildPipeline.ToString(),
                BuildBundleType = YooAssetBuildBundleTypeRawBundle,
                BuildTarget = target,
                PackageName = packageName,
                PackageVersion = string.IsNullOrWhiteSpace(packageVersion)
                    ? CreatePackageVersion(target)
                    : packageVersion.Trim(),
                VerifyBuildingResult = true,
                FileNameStyle = EFileNameStyle.HashName,
                BuildinFileCopyOption = buildinFileCopyOption,
                BuildinFileCopyParams = string.Empty,
                ClearBuildCacheFiles = true,
                UseAssetDependencyDB = true
            };

            var pipeline = new RawFileBuildPipeline();
            BuildResult result = pipeline.Run(buildParameters, true);
            if (!result.Success)
            {
                throw new Exception($"YooAsset RawFile package build failed: {result.ErrorInfo}");
            }

            Debug.Log($"[BuildRawFilePackage] Output: {result.OutputPackageDirectory}");
            return result;
        }

        public static void AppendRawFilePackageToStreamingAssets(
            string packageName,
            string packageVersion,
            BuildResult rawFileBuildResult)
        {
            if (rawFileBuildResult == null || string.IsNullOrWhiteSpace(rawFileBuildResult.OutputPackageDirectory))
            {
                throw new InvalidOperationException("RawFile build result is missing; can not copy it to StreamingAssets.");
            }

            if (!YooAssetLocalManifestBridge.TryCopyBuiltPackageToBuildin(
                    rawFileBuildResult.OutputPackageDirectory,
                    AssetBundleBuilderHelper.GetStreamingAssetsRoot(),
                    NormalizePackageName(packageName, HotfixRuntimeSettings.DefaultRawFilePackageName),
                    packageVersion,
                    out var error))
            {
                throw new InvalidOperationException(error);
            }

            AssetDatabase.Refresh();
        }

        public static RuntimePackageConfig GetRuntimePackageConfigFromCollectorSettings()
        {
            var mainPackageName = GetConfiguredMainPackageName();
            var includeRawFilePackage = TryGetConfiguredRawFilePackageName(mainPackageName, out var rawFilePackageName);
            return new RuntimePackageConfig(mainPackageName, includeRawFilePackage, rawFilePackageName);
        }

        public static string GetConfiguredMainPackageName()
        {
            var packageNames = GetConfiguredPackageNames();
            var defaultPackage = packageNames.FirstOrDefault(packageName =>
                string.Equals(packageName, HotfixRuntimeSettings.DefaultMainPackageName, StringComparison.OrdinalIgnoreCase));

            return NormalizePackageName(
                defaultPackage ?? packageNames.FirstOrDefault(),
                HotfixRuntimeSettings.DefaultMainPackageName);
        }

        public static List<string> FindAllAOTMetaAssemblies(BuildTarget buildTarget)
        {
            string folder = SettingsUtil.GetAssembliesPostIl2CppStripDir(buildTarget);
            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException(
                    $"AOT metadata folder not found. 请先执行 Build/热更新/内部工具/安全生成 HybridCLR 数据。Folder: {folder}");
            }

            var aotAssemblies = GetConfiguredAOTMetaAssemblies();
            foreach (var dll in aotAssemblies)
            {
                string dllPath = Path.Combine(folder, dll);
                if (!File.Exists(dllPath))
                {
                    throw new FileNotFoundException($"AOT metadata assembly not found: {dllPath}", dllPath);
                }
            }

            return aotAssemblies;
        }

        public static List<string> GetAllHotfixAssemblies()
        {
            return SettingsUtil.HotUpdateAssemblyFilesExcludePreserved
                .Where(dll => !string.IsNullOrWhiteSpace(dll))
                .Select(NormalizeDllName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public static AOTAssemblyManifest CreateOrUpdateAOTAssemblyManifest(
            BuildTarget target,
            List<string> aotAssemblies,
            string releaseVersion = null)
        {
            Directory.CreateDirectory(ConfigsPath);

            var manifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(AOTAssemblyManifestAssetPath);
            if (manifest == null)
            {
                manifest = ScriptableObject.CreateInstance<AOTAssemblyManifest>();
                manifest.name = AOTAssemblyManifest.AssetName;
                AssetDatabase.CreateAsset(manifest, AOTAssemblyManifestAssetPath);
            }

            manifest.ReleaseVersion = string.IsNullOrWhiteSpace(releaseVersion)
                ? CreatePackageVersion(target)
                : releaseVersion.Trim();
            var releaseProfile = HotfixReleaseProfile.LoadSelectedOrDefault();
            manifest.ReleaseSequence = releaseProfile == null ? 0 : releaseProfile.ReleaseSequence;
            manifest.AppVersion = GetAppVersion();
            manifest.BuildTarget = GetRuntimePlatformName(target);
            manifest.AotMetadataAssemblies = NormalizeDllNames(aotAssemblies);
            manifest.AotMetadataFiles = CreateAssemblyFileRecords(AOTCodesPath, manifest.AotMetadataAssemblies);
            manifest.AotVersion = CreateAotVersion(manifest.AppVersion, manifest.BuildTarget, manifest.AotMetadataFiles);
            manifest.BaselineGeneratedAtUtc = DateTime.UtcNow.ToString("O");
            manifest.BaselineGitCommit = string.Empty;
            manifest.BaselineFingerprint = CreateAOTBaselineFingerprint(
                manifest.AppVersion,
                manifest.BuildTarget,
                manifest.BaselineGeneratedAtUtc,
                manifest.AotMetadataAssemblies,
                manifest.AotMetadataFiles);

            HotfixManifestSigningUtility.SignOrClear(manifest);

            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return manifest;
        }

        public static HotfixAssemblyManifest CreateOrUpdateHotfixAssemblyManifest(
            BuildTarget target,
            List<string> hotfixAssemblies,
            string requiredAotVersion,
            string releaseVersion = null)
        {
            Directory.CreateDirectory(ConfigsPath);

            var oldManifest = AssetDatabase.LoadAssetAtPath<AssemblyManifest>(AssemblyManifestAssetPath);
            var manifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            if (manifest == null)
            {
                manifest = ScriptableObject.CreateInstance<HotfixAssemblyManifest>();
                manifest.name = HotfixAssemblyManifest.AssetName;

                manifest.AppVersionMin = GetAppVersion();
                manifest.AppVersionMax = GetAppVersion();

                // CodeEntry 模式下不再回填 scene / prefab
                manifest.EntrySceneAddress = string.Empty;
                manifest.EntryPrefabAddress = string.Empty;

                manifest.EntryTypeName = "HotfixDemo.HotfixCodeEntry";
                manifest.EntryMethodName = string.Empty;

                AssetDatabase.CreateAsset(manifest, HotfixAssemblyManifestAssetPath);
            }

            if (string.IsNullOrWhiteSpace(manifest.AppVersionMin))
            {
                manifest.AppVersionMin = GetAppVersion();
            }

            if (string.IsNullOrWhiteSpace(manifest.AppVersionMax))
            {
                manifest.AppVersionMax = GetAppVersion();
            }

            var releaseProfile = HotfixReleaseProfile.LoadSelectedOrDefault();
            manifest.ReleaseVersion = string.IsNullOrWhiteSpace(releaseVersion)
                ? CreatePackageVersion(target)
                : releaseVersion.Trim();
            manifest.ReleaseSequence = releaseProfile == null ? 0 : releaseProfile.ReleaseSequence;
            manifest.BuildTarget = GetRuntimePlatformName(target);
            manifest.RequiredAotVersion = requiredAotVersion ?? string.Empty;
            if (releaseProfile != null)
            {
                if (!releaseProfile.IsCompatibleWith(target))
                {
                    throw new InvalidOperationException(
                        $"ReleaseProfile BuildTarget mismatch. Profile={releaseProfile.BuildTarget}, Build={target}.");
                }

                releaseProfile.ApplyToHotfixManifest(manifest);
            }

            var dependencySortResult = HotfixAssemblyDependencySorter.Sort(HotfixCodesPath, hotfixAssemblies);
            manifest.HotUpdateAssemblies = dependencySortResult.SortedAssemblies;
            manifest.HotUpdateFiles = CreateAssemblyFileRecords(HotfixCodesPath, manifest.HotUpdateAssemblies);
            manifest.HotUpdateDependencies = dependencySortResult.Dependencies;
            string generatedHotfixVersion = CreateHotfixVersion(
                manifest.AppVersionMin,
                manifest.AppVersionMax,
                manifest.BuildTarget,
                manifest.RequiredAotVersion,
                manifest.HotUpdateFiles);
            manifest.HotfixVersion = releaseProfile == null || string.IsNullOrWhiteSpace(releaseProfile.HotfixVersion)
                ? generatedHotfixVersion
                : releaseProfile.HotfixVersion.Trim();

            if (string.IsNullOrWhiteSpace(manifest.EntryTypeName))
            {
                manifest.EntryTypeName = "HotfixDemo.HotfixCodeEntry";
            }

            manifest.EntryMethodName = string.Empty;

            HotfixManifestSigningUtility.SignOrClear(manifest);

            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return manifest;
        }

        public static void BindRawFileManifestToHotfixManifest(
            HotfixAssemblyManifest manifest,
            string rawFilePackageName,
            string packageVersion,
            BuildResult rawFileBuildResult)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            if (rawFileBuildResult == null || string.IsNullOrWhiteSpace(rawFileBuildResult.OutputPackageDirectory))
            {
                throw new InvalidOperationException("RawFile build result is missing; can not bind its manifest to the Hotfix manifest.");
            }

            string normalizedPackageName = NormalizePackageName(
                rawFilePackageName,
                HotfixRuntimeSettings.DefaultRawFilePackageName);
            if (string.IsNullOrWhiteSpace(packageVersion))
            {
                throw new InvalidOperationException("RawFile package version is empty.");
            }

            string normalizedPackageVersion = packageVersion.Trim();
            if (!YooAssetLocalManifestBridge.TryGetBuiltManifestFingerprint(
                    rawFileBuildResult.OutputPackageDirectory,
                    normalizedPackageName,
                    normalizedPackageVersion,
                    out var fingerprint))
            {
                throw new InvalidOperationException(fingerprint.Error);
            }

            manifest.RawFilePackageName = normalizedPackageName;
            manifest.RawFilePackageVersion = normalizedPackageVersion;
            manifest.RawFileManifestSha256 = fingerprint.Sha256;
            HotfixManifestSigningUtility.SignOrClear(manifest);
            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssetIfDirty(manifest);
        }

        public static void ClearRawFileManifestBinding(HotfixAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            manifest.RawFilePackageName = string.Empty;
            manifest.RawFilePackageVersion = string.Empty;
            manifest.RawFileManifestSha256 = string.Empty;
            HotfixManifestSigningUtility.SignOrClear(manifest);
            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssetIfDirty(manifest);
        }

        private static void ApplyRawFileManifestBinding(
            RuntimePackageConfig packageConfig,
            string packageVersion,
            BuildResult rawFileBuildResult,
            HotfixAssemblyManifest hotfixManifest)
        {
            if (packageConfig.IncludeRawFilePackage)
            {
                BindRawFileManifestToHotfixManifest(
                    hotfixManifest,
                    packageConfig.RawFilePackageName,
                    packageVersion,
                    rawFileBuildResult);
                return;
            }

            ClearRawFileManifestBinding(hotfixManifest);
        }

        public static AssemblyManifest CreateOrUpdateAssemblyManifest(List<string> aotAssemblies, List<string> hotfixAssemblies)
        {
            Directory.CreateDirectory(ConfigsPath);

            var manifest = AssetDatabase.LoadAssetAtPath<AssemblyManifest>(AssemblyManifestAssetPath);
            if (manifest == null)
            {
                manifest = ScriptableObject.CreateInstance<AssemblyManifest>();
                manifest.name = AssemblyManifest.AssetName;
                manifest.EntrySceneAddress = DefaultEntrySceneAddress;
                AssetDatabase.CreateAsset(manifest, AssemblyManifestAssetPath);
            }

            manifest.AotMetadataAssemblies = NormalizeDllNames(aotAssemblies);
            manifest.HotUpdateAssemblies = NormalizeDllNames(hotfixAssemblies);
            if (string.IsNullOrWhiteSpace(manifest.EntrySceneAddress))
            {
                manifest.EntrySceneAddress = DefaultEntrySceneAddress;
            }

            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return manifest;
        }

        [Obsolete("Use BuildInitialYooAssetPackage instead.")]
        public static void CopyABAOTHotUpdateDlls(BuildTarget target)
        {
            CompileDllCommand.CompileDll(target);
            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
            BuildYooAssetPackage(GetConfiguredMainPackageName(), target, true);
        }

        [Obsolete("Use BuildYooAssetPackage instead.")]
        public static void BuildSceneAssetBundleActiveBuildTargetExcludeAOT()
        {
            BuildHotfixYooAssetPackage();
        }

        public static void ValidateStartupPackageForBuild(BuildTarget target)
        {
            ValidateStartupPackageForBuild(target, true);
        }

        public static void ValidateStartupPackageForBuild(BuildTarget target, bool validateRawFileBinding)
        {
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            var playerPlayMode = settings == null ? EPlayMode.HostPlayMode : settings.PlayerPlayMode;
            ValidateStartupPackageForBuild(target, settings, playerPlayMode, true, validateRawFileBinding);
        }

        public static void ValidateStartupPackageForPlayerBuild(BuildTarget target, EPlayMode playerPlayMode)
        {
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            ValidateStartupPackageForBuild(target, settings, playerPlayMode, false, true);
        }

        private static void ValidateStartupPackageForBuild(
            BuildTarget target,
            HotfixRuntimeSettings settings,
            EPlayMode playerPlayMode,
            bool validatePackageAssets,
            bool validateRawFileBinding)
        {
            var packageMode = settings == null ? StartupPackageMode.FirstPackage : settings.StartupPackageMode;
            ValidateStartupDownloadTags(settings);
            if (packageMode == StartupPackageMode.EmptyPackage &&
                playerPlayMode == EPlayMode.OfflinePlayMode)
            {
                throw new InvalidOperationException(
                    "StartupPackageMode.EmptyPackage requires HostPlayMode or WebPlayMode so the player can request the remote package version and manifest before downloading startup resources.");
            }

            if (packageMode == StartupPackageMode.OfflinePackage &&
                playerPlayMode != EPlayMode.OfflinePlayMode)
            {
                throw new InvalidOperationException(
                    $"StartupPackageMode.OfflinePackage requires OfflinePlayMode for player builds, current mode is {playerPlayMode}.");
            }

            if (packageMode == StartupPackageMode.EmptyPackage && !validatePackageAssets)
            {
                return;
            }

            ValidateSplitAssemblyManifestsForBuild(target);
            if (validateRawFileBinding)
            {
                ValidateRawFileManifestBinding(settings);
            }
            // var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            // if (packageMode != StartupPackageMode.EmptyPackage)
            // {
            //     ValidateFirstPackageAssetFiles(hotfixManifest);
            // }

            if (validatePackageAssets)
            {
                ValidateStartupResourceCollection(settings);
            }
        }

        public static void ValidateStartupDownloadTags(HotfixRuntimeSettings settings)
        {
            if (settings == null)
            {
                return;
            }

            ValidateStartupDownloadTags(settings.StartupDownloadMode, settings.StartupDownloadTags);
            if (settings.IncludeRawFilePackage)
            {
                ValidateRawFilePackageForBuild(
                    settings.MainPackageName,
                    settings.RawFilePackageName,
                    settings.StartupDownloadMode,
                    settings.RawFileStartupDownloadTags);
            }
        }

        public static void ValidateStartupDownloadTags(StartupDownloadMode downloadMode, string[] tags)
        {
            if (downloadMode != StartupDownloadMode.DownloadByTags)
            {
                return;
            }

            if (!ContainsTag(tags, RequiredStartupTag))
            {
                throw new InvalidOperationException(
                    $"StartupDownloadMode.DownloadByTags requires StartupDownloadTags to include '{RequiredStartupTag}'. Current tags: {FormatTags(tags)}");
            }
        }

        public static void ValidateRawFilePackageForBuild(
            string mainPackageName,
            string rawFilePackageName,
            StartupDownloadMode downloadMode,
            string[] rawFileTags)
        {
            string normalizedMainPackageName = NormalizePackageName(
                mainPackageName,
                HotfixRuntimeSettings.DefaultMainPackageName);
            string normalizedRawFilePackageName = NormalizePackageName(
                rawFilePackageName,
                HotfixRuntimeSettings.DefaultRawFilePackageName);
            if (string.Equals(
                    normalizedMainPackageName,
                    normalizedRawFilePackageName,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"RawFile package must be different from the main package: {normalizedMainPackageName}");
            }

            var setting = AssetBundleCollectorSettingData.Setting;
            var package = setting == null || setting.Packages == null
                ? null
                : setting.Packages.FirstOrDefault(candidate =>
                    candidate != null &&
                    string.Equals(
                        candidate.PackageName,
                        normalizedRawFilePackageName,
                        StringComparison.OrdinalIgnoreCase));
            if (package == null)
            {
                throw new InvalidOperationException(
                    $"RawFile package is enabled but its YooAsset collector package was not found: {normalizedRawFilePackageName}");
            }

            if (downloadMode != StartupDownloadMode.DownloadByTags)
            {
                return;
            }

            string[] normalizedTags = HotfixUtility.NormalizeTags(rawFileTags);
            if (normalizedTags.Length == 0)
            {
                throw new InvalidOperationException(
                    $"StartupDownloadMode.DownloadByTags requires RawFileStartupDownloadTags when package '{normalizedRawFilePackageName}' is enabled.");
            }

            var collectorTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var group in package.Groups ?? Enumerable.Empty<AssetBundleCollectorGroup>())
            {
                if (group == null)
                {
                    continue;
                }

                foreach (var tag in NormalizeTagText(group.AssetTags))
                {
                    collectorTags.Add(tag);
                }

                foreach (var collector in group.Collectors ?? Enumerable.Empty<AssetBundleCollector>())
                {
                    if (collector == null)
                    {
                        continue;
                    }

                    foreach (var tag in NormalizeTagText(collector.AssetTags))
                    {
                        collectorTags.Add(tag);
                    }
                }
            }

            string[] missingTags = normalizedTags.Where(tag => !collectorTags.Contains(tag)).ToArray();
            if (missingTags.Length > 0)
            {
                throw new InvalidOperationException(
                    $"RawFile startup download tags are not present in YooAsset collectors. " +
                    $"Package={normalizedRawFilePackageName}, Missing={FormatTags(missingTags)}, " +
                    $"CollectorTags={FormatTags(collectorTags)}");
            }
        }

        private static void ValidateRawFileManifestBinding(HotfixRuntimeSettings settings)
        {
            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            if (hotfixManifest == null)
            {
                throw new InvalidOperationException($"Hotfix manifest missing: {HotfixAssemblyManifestAssetPath}");
            }

            if (settings == null || !settings.IncludeRawFilePackage)
            {
                if (!string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageName) ||
                    !string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageVersion) ||
                    !string.IsNullOrWhiteSpace(hotfixManifest.RawFileManifestSha256))
                {
                    throw new InvalidOperationException(
                        "Hotfix manifest contains a RawFile trust binding while RuntimeSettings has RawFile disabled. Rebuild the package.");
                }

                return;
            }

            if (!string.Equals(
                    hotfixManifest.RawFilePackageName,
                    settings.RawFilePackageName,
                    StringComparison.Ordinal) ||
                string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageVersion) ||
                string.IsNullOrWhiteSpace(hotfixManifest.RawFileManifestSha256))
            {
                throw new InvalidOperationException(
                    $"Hotfix manifest RawFile trust binding is missing or stale. " +
                    $"Expected package={settings.RawFilePackageName}, " +
                    $"Actual={hotfixManifest.RawFilePackageName}:{hotfixManifest.RawFilePackageVersion}:{hotfixManifest.RawFileManifestSha256}");
            }
        }

        public static void ValidateSplitAssemblyManifestsForBuild(BuildTarget target)
        {
            var aotManifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(AOTAssemblyManifestAssetPath);
            if (aotManifest == null)
            {
                throw new InvalidOperationException($"AOT manifest missing: {AOTAssemblyManifestAssetPath}");
            }

            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            if (hotfixManifest == null)
            {
                throw new InvalidOperationException($"Hotfix manifest missing: {HotfixAssemblyManifestAssetPath}");
            }

            string buildTargetName = GetRuntimePlatformName(target);
            if (!string.Equals(aotManifest.BuildTarget, buildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"AOT manifest target mismatch. Manifest={aotManifest.BuildTarget}, Build={buildTargetName}");
            }

            if (!string.Equals(hotfixManifest.BuildTarget, buildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Hotfix manifest target mismatch. Manifest={hotfixManifest.BuildTarget}, Build={buildTargetName}");
            }

            if (string.IsNullOrWhiteSpace(aotManifest.AotVersion))
            {
                throw new InvalidOperationException("AOT manifest AotVersion is empty.");
            }

            if (string.IsNullOrWhiteSpace(hotfixManifest.RequiredAotVersion))
            {
                throw new InvalidOperationException("Hotfix manifest RequiredAotVersion is empty.");
            }

            if (!string.Equals(aotManifest.AotVersion, hotfixManifest.RequiredAotVersion, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Hotfix manifest requires AOT version {hotfixManifest.RequiredAotVersion}, but AOT manifest version is {aotManifest.AotVersion}.");
            }

            if (aotManifest.AotMetadataAssemblies == null || aotManifest.AotMetadataAssemblies.Count == 0)
            {
                throw new InvalidOperationException("AOT manifest has no metadata assemblies.");
            }

            if (hotfixManifest.HotUpdateAssemblies == null || hotfixManifest.HotUpdateAssemblies.Count == 0)
            {
                throw new InvalidOperationException("Hotfix manifest has no hot update assemblies.");
            }

            if (string.IsNullOrWhiteSpace(hotfixManifest.EntryTypeName))
            {
                throw new InvalidOperationException("Hotfix manifest EntryTypeName is empty. IHotfixEntry is required.");
            }

            ValidateManifestSignatureForBuild(aotManifest, hotfixManifest);

            ValidateHotfixEntryType(hotfixManifest);

            ValidateAssemblyFiles(AOTCodesPath, aotManifest.AotMetadataAssemblies, "AOT metadata");
            ValidateAssemblyFiles(HotfixCodesPath, hotfixManifest.HotUpdateAssemblies, "Hotfix DLL");
            ValidateAssemblyRecordsUnchanged(
                aotManifest.AotMetadataFiles,
                CreateAssemblyFileRecords(AOTCodesPath, aotManifest.AotMetadataAssemblies),
                "AOT metadata");
            ValidateAssemblyRecordsUnchanged(
                hotfixManifest.HotUpdateFiles,
                CreateAssemblyFileRecords(HotfixCodesPath, hotfixManifest.HotUpdateAssemblies),
                "Hotfix DLL");
            var dependencySortResult = HotfixAssemblyDependencySorter.Sort(HotfixCodesPath, hotfixManifest.HotUpdateAssemblies);
            if (!AreSameOrderedList(hotfixManifest.HotUpdateAssemblies, dependencySortResult.SortedAssemblies) ||
                !AreSameDependencyRecords(hotfixManifest.HotUpdateDependencies, dependencySortResult.Dependencies))
            {
                throw new InvalidOperationException(
                    "Hotfix DLL dependency records are out of date. Please rebuild the Hotfix manifest so HotUpdateAssemblies is generated by dependency sorting.");
            }
        }

        private static void ValidateHotfixEntryType(HotfixAssemblyManifest hotfixManifest)
        {
            string entryTypeName = hotfixManifest.EntryTypeName.Trim();
            Type entryType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(entryTypeName, false))
                .FirstOrDefault(type => type != null);
            if (entryType == null)
            {
                throw new InvalidOperationException(
                    $"Hotfix entry type not found in compiled assemblies: {entryTypeName}. " +
                    "Please ensure the type is included in HotUpdateAssemblies and Unity scripts have recompiled.");
            }

            if (!typeof(IHotfixEntry).IsAssignableFrom(entryType))
            {
                throw new InvalidOperationException(
                    $"Hotfix entry type must implement {typeof(IHotfixEntry).FullName}: {entryTypeName}");
            }

            if (entryType.IsAbstract || entryType.IsInterface ||
                entryType.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new InvalidOperationException(
                    $"Hotfix entry type must be a concrete class with a public parameterless constructor: {entryTypeName}");
            }
        }

        public static void ValidateAOTManifestNotExpired(BuildTarget target, AOTAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                throw new InvalidOperationException($"AOT manifest missing: {AOTAssemblyManifestAssetPath}");
            }

            string buildTargetName = GetRuntimePlatformName(target);
            if (!string.Equals(manifest.BuildTarget, buildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"AOT manifest target mismatch. Manifest={manifest.BuildTarget}, Build={buildTargetName}");
            }

            string appVersion = GetAppVersion();
            if (!string.Equals(manifest.AppVersion, appVersion, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"AOT manifest AppVersion mismatch. Manifest={manifest.AppVersion}, PlayerSettings={appVersion}. " +
                    "请构建首包建立新的 App 基线，或明确选择 AOT 元数据补丁。");
            }

            var manifestAssemblies = NormalizeDllNames(manifest.AotMetadataAssemblies);
            if (manifestAssemblies.Count == 0)
            {
                throw new InvalidOperationException("AOT manifest has no metadata assemblies.");
            }

            var configuredAssemblies = GetConfiguredAOTMetaAssemblies();
            if (!AreSameList(manifestAssemblies, configuredAssemblies))
            {
                throw new InvalidOperationException(
                    "HybridCLR AOT metadata assembly list changed. 普通热更包不能更新 AOT 基线，请构建首包或选择 AOT 元数据补丁。");
            }

            ValidateAssemblyFiles(AOTCodesPath, manifestAssemblies, "AOT metadata");
            var currentRecords = CreateAssemblyFileRecords(AOTCodesPath, manifestAssemblies);
            ValidateAssemblyRecordsUnchanged(manifest.AotMetadataFiles, currentRecords, "AOT metadata");

            string manifestFingerprint = string.IsNullOrWhiteSpace(manifest.BaselineFingerprint)
                ? CreateAOTBaselineFingerprint(
                    manifest.AppVersion,
                    manifest.BuildTarget,
                    manifest.BaselineGeneratedAtUtc,
                    manifestAssemblies,
                    manifest.AotMetadataFiles)
                : manifest.BaselineFingerprint;
            string currentFingerprint = CreateAOTBaselineFingerprint(
                manifest.AppVersion,
                manifest.BuildTarget,
                manifest.BaselineGeneratedAtUtc,
                manifestAssemblies,
                currentRecords);
            if (!string.Equals(manifestFingerprint, currentFingerprint, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "AOT manifest baseline fingerprint changed. 普通热更包已阻断，请构建首包建立新基线，或明确选择 AOT 元数据补丁。");
            }
        }

        private static AOTAssemblyManifest LoadValidatedAOTManifestForHotfixBuild(BuildTarget target)
        {
            var aotManifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(AOTAssemblyManifestAssetPath);
            ValidateAOTManifestNotExpired(target, aotManifest);
            return aotManifest;
        }

        public static void ValidateHotfixAppVersionRange(HotfixAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                throw new InvalidOperationException($"Hotfix manifest missing: {HotfixAssemblyManifestAssetPath}");
            }

            string appVersion = GetAppVersion();
            if (!string.IsNullOrWhiteSpace(manifest.AppVersionMin) &&
                CompareVersion(appVersion, manifest.AppVersionMin) < 0)
            {
                throw new InvalidOperationException(
                    $"Hotfix manifest AppVersionMin mismatch. Current={appVersion}, Min={manifest.AppVersionMin}");
            }

            if (!string.IsNullOrWhiteSpace(manifest.AppVersionMax) &&
                CompareVersion(appVersion, manifest.AppVersionMax) > 0)
            {
                throw new InvalidOperationException(
                    $"Hotfix manifest AppVersionMax mismatch. Current={appVersion}, Max={manifest.AppVersionMax}");
            }

            if (!string.IsNullOrWhiteSpace(manifest.AppVersionMin) &&
                !string.IsNullOrWhiteSpace(manifest.AppVersionMax) &&
                CompareVersion(manifest.AppVersionMin, manifest.AppVersionMax) > 0)
            {
                throw new InvalidOperationException(
                    $"Hotfix manifest version range is invalid. Min={manifest.AppVersionMin}, Max={manifest.AppVersionMax}");
            }
        }

        private static List<string> GetConfiguredAOTMetaAssemblies()
        {
            if (TryGetGeneratedAOTAssemblyList(out var generatedAssemblies))
            {
                return generatedAssemblies;
            }

            return NormalizeDllNames(SettingsUtil.AOTAssemblyNames);
        }

        private static bool TryGetGeneratedAOTAssemblyList(out List<string> assemblies)
        {
            assemblies = new List<string>();
            var referencesType = FindAOTGenericReferencesType();
            if (referencesType == null)
            {
                return false;
            }

            var field = referencesType.GetField(
                "PatchedAOTAssemblyList",
                BindingFlags.Public | BindingFlags.Static);
            if (field != null && field.GetValue(null) is IEnumerable<string> fieldValue)
            {
                assemblies = NormalizeDllNames(fieldValue);
                return true;
            }

            var property = referencesType.GetProperty(
                "PatchedAOTAssemblyList",
                BindingFlags.Public | BindingFlags.Static);
            if (property != null && property.GetValue(null) is IEnumerable<string> propertyValue)
            {
                assemblies = NormalizeDllNames(propertyValue);
                return true;
            }

            return false;
        }

        private static Type FindAOTGenericReferencesType()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var referencesType = assembly.GetType("AOTGenericReferences", false);
                if (referencesType != null)
                {
                    return referencesType;
                }
            }

            return null;
        }

        private static List<string> GetManifestOrConfiguredHotfixAssemblies()
        {
            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            if (hotfixManifest != null &&
                hotfixManifest.HotUpdateAssemblies != null &&
                hotfixManifest.HotUpdateAssemblies.Count > 0)
            {
                return NormalizeDllNames(hotfixManifest.HotUpdateAssemblies);
            }

            var manifest = AssetDatabase.LoadAssetAtPath<AssemblyManifest>(AssemblyManifestAssetPath);
            if (manifest != null && manifest.HotUpdateAssemblies != null && manifest.HotUpdateAssemblies.Count > 0)
            {
                return NormalizeDllNames(manifest.HotUpdateAssemblies);
            }

            return GetAllHotfixAssemblies();
        }

        private static List<AssemblyFileRecord> CreateAssemblyFileRecords(string folder, IEnumerable<string> dllNames)
        {
            var records = new List<AssemblyFileRecord>();
            foreach (var dllName in NormalizeDllNames(dllNames))
            {
                string filePath = GetAssemblyBytesPath(folder, dllName);
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Assembly bytes file not found: {filePath}", filePath);
                }

                var fileInfo = new FileInfo(filePath);
                string normalizedDllName = NormalizeDllName(dllName);
                records.Add(new AssemblyFileRecord
                {
                    FileName = normalizedDllName,
                    AssemblyName = normalizedDllName,
                    Sha256 = ComputeFileSha256(filePath),
                    Size = fileInfo.Length
                });
            }

            return records;
        }

        private static string CreateAotVersion(string appVersion, string buildTarget, IEnumerable<AssemblyFileRecord> records)
        {
            return "aot-" + ComputeSha256HashPrefix(BuildVersionSeed(appVersion, buildTarget, string.Empty, records));
        }

        private static string CreateAOTBaselineFingerprint(
            string appVersion,
            string buildTarget,
            string generatedAtUtc,
            IEnumerable<string> aotAssemblies,
            IEnumerable<AssemblyFileRecord> records)
        {
            var builder = new StringBuilder();
            builder.Append(appVersion ?? string.Empty).Append('|');
            builder.Append(buildTarget ?? string.Empty).Append('|');
            builder.Append(generatedAtUtc ?? string.Empty).Append('|');
            foreach (var assembly in NormalizeDllNames(aotAssemblies).OrderBy(name => name, StringComparer.OrdinalIgnoreCase))
            {
                builder.Append(assembly).Append('|');
            }

            builder.Append(BuildVersionSeed(appVersion, buildTarget, "aot-baseline", records));
            return ComputeSha256HashPrefix(builder.ToString());
        }

        private static string CreateHotfixVersion(
            string appVersionMin,
            string appVersionMax,
            string buildTarget,
            string requiredAotVersion,
            IEnumerable<AssemblyFileRecord> records)
        {
            string seed = BuildVersionSeed($"{appVersionMin}-{appVersionMax}", buildTarget, requiredAotVersion, records);
            return "hotfix-" + ComputeSha256HashPrefix(seed);
        }

        private static string BuildVersionSeed(
            string appVersion,
            string buildTarget,
            string extra,
            IEnumerable<AssemblyFileRecord> records)
        {
            var builder = new StringBuilder();
            builder.Append(appVersion ?? string.Empty).Append('|');
            builder.Append(buildTarget ?? string.Empty).Append('|');
            builder.Append(extra ?? string.Empty).Append('|');
            foreach (var record in (records ?? Enumerable.Empty<AssemblyFileRecord>())
                         .Where(record => record != null)
                         .OrderBy(record => record.AssemblyName, StringComparer.OrdinalIgnoreCase))
            {
                builder.Append(record.AssemblyName).Append(':');
                builder.Append(record.Sha256).Append(':');
                builder.Append(record.Size).Append('|');
            }

            return builder.ToString();
        }

        /// <summary>
        /// 使用 SHA256 计算哈希并截取前16位作为版本标识，与运行时 FNV-1a 灰度哈希不同。
        /// </summary>
        private static string ComputeSha256HashPrefix(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value ?? string.Empty));
                return BitConverter.ToString(bytes).Replace("-", string.Empty).Substring(0, 16).ToLowerInvariant();
            }
        }

        private static string ComputeFileSha256(string filePath)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] bytes = sha256.ComputeHash(stream);
                return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        private static int CompareVersion(string left, string right)
        {
            if (Version.TryParse(left, out var leftVersion) && Version.TryParse(right, out var rightVersion))
            {
                return leftVersion.CompareTo(rightVersion);
            }

            return string.Compare(left ?? string.Empty, right ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }

        private static void ValidateFirstPackageAssetFiles(HotfixAssemblyManifest hotfixManifest)
        {
            if (hotfixManifest == null)
            {
                throw new InvalidOperationException($"Hotfix manifest missing: {HotfixAssemblyManifestAssetPath}");
            }
        }
        

        private static void ValidateAssemblyFiles(string folder, IEnumerable<string> dllNames, string label)
        {
            foreach (var dllName in NormalizeDllNames(dllNames))
            {
                string filePath = GetAssemblyBytesPath(folder, dllName);
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"{label} bytes file missing: {filePath}", filePath);
                }
            }
        }

        private static void ValidateAssemblyRecordsUnchanged(
            IEnumerable<AssemblyFileRecord> expectedRecords,
            IEnumerable<AssemblyFileRecord> currentRecords,
            string label)
        {
            var expected = ToRecordMap(expectedRecords, label);
            var current = ToRecordMap(currentRecords, label);
            foreach (var expectedPair in expected)
            {
                if (!current.TryGetValue(expectedPair.Key, out var currentRecord))
                {
                    throw new InvalidOperationException($"{label} file missing from current scan: {expectedPair.Key}");
                }

                var expectedRecord = expectedPair.Value;
                if (expectedRecord.Size != currentRecord.Size ||
                    !string.Equals(expectedRecord.Sha256, currentRecord.Sha256, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        $"{label} file changed: {expectedPair.Key}. Expected size={expectedRecord.Size}, sha256={expectedRecord.Sha256}; " +
                        $"Current size={currentRecord.Size}, sha256={currentRecord.Sha256}.");
                }
            }

            foreach (var currentPair in current)
            {
                if (!expected.ContainsKey(currentPair.Key))
                {
                    throw new InvalidOperationException($"Unexpected {label} file in current scan: {currentPair.Key}");
                }
            }
        }

        private static Dictionary<string, AssemblyFileRecord> ToRecordMap(
            IEnumerable<AssemblyFileRecord> records,
            string label = "Assembly")
        {
            var map = new Dictionary<string, AssemblyFileRecord>(StringComparer.OrdinalIgnoreCase);
            if (records == null)
            {
                return map;
            }

            foreach (var record in records)
            {
                string recordName = GetAssemblyRecordFileName(record);
                if (string.IsNullOrWhiteSpace(recordName))
                {
                    throw new InvalidOperationException($"{label} hash records contain an empty entry.");
                }

                string normalizedName = NormalizeDllName(recordName);
                if (record.Size <= 0 || !IsSha256Hex(record.Sha256))
                {
                    throw new InvalidOperationException(
                        $"{label} hash record is invalid: {normalizedName}. Size={record.Size}, Sha256={record.Sha256}");
                }

                if (map.ContainsKey(normalizedName))
                {
                    throw new InvalidOperationException($"{label} hash records contain duplicate entry: {normalizedName}");
                }

                map.Add(normalizedName, record);
            }

            return map;
        }

        private static bool IsSha256Hex(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string text = value.Trim();
            if (text.Length != 64)
            {
                return false;
            }

            return text.All(IsHexChar);
        }

        private static bool IsHexChar(char character)
        {
            return character >= '0' && character <= '9' ||
                   character >= 'a' && character <= 'f' ||
                   character >= 'A' && character <= 'F';
        }

        private static string GetAssemblyRecordFileName(AssemblyFileRecord record)
        {
            if (record == null)
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(record.FileName) ? record.AssemblyName : record.FileName;
        }


        private static string FindAssetPathByAddress(string address, string extension)
        {
            string assetName = Path.GetFileNameWithoutExtension(address.Trim());
            foreach (var guid in AssetDatabase.FindAssets(assetName))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (string.Equals(Path.GetExtension(assetPath), extension, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(Path.GetFileNameWithoutExtension(assetPath), assetName, StringComparison.OrdinalIgnoreCase))
                {
                    return assetPath;
                }
            }

            return string.Empty;
        }

        private static void ValidateStartupResourceCollection(HotfixRuntimeSettings settings)
        {
            string packageName = settings == null ? GetConfiguredMainPackageName() : settings.MainPackageName;
            bool requireStartupTag = settings != null && settings.StartupDownloadMode == StartupDownloadMode.DownloadByTags;

            ValidateRequiredCollectorPath(ConfigsPath, "Configs", packageName, requireStartupTag);
            ValidateRequiredCollectorPath(AOTCodesPath, "AOTCodes", packageName, requireStartupTag);
            ValidateRequiredCollectorPath(HotfixCodesPath, "HotfixCodes", packageName, requireStartupTag);
            if (Directory.Exists(DatasPath))
            {
                ValidateRequiredCollectorPath(DatasPath, "Datas", packageName, requireStartupTag);
            }

            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            if (hotfixManifest == null)
            {
                return;
            }

            ValidateEntryAssetIfConfigured(hotfixManifest.EntrySceneAddress, ".unity", "EntryScene", packageName, requireStartupTag);
            ValidateEntryAssetIfConfigured(hotfixManifest.EntryPrefabAddress, ".prefab", "EntryPrefab", packageName, requireStartupTag);
        }

        private static void ValidateEntryAssetIfConfigured(
            string address,
            string extension,
            string label,
            string packageName,
            bool requireStartupTag)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return;
            }

            string assetPath = FindAssetPathByAddress(address, extension);
            if (string.IsNullOrWhiteSpace(assetPath))
            {
                throw new InvalidOperationException(
                    $"Required startup {label} asset not found. Address: {address}, Extension: {extension}");
            }

            ValidateRequiredCollectorAsset(assetPath, label, packageName, requireStartupTag);
        }

        private static void ValidateRequiredCollectorAsset(
            string assetPath,
            string label,
            string packageName,
            bool requireStartupTag)
        {
            ValidateRequiredCollectorItem(assetPath, label, packageName, requireStartupTag, false);
        }

        private static void ValidateRequiredCollectorPath(
            string requiredPath,
            string label,
            string packageName,
            bool requireStartupTag)
        {
            ValidateRequiredCollectorItem(requiredPath, label, packageName, requireStartupTag, true);
        }

        private static void ValidateRequiredCollectorItem(
            string assetPath,
            string label,
            string packageName,
            bool requireStartupTag,
            bool isFolder)
        {
            string normalizedPath = NormalizeAssetPath(assetPath);
            if (!TryFindCollectorForAssetPath(normalizedPath, packageName, out var collector))
            {
                string itemType = isFolder ? "path" : "asset";
                throw new InvalidOperationException(
                    $"Required startup {itemType} is not collected by YooAsset collector. Label: {label}, Path: {normalizedPath}, Package: {packageName}");
            }

            if (requireStartupTag && !collector.HasTag(RequiredStartupTag))
            {
                throw new InvalidOperationException(
                    $"Required startup resource is missing YooAsset tag '{RequiredStartupTag}'. Label: {label}, Path: {normalizedPath}, Collector: {collector.Describe()}, CurrentTags: {FormatTags(collector.Tags)}");
            }
        }

        private static bool TryFindCollectorForAssetPath(
            string assetPath,
            string packageName,
            out CollectorMatch match)
        {
            match = default(CollectorMatch);
            string normalizedAssetPath = NormalizeAssetPath(assetPath);
            var setting = AssetBundleCollectorSettingData.Setting;
            if (setting == null || setting.Packages == null)
            {
                return false;
            }

            string normalizedPackageName = NormalizePackageName(packageName, GetConfiguredMainPackageName());
            foreach (var package in setting.Packages)
            {
                if (package == null ||
                    package.Groups == null ||
                    !string.Equals(package.PackageName, normalizedPackageName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                foreach (var group in package.Groups)
                {
                    if (group == null || group.Collectors == null)
                    {
                        continue;
                    }

                    foreach (var collector in group.Collectors)
                    {
                        if (collector == null || string.IsNullOrWhiteSpace(collector.CollectPath))
                        {
                            continue;
                        }

                        string collectPath = NormalizeAssetPath(collector.CollectPath);
                        if (string.Equals(normalizedAssetPath, collectPath, StringComparison.OrdinalIgnoreCase) ||
                            normalizedAssetPath.StartsWith(collectPath + "/", StringComparison.OrdinalIgnoreCase))
                        {
                            match = new CollectorMatch(
                                package.PackageName,
                                group.GroupName,
                                collector.CollectPath,
                                NormalizeTagText(group.AssetTags, collector.AssetTags));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool ContainsTag(IEnumerable<string> tags, string requiredTag)
        {
            return tags != null &&
                   tags.Any(tag => string.Equals(tag, requiredTag, StringComparison.OrdinalIgnoreCase));
        }

        private static List<string> NormalizeTagText(params string[] tagTexts)
        {
            var tags = new List<string>();
            var exists = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var tagText in tagTexts ?? Array.Empty<string>())
            {
                if (string.IsNullOrWhiteSpace(tagText))
                {
                    continue;
                }

                var splitTags = tagText.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in splitTags)
                {
                    var normalizedTag = tag.Trim();
                    if (!string.IsNullOrWhiteSpace(normalizedTag) && exists.Add(normalizedTag))
                    {
                        tags.Add(normalizedTag);
                    }
                }
            }

            return tags;
        }

        private static string FormatTags(IEnumerable<string> tags)
        {
            var normalizedTags = tags == null
                ? new List<string>()
                : tags.Where(tag => !string.IsNullOrWhiteSpace(tag)).Select(tag => tag.Trim()).ToList();
            return normalizedTags.Count == 0 ? "空" : string.Join(", ", normalizedTags);
        }

        private static string NormalizeAssetPath(string assetPath)
        {
            return (assetPath ?? string.Empty).Replace('\\', '/').Trim().TrimEnd('/');
        }

        private static List<string> NormalizeDllNames(IEnumerable<string> dllNames)
        {
            if (dllNames == null)
            {
                return new List<string>();
            }

            return dllNames
                .Where(dll => !string.IsNullOrWhiteSpace(dll))
                .Select(NormalizeDllName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static bool AreSameList(List<string> left, List<string> right)
        {
            left = NormalizeDllNames(left);
            right = NormalizeDllNames(right);
            if (left.Count != right.Count)
            {
                return false;
            }

            left.Sort(StringComparer.OrdinalIgnoreCase);
            right.Sort(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < left.Count; i++)
            {
                if (!string.Equals(left[i], right[i], StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreSameOrderedList(List<string> left, List<string> right)
        {
            left = NormalizeDllNames(left);
            right = NormalizeDllNames(right);
            if (left.Count != right.Count)
            {
                return false;
            }

            for (int i = 0; i < left.Count; i++)
            {
                if (!string.Equals(left[i], right[i], StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreSameDependencyRecords(
            List<AssemblyDependencyRecord> left,
            List<AssemblyDependencyRecord> right)
        {
            left = NormalizeDependencyRecords(left);
            right = NormalizeDependencyRecords(right);
            if (left.Count != right.Count)
            {
                return false;
            }

            for (int i = 0; i < left.Count; i++)
            {
                if (!string.Equals(left[i].DllName, right[i].DllName, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(left[i].AssemblyName, right[i].AssemblyName, StringComparison.OrdinalIgnoreCase) ||
                    !AreSameOrderedList(left[i].DependsOn, right[i].DependsOn))
                {
                    return false;
                }
            }

            return true;
        }

        private static List<AssemblyDependencyRecord> NormalizeDependencyRecords(
            IEnumerable<AssemblyDependencyRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyDependencyRecord>())
                .Where(record => record != null && !string.IsNullOrWhiteSpace(record.DllName))
                .Select(record => new AssemblyDependencyRecord
                {
                    AssemblyName = record.AssemblyName ?? string.Empty,
                    DllName = NormalizeDllName(record.DllName),
                    DependsOn = NormalizeDllNames(record.DependsOn)
                        .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                        .ToList()
                })
                .OrderBy(record => record.DllName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static string NormalizeDllName(string dllName)
        {
            return dllName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) ? dllName : $"{dllName}.dll";
        }

        private static string GetAssemblyBytesPath(string folder, string dllName)
        {
            return Path.Combine(folder, $"{NormalizeDllName(dllName)}.bytes");
        }

        public static string CreatePackageVersion(BuildTarget target)
        {
            var releaseProfile = HotfixReleaseProfile.LoadSelectedOrDefault();
            if (releaseProfile != null && !string.IsNullOrWhiteSpace(releaseProfile.ResourceVersion))
            {
                if (!releaseProfile.IsCompatibleWith(target))
                {
                    throw new InvalidOperationException(
                        $"ReleaseProfile BuildTarget mismatch. Profile={releaseProfile.BuildTarget}, Build={target}.");
                }

                return releaseProfile.ResourceVersion.Trim();
            }

            return DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
        }

        private static void ValidateManifestSignatureForBuild(
            AOTAssemblyManifest aotManifest,
            HotfixAssemblyManifest hotfixManifest)
        {
            var profile = HotfixReleaseProfile.LoadSelectedOrDefault();
            if (profile == null || !profile.IsFormalRelease)
            {
                return;
            }

            var runtimeSettings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(
                HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            string aotError = string.Empty;
            if (runtimeSettings == null ||
                !runtimeSettings.TryGetTrustedManifestPublicKey(aotManifest.SigningKeyId, out var aotPublicKey) ||
                !AssemblyManifestSignatureUtility.Verify(aotManifest, aotPublicKey, out aotError))
            {
                throw new InvalidOperationException(
                    "AOT manifest signature validation failed before build. " +
                    (string.IsNullOrWhiteSpace(aotError)
                        ? $"Trusted key not found: {aotManifest.SigningKeyId}"
                        : aotError));
            }

            string hotfixError = string.Empty;
            if (!runtimeSettings.TryGetTrustedManifestPublicKey(hotfixManifest.SigningKeyId, out var hotfixPublicKey) ||
                !AssemblyManifestSignatureUtility.Verify(hotfixManifest, hotfixPublicKey, out hotfixError))
            {
                throw new InvalidOperationException(
                    "Hotfix manifest signature validation failed before build. " +
                    (string.IsNullOrWhiteSpace(hotfixError)
                        ? $"Trusted key not found: {hotfixManifest.SigningKeyId}"
                        : hotfixError));
            }

            if (string.IsNullOrWhiteSpace(hotfixManifest.ReleaseVersion))
            {
                throw new InvalidOperationException("Signed Hotfix manifest ReleaseVersion is empty.");
            }

            if (string.IsNullOrWhiteSpace(aotManifest.ReleaseVersion) ||
                aotManifest.ReleaseSequence <= 0 ||
                hotfixManifest.ReleaseSequence <= 0)
            {
                throw new InvalidOperationException(
                    $"Signed manifests require a release identity. " +
                    $"AOT={aotManifest.ReleaseVersion}:{aotManifest.ReleaseSequence}, " +
                    $"Hotfix={hotfixManifest.ReleaseVersion}:{hotfixManifest.ReleaseSequence}");
            }
        }

        private static bool ShouldCopyPackageToStreamingAssets()
        {
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            return settings == null || settings.StartupPackageMode != StartupPackageMode.EmptyPackage;
        }

        private static string GetRuntimePlatformName(BuildTarget target)
        {
            return HotfixUtility.GetPlatformNameForBuildTarget(target);
        }

        private static string GetAppVersion()
        {
            return string.IsNullOrWhiteSpace(PlayerSettings.bundleVersion)
                ? "1.0.0"
                : PlayerSettings.bundleVersion.Trim();
        }

        private static void BuildWithAOTCollectorDisabled(string packageName, Action buildAction)
        {
            var package = AssetBundleCollectorSettingData.Setting.GetPackage(packageName);
            if (package == null)
            {
                throw new InvalidOperationException($"YooAsset collector package not found: {packageName}");
            }

            var snapshots = new List<CollectorSnapshot>();
            foreach (var group in package.Groups)
            {
                for (int i = group.Collectors.Count - 1; i >= 0; i--)
                {
                    if (IsAOTCollector(group.Collectors[i]))
                    {
                        snapshots.Add(new CollectorSnapshot(group, i, group.Collectors[i]));
                        group.Collectors.RemoveAt(i);
                    }
                }
            }

            try
            {
                buildAction();
            }
            finally
            {
                foreach (var snapshot in snapshots.OrderBy(snapshot => snapshot.Index))
                {
                    snapshot.Group.Collectors.Insert(snapshot.Index, snapshot.Collector);
                }
            }
        }

        private static bool IsAOTCollector(AssetBundleCollector collector)
        {
            return string.Equals(
                collector.CollectPath.Replace('\\', '/'),
                AOTCodesPath,
                StringComparison.OrdinalIgnoreCase);
        }

        private static bool TryGetConfiguredRawFilePackageName(string mainPackageName, out string rawFilePackageName)
        {
            var packageNames = GetConfiguredPackageNames()
                .Where(packageName => !string.Equals(packageName, mainPackageName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (packageNames.Count > 1)
            {
                throw new InvalidOperationException(
                    "The current runtime supports one main package and at most one RawFile package. " +
                    $"Multiple secondary YooAsset packages were found: {string.Join(", ", packageNames)}. " +
                    "Merge them into one RawFile package or extend RuntimePackageConfig to explicit package descriptors.");
            }

            var rawFilePackage = packageNames.FirstOrDefault(packageName =>
                                     string.Equals(packageName, HotfixRuntimeSettings.DefaultRawFilePackageName, StringComparison.OrdinalIgnoreCase))
                                 ?? packageNames.FirstOrDefault(IsRawFilePackageName)
                                 ?? (packageNames.Count == 1 ? packageNames[0] : null);
            if (string.IsNullOrWhiteSpace(rawFilePackage))
            {
                rawFilePackageName = HotfixRuntimeSettings.DefaultRawFilePackageName;
                return false;
            }

            rawFilePackageName = rawFilePackage.Trim();
            return true;
        }

        private static bool IsRawFilePackageName(string packageName)
        {
            return packageName.IndexOf("RawFile", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   packageName.IndexOf("Raw", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static List<string> GetConfiguredPackageNames()
        {
            var setting = AssetBundleCollectorSettingData.Setting;
            if (setting == null || setting.Packages == null)
            {
                return new List<string>();
            }

            return setting.Packages
                .Where(package => !string.IsNullOrWhiteSpace(package.PackageName))
                .Select(package => package.PackageName.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static string NormalizePackageName(string packageName, string fallback)
        {
            return HotfixUtility.NormalizePackageName(packageName, fallback);
        }

        private struct CollectorMatch
        {
            public readonly string PackageName;
            public readonly string GroupName;
            public readonly string CollectPath;
            public readonly List<string> Tags;

            public CollectorMatch(string packageName, string groupName, string collectPath, List<string> tags)
            {
                PackageName = packageName ?? string.Empty;
                GroupName = groupName ?? string.Empty;
                CollectPath = collectPath ?? string.Empty;
                Tags = tags ?? new List<string>();
            }

            public bool HasTag(string tag)
            {
                return Tags != null &&
                       Tags.Any(value => string.Equals(value, tag, StringComparison.OrdinalIgnoreCase));
            }

            public string Describe()
            {
                return $"Package={PackageName}, Group={GroupName}, Collector={CollectPath}";
            }
        }

        private struct CollectorSnapshot
        {
            public readonly AssetBundleCollectorGroup Group;
            public readonly int Index;
            public readonly AssetBundleCollector Collector;

            public CollectorSnapshot(AssetBundleCollectorGroup group, int index, AssetBundleCollector collector)
            {
                Group = group;
                Index = index;
                Collector = collector;
            }
        }

        private static void PrepareDirectory(string path)
        {
            Directory.CreateDirectory(path);
            FolderUtils.ClearFolder(path);
        }
    }
}
