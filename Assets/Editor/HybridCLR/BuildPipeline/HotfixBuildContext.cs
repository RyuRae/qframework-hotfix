using Framework;
using Framework.Assemblies;
using UnityEditor;
using UnityEngine;
using YooAsset;

namespace HybridCLR.Editor
{
    public sealed class HotfixBuildContext
    {
        public readonly HotfixBuildMode Mode;
        public readonly BuildTarget BuildTarget;
        public readonly string BuildTargetName;
        public readonly string AppVersion;
        public readonly HotfixRuntimeSettings RuntimeSettings;
        public readonly HotfixRemoteSettings RemoteSettings;
        public readonly AOTAssemblyManifest AOTManifest;
        public readonly HotfixAssemblyManifest HotfixManifest;
        public readonly EPlayMode PlayerPlayMode;
        public readonly StartupPackageMode StartupPackageMode;
        public readonly StartupDownloadMode StartupDownloadMode;
        public readonly string[] StartupDownloadTags;
        public readonly string MainPackageName;
        public readonly bool IncludeRawFilePackage;
        public readonly string RawFilePackageName;
        public readonly string RemoteEnvironmentName;
        public readonly string RemoteChannel;
        public readonly string RemoteRegion;
        public readonly string EntrySceneAddress;
        public readonly string EntryPrefabAddress;
        public readonly string EntryMethod;

        private HotfixBuildContext(
            HotfixBuildMode mode,
            BuildTarget buildTarget,
            HotfixRuntimeSettings runtimeSettings,
            HotfixRemoteSettings remoteSettings,
            AOTAssemblyManifest aotManifest,
            HotfixAssemblyManifest hotfixManifest)
        {
            Mode = mode;
            BuildTarget = buildTarget;
            BuildTargetName = HotfixUtility.GetPlatformNameForBuildTarget(buildTarget);
            AppVersion = string.IsNullOrWhiteSpace(PlayerSettings.bundleVersion)
                ? string.Empty
                : PlayerSettings.bundleVersion.Trim();

            RuntimeSettings = runtimeSettings;
            RemoteSettings = remoteSettings;
            AOTManifest = aotManifest;
            HotfixManifest = hotfixManifest;

            PlayerPlayMode = runtimeSettings == null ? EPlayMode.HostPlayMode : runtimeSettings.PlayerPlayMode;
            StartupPackageMode = runtimeSettings == null
                ? StartupPackageMode.FirstPackage
                : runtimeSettings.StartupPackageMode;
            StartupDownloadMode = runtimeSettings == null
                ? StartupDownloadMode.DownloadAll
                : runtimeSettings.StartupDownloadMode;
            StartupDownloadTags = runtimeSettings == null
                ? new string[0]
                : runtimeSettings.StartupDownloadTags;

            MainPackageName = runtimeSettings == null
                ? BuildAssetsCommand.GetConfiguredMainPackageName()
                : runtimeSettings.MainPackageName;
            IncludeRawFilePackage = runtimeSettings != null && runtimeSettings.IncludeRawFilePackage;
            RawFilePackageName = runtimeSettings == null
                ? HotfixRuntimeSettings.DefaultRawFilePackageName
                : runtimeSettings.RawFilePackageName;

            RemoteEnvironmentName = ReadSerializedEnum(remoteSettings, "defaultEnvironment", "缺失");
            RemoteChannel = ReadSerializedString(remoteSettings, "defaultChannel", "default");
            RemoteRegion = ReadSerializedString(remoteSettings, "defaultRegion", "global");

            EntrySceneAddress = hotfixManifest == null || string.IsNullOrWhiteSpace(hotfixManifest.EntrySceneAddress)
                ? BuildAssetsCommand.DefaultEntrySceneAddress
                : hotfixManifest.EntrySceneAddress.Trim();
            EntryPrefabAddress = hotfixManifest == null ? string.Empty : hotfixManifest.EntryPrefabAddress ?? string.Empty;
            EntryMethod = hotfixManifest == null ||
                          string.IsNullOrWhiteSpace(hotfixManifest.EntryTypeName) ||
                          string.IsNullOrWhiteSpace(hotfixManifest.EntryMethodName)
                ? string.Empty
                : $"{hotfixManifest.EntryTypeName}.{hotfixManifest.EntryMethodName}";
        }

        public bool ShouldCopyInitialPackageToStreamingAssets =>
            Mode == HotfixBuildMode.InitialPackage && StartupPackageMode != StartupPackageMode.EmptyPackage;

        public static HotfixBuildContext Create(HotfixBuildMode mode)
        {
            return new HotfixBuildContext(
                mode,
                EditorUserBuildSettings.activeBuildTarget,
                AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath),
                AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(HotfixBuildProfileUtility.RemoteSettingsAssetPath),
                AssetDatabase.LoadAssetAtPath<AOTAssemblyManifest>(BuildAssetsCommand.AOTAssemblyManifestAssetPath),
                AssetDatabase.LoadAssetAtPath<HotfixAssemblyManifest>(BuildAssetsCommand.HotfixAssemblyManifestAssetPath));
        }

        private static string ReadSerializedString(Object target, string propertyName, string fallback)
        {
            if (target == null)
            {
                return fallback;
            }

            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty(propertyName);
            return property == null || string.IsNullOrWhiteSpace(property.stringValue)
                ? fallback
                : property.stringValue.Trim();
        }

        private static string ReadSerializedEnum(Object target, string propertyName, string fallback)
        {
            if (target == null)
            {
                return fallback;
            }

            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty(propertyName);
            if (property == null || property.enumNames == null || property.enumValueIndex < 0 ||
                property.enumValueIndex >= property.enumNames.Length)
            {
                return fallback;
            }

            return property.enumNames[property.enumValueIndex];
        }
    }
}
