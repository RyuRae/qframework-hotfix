using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Framework
{
    /// <summary>热更新启动界面的语言选择。</summary>
    public enum HotfixLanguage
    {
        FollowSystem,
        ChineseSimplified,
        English
    }

    /// <summary>启动、下载和程序集加载阶段的本地化文案键。</summary>
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
        AotMetadataLoaded,
        PreloadingHotfixResources,
        HotfixResourcePreloadFailed,
        HotfixResourcePreloadCanceled,
        HotfixResourcesPreloaded
    }

    /// <summary>单条中英文热更新提示文案。</summary>
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

    /// <summary>热更新启动层的轻量本地化配置。</summary>
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

        /// <summary>从 Resources 载入并缓存本地化配置。</summary>
        public static HotfixLocalizationSettings Load()
        {
            return cachedSettings != null
                ? cachedSettings
                : cachedSettings = Resources.Load<HotfixLocalizationSettings>(ResourcesPath);
        }

        /// <summary>按当前语言获取文案，并使用不受地区影响的格式化规则填充参数。</summary>
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
                string languageName = HotfixUtility.GetCommandLineValue(languageCommandLineKey);
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

    }

    /// <summary>热更新启动层获取本地化文案的静态门面。</summary>
    public static class HotfixText
    {
        public static string Get(HotfixTextKey key, params object[] args)
        {
            var settings = HotfixLocalizationSettings.Load();
            return settings == null ? key.ToString() : settings.Get(key, args);
        }
    }
}
