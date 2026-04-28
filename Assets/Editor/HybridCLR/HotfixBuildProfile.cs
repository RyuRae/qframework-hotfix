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

        [MenuItem("Build/Hotfix/Apply Build Play Mode To Runtime Settings")]
        public static void ApplyActiveBuildTargetPlayMode()
        {
            ApplyPlayModeToRuntimeSettings(EditorUserBuildSettings.activeBuildTarget);
        }

        public static EPlayMode ApplyPlayModeToRuntimeSettings(BuildTarget target)
        {
            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);
            SetRuntimeSettingsPlayMode(playMode, $"set to {playMode} for {target}");
            return playMode;
        }

        public static EPlayMode ApplyPlayModeToRuntimeSettingsForBuild(BuildTarget target)
        {
            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);
            SetRuntimeSettingsPlayMode(playMode, $"set to {playMode} for {target} build");
            return playMode;
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
            HotfixBuildProfileUtility.ApplyPlayModeToRuntimeSettingsForBuild(report.summary.platform);
        }
    }
}
