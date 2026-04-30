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

        private static bool IsStandaloneTarget(BuildTarget target)
        {
            return target == BuildTarget.StandaloneWindows ||
                   target == BuildTarget.StandaloneWindows64 ||
                   target == BuildTarget.StandaloneOSX ||
                   target == BuildTarget.StandaloneLinux64;
        }
    }

    public static class HotfixBuildProfileUtility
    {
        public const string RuntimeSettingsAssetPath = "Assets/AssetsPackage/Resources/HotfixRuntimeSettings.asset";
        public const string RemoteSettingsAssetPath = "Assets/AssetsPackage/Resources/HotfixRemoteSettings.asset";

        [MenuItem("Build/Hotfix/Apply Build Play Mode To Runtime Settings")]
        public static void ApplyActiveBuildTargetPlayMode()
        {
            ApplyPlayModeToRuntimeSettings(EditorUserBuildSettings.activeBuildTarget);
        }

        [MenuItem("Build/Hotfix/Sync Package Names From YooAsset Collector")]
        public static void SyncPackageNamesFromCollectorSettingsMenu()
        {
            SyncPackageNamesFromCollectorSettings();
        }

        public static EPlayMode ApplyPlayModeToRuntimeSettings(BuildTarget target)
        {
            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);
            SetRuntimeSettingsPlayMode(playMode, $"set to {playMode} for {target}");
            SyncPackageNamesFromCollectorSettings();
            return playMode;
        }

        public static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(BuildTarget target)
        {
            return ApplyPlayModeToRuntimeSettingsForBuild(target, EditorUserBuildSettings.development);
        }

        public static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(BuildTarget target, BuildOptions options)
        {
            return ApplyPlayModeToRuntimeSettingsForBuild(target, IsDevelopmentBuild(options));
        }

        private static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(
            BuildTarget target,
            bool allowDevelopmentEnvironment)
        {
            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);
            ValidateRemoteSettingsForBuild(target, allowDevelopmentEnvironment, playMode);
            SetRuntimeSettingsPlayMode(playMode, $"set to {playMode} for {target} build");
            SyncPackageNamesFromCollectorSettings();
            return playMode;
        }

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

        public static void ValidateRemoteSettingsForBuild(BuildTarget target)
        {
            ValidateRemoteSettingsForBuild(target, EditorUserBuildSettings.development);
        }

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
            var settings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(RemoteSettingsAssetPath);
            if (settings == null)
            {
                throw new InvalidOperationException($"Hotfix remote settings missing: {RemoteSettingsAssetPath}");
            }

            string platform = GetRuntimePlatformName(target);
            if (!settings.TryValidateForPlayerBuild(allowDevelopmentEnvironment, platform, out var error))
            {
                throw new InvalidOperationException(error);
            }

            BuildAssetsCommand.ValidateStartupPackageForPlayerBuild(target, playerPlayMode);
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

    public sealed class HotfixBuildPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            HotfixBuildProfileUtility.ApplyPlayModeToRuntimeSettingsForBuild(
                report.summary.platform,
                report.summary.options);
        }
    }
}
