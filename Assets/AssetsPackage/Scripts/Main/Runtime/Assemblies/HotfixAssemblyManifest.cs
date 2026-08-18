using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Assemblies
{
    /// <summary>
    /// 热更程序集之间的直接依赖记录，构建期生成并用于诊断最终加载顺序。
    /// </summary>
    [Serializable]
    public sealed class AssemblyDependencyRecord
    {
        public string AssemblyName = string.Empty;
        public string DllName = string.Empty;
        public List<string> DependsOn = new List<string>();
    }

    /// <summary>
    /// 热更代码发布清单，绑定 App 兼容范围、AOT 版本、RawFile 包、入口类型和签名信息。
    /// </summary>
    [CreateAssetMenu(fileName = AssetName, menuName = "Hotfix/Hotfix Assembly Manifest", order = 3)]
    public sealed class HotfixAssemblyManifest : ScriptableObject
    {
        public const string AssetName = "HotfixAssemblyManifest";

        [Header("Release Identity")]
        [Tooltip("与本次 YooAsset PackageVersion 一致，参与签名，用于识别清单所属发布。")]
        public string ReleaseVersion = string.Empty;
        [Tooltip("单调递增的正式发布序号，用于阻止已签名旧版本回滚。开发环境可为 0。")]
        public long ReleaseSequence;

        public string AppVersionMin = string.Empty;
        public string AppVersionMax = string.Empty;
        public string BuildTarget = string.Empty;
        public string RequiredAotVersion = string.Empty;
        public string HotfixVersion = string.Empty;

        [Header("RawFile Package Trust")]
        [Tooltip("本次发布绑定的 RawFile 包名；未启用 RawFile 时为空。")]
        public string RawFilePackageName = string.Empty;
        [Tooltip("必须与主包 ReleaseVersion 一致；未启用 RawFile 时为空。")]
        public string RawFilePackageVersion = string.Empty;
        [Tooltip("RawFile YooAsset Manifest 的确定性 SHA-256；正式发布参与签名并在运行时校验。")]
        public string RawFileManifestSha256 = string.Empty;

        public List<string> HotUpdateAssemblies = new List<string>();
        public List<AssemblyFileRecord> HotUpdateFiles = new List<AssemblyFileRecord>();
        public List<AssemblyDependencyRecord> HotUpdateDependencies = new List<AssemblyDependencyRecord>();
        
        [Tooltip("Deprecated. Scene loading should be handled by CodeEntry.")]
        public string EntrySceneAddress = string.Empty;
        [Tooltip("Deprecated. Prefab loading should be handled by CodeEntry.")]
        public string EntryPrefabAddress = string.Empty;

        [Header("IHotfixEntry")]
        [Tooltip("实现 Framework.IHotfixEntry 且带公共无参构造函数的热更入口类型。")]
        public string EntryTypeName = "HotfixDemo.HotfixCodeEntry";

        [HideInInspector]
        public string EntryMethodName = string.Empty;

        [Header("Signature")]
        public int SignatureVersion;
        public string SignatureAlgorithm = string.Empty;
        public string SigningKeyId = string.Empty;
        [TextArea(2, 6)]
        public string Signature = string.Empty;
    }
}
