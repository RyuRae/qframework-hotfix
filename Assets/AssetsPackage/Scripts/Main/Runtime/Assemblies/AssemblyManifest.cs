using System.Collections.Generic;
using UnityEngine;

namespace Framework.Assemblies
{
    [CreateAssetMenu(fileName = "AssemblyManifest", menuName = "Hotfix/Assembly Manifest", order = 1)]
    public class AssemblyManifest : ScriptableObject
    {
        public const string AssetName = "AssemblyManifest";

        public List<string> AotMetadataAssemblies = new List<string>();
        public List<string> HotUpdateAssemblies = new List<string>();

        public string EntrySceneAddress = "main";
        public string EntryPrefabAddress = string.Empty;
        public string EntryTypeName = string.Empty;
        public string EntryMethodName = string.Empty;
    }
}
