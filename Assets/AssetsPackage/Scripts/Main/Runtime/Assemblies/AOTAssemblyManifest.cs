using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Assemblies
{
    [Serializable]
    public sealed class AssemblyFileRecord
    {
        public string FileName = string.Empty;
        public string AssemblyName = string.Empty;
        public string Sha256 = string.Empty;
        public long Size;
    }

    [CreateAssetMenu(fileName = AssetName, menuName = "Hotfix/AOT Assembly Manifest", order = 2)]
    public sealed class AOTAssemblyManifest : ScriptableObject
    {
        public const string AssetName = "AOTAssemblyManifest";

        public string AppVersion = string.Empty;
        public string BuildTarget = string.Empty;
        public string AotVersion = string.Empty;
        public string BaselineFingerprint = string.Empty;
        public string BaselineGeneratedAtUtc = string.Empty;
        public string BaselineGitCommit = string.Empty;
        public List<string> AotMetadataAssemblies = new List<string>();
        public List<AssemblyFileRecord> AotMetadataFiles = new List<AssemblyFileRecord>();
    }
}
