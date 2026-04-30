using System.Collections.Generic;
using UnityEngine;

namespace Framework.Assemblies
{
    [CreateAssetMenu(fileName = AssetName, menuName = "Hotfix/Hotfix Assembly Manifest", order = 3)]
    public sealed class HotfixAssemblyManifest : ScriptableObject
    {
        public const string AssetName = "HotfixAssemblyManifest";

        public string AppVersionMin = string.Empty;
        public string AppVersionMax = string.Empty;
        public string BuildTarget = string.Empty;
        public string RequiredAotVersion = string.Empty;
        public string HotfixVersion = string.Empty;
        public List<string> HotUpdateAssemblies = new List<string>();
        public List<AssemblyFileRecord> HotUpdateFiles = new List<AssemblyFileRecord>();
        
        [Tooltip("Deprecated. Scene loading should be handled by CodeEntry.")]
        public string EntrySceneAddress = string.Empty;
        [Tooltip("Deprecated. Prefab loading should be handled by CodeEntry.")]
        public string EntryPrefabAddress = string.Empty;

        [Header("Code Entry")]
        public string EntryTypeName = "HotfixDemo.HotfixCodeEntry";
        public string EntryMethodName = "Entrance";
    }
}
