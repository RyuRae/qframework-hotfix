using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Luban.Editor
{
    public enum LubanTaskCategory { GameConfig, Localization, Client, Server, Development }

    [Serializable]
    public sealed class LubanTableSelection
    {
        public bool Enabled = true;
        public string Name = string.Empty;
        public string InputPath = string.Empty;
        [Tooltip("Luban --outputTable 参数。留空表示由当前任务生成全部表。")]
        public string OutputTable = string.Empty;
    }

    [Serializable]
    public sealed class LubanBuildTask
    {
        public bool Enabled = true;
        public string Name = "Client";
        public LubanTaskCategory Category = LubanTaskCategory.GameConfig;
        public string ConfigPath = "LubanConfig/DataTables/luban.conf";
        public string DataSourceRoot = "LubanConfig/DataTables/Datas";
        public string Target = "client";
        public string CodeTarget = "cs-bin";
        public string DataTarget = "bin";
        public string CodeOutputDirectory = "Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GenCodes/Bin";
        public string DataOutputDirectory = "Assets/AssetsPackage/AssetsHotFix/Datas/bin";
        public bool GenerateCode = true;
        public bool GenerateData = true;
        [Tooltip("切换代码 Target 时，在生成前删除当前代码输出目录。避免 cs-bin 与 JSON C# 类型同时存在。")]
        public bool CleanCodeOutputBeforeGenerate;
        public bool ValidationFailAsError = true;
        public List<LubanTableSelection> Tables = new List<LubanTableSelection>();
    }

    [CreateAssetMenu(fileName = "LubanBuildProfile", menuName = "Luban/Build Profile")]
    public sealed class LubanBuildProfile : ScriptableObject
    {
        public const string DefaultAssetPath = "Assets/Editor/Luban/LubanBuildProfile.asset";
        public string LubanDllPath = "LubanConfig/DataTables/Luban/Luban.dll";
        public bool ValidateBeforeGenerate = true;
        public bool CleanupStaleOutputs;
        public bool RefreshAssetDatabase = true;
        public bool SyncLocalizationCollectors = true;
        public List<LubanBuildTask> Tasks = new List<LubanBuildTask>();
    }
}
