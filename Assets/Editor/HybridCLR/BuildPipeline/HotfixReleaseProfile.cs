using System;
using System.IO;
using Framework;
using Framework.Assemblies;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    [CreateAssetMenu(fileName = "HotfixReleaseProfile", menuName = "Hotfix/Release Profile", order = 10)]
    public sealed class HotfixReleaseProfile : ScriptableObject
    {
        public const string DefaultAssetPath = "Assets/Editor/HybridCLR/HotfixReleaseProfile.asset";
        private const string SelectedProfileKeySuffix = ".HotfixBuildCenter.ReleaseProfile";

        [Header("Identity")]
        public BuildTarget BuildTarget = BuildTarget.StandaloneWindows64;
        public string AppVersion = "1.0.0";
        public string AppVersionMin = "1.0.0";
        public string AppVersionMax = "1.0.0";
        public string ResourceVersion = string.Empty;
        public string HotfixVersion = string.Empty;

        [Header("Remote")]
        public HotfixRemoteEnvironment RemoteEnvironment = HotfixRemoteEnvironment.Development;
        public string Channel = "default";
        public string Region = "global";
        public bool AllowDevelopmentCdn = true;

        [Header("Startup")]
        public StartupPackageMode StartupPackageMode = StartupPackageMode.FirstPackage;
        public StartupDownloadMode StartupDownloadMode = StartupDownloadMode.DownloadAll;
        public StartupUpdatePolicy StartupUpdatePolicy = StartupUpdatePolicy.AllowCached;
        public string[] StartupDownloadTags = Array.Empty<string>();

        [Header("Code Entry")]
        public string EntryTypeName = "HotfixDemo.HotfixCodeEntry";
        public string EntryMethodName = "Entrance";

        public string DisplayName => string.IsNullOrWhiteSpace(name) ? "未命名 ReleaseProfile" : name;

        public bool AllowsDevelopmentCdnForBuild =>
            AllowDevelopmentCdn && RemoteEnvironment != HotfixRemoteEnvironment.Production;

        public bool IsFormalRelease => !AllowsDevelopmentCdnForBuild;

        public bool IsCompatibleWith(BuildTarget target)
        {
            return BuildTarget == target;
        }

        public void ApplyToEditorSettings()
        {
            if (!string.IsNullOrWhiteSpace(AppVersion))
            {
                PlayerSettings.bundleVersion = AppVersion.Trim();
            }

            var runtimeSettings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            if (runtimeSettings != null)
            {
                runtimeSettings.SetStartupSettingsForEditor(
                    StartupPackageMode,
                    StartupDownloadMode,
                    StartupUpdatePolicy,
                    StartupDownloadTags);
                EditorUtility.SetDirty(runtimeSettings);
                AssetDatabase.SaveAssetIfDirty(runtimeSettings);
            }

            var remoteSettings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(HotfixBuildProfileUtility.RemoteSettingsAssetPath);
            if (remoteSettings != null)
            {
                remoteSettings.SetDefaultSelectorForEditor(RemoteEnvironment, Channel, Region);
                EditorUtility.SetDirty(remoteSettings);
                AssetDatabase.SaveAssetIfDirty(remoteSettings);
            }

            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(BuildAssetsCommand.HotfixAssemblyManifestAssetPath);
            if (hotfixManifest != null)
            {
                ApplyToHotfixManifest(hotfixManifest);
                EditorUtility.SetDirty(hotfixManifest);
                AssetDatabase.SaveAssetIfDirty(hotfixManifest);
            }
        }

        public void CaptureCurrentEditorSettings()
        {
            BuildTarget = EditorUserBuildSettings.activeBuildTarget;
            AppVersion = string.IsNullOrWhiteSpace(PlayerSettings.bundleVersion)
                ? AppVersion
                : PlayerSettings.bundleVersion.Trim();

            var runtimeSettings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            if (runtimeSettings != null)
            {
                StartupPackageMode = runtimeSettings.StartupPackageMode;
                StartupDownloadMode = runtimeSettings.StartupDownloadMode;
                StartupUpdatePolicy = runtimeSettings.StartupUpdatePolicy;
                StartupDownloadTags = runtimeSettings.StartupDownloadTags;
            }

            var remoteSettings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(HotfixBuildProfileUtility.RemoteSettingsAssetPath);
            if (remoteSettings != null)
            {
                RemoteEnvironment = remoteSettings.DefaultEnvironment;
                Channel = remoteSettings.DefaultChannel;
                Region = remoteSettings.DefaultRegion;
            }

            var hotfixManifest = AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(BuildAssetsCommand.HotfixAssemblyManifestAssetPath);
            if (hotfixManifest != null)
            {
                AppVersionMin = string.IsNullOrWhiteSpace(hotfixManifest.AppVersionMin)
                    ? AppVersion
                    : hotfixManifest.AppVersionMin.Trim();
                AppVersionMax = string.IsNullOrWhiteSpace(hotfixManifest.AppVersionMax)
                    ? AppVersion
                    : hotfixManifest.AppVersionMax.Trim();
                HotfixVersion = hotfixManifest.HotfixVersion ?? string.Empty;
                EntryTypeName = string.IsNullOrWhiteSpace(hotfixManifest.EntryTypeName)
                    ? EntryTypeName
                    : hotfixManifest.EntryTypeName.Trim();
                EntryMethodName = string.IsNullOrWhiteSpace(hotfixManifest.EntryMethodName)
                    ? EntryMethodName
                    : hotfixManifest.EntryMethodName.Trim();
            }
            else
            {
                AppVersionMin = string.IsNullOrWhiteSpace(AppVersionMin) ? AppVersion : AppVersionMin;
                AppVersionMax = string.IsNullOrWhiteSpace(AppVersionMax) ? AppVersion : AppVersionMax;
            }
        }

        public void ApplyToHotfixManifest(HotfixAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(AppVersionMin))
            {
                manifest.AppVersionMin = AppVersionMin.Trim();
            }

            if (!string.IsNullOrWhiteSpace(AppVersionMax))
            {
                manifest.AppVersionMax = AppVersionMax.Trim();
            }

            if (!string.IsNullOrWhiteSpace(HotfixVersion))
            {
                manifest.HotfixVersion = HotfixVersion.Trim();
            }

            if (!string.IsNullOrWhiteSpace(EntryTypeName))
            {
                manifest.EntryTypeName = EntryTypeName.Trim();
            }

            if (!string.IsNullOrWhiteSpace(EntryMethodName))
            {
                manifest.EntryMethodName = EntryMethodName.Trim();
            }
        }

        public bool ValidateForBuild(HotfixBuildContext context, out string error)
        {
            error = string.Empty;
            if (context == null)
            {
                error = "ReleaseProfile validation requires a build context.";
                return false;
            }

            if (!IsCompatibleWith(context.BuildTarget))
            {
                error = $"ReleaseProfile BuildTarget mismatch. Profile={BuildTarget}, Current={context.BuildTarget}.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AppVersion))
            {
                error = "ReleaseProfile AppVersion is empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AppVersionMin) || string.IsNullOrWhiteSpace(AppVersionMax))
            {
                error = "ReleaseProfile AppVersionMin/AppVersionMax can not be empty.";
                return false;
            }

            if (CompareVersion(AppVersionMin, AppVersionMax) > 0)
            {
                error = $"ReleaseProfile app version range is invalid. Min={AppVersionMin}, Max={AppVersionMax}.";
                return false;
            }

            if (!IsValidPackageVersion(ResourceVersion))
            {
                error = $"ReleaseProfile ResourceVersion contains invalid file name characters: {ResourceVersion}";
                return false;
            }

            if (RemoteEnvironment == HotfixRemoteEnvironment.Development && !AllowDevelopmentCdn)
            {
                error = "Formal ReleaseProfile can not use Development remote environment.";
                return false;
            }

            if (StartupDownloadMode == StartupDownloadMode.DownloadByTags &&
                !ContainsTag(StartupDownloadTags, HotfixRuntimeSettings.DefaultStartupTag))
            {
                error = $"ReleaseProfile DownloadByTags requires '{HotfixRuntimeSettings.DefaultStartupTag}' tag.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(EntryTypeName) || string.IsNullOrWhiteSpace(EntryMethodName))
            {
                error = "ReleaseProfile CodeEntry type and method are required.";
                return false;
            }

            if (context.RemoteSettings != null &&
                !context.RemoteSettings.TryValidateForPlayerBuild(
                    AllowsDevelopmentCdnForBuild,
                    context.BuildTargetName,
                    RemoteEnvironment,
                    Channel,
                    Region,
                    out error))
            {
                return false;
            }

            return true;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }

        public static HotfixReleaseProfile GetOrCreateDefault()
        {
            var profile = AssetDatabase.LoadAssetAtPath<HotfixReleaseProfile>(DefaultAssetPath);
            if (profile != null)
            {
                return profile;
            }

            string directory = Path.GetDirectoryName(DefaultAssetPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            profile = CreateInstance<HotfixReleaseProfile>();
            profile.name = "HotfixReleaseProfile";
            profile.BuildTarget = EditorUserBuildSettings.activeBuildTarget;
            profile.AppVersion = string.IsNullOrWhiteSpace(PlayerSettings.bundleVersion)
                ? "1.0.0"
                : PlayerSettings.bundleVersion.Trim();
            profile.AppVersionMin = profile.AppVersion;
            profile.AppVersionMax = profile.AppVersion;
            profile.CaptureCurrentEditorSettings();
            profile.ResourceVersion = string.Empty;
            profile.HotfixVersion = string.Empty;
            AssetDatabase.CreateAsset(profile, DefaultAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return profile;
        }

        public static HotfixReleaseProfile LoadSelectedOrDefault()
        {
            string path = EditorPrefs.GetString(GetSelectedProfilePrefsKey(), DefaultAssetPath);
            var profile = AssetDatabase.LoadAssetAtPath<HotfixReleaseProfile>(path);
            if (profile != null)
            {
                return profile;
            }

            return AssetDatabase.LoadAssetAtPath<HotfixReleaseProfile>(DefaultAssetPath);
        }

        public static void SaveSelectedProfile(HotfixReleaseProfile profile)
        {
            string path = profile == null ? string.Empty : AssetDatabase.GetAssetPath(profile);
            if (!string.IsNullOrWhiteSpace(path))
            {
                EditorPrefs.SetString(GetSelectedProfilePrefsKey(), path);
            }
        }

        private static string GetSelectedProfilePrefsKey()
        {
            return Application.dataPath + SelectedProfileKeySuffix;
        }

        private static bool ContainsTag(string[] tags, string requiredTag)
        {
            if (tags == null)
            {
                return false;
            }

            foreach (var tag in tags)
            {
                if (string.Equals(tag, requiredTag, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static int CompareVersion(string left, string right)
        {
            if (Version.TryParse(left, out var leftVersion) &&
                Version.TryParse(right, out var rightVersion))
            {
                return leftVersion.CompareTo(rightVersion);
            }

            return string.Compare(left ?? string.Empty, right ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsValidPackageVersion(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            string normalized = value.Trim();
            if (normalized.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 ||
                normalized.IndexOfAny(new[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*' }) >= 0)
            {
                return false;
            }

            foreach (char character in normalized)
            {
                if (char.IsControl(character))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
