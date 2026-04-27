using HybridCLR.Editor;
using Framework.Assemblies;
using Framework.Utils;
using QFramework;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;


public static class AOTMetaAssembliesHelper
{
    //[MenuItem("HybridCLR/CreateAOTMetaAssemblies/ActiveBuildTarget", priority = 100)]
    public static void AOTMetaAssembliesActiveBuildTarget()
    {
        DoCopyAOTAssemblies(EditorUserBuildSettings.activeBuildTarget);
        LogKit.I("构建热更新程序集完成！");
    }

    private static List<AssemblyFileData> m_ListAssemblyFileData = new List<AssemblyFileData>();

    public static void DoCopyAOTAssemblies(BuildTarget buildTarget)
    {
        FindAllAOTMetaAssemblies(buildTarget);
        // 清空AOT文件夹
        FolderUtils.ClearFolder(PathUtils.AOTAssemblyTextAssetPath);

        //判断AOT文件夹是否存在
        if (!Directory.Exists(PathUtils.AOTAssemblyTextAssetPath))
        {
            Directory.CreateDirectory(PathUtils.AOTAssemblyTextAssetPath);
        }

        string mergedFileName = PathUtils.GetMergedFileName();
        string mergedFilePath = $"{PathUtils.AOTAssemblyTextAssetPath}/{mergedFileName}{PathUtils.AssemblyAssetExtension}";
        using StreamWriter mergedFileWriter = new StreamWriter(mergedFilePath);
        long currentPosition = 0;
        m_ListAssemblyFileData.Clear();
        foreach (var dll in AOTMetaAssemblies)
        {
            string dllPath = $"{SettingsUtil.GetAssembliesPostIl2CppStripDir(buildTarget)}/{dll}";
            if (!File.Exists(dllPath))
            {
                LogKit.E($"ab中添加AOT补充元数据dll:{dllPath} 时发生错误,文件不存在。裁剪后的AOT dll在BuildPlayer时才能生成，因此需要你先构建一次游戏App后再打包。");
                continue;
            }
            using StreamReader fileReader = new StreamReader(dllPath);
            long startPosition = currentPosition;
            fileReader.BaseStream.CopyTo(mergedFileWriter.BaseStream);
            long endPosition = currentPosition + fileReader.BaseStream.Length - 1;
            currentPosition = endPosition + 1;
            AssemblyFileData fileData = new AssemblyFileData(dll, startPosition, endPosition);
            m_ListAssemblyFileData.Add(fileData);
        }
        mergedFileWriter.Close();
        mergedFileWriter.Dispose();
        // 刷新资源
        AssetDatabase.Refresh();
        CreateAssembliesRecord();
    }

    public static List<string> AOTMetaAssemblies = new List<string>();
    public static void FindAllAOTMetaAssemblies(BuildTarget buildTarget)
    {
        string folder = $"{SettingsUtil.GetAssembliesPostIl2CppStripDir(buildTarget)}";
        AOTMetaAssemblies.Clear();
        if (!Directory.Exists(folder))
        {
#if UNITY_EDITOR_WIN
            LogKit.E($"AOTMetaAssemblies文件夹不存在，因此需要你先在菜单栏中(HybridCLR>>Generate>>All)操作。FolderPath:{folder}");
#elif UNITY_EDITOR_OSX
            //Logger.LogError("Error", $"AOTMetaAssemblies文件夹不存在，请检查是否制作UnityEditor.CoreModule.dll,并修改覆盖Unity安装路径，然后需要你先在菜单栏中(HybridCLR>>Generate>>All)操作。FolderPath:{folder}");
            Debug.LogError($"AOTMetaAssemblies文件夹不存在，请检查是否制作UnityEditor.CoreModule.dll,并修改覆盖Unity安装路径，然后需要你先在菜单栏中(HybridCLR>>Generate>>All)操作。FolderPath:{folder}");
#endif
            return;
        }

        foreach (var dll in AOTGenericReferences.PatchedAOTAssemblyList)
        {
            string fileName = dll.EndsWith(".dll") ? dll : $"{dll}.dll";
            string dllPath = Path.Combine(folder, fileName);
            if (!File.Exists(dllPath))
            {
                LogKit.E($"AOT补充元数据dll不存在: {dllPath}");
                continue;
            }

            if (AOTMetaAssemblies.Contains(fileName))
            {
                continue;
            }

            AOTMetaAssemblies.Add(fileName);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    //生成AOT元数据记录
    private static void CreateAssembliesRecord()
    {
        string assemblyFileData = JsonConvert.SerializeObject(m_ListAssemblyFileData);
        string path = $"{PathUtils.AOTAssemblyTextAssetPath}/{PathUtils.AssembliesVersionTextFileName}";
        File.WriteAllText(path, assemblyFileData);
        var m_UpdateAs = JsonConvert.DeserializeObject<List<AssemblyFileData>>(assemblyFileData);
    }
}
