using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Framework;
using Framework.Assemblies;
using Framework.Utils;
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
        public const string AssemblyManifestAssetPath = ConfigsPath + "/AssemblyManifest.asset";
        public const string AOTAssemblyManifestAssetPath = ConfigsPath + "/AOTAssemblyManifest.asset";
        public const string HotfixAssemblyManifestAssetPath = ConfigsPath + "/HotfixAssemblyManifest.asset";
        // Mirrors YooAsset's internal EBuildBundleType.AssetBundle without referencing an internal enum.
        private const int YooAssetBuildBundleTypeAssetBundle = 2;

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

        [MenuItem("Build/Build Initial YooAsset Package")]
        public static void BuildInitialYooAssetPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            CompileDllCommand.CompileDll(target);

            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
            CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
            ValidateStartupPackageForBuild(target);

            BuildYooAssetPackage(packageConfig.MainPackageName, target, ShouldCopyPackageToStreamingAssets());
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/Build Hotfix YooAsset Package")]
        public static void BuildHotfixYooAssetPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var packageConfig = HotfixBuildProfileUtility.SyncPackageNamesFromCollectorSettings();
            CompileDllCommand.CompileDll(target);

            var aotManifest = EnsureAOTAssemblyManifest(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotManifest.AotMetadataAssemblies, hotfixAssemblies);
            ValidateSplitAssemblyManifestsForBuild(target);
            BuildYooAssetPackage(packageConfig.MainPackageName, target, false);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/BuildAssetsAndCopyToAssetsPackage")]
        public static void BuildAndCopyToAssetsPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);

            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
            CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
            ValidateStartupPackageForBuild(target);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/BuildAssetsAndCopyToStreamingAssets")]
        public static void BuildAndCopyABAOTHotUpdateDlls()
        {
            BuildInitialYooAssetPackage();
        }

        [MenuItem("Build/CopyAotDllsToAssetsPackage")]
        public static void BuildAndCopyAOTHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);
            CopyAotMetaDataDlls(target);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/CopyHotUpdateDllsToAssetsPackage")]
        public static void BuildAndCopyHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);
            CopyHotUpdateDlls();
            AssetDatabase.Refresh();
        }

        public static void CopyHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            var aotManifest = EnsureAOTAssemblyManifest(target);
            CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotManifest.AotMetadataAssemblies, hotfixAssemblies);
        }

        public static void CopyAotMetaDataDlls(BuildTarget target)
        {
            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var aotManifest = CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
            var hotfixAssemblies = GetManifestOrConfiguredHotfixAssemblies();
            CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
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
            CreateOrUpdateHotfixAssemblyManifest(target, hotfixAssemblies, aotManifest.AotVersion);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
        }

        public static BuildResult BuildYooAssetPackage(string packageName, BuildTarget target, bool copyToStreamingAssets)
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
                PackageVersion = CreatePackageVersion(),
                EnableSharePackRule = true,
                VerifyBuildingResult = true,
                FileNameStyle = EFileNameStyle.HashName,
                BuildinFileCopyOption = copyToStreamingAssets
                    ? EBuildinFileCopyOption.ClearAndCopyAll
                    : EBuildinFileCopyOption.None,
                BuildinFileCopyParams = string.Empty,
                CompressOption = ECompressOption.LZ4,
                ClearBuildCacheFiles = copyToStreamingAssets,
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
                    $"AOT metadata folder not found. Please run HybridCLR/Generate/All first. Folder: {folder}");
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

        public static AOTAssemblyManifest CreateOrUpdateAOTAssemblyManifest(BuildTarget target, List<string> aotAssemblies)
        {
            Directory.CreateDirectory(ConfigsPath);

            var manifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(AOTAssemblyManifestAssetPath);
            if (manifest == null)
            {
                manifest = ScriptableObject.CreateInstance<AOTAssemblyManifest>();
                manifest.name = AOTAssemblyManifest.AssetName;
                AssetDatabase.CreateAsset(manifest, AOTAssemblyManifestAssetPath);
            }

            manifest.AppVersion = GetAppVersion();
            manifest.BuildTarget = GetRuntimePlatformName(target);
            manifest.AotMetadataAssemblies = NormalizeDllNames(aotAssemblies);
            manifest.AotMetadataFiles = CreateAssemblyFileRecords(AOTCodesPath, manifest.AotMetadataAssemblies);
            manifest.AotVersion = CreateAotVersion(manifest.AppVersion, manifest.BuildTarget, manifest.AotMetadataFiles);

            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return manifest;
        }

        public static HotfixAssemblyManifest CreateOrUpdateHotfixAssemblyManifest(
            BuildTarget target,
            List<string> hotfixAssemblies,
            string requiredAotVersion)
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
                manifest.EntrySceneAddress = oldManifest == null || string.IsNullOrWhiteSpace(oldManifest.EntrySceneAddress)
                    ? DefaultEntrySceneAddress
                    : oldManifest.EntrySceneAddress;
                manifest.EntryPrefabAddress = oldManifest == null ? string.Empty : oldManifest.EntryPrefabAddress;
                manifest.EntryTypeName = oldManifest == null ? string.Empty : oldManifest.EntryTypeName;
                manifest.EntryMethodName = oldManifest == null ? string.Empty : oldManifest.EntryMethodName;
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

            manifest.BuildTarget = GetRuntimePlatformName(target);
            manifest.RequiredAotVersion = requiredAotVersion ?? string.Empty;
            manifest.HotUpdateAssemblies = NormalizeDllNames(hotfixAssemblies);
            manifest.HotUpdateFiles = CreateAssemblyFileRecords(HotfixCodesPath, manifest.HotUpdateAssemblies);
            manifest.HotfixVersion = CreateHotfixVersion(
                manifest.AppVersionMin,
                manifest.AppVersionMax,
                manifest.BuildTarget,
                manifest.RequiredAotVersion,
                manifest.HotUpdateFiles);

            if (string.IsNullOrWhiteSpace(manifest.EntrySceneAddress))
            {
                manifest.EntrySceneAddress = DefaultEntrySceneAddress;
            }

            EditorUtility.SetDirty(manifest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return manifest;
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
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            var playerPlayMode = settings == null ? EPlayMode.HostPlayMode : settings.PlayerPlayMode;
            ValidateStartupPackageForBuild(target, settings, playerPlayMode, true);
        }

        public static void ValidateStartupPackageForPlayerBuild(BuildTarget target, EPlayMode playerPlayMode)
        {
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            ValidateStartupPackageForBuild(target, settings, playerPlayMode, false);
        }

        private static void ValidateStartupPackageForBuild(
            BuildTarget target,
            HotfixRuntimeSettings settings,
            EPlayMode playerPlayMode,
            bool validatePackageAssets)
        {
            var packageMode = settings == null ? StartupPackageMode.FirstPackage : settings.StartupPackageMode;
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
            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(HotfixAssemblyManifestAssetPath);
            if (packageMode != StartupPackageMode.EmptyPackage)
            {
                ValidateFirstPackageAssetFiles(hotfixManifest);
            }

            if (validatePackageAssets)
            {
                ValidateCollectorContainsRequiredPath(ConfigsPath);
                ValidateCollectorContainsRequiredPath(AOTCodesPath);
                ValidateCollectorContainsRequiredPath(HotfixCodesPath);
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

            if (string.IsNullOrWhiteSpace(hotfixManifest.EntrySceneAddress) &&
                string.IsNullOrWhiteSpace(hotfixManifest.EntryPrefabAddress))
            {
                throw new InvalidOperationException("Hotfix manifest must configure EntrySceneAddress or EntryPrefabAddress.");
            }

            ValidateAssemblyFiles(AOTCodesPath, aotManifest.AotMetadataAssemblies, "AOT metadata");
            ValidateAssemblyFiles(HotfixCodesPath, hotfixManifest.HotUpdateAssemblies, "Hotfix DLL");
            ValidateEntryResource(hotfixManifest);
        }

        private static AOTAssemblyManifest EnsureAOTAssemblyManifest(BuildTarget target)
        {
            var manifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(AOTAssemblyManifestAssetPath);
            if (manifest != null &&
                manifest.AotMetadataAssemblies != null &&
                manifest.AotMetadataAssemblies.Count > 0 &&
                string.Equals(manifest.BuildTarget, GetRuntimePlatformName(target), StringComparison.OrdinalIgnoreCase))
            {
                return manifest;
            }

            var aotAssemblies = GetManifestOrConfiguredAOTAssemblies();
            return CreateOrUpdateAOTAssemblyManifest(target, aotAssemblies);
        }

        private static List<string> GetConfiguredAOTMetaAssemblies()
        {
            return AOTGenericReferences.PatchedAOTAssemblyList
                .Where(dll => !string.IsNullOrWhiteSpace(dll))
                .Select(NormalizeDllName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static List<string> GetManifestOrConfiguredAOTAssemblies()
        {
            var aotManifest = AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(AOTAssemblyManifestAssetPath);
            if (aotManifest != null && aotManifest.AotMetadataAssemblies != null && aotManifest.AotMetadataAssemblies.Count > 0)
            {
                return NormalizeDllNames(aotManifest.AotMetadataAssemblies);
            }

            var manifest = AssetDatabase.LoadAssetAtPath<AssemblyManifest>(AssemblyManifestAssetPath);
            if (manifest != null && manifest.AotMetadataAssemblies != null && manifest.AotMetadataAssemblies.Count > 0)
            {
                return NormalizeDllNames(manifest.AotMetadataAssemblies);
            }

            return GetConfiguredAOTMetaAssemblies();
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
                records.Add(new AssemblyFileRecord
                {
                    AssemblyName = dllName,
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
            foreach (var record in records.OrderBy(record => record.AssemblyName, StringComparer.OrdinalIgnoreCase))
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

        private static void ValidateFirstPackageAssetFiles(HotfixAssemblyManifest hotfixManifest)
        {
            if (hotfixManifest == null)
            {
                throw new InvalidOperationException($"Hotfix manifest missing: {HotfixAssemblyManifestAssetPath}");
            }

            ValidateEntryResource(hotfixManifest);
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

        private static void ValidateEntryResource(HotfixAssemblyManifest manifest)
        {
            if (!string.IsNullOrWhiteSpace(manifest.EntrySceneAddress))
            {
                string scenePath = FindAssetPathByAddress(manifest.EntrySceneAddress, ".unity");
                if (string.IsNullOrEmpty(scenePath))
                {
                    throw new FileNotFoundException(
                        $"Entry scene not found by address '{manifest.EntrySceneAddress}'. The current AddressByFileName rule expects a scene file with the same name.");
                }

                ValidateCollectorContainsRequiredAsset(scenePath);
                return;
            }

            if (!string.IsNullOrWhiteSpace(manifest.EntryPrefabAddress))
            {
                string prefabPath = FindAssetPathByAddress(manifest.EntryPrefabAddress, ".prefab");
                if (string.IsNullOrEmpty(prefabPath))
                {
                    throw new FileNotFoundException(
                        $"Entry prefab not found by address '{manifest.EntryPrefabAddress}'. The current AddressByFileName rule expects a prefab file with the same name.");
                }

                ValidateCollectorContainsRequiredAsset(prefabPath);
            }
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

        private static void ValidateCollectorContainsRequiredAsset(string assetPath)
        {
            if (!IsAssetPathCollected(assetPath))
            {
                throw new InvalidOperationException($"Required startup asset is not collected by YooAsset collector: {assetPath}");
            }
        }

        private static void ValidateCollectorContainsRequiredPath(string requiredPath)
        {
            if (!IsAssetPathCollected(requiredPath))
            {
                throw new InvalidOperationException($"Required startup path is not collected by YooAsset collector: {requiredPath}");
            }
        }

        private static bool IsAssetPathCollected(string assetPath)
        {
            string normalizedAssetPath = NormalizeAssetPath(assetPath);
            var setting = AssetBundleCollectorSettingData.Setting;
            if (setting == null || setting.Packages == null)
            {
                return false;
            }

            foreach (var package in setting.Packages)
            {
                if (package == null || package.Groups == null)
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
                            return true;
                        }
                    }
                }
            }

            return false;
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

        private static string NormalizeDllName(string dllName)
        {
            return dllName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) ? dllName : $"{dllName}.dll";
        }

        private static string GetAssemblyBytesPath(string folder, string dllName)
        {
            return Path.Combine(folder, $"{NormalizeDllName(dllName)}.bytes");
        }

        private static string CreatePackageVersion()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
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
