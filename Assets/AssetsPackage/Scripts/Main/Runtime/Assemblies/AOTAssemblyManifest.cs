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

        [Header("Release Identity")]
        [Tooltip("与本次 YooAsset PackageVersion 一致，参与签名，用于识别清单所属发布。")]
        public string ReleaseVersion = string.Empty;
        [Tooltip("单调递增的正式发布序号，用于阻止已签名旧版本回滚。开发环境可为 0。")]
        public long ReleaseSequence;

        public string AppVersion = string.Empty;
        public string BuildTarget = string.Empty;
        public string AotVersion = string.Empty;
        public string BaselineFingerprint = string.Empty;
        public string BaselineGeneratedAtUtc = string.Empty;
        public string BaselineGitCommit = string.Empty;
        public List<string> AotMetadataAssemblies = new List<string>();
        public List<AssemblyFileRecord> AotMetadataFiles = new List<AssemblyFileRecord>();

        [Header("Signature")]
        public int SignatureVersion;
        public string SignatureAlgorithm = string.Empty;
        public string SigningKeyId = string.Empty;
        [TextArea(2, 6)]
        public string Signature = string.Empty;
    }
}
