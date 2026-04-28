using System;
using System.IO;
using Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public const string BootScenePath = "Assets/Scenes/Boot.unity";

        [MenuItem("Build/Hotfix/Apply Build Play Mode To Boot Scene")]
        public static void ApplyActiveBuildTargetPlayMode()
        {
            ApplyPlayModeToBootScene(EditorUserBuildSettings.activeBuildTarget);
        }

        public static EPlayMode ApplyPlayModeToBootScene(BuildTarget target)
        {
            var profile = GetOrCreateProfile();
            var playMode = profile.GetPlayMode(target);
            ValidatePlayerPlayMode(playMode, target);

            var scene = OpenBootScene(out bool shouldCloseScene);
            try
            {
                var boot = FindBoot(scene);
                if (boot == null)
                {
                    throw new InvalidOperationException($"Boot component not found in scene: {BootScenePath}");
                }

                if (boot.playMode != playMode)
                {
                    boot.playMode = playMode;
                    EditorUtility.SetDirty(boot);
                    EditorSceneManager.MarkSceneDirty(scene);
                    EditorSceneManager.SaveScene(scene);
                }

                Debug.Log($"[HotfixBuildProfile] Boot play mode set to {playMode} for {target}.");
                return playMode;
            }
            finally
            {
                if (shouldCloseScene)
                {
                    EditorSceneManager.CloseScene(scene, true);
                }
            }
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

        private static void ValidatePlayerPlayMode(EPlayMode playMode, BuildTarget target)
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

        private static Scene OpenBootScene(out bool shouldCloseScene)
        {
            var scene = SceneManager.GetSceneByPath(BootScenePath);
            if (scene.IsValid() && scene.isLoaded)
            {
                shouldCloseScene = false;
                return scene;
            }

            shouldCloseScene = true;
            return EditorSceneManager.OpenScene(BootScenePath, OpenSceneMode.Additive);
        }

        private static Boot FindBoot(Scene scene)
        {
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                var boot = rootGameObject.GetComponentInChildren<Boot>(true);
                if (boot != null)
                {
                    return boot;
                }
            }

            return null;
        }
    }
}
