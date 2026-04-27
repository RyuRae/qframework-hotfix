using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public const string DefaultPackageName = "DefaultPackage";
        public const string DefaultEntrySceneAddress = "main";
        public const string AOTCodesPath = "Assets/AssetsPackage/AssetsHotFix/AOTCodes";
        public const string HotfixCodesPath = "Assets/AssetsPackage/AssetsHotFix/HotfixCodes";
        public const string ConfigsPath = "Assets/AssetsPackage/AssetsHotFix/Configs";
        public const string AssemblyManifestAssetPath = ConfigsPath + "/AssemblyManifest.asset";

        [MenuItem("Build/Build Hotfix YooAsset Package")]
        public static void BuildHotfixYooAssetPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);

            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);

            BuildYooAssetPackage(DefaultPackageName, target, true);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/BuildAssetsAndCopyToAssetsPackage")]
        public static void BuildAndCopyToAssetsPackage()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            CompileDllCommand.CompileDll(target);

            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
            AssetDatabase.Refresh();
        }

        [MenuItem("Build/BuildAssetsAndCopyToStreamingAssets")]
        public static void BuildAndCopyABAOTHotUpdateDlls()
        {
            BuildHotfixYooAssetPackage();
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
            CreateOrUpdateAssemblyManifest(GetManifestOrConfiguredAOTAssemblies(), hotfixAssemblies);
        }

        public static void CopyAotMetaDataDlls(BuildTarget target)
        {
            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            CreateOrUpdateAssemblyManifest(aotAssemblies, GetAllHotfixAssemblies());
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
            CreateOrUpdateAssemblyManifest(
                FindAllAOTMetaAssemblies(EditorUserBuildSettings.activeBuildTarget),
                GetAllHotfixAssemblies());
        }

        public static BuildResult BuildYooAssetPackage(string packageName, BuildTarget target, bool copyToStreamingAssets)
        {
            var buildParameters = new BuiltinBuildParameters
            {
                BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot(),
                BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot(),
                BuildPipeline = EBuildPipeline.BuiltinBuildPipeline.ToString(),
                BuildBundleType = (int)EBuildBundleType.AssetBundle,
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

        public static void CreateOrUpdateAssemblyManifest(List<string> aotAssemblies, List<string> hotfixAssemblies)
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
        }

        [Obsolete("Use BuildHotfixYooAssetPackage instead.")]
        public static void CopyABAOTHotUpdateDlls(BuildTarget target)
        {
            CompileDllCommand.CompileDll(target);
            var aotAssemblies = CopyAOTAssembliesToTargetPath(target);
            var hotfixAssemblies = CopyHotUpdateAssembliesToTargetPath(target);
            CreateOrUpdateAssemblyManifest(aotAssemblies, hotfixAssemblies);
            BuildYooAssetPackage(DefaultPackageName, target, true);
        }

        [Obsolete("Use BuildYooAssetPackage instead.")]
        public static void BuildSceneAssetBundleActiveBuildTargetExcludeAOT()
        {
            BuildYooAssetPackage(DefaultPackageName, EditorUserBuildSettings.activeBuildTarget, false);
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
            var manifest = AssetDatabase.LoadAssetAtPath<AssemblyManifest>(AssemblyManifestAssetPath);
            if (manifest != null && manifest.AotMetadataAssemblies != null && manifest.AotMetadataAssemblies.Count > 0)
            {
                return NormalizeDllNames(manifest.AotMetadataAssemblies);
            }

            return GetConfiguredAOTMetaAssemblies();
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

        private static string CreatePackageVersion()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
        }

        private static void PrepareDirectory(string path)
        {
            Directory.CreateDirectory(path);
            FolderUtils.ClearFolder(path);
        }
    }
}
