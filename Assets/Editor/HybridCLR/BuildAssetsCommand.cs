using HybridCLR.Editor.Commands;
using Main.Editor;
using MsbFramework.Utils;
using QFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using YooAsset;

namespace HybridCLR.Editor
{
    public static class BuildAssetsCommand
    {
        public static string HybridCLRBuildCacheDir => Application.dataPath + "/HybridCLRBuildCache";

        public static string AssetBundleOutputDir => $"{HybridCLRBuildCacheDir}/AssetBundleOutput";

        public static string AssetBundleSourceDataTempDir => $"{HybridCLRBuildCacheDir}/AssetBundleSourceData";


        public static string GetAssetBundleOutputDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleOutputDir}/{target}";
        }

        public static string GetAssetBundleTempDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleSourceDataTempDir}/{target}";
        }

        public static string ToRelativeAssetPath(string s)
        {
            return s.Substring(s.IndexOf("Assets/"));
        }

        /// <summary>
        /// 将HotFix.dll和HotUpdatePrefab.prefab打入common包.
        /// 将HotUpdateScene.unity打入scene包.
        /// </summary>
        /// <param name="tempDir"></param>
        /// <param name="outputDir"></param>
        /// <param name="target"></param>
        private static void BuildAssetBundles(string tempDir, string outputDir, BuildTarget target)
        {
            Directory.CreateDirectory(tempDir);
            Directory.CreateDirectory(outputDir);
            
            List<AssetBundleBuild> abs = new List<AssetBundleBuild>();

            {
                var prefabAssets = new List<string>();
                string testPrefab = $"{Application.dataPath}/Prefabs/Cube.prefab";
                prefabAssets.Add(testPrefab);
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                abs.Add(new AssetBundleBuild
                {
                    assetBundleName = "prefabs",
                    assetNames = prefabAssets.Select(s => ToRelativeAssetPath(s)).ToArray(),
                });
            }

            BuildPipeline.BuildAssetBundles(outputDir, abs.ToArray(), BuildAssetBundleOptions.None, target);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        public static void BuildAssetBundleByTarget(BuildTarget target)
        {
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("Build/BuildAssetsAndCopyToAssetsPackage")]
        public static void BuildAndCopyAOTHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            //BuildAssetBundleByTarget(target);
            CompileDllCommand.CompileDll(target);
            CopyAOTHotUpdateDlls(target);
            AssetDatabase.Refresh();
        }



        [MenuItem("Build/BuildAssetsAndCopyToStreamingAssets")]
        public static void BuildAndCopyABAOTHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            BuildAssetBundleByTarget(target);
            CompileDllCommand.CompileDll(target);
            CopyABAOTHotUpdateDlls(target);
            AssetDatabase.Refresh();
        }

        public static void CopyAOTHotUpdateDlls(BuildTarget target)
        {
            //CopyAssetBundlesToStreamingAssets(target);
            CopyAOTAssembliesToTargetPath();
            CopyHotUpdateAssembliesToTargetPath();
            CopyAssembiesSettingToTargetPath();
        }


        public static void CopyABAOTHotUpdateDlls(BuildTarget target)
        {
            CopyAssetBundlesToStreamingAssets(target);
            CopyAOTAssembliesToStreamingAssets();
            CopyHotUpdateAssembliesToStreamingAssets();
        }

        //[MenuItem("HybridCLR/Build/BuildAssetbundle")]
        public static void BuildSceneAssetBundleActiveBuildTargetExcludeAOT()
        {
            BuildAssetBundleByTarget(EditorUserBuildSettings.activeBuildTarget);
        }

        /// <summary>
        /// 复制aot元数据文件到目标目录
        /// </summary>
        public static void CopyAOTAssembliesToTargetPath()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            string aotAssembliesSrcDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            string aotAssembliesDstDir = YooAssetHybridCLRSetting.Instance.AOTCodesPath;
            // 清空AOT文件夹
            FolderUtils.ClearFolder(aotAssembliesDstDir);
            FindAllAOTMetaAssemblies(target);
            foreach (var dll in YooAssetHybridCLRSetting.Instance.AOTMetaAssemblies)
            {
                string srcDllPath = $"{aotAssembliesSrcDir}/{dll}";
                if (!File.Exists(srcDllPath))
                {
                    Debug.LogError($"ab中添加AOT补充元数据dll:{srcDllPath} 时发生错误,文件不存在。裁剪后的AOT dll在BuildPlayer时才能生成，因此需要你先构建一次游戏App后再打包。");
                    continue;
                }
                string dllBytesPath = $"{aotAssembliesDstDir}/{dll}.bytes";
                File.Copy(srcDllPath, dllBytesPath, true);
                Debug.Log($"[CopyAOTAssembliesToStreamingAssets] copy AOT dll {srcDllPath} -> {dllBytesPath}");
            }
        }

        /// <summary>
        /// 拷贝热更新文件到目标目录
        /// </summary>
        public static void CopyHotUpdateAssembliesToTargetPath()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;

            string hotfixDllSrcDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            string hotfixAssembliesDstDir = YooAssetHybridCLRSetting.Instance.HotfixCodesPath;
            // 清空AOT文件夹
            FolderUtils.ClearFolder(hotfixAssembliesDstDir);
            GetAllHotfixAssemblies();
            foreach (var dll in YooAssetHybridCLRSetting.Instance.HotfixAssemblies)
            {
                string dllPath = $"{hotfixDllSrcDir}/{dll}";
                string dllBytesPath = $"{hotfixAssembliesDstDir}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                Debug.Log($"[CopyHotUpdateAssembliesToStreamingAssets] copy hotfix dll {dllPath} -> {dllBytesPath}");
            }
        }

        /// <summary>
        /// 复制设置文件到目标
        /// </summary>
        public static void CopyAssembiesSettingToTargetPath()
        {
            string settingAssetSourcePath = YooAssetHybridCLRSetting.Instance.YooAssetHybridCLRSettingSourcePath;
            string settingAssetDesPath = YooAssetHybridCLRSetting.Instance.YooAssetHybridCLRSettingDesPath;
            if (File.Exists(settingAssetDesPath))
                File.Delete(settingAssetDesPath);
            File.Copy(settingAssetSourcePath, settingAssetDesPath, true);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void CopyAssetBundlesToStreamingAssets(BuildTarget target)
        {
            string streamingAssetPathDst = Application.streamingAssetsPath;
            Directory.CreateDirectory(streamingAssetPathDst);
            string outputDir = GetAssetBundleOutputDirByTarget(target);
            var abs = new string[] { "prefabs" };
            foreach (var ab in abs)
            {
                string srcAb = ToRelativeAssetPath($"{outputDir}/{ab}");
                string dstAb = ToRelativeAssetPath($"{streamingAssetPathDst}/{ab}");
                Debug.Log($"[CopyAssetBundlesToStreamingAssets] copy assetbundle {srcAb} -> {dstAb}");
                AssetDatabase.CopyAsset( srcAb, dstAb);
            }
        }

        public static void CopyAOTAssembliesToStreamingAssets()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            string aotAssembliesSrcDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            string aotAssembliesDstDir = Application.streamingAssetsPath;

            foreach (var dll in SettingsUtil.AOTAssemblyNames)
            {
                string srcDllPath = $"{aotAssembliesSrcDir}/{dll}.dll";
                if (!File.Exists(srcDllPath))
                {
                    Debug.LogError($"ab中添加AOT补充元数据dll:{srcDllPath} 时发生错误,文件不存在。裁剪后的AOT dll在BuildPlayer时才能生成，因此需要你先构建一次游戏App后再打包。");
                    continue;
                }
                string dllBytesPath = $"{aotAssembliesDstDir}/{dll}.dll.bytes";
                File.Copy(srcDllPath, dllBytesPath, true);
                Debug.Log($"[CopyAOTAssembliesToStreamingAssets] copy AOT dll {srcDllPath} -> {dllBytesPath}");
            }
        }

        public static void CopyHotUpdateAssembliesToStreamingAssets()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;

            string hotfixDllSrcDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            string hotfixAssembliesDstDir = Application.streamingAssetsPath;
            foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesExcludePreserved)
            {
                string dllPath = $"{hotfixDllSrcDir}/{dll}";
                string dllBytesPath = $"{hotfixAssembliesDstDir}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                Debug.Log($"[CopyHotUpdateAssembliesToStreamingAssets] copy hotfix dll {dllPath} -> {dllBytesPath}");
            }
        }

        public static void FindAllAOTMetaAssemblies(BuildTarget buildTarget)
        {
            string folder = $"{SettingsUtil.GetAssembliesPostIl2CppStripDir(buildTarget)}";
            //SettingsUtil.AOTAssemblyNames.Clear();
            YooAssetHybridCLRSetting.Instance.AOTMetaAssemblies.Clear();
            if (!Directory.Exists(folder))
            {
#if UNITY_EDITOR_WIN
                LogKit.E($"AOTMetaAssemblies文件夹不存在，因此需要你先在菜单栏中(HybridCLR>>Generate>>All)操作。FolderPath:{folder}");
#elif UNITY_EDITOR_OSX
            Logger.Error($"AOTMetaAssemblies文件夹不存在，请检查是否制作UnityEditor.CoreModule.dll,并修改覆盖Unity安装路径，然后需要你先在菜单栏中(HybridCLR>>Generate>>All)操作。FolderPath:{folder}");
#endif
                return;
            }
            DirectoryInfo root = new DirectoryInfo(folder);
            foreach (var fileInfo in root.GetFiles("*dll", SearchOption.AllDirectories))
            {
                string fileName = fileInfo.Name;
                YooAssetHybridCLRSetting.Instance.AOTMetaAssemblies.Add(fileName);
            }
            EditorUtility.SetDirty(YooAssetHybridCLRSetting.Instance);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void GetAllHotfixAssemblies()
        {
            YooAssetHybridCLRSetting.Instance.HotfixAssemblies.Clear();
            foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesExcludePreserved)
            {
                if (!YooAssetHybridCLRSetting.Instance.HotfixAssemblies.Contains(dll))
                    YooAssetHybridCLRSetting.Instance.HotfixAssemblies.Add(dll);
            }
            EditorUtility.SetDirty(YooAssetHybridCLRSetting.Instance);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
