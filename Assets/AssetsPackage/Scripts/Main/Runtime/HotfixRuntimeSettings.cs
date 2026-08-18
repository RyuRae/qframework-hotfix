using System;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Framework
{
    /// <summary>
    /// 包体内可信的 Manifest RSA 公钥，只保存公钥材料，不允许保存发布私钥。
    /// </summary>
    [Serializable]
    public sealed class HotfixManifestPublicKey
    {
        [Tooltip("发布签名密钥标识。Manifest 通过该值选择包体内可信公钥。")]
        public string KeyId = string.Empty;

        [Tooltip("RSA 公钥 Modulus，Base64 编码。只包含公钥，不要在 Assets 中保存私钥。")]
        [TextArea(2, 6)]
        public string Modulus = string.Empty;

        [Tooltip("RSA 公钥 Exponent，Base64 编码。常见值为 AQAB。")]
        public string Exponent = "AQAB";
    }

    /// <summary>
    /// 启动阶段的资源下载范围。
    /// </summary>
    public enum StartupDownloadMode
    {
        /// <summary>
        /// 差异全量下载
        /// </summary>
        DownloadAll,
        /// <summary>
        /// 按Tag下载资源
        /// </summary>
        DownloadByTags,
        /// <summary>
        /// 跳过下载
        /// </summary>
        Skip
    }

    /// <summary>
    /// 远端更新失败时的启动容灾策略。
    /// </summary>
    public enum StartupUpdatePolicy
    {
        /// <summary>
        /// 必须完成远端更新，失败时阻断启动。
        /// </summary>
        MustUpdate,
        /// <summary>
        /// 远端更新失败时允许使用上次可用缓存或首包内置版本启动。
        /// </summary>
        AllowCached,
        /// <summary>
        /// 非 Wi-Fi 环境优先使用本地可用版本。
        /// </summary>
        WifiOnly,
        /// <summary>
        /// 优先使用本地可用版本启动，预留进入游戏后的后台下载策略。
        /// </summary>
        BackgroundDownload
    }

    /// <summary>
    /// Player 首次启动时资源位于包内还是远端的发布策略。
    /// </summary>
    public enum StartupPackageMode
    {
        /// <summary>
        /// 首包必须包含启动 UI、YooAsset manifest、AOT metadata、热更 DLL 和入口资源。
        /// </summary>
        FirstPackage,

        /// <summary>
        /// 离线包必须完整内置启动所需资源，不依赖远端版本。
        /// </summary>
        OfflinePackage,

        /// <summary>
        /// 真正空包不内置 YooAsset 启动资源，启动后先拉远端版本和 manifest，再下载所需资源。
        /// </summary>
        EmptyPackage
    }

    /// <summary>
    /// Player 运行时热更新配置，由 ReleaseProfile 在构建阶段同步，启动时通过 Resources.Load 读取。
    /// </summary>
    [CreateAssetMenu(fileName = "HotfixRuntimeSettings", menuName = "Hotfix/Runtime Settings", order = 0)]
    public sealed class HotfixRuntimeSettings : ScriptableObject
    {
        public const string AssetName = "HotfixRuntimeSettings";
        public const string ResourcesPath = AssetName;
        public const string DefaultMainPackageName = "DefaultPackage";
        public const string DefaultRawFilePackageName = "RawFilePackage";
        public const string DefaultStartupTag = "startup";

        [Header("编辑器默认运行模式")]
        [SerializeField]
        private EPlayMode editorPlayMode = EPlayMode.EditorSimulateMode;

        [Header("发布后默认运行模式")]
        [SerializeField]
        private EPlayMode playerPlayMode = EPlayMode.HostPlayMode;

        [Header("主包名称")]
        [SerializeField]
        private string mainPackageName = DefaultMainPackageName;

        [Header("是否包含原生文件包")]
        [SerializeField]
        private bool includeRawFilePackage;

        [Header("原生文件包名称")]
        [SerializeField]
        private string rawfilePackageName = DefaultRawFilePackageName;

        [Header("启动阶段下载模式")]
        [SerializeField]
        private StartupDownloadMode startupDownloadMode = StartupDownloadMode.DownloadAll;

        [Header("启动阶段更新策略")]
        [SerializeField]
        private StartupUpdatePolicy startupUpdatePolicy = StartupUpdatePolicy.AllowCached;

        [Header("启动包策略")]
        [SerializeField]
        private StartupPackageMode startupPackageMode = StartupPackageMode.FirstPackage;

        [Header("启动阶段按Tag下载资源")]
        [SerializeField]
        private string[] startupDownloadTags = new string[0];

        [Header("启动阶段按Tag下载RawFile资源")]
        [SerializeField]
        private string[] rawfileStartupDownloadTags = new string[0];

        [Header("热更清单信任根")]
        [SerializeField]
        [Tooltip("开启后，AOT/Hotfix Manifest 必须通过包体内可信 RSA 公钥验签，才允许加载任何 DLL。正式环境必须开启。")]
        private bool requireSignedAssemblyManifests;

        [SerializeField]
        [Tooltip("允许验证热更 Manifest 的 RSA 公钥。可同时保留新旧公钥完成轮换。私钥只能由外部构建环境提供。")]
        private HotfixManifestPublicKey[] trustedManifestPublicKeys = Array.Empty<HotfixManifestPublicKey>();

        public EPlayMode PlayMode
        {
            get
            {
#if UNITY_EDITOR
                return editorPlayMode;
#else
                return playerPlayMode;
#endif
            }
        }

        public EPlayMode PlayerPlayMode => playerPlayMode;
        public string MainPackageName => HotfixUtility.NormalizePackageName(mainPackageName, DefaultMainPackageName);
        public bool IncludeRawFilePackage => includeRawFilePackage && !string.IsNullOrEmpty(RawFilePackageName);
        public string RawFilePackageName => HotfixUtility.NormalizePackageName(rawfilePackageName, DefaultRawFilePackageName);
        public StartupDownloadMode StartupDownloadMode => startupDownloadMode;
        public StartupUpdatePolicy StartupUpdatePolicy => startupUpdatePolicy;
        public StartupPackageMode StartupPackageMode => startupPackageMode;
        public string[] StartupDownloadTags => HotfixUtility.NormalizeTags(startupDownloadTags);
        public string[] RawFileStartupDownloadTags => HotfixUtility.NormalizeTags(rawfileStartupDownloadTags);
        public bool RequireSignedAssemblyManifests => requireSignedAssemblyManifests;
        public HotfixManifestPublicKey[] TrustedManifestPublicKeys => trustedManifestPublicKeys ?? Array.Empty<HotfixManifestPublicKey>();

        /// <summary>
        /// 按签名 KeyId 查找包体内可信公钥，用于运行时验证 AOT/Hotfix Manifest。
        /// </summary>
        public bool TryGetTrustedManifestPublicKey(string keyId, out HotfixManifestPublicKey publicKey)
        {
            publicKey = null;
            if (string.IsNullOrWhiteSpace(keyId))
            {
                return false;
            }

            foreach (var candidate in TrustedManifestPublicKeys)
            {
                if (candidate != null &&
                    string.Equals(candidate.KeyId, keyId.Trim(), StringComparison.Ordinal))
                {
                    publicKey = candidate;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 从 Resources 载入当前 Player 使用的热更新运行时配置。
        /// </summary>
        public static HotfixRuntimeSettings Load()
        {
            return Resources.Load<HotfixRuntimeSettings>(ResourcesPath);
        }

#if UNITY_EDITOR
        public void SetPlayerPlayModeForEditor(EPlayMode value)
        {
            playerPlayMode = value;
        }

        public void SetPackageNamesForEditor(
            string mainPackageName,
            bool includeRawFilePackage,
            string rawfilePackageName)
        {
            this.mainPackageName = HotfixUtility.NormalizePackageName(mainPackageName, DefaultMainPackageName);
            this.includeRawFilePackage = includeRawFilePackage;
            this.rawfilePackageName = HotfixUtility.NormalizePackageName(rawfilePackageName, DefaultRawFilePackageName);
        }

        public void SetStartupSettingsForEditor(
            StartupPackageMode packageMode,
            StartupDownloadMode downloadMode,
            StartupUpdatePolicy updatePolicy,
            string[] downloadTags,
            string[] rawFileDownloadTags)
        {
            startupPackageMode = packageMode;
            startupDownloadMode = downloadMode;
            startupUpdatePolicy = updatePolicy;
            startupDownloadTags = HotfixUtility.NormalizeTags(downloadTags);
            rawfileStartupDownloadTags = HotfixUtility.NormalizeTags(rawFileDownloadTags);
        }

        public void SetManifestTrustForEditor(
            bool requireSignedManifests,
            string keyId,
            string modulus,
            string exponent)
        {
            requireSignedAssemblyManifests = requireSignedManifests;
            if (string.IsNullOrWhiteSpace(keyId))
            {
                return;
            }

            string normalizedKeyId = keyId.Trim();
            var keys = new List<HotfixManifestPublicKey>(TrustedManifestPublicKeys);
            var key = keys.Find(candidate => candidate != null &&
                                             string.Equals(candidate.KeyId, normalizedKeyId, StringComparison.Ordinal));
            if (key == null)
            {
                key = new HotfixManifestPublicKey();
                keys.Add(key);
            }

            key.KeyId = normalizedKeyId;
            key.Modulus = modulus == null ? string.Empty : modulus.Trim();
            key.Exponent = string.IsNullOrWhiteSpace(exponent) ? "AQAB" : exponent.Trim();
            trustedManifestPublicKeys = keys.ToArray();
        }
#endif
    }
}
