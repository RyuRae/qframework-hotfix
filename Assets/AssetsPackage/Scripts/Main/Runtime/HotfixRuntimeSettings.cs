using System;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace Framework
{
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

    [CreateAssetMenu(fileName = "HotfixRuntimeSettings", menuName = "Hotfix/Runtime Settings", order = 0)]
    public sealed class HotfixRuntimeSettings : ScriptableObject
    {
        public const string AssetName = "HotfixRuntimeSettings";
        public const string ResourcesPath = AssetName;
        public const string DefaultMainPackageName = "DefaultPackage";
        public const string DefaultRawFilePackageName = "RawFilePackage";

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
#endif
    }
}
