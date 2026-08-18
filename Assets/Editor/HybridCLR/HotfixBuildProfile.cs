using System;
using System.IO;
using Framework;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using YooAsset;

namespace HybridCLR.Editor
{
    /// <summary>按 Unity 平台保存 Player 使用的 YooAsset 运行模式。</summary>
    public sealed class HotfixBuildProfile : ScriptableObject
    {
        public const string AssetPath = "Assets/Editor/HybridCLR/HotfixBuildProfile.asset";

        [SerializeField]
        private EPlayMode defaultPlayerPlayMode = EPlayMode.HostPlayMode;

        [SerializeField]
        private EPlayMode standalonePlayMode = EPlayMode.HostPlayMode;

        [SerializeField]
        private EPlayMode androidPlayMode = EPlayMode.HostPlayMode;

        [SerializeField]
        private EPlayMode iosPlayMode = EPlayMode.HostPlayMode;

        [SerializeField]
        private EPlayMode webGLPlayMode = EPlayMode.WebPlayMode;

        /// <summary>获取指定平台发布时应使用的 YooAsset PlayMode。</summary>
        public EPlayMode GetPlayMode(BuildTarget target)
        {
            if (target == BuildTarget.WebGL)
            {
                return webGLPlayMode;
            }

            if (target == BuildTarget.Android)
            {
                return androidPlayMode;
            }

            if (target == BuildTarget.iOS)
            {
                return iosPlayMode;
            }

            if (IsStandaloneTarget(target))
            {
                return standalonePlayMode;
            }

            return defaultPlayerPlayMode;
        }

#if UNITY_EDITOR
        /// <summary>设置指定平台的 Player PlayMode；调用方负责将 Profile 标记为已修改。</summary>
        public void SetPlayModeForEditor(BuildTarget target, EPlayMode playMode)
        {
            if (target == BuildTarget.WebGL)
            {
                webGLPlayMode = playMode;
                return;
            }

            if (target == BuildTarget.Android)
            {
                androidPlayMode = playMode;
                return;
            }

            if (target == BuildTarget.iOS)
            {
                iosPlayMode = playMode;
                return;
            }

            if (IsStandaloneTarget(target))
            {
                standalonePlayMode = playMode;
                return;
            }

            defaultPlayerPlayMode = playMode;
        }
#endif

        private static bool IsStandaloneTarget(BuildTarget target)
        {
            return target == BuildTarget.StandaloneWindows ||
                   target == BuildTarget.StandaloneWindows64 ||
                   target == BuildTarget.StandaloneOSX ||
                   target == BuildTarget.StandaloneLinux64;
        }
    }

    /// <summary>同步 BuildProfile、运行时设置和 YooAsset Collector 包名，并执行 Player 构建前校验。</summary>
    public static class HotfixBuildProfileUtility
    {
        public const string RuntimeSettingsAssetPath = "Assets/AssetsPackage/Resources/HotfixRuntimeSettings.asset";
        public const string RemoteSettingsAssetPath = "Assets/AssetsPackage/Resources/HotfixRemoteSettings.asset";

        public static bool SkipRemoteSettingsValidationForCurrentBuild { get; set; }

        /// <summary>将活动构建平台的 PlayMode 应用到 Player 运行时配置。</summary>
        [MenuItem("Build/热更新/内部工具/应用构建 PlayMode 到运行时设置", false, HotfixBuildMenuPriority.InternalApplyPlayMode)]
        public static void ApplyActiveBuildTargetPlayMode()
        {
            ApplyPlayModeToRuntimeSettings(EditorUserBuildSettings.activeBuildTarget);
        }

        /// <summary>从 YooAsset Collector 将主包和 RawFile 包名同步到运行时配置。</summary>
        [MenuItem("Build/热更新/内部工具/从 YooAsset Collector 同步包名", false, HotfixBuildMenuPriority.InternalSyncPackageNames)]
        public static void SyncPackageNamesFromCollectorSettingsMenu()
        {
            SyncPackageNamesFromCollectorSettings();
        }

        /// <summary>将指定平台 PlayMode 与包名写入运行时配置。</summary>
        public static EPlayMode ApplyPlayModeToRuntimeSettings(BuildTarget target)
        {
            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);
            SetRuntimeSettingsPlayMode(playMode, $"set to {playMode} for {target}");
            SyncPackageNamesFromCollectorSettings();
            return playMode;
        }

        /// <summary>Player 构建前应用 ReleaseProfile，并校验最终 PlayMode、CDN 和资源包名。</summary>
        public static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(BuildTarget target)
        {
            return ApplyPlayModeToRuntimeSettingsForBuild(target, EditorUserBuildSettings.development);
        }

        /// <summary>按本次 BuildOptions 判定是否允许开发环境，再应用并校验 Player 配置。</summary>
        public static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(BuildTarget target, BuildOptions options)
        {
            return ApplyPlayModeToRuntimeSettingsForBuild(target, IsDevelopmentBuild(options));
        }

        private static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(
            BuildTarget target,
            bool allowDevelopmentEnvironment)
        {
            var releaseProfile = HotfixReleaseProfile.LoadSelectedOrDefault();
            if (releaseProfile != null)
            {
                if (!releaseProfile.IsCompatibleWith(target))
                {
                    throw new InvalidOperationException(
                        $"ReleaseProfile BuildTarget mismatch. Profile={releaseProfile.BuildTarget}, Build={target}.");
                }

                releaseProfile.ApplyToEditorSettings();
                allowDevelopmentEnvironment = allowDevelopmentEnvironment && releaseProfile.AllowsDevelopmentCdnForBuild;
            }
            else if (!allowDevelopmentEnvironment)
            {
                throw new InvalidOperationException(
                    $"Release player build requires a ReleaseProfile. Missing: {HotfixReleaseProfile.DefaultAssetPath}");
            }

            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);
            ValidateRemoteSettingsForBuild(target, allowDevelopmentEnvironment, playMode);
            SetRuntimeSettingsPlayMode(playMode, $"set to {playMode} for {target} build");
            SyncPackageNamesFromCollectorSettings();
            return playMode;
        }

        /// <summary>从 YooAsset Collector 推导主包/RawFile 包并同步到运行时配置。</summary>
        public static BuildAssetsCommand.RuntimePackageConfig SyncPackageNamesFromCollectorSettings()
        {
            var packageConfig = BuildAssetsCommand.GetRuntimePackageConfigFromCollectorSettings();
            SetRuntimeSettingsPackageNames(packageConfig, "synced from YooAsset collector settings");
            return packageConfig;
        }

        private static void SetRuntimeSettingsPlayMode(EPlayMode playMode, string logReason)
        {
            var settings = GetOrCreateRuntimeSettings();
            if (settings.PlayerPlayMode != playMode)
            {
                settings.SetPlayerPlayModeForEditor(playMode);
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssetIfDirty(settings);
            }

            Debug.Log($"[HotfixBuildProfile] Runtime play mode {logReason}.");
        }

        private static void SetRuntimeSettingsPackageNames(
            BuildAssetsCommand.RuntimePackageConfig packageConfig,
            string logReason)
        {
            var settings = GetOrCreateRuntimeSettings();
            if (settings.MainPackageName != packageConfig.MainPackageName ||
                settings.IncludeRawFilePackage != packageConfig.IncludeRawFilePackage ||
                settings.RawFilePackageName != packageConfig.RawFilePackageName)
            {
                settings.SetPackageNamesForEditor(
                    packageConfig.MainPackageName,
                    packageConfig.IncludeRawFilePackage,
                    packageConfig.RawFilePackageName);
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssetIfDirty(settings);
            }

            Debug.Log(
                $"[HotfixBuildProfile] Runtime packages {logReason}. Main={packageConfig.MainPackageName}, " +
                $"IncludeRawFile={packageConfig.IncludeRawFilePackage}, RawFile={packageConfig.RawFilePackageName}");
        }

        private static HotfixBuildProfile GetOrCreateProfile()
        {
            var profile = AssetDatabase.LoadAssetAtPath<HotfixBuildProfile>(HotfixBuildProfile.AssetPath);
            if (profile != null)
            {
                return profile;
            }

            string directory = Path.GetDirectoryName(HotfixBuildProfile.AssetPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            profile = ScriptableObject.CreateInstance<HotfixBuildProfile>();
            AssetDatabase.CreateAsset(profile, HotfixBuildProfile.AssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[HotfixBuildProfile] Created default profile: {HotfixBuildProfile.AssetPath}");
            return profile;
        }

        /// <summary>阻断 Player 平台使用 EditorSimulateMode 或不兼容的 WebGL PlayMode。</summary>
        public static void ValidatePlayerPlayMode(EPlayMode playMode, BuildTarget target)
        {
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                throw new InvalidOperationException(
                    $"Invalid YooAsset play mode for player build: {playMode}. Use HostPlayMode, OfflinePlayMode, or WebPlayMode.");
            }

            if (target == BuildTarget.WebGL && playMode != EPlayMode.WebPlayMode)
            {
                throw new InvalidOperationException(
                    $"Invalid YooAsset play mode for WebGL build: {playMode}. WebGL must use WebPlayMode.");
            }

            if (target != BuildTarget.WebGL && playMode == EPlayMode.WebPlayMode)
            {
                throw new InvalidOperationException(
                    $"Invalid YooAsset play mode for {target}: WebPlayMode only supports WebGL.");
            }
        }

        /// <summary>使用当前构建的 Development 标记校验远端设置。</summary>
        public static void ValidateRemoteSettingsForBuild(BuildTarget target)
        {
            ValidateRemoteSettingsForBuild(target, EditorUserBuildSettings.development);
        }

        /// <summary>按显式环境权限校验指定平台的远端地址与首包策略。</summary>
        public static void ValidateRemoteSettingsForBuild(BuildTarget target, bool allowDevelopmentEnvironment)
        {
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(RuntimeSettingsAssetPath);
            var playerPlayMode = settings == null ? EPlayMode.HostPlayMode : settings.PlayerPlayMode;
            ValidateRemoteSettingsForBuild(target, allowDevelopmentEnvironment, playerPlayMode);
        }

        private static void ValidateRemoteSettingsForBuild(
            BuildTarget target,
            bool allowDevelopmentEnvironment,
            EPlayMode playerPlayMode)
        {
            var remoteSettings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(RemoteSettingsAssetPath);
            if (remoteSettings == null)
            {
                throw new InvalidOperationException($"Hotfix remote settings missing: {RemoteSettingsAssetPath}");
            }

            string platform = GetRuntimePlatformName(target);
            if (!remoteSettings.TryValidateForPlayerBuild(allowDevelopmentEnvironment, platform, out var error))
            {
                throw new InvalidOperationException(error);
            }

            var runtimeSettings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(RuntimeSettingsAssetPath);
            if (runtimeSettings != null &&
                runtimeSettings.StartupPackageMode == StartupPackageMode.EmptyPackage &&
                !remoteSettings.TryValidateForEmptyPackageBuild(
                    allowDevelopmentEnvironment,
                    platform,
                    out error))
            {
                throw new InvalidOperationException(error);
            }

            BuildAssetsCommand.ValidateStartupPackageForPlayerBuild(target, playerPlayMode);
            ValidateBuildinPackageForPlayerBuild(runtimeSettings);
        }

        private static void ValidateBuildinPackageForPlayerBuild(HotfixRuntimeSettings runtimeSettings)
        {
            if (runtimeSettings == null ||
                runtimeSettings.StartupPackageMode == StartupPackageMode.EmptyPackage)
            {
                return;
            }

            string packageName = runtimeSettings.MainPackageName;
            string packageRoot = Path.Combine(YooAssetSettingsData.GetYooDefaultBuildinRoot(), packageName);
            string versionFile = Path.Combine(
                packageRoot,
                YooAssetSettingsData.GetPackageVersionFileName(packageName));
            if (!File.Exists(versionFile))
            {
                throw new InvalidOperationException(
                    $"StartupPackageMode.{runtimeSettings.StartupPackageMode} requires a built-in YooAsset package, " +
                    $"but the package version file is missing: {versionFile}. " +
                    "Run Build/热更新/一键构建/构建首包 before building the Player.");
            }

            string packageVersion = File.ReadAllText(versionFile).Trim();
            if (string.IsNullOrWhiteSpace(packageVersion))
            {
                throw new InvalidOperationException($"Built-in YooAsset package version is empty: {versionFile}");
            }

            string manifestFile = Path.Combine(
                packageRoot,
                YooAssetSettingsData.GetManifestBinaryFileName(packageName, packageVersion));
            string manifestHashFile = Path.Combine(
                packageRoot,
                YooAssetSettingsData.GetPackageHashFileName(packageName, packageVersion));
            if (!File.Exists(manifestFile) || !File.Exists(manifestHashFile))
            {
                throw new InvalidOperationException(
                    $"Built-in YooAsset manifest is incomplete for {packageName} {packageVersion}. " +
                    $"Manifest: {manifestFile}, Hash: {manifestHashFile}. " +
                    "Run Build/热更新/一键构建/构建首包 again before building the Player.");
            }
        }

        private static bool IsDevelopmentBuild(BuildOptions options)
        {
            return (options & BuildOptions.Development) == BuildOptions.Development;
        }

        private static string GetRuntimePlatformName(BuildTarget target)
        {
            return HotfixUtility.GetPlatformNameForBuildTarget(target);
        }

        private static HotfixRuntimeSettings GetOrCreateRuntimeSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(RuntimeSettingsAssetPath);
            if (settings != null)
            {
                return settings;
            }

            string directory = Path.GetDirectoryName(RuntimeSettingsAssetPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            settings = ScriptableObject.CreateInstance<HotfixRuntimeSettings>();
            AssetDatabase.CreateAsset(settings, RuntimeSettingsAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[HotfixBuildProfile] Created runtime settings: {RuntimeSettingsAssetPath}");
            return settings;
        }
    }

    /// <summary>真实 Player 构建前同步并校验 PlayMode、远端环境与发布 Profile。</summary>
    public sealed class HotfixBuildPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        /// <summary>Unity Player 构建开始前统一应用和校验热更新发布配置。</summary>
        public void OnPreprocessBuild(BuildReport report)
        {
            if (HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild)
            {
                Debug.Log("[HotfixBuild] 安全生成 HybridCLR 数据期间跳过远端设置校验。");
                return;
            }

            HotfixBuildProfileUtility.ApplyPlayModeToRuntimeSettingsForBuild(
                report.summary.platform,
                report.summary.options);
        }
    }
}
