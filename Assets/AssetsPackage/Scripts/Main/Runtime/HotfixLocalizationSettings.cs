using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Framework
{
    public enum HotfixLanguage
    {
        FollowSystem,
        ChineseSimplified,
        English
    }

    public enum HotfixTextKey
    {
        StartupRuntimeConfigMissing,
        HotUpdateProcedureInitializeFailed,
        SceneLoading,
        InvalidEditorSimulateInPlayer,
        InvalidWebGLPlayMode,
        InvalidNonWebGLPlayMode,
        RemoteEnvironmentConfigMissing,
        RemoteMainFallbackSame,
        RemoteUrlEmpty,
        RemoteUrlInvalid,
        RemoteUrlSchemeUnsupported,
        RemoteUrlRequiresHttps,
        RemoteUrlDomainNotAllowed,
        RemoteSettingsMissing,
        ResourcePackageInitializeFailed,
        RawFilePackageInitializeFailed,
        ResourcePackageInitializeSucceed,
        DownloadInfoPrompt,
        DownloadFailed,
        DownloadCanceledDefault,
        StartupUsingLocalCacheDefault,
        ResourceDownloadingProgress,
        DownloadFileBegin,
        DownloadFileCompleted,
        DownloadFileFailed,
        AssetLoading,
        FileDownloading,
        UserCanceledResourceDownload,
        UserCanceledResourceUpdate,
        DownloadPaused,
        NoDownloadTaskToPause,
        DownloadResumed,
        NoDownloadTaskToResume,
        MainPackageLocalCacheUnavailable,
        RawFilePackageLocalCacheUnavailable,
        UserCanceledStartupUpdate,
        DownloadFailedUseLocalCacheReason,
        DownloadFailedExitReason,
        RetryOrUseCachePrompt,
        RetryOrExitPrompt,
        PackageDownloadFailedPrompt,
        LocalCacheStartupFailed,
        BundleNotFoundHint,
        DnsResolveFailedHint,
        ServerUnreachableHint,
        DownloadVerifyFailedHint,
        NetworkOrRemoteResourceErrorHint,
        StartupLocalCacheOnlyReason,
        RequestRemoteVersionFailedTitle,
        RequestRemoteVersionFailedUseCacheReason,
        StartupFailurePrompt,
        VersionFileNotFoundHint,
        RemoteManifestInvalidHint,
        UpdateManifestFailedTitle,
        UpdateManifestFailedUseCacheReason,
        ManifestFileNotFoundHint,
        LoadingHotUpdateAssemblies,
        HotUpdateAssemblyLoadFailed,
        HotUpdateAssembliesLoaded,
        LoadingAotMetadata,
        AotMetadataLoadFailed,
        AotMetadataLoaded
    }

    [Serializable]
    public sealed class HotfixLocalizedText
    {
        [SerializeField]
        private string key;

        [SerializeField]
        [TextArea(1, 4)]
        private string chineseSimplified;

        [SerializeField]
        [TextArea(1, 4)]
        private string english;

        public string Key => key;

        public string Get(HotfixLanguage language)
        {
            switch (language)
            {
                case HotfixLanguage.ChineseSimplified:
                    return chineseSimplified;
                case HotfixLanguage.English:
                    return english;
                default:
                    return english;
            }
        }
    }

    [CreateAssetMenu(fileName = AssetName, menuName = "Hotfix/Localization Settings", order = 2)]
    public sealed class HotfixLocalizationSettings : ScriptableObject
    {
        public const string AssetName = "HotfixLocalizationSettings";
        public const string ResourcesPath = AssetName;

        [SerializeField]
        private HotfixLanguage defaultLanguage = HotfixLanguage.FollowSystem;

        [SerializeField]
        private bool enableRuntimeOverride = true;

        [SerializeField]
        private string languageOverrideKey = "Hotfix.Language";

        [SerializeField]
        private string languageCommandLineKey = "hotfix-language";

        [SerializeField]
        private HotfixLocalizedText[] texts = Array.Empty<HotfixLocalizedText>();

        private static HotfixLocalizationSettings cachedSettings;
        private Dictionary<string, HotfixLocalizedText> textLookup;

        public static HotfixLocalizationSettings Load()
        {
            return cachedSettings != null
                ? cachedSettings
                : cachedSettings = Resources.Load<HotfixLocalizationSettings>(ResourcesPath);
        }

        public string Get(HotfixTextKey key, params object[] args)
        {
            string template = ResolveText(key);
            if (args == null || args.Length == 0)
            {
                return template;
            }

            try
            {
                return string.Format(CultureInfo.InvariantCulture, template, args);
            }
            catch (FormatException)
            {
                return template;
            }
        }

        private string ResolveText(HotfixTextKey key)
        {
            EnsureLookup();

            string textKey = key.ToString();
            if (textLookup.TryGetValue(textKey, out var text) && text != null)
            {
                string value = text.Get(ResolveLanguage());
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return textKey;
        }

        private void EnsureLookup()
        {
            if (textLookup != null)
            {
                return;
            }

            textLookup = new Dictionary<string, HotfixLocalizedText>(StringComparer.OrdinalIgnoreCase);
            if (texts == null)
            {
                return;
            }

            foreach (var text in texts)
            {
                if (text == null || string.IsNullOrWhiteSpace(text.Key))
                {
                    continue;
                }

                textLookup[text.Key.Trim()] = text;
            }
        }

        private HotfixLanguage ResolveLanguage()
        {
            if (enableRuntimeOverride)
            {
                string languageName = GetCommandLineValue(languageCommandLineKey);
                if (string.IsNullOrWhiteSpace(languageName))
                {
                    languageName = PlayerPrefs.GetString(languageOverrideKey, string.Empty);
                }

                if (Enum.TryParse(languageName, true, out HotfixLanguage language) &&
                    language != HotfixLanguage.FollowSystem)
                {
                    return language;
                }
            }

            return defaultLanguage == HotfixLanguage.FollowSystem
                ? ResolveSystemLanguage()
                : defaultLanguage;
        }

        private static HotfixLanguage ResolveSystemLanguage()
        {
            return Application.systemLanguage == SystemLanguage.ChineseSimplified ||
                   Application.systemLanguage == SystemLanguage.ChineseTraditional ||
                   Application.systemLanguage == SystemLanguage.Chinese
                ? HotfixLanguage.ChineseSimplified
                : HotfixLanguage.English;
        }

        private static string GetCommandLineValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            string normalizedKey = $"--{key.Trim()}=";
            var args = Environment.GetCommandLineArgs();
            foreach (var arg in args)
            {
                if (arg.StartsWith(normalizedKey, StringComparison.OrdinalIgnoreCase))
                {
                    return arg.Substring(normalizedKey.Length).Trim();
                }
            }

            return string.Empty;
        }
    }

    public static class HotfixText
    {
        public static string Get(HotfixTextKey key, params object[] args)
        {
            var settings = HotfixLocalizationSettings.Load();
            return settings == null ? key.ToString() : settings.Get(key, args);
        }
    }
}
