using System;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 热更新远端资源环境，用于在同一个包体内切换不同 CDN 配置。
    /// </summary>
    public enum HotfixRemoteEnvironment
    {
        /// <summary>开发环境，通常指向本地或内网 CDN。</summary>
        Development,

        /// <summary>测试环境，供 QA 或自动化验证使用。</summary>
        Testing,

        /// <summary>预发环境，尽量保持和正式环境一致，用于上线前验证。</summary>
        Staging,

        /// <summary>正式环境，面向线上用户。</summary>
        Production
    }

    /// <summary>
    /// 已解析完成的远端资源地址，YooAsset 初始化时只依赖这里的最终结果。
    /// </summary>
    [Serializable]
    public sealed class HotfixRemoteAddress
    {
        /// <summary>当前使用的环境。</summary>
        public HotfixRemoteEnvironment Environment;

        /// <summary>当前运行平台，例如 Android、iOS、Windows。</summary>
        public string Platform;

        /// <summary>当前渠道，例如 official、tap、appstore。</summary>
        public string Channel;

        /// <summary>当前地区，例如 cn、us、global。</summary>
        public string Region;

        /// <summary>当前 YooAsset 资源包名。</summary>
        public string PackageName;

        /// <summary>主 CDN 根目录，最终文件名由 YooAsset 回调时继续拼接。</summary>
        public string MainUrl;

        /// <summary>备用 CDN 根目录，主 CDN 失败时由 YooAsset 使用。</summary>
        public string FallbackUrl;

        /// <summary>是否启用证书 pinning，当前仅作为后续 HTTPS 加固的配置预留。</summary>
        public bool CertificatePinningEnabled;

        /// <summary>证书公钥 pin，当前仅作为后续 HTTPS 加固的配置预留。</summary>
        public string CertificatePublicKeyPin;
    }

    /// <summary>
    /// 单个环境下的 CDN 配置模板，支持按平台、渠道、地区和包名动态生成远端地址。
    /// </summary>
    [Serializable]
    public sealed class HotfixRemoteEnvironmentConfig
    {
        [SerializeField]
        [Tooltip("当前配置对应的环境。")]
        private HotfixRemoteEnvironment environment;

        [SerializeField]
        [Tooltip("主 CDN 地址模板，支持 {Environment}/{Platform}/{Channel}/{Region}/{PackageName}。")]
        private string mainCdnUrlTemplate;

        [SerializeField]
        [Tooltip("备用 CDN 地址模板，启动时会校验它不能和主 CDN 完全相同。")]
        private string fallbackCdnUrlTemplate;

        [SerializeField]
        [Tooltip("开启后，该环境下的主/备 CDN 地址都必须使用 HTTPS。")]
        private bool requireHttps;

        [SerializeField]
        [Tooltip("允许访问的 CDN 域名白名单。留空表示不限制，运行时直接覆盖 URL 时会跳过该校验。")]
        private string[] allowedDomains = Array.Empty<string>();

        [Header("证书预留")]
        [SerializeField]
        [Tooltip("证书 pinning 预留开关，后续接入自定义下载器或证书校验时使用。")]
        private bool certificatePinningEnabled;

        [SerializeField]
        [Tooltip("证书公钥 pin 预留字段，用于记录允许的证书公钥摘要。")]
        private string certificatePublicKeyPin;

        [Header("灰度CDN预留")]
        [SerializeField]
        [Tooltip("开启后，命中灰度比例的设备会使用灰度 CDN 模板。")]
        private bool enableGrayRelease;

        [SerializeField]
        [Range(0, 100)]
        [Tooltip("灰度比例，0 表示关闭灰度，100 表示全部使用灰度 CDN。")]
        private int grayReleasePercent;

        [SerializeField]
        [Tooltip("灰度主 CDN 地址模板。留空时继续使用普通主 CDN 模板。")]
        private string grayMainCdnUrlTemplate;

        [SerializeField]
        [Tooltip("灰度备用 CDN 地址模板。留空时继续使用普通备用 CDN 模板。")]
        private string grayFallbackCdnUrlTemplate;

        [SerializeField]
        [Tooltip("灰度分流盐值。调整该值会重新洗牌灰度命中用户。")]
        private string grayReleaseSalt = "hotfix";

        public HotfixRemoteEnvironment Environment => environment;
        public string MainCdnUrlTemplate => mainCdnUrlTemplate;
        public string FallbackCdnUrlTemplate => fallbackCdnUrlTemplate;
        public bool RequireHttps => requireHttps;
        public string[] AllowedDomains => allowedDomains ?? Array.Empty<string>();
        public bool CertificatePinningEnabled => certificatePinningEnabled;
        public string CertificatePublicKeyPin => certificatePublicKeyPin;
        public bool EnableGrayRelease => enableGrayRelease;
        public int GrayReleasePercent => grayReleasePercent;
        public string GrayMainCdnUrlTemplate => grayMainCdnUrlTemplate;
        public string GrayFallbackCdnUrlTemplate => grayFallbackCdnUrlTemplate;
        public string GrayReleaseSalt => grayReleaseSalt;
    }

    [CreateAssetMenu(fileName = AssetName, menuName = "Hotfix/Remote Settings", order = 1)]
    public sealed class HotfixRemoteSettings : ScriptableObject
    {
        // Resources.Load 使用固定资源名，避免调用处散落 magic string。
        public const string AssetName = "HotfixRemoteSettings";
        public const string ResourcesPath = AssetName;

        [Header("默认环境")]
        [SerializeField]
        [Tooltip("默认远端环境。没有 PlayerPrefs 或命令行覆盖时会使用它。")]
        private HotfixRemoteEnvironment defaultEnvironment = HotfixRemoteEnvironment.Development;

        [SerializeField]
        [Tooltip("默认渠道。建议用 official/default 这类稳定值，渠道包可通过运行时覆盖修改。")]
        private string defaultChannel = "default";

        [SerializeField]
        [Tooltip("默认地区。没有地区差异时可以保持 global。")]
        private string defaultRegion = "global";

        [Header("运行时覆盖")]
        [SerializeField]
        [Tooltip("是否允许通过 PlayerPrefs 或命令行覆盖环境、渠道、地区和 CDN 地址。")]
        private bool enableRuntimeOverride = true;

        [SerializeField]
        [Tooltip("PlayerPrefs 中用于覆盖远端环境的 key。")]
        private string environmentOverrideKey = "Hotfix.Remote.Environment";

        [SerializeField]
        [Tooltip("PlayerPrefs 中用于覆盖渠道的 key。")]
        private string channelOverrideKey = "Hotfix.Remote.Channel";

        [SerializeField]
        [Tooltip("PlayerPrefs 中用于覆盖地区的 key。")]
        private string regionOverrideKey = "Hotfix.Remote.Region";

        [SerializeField]
        [Tooltip("PlayerPrefs 中用于直接覆盖主 CDN 模板的 key，适合线上应急切换。")]
        private string mainUrlOverrideKey = "Hotfix.Remote.MainUrl";

        [SerializeField]
        [Tooltip("PlayerPrefs 中用于直接覆盖备用 CDN 模板的 key，适合线上应急切换。")]
        private string fallbackUrlOverrideKey = "Hotfix.Remote.FallbackUrl";

        [SerializeField]
        [Tooltip("命令行中用于覆盖远端环境的参数名，例如 --hotfix-env=Production。")]
        private string environmentCommandLineKey = "hotfix-env";

        [SerializeField]
        [Tooltip("命令行中用于覆盖渠道的参数名，例如 --hotfix-channel=official。")]
        private string channelCommandLineKey = "hotfix-channel";

        [SerializeField]
        [Tooltip("命令行中用于覆盖地区的参数名，例如 --hotfix-region=cn。")]
        private string regionCommandLineKey = "hotfix-region";

        [SerializeField]
        [Tooltip("命令行中用于直接覆盖主 CDN 模板的参数名。")]
        private string mainUrlCommandLineKey = "hotfix-main-url";

        [SerializeField]
        [Tooltip("命令行中用于直接覆盖备用 CDN 模板的参数名。")]
        private string fallbackUrlCommandLineKey = "hotfix-fallback-url";

        [Header("环境配置")]
        [SerializeField]
        [Tooltip("各环境的远端资源地址配置。运行时会按当前环境从这里选择一项。")]
        private HotfixRemoteEnvironmentConfig[] environments = Array.Empty<HotfixRemoteEnvironmentConfig>();

        public static HotfixRemoteSettings Load()
        {
            return Resources.Load<HotfixRemoteSettings>(ResourcesPath);
        }

        public bool TryValidateForPlayerBuild(bool allowDevelopmentEnvironment, out string error)
        {
            return TryValidateForPlayerBuild(allowDevelopmentEnvironment, GetRuntimePlatformName(), out error);
        }

        public bool TryValidateForPlayerBuild(bool allowDevelopmentEnvironment, string platform, out string error)
        {
            error = string.Empty;
            string normalizedPlatform = NormalizeSelector(platform);

            var config = FindEnvironmentConfig(defaultEnvironment);
            if (config == null)
            {
                error = $"Hotfix remote environment config missing: {defaultEnvironment}";
                return false;
            }

            if (!allowDevelopmentEnvironment && defaultEnvironment == HotfixRemoteEnvironment.Development)
            {
                error = "Release player build can not use Development hotfix remote environment.";
                return false;
            }

            string mainUrl = NormalizeBaseUrl(ReplaceTokens(
                config.MainCdnUrlTemplate,
                defaultEnvironment,
                normalizedPlatform,
                NormalizeSelector(defaultChannel),
                NormalizeSelector(defaultRegion),
                "DefaultPackage"));
            string fallbackUrl = NormalizeBaseUrl(ReplaceTokens(
                config.FallbackCdnUrlTemplate,
                defaultEnvironment,
                normalizedPlatform,
                NormalizeSelector(defaultChannel),
                NormalizeSelector(defaultRegion),
                "DefaultPackage"));

            if (!ValidateUrl(config, mainUrl, "main", false, out error) ||
                !ValidateUrl(config, fallbackUrl, "fallback", false, out error))
            {
                return false;
            }

            if (string.Equals(mainUrl, fallbackUrl, StringComparison.OrdinalIgnoreCase))
            {
                error = $"Hotfix remote config error: main URL and fallback URL must not be identical. URL: {mainUrl}";
                return false;
            }

            if (!allowDevelopmentEnvironment &&
                (IsLoopbackUrl(mainUrl) || IsLoopbackUrl(fallbackUrl)))
            {
                error = $"Release player build can not use loopback hotfix CDN URL. Main: {mainUrl}, Fallback: {fallbackUrl}";
                return false;
            }

            return true;
        }

        public bool TryResolve(string packageName, out HotfixRemoteAddress address, out string error)
        {
            address = null;
            error = string.Empty;

            // 先确定环境、渠道和地区，再把地址模板解析成最终 CDN 根目录。
            var environment = ResolveEnvironment();
            var config = FindEnvironmentConfig(environment);
            if (config == null)
            {
                error = HotfixText.Get(HotfixTextKey.RemoteEnvironmentConfigMissing, environment);
                return false;
            }

            string platform = GetRuntimePlatformName();
            string channel = ResolveSelector(defaultChannel, channelOverrideKey, channelCommandLineKey);
            string region = ResolveSelector(defaultRegion, regionOverrideKey, regionCommandLineKey);
            string normalizedPackageName = string.IsNullOrWhiteSpace(packageName) ? "DefaultPackage" : packageName.Trim();

            string mainTemplate = config.MainCdnUrlTemplate;
            string fallbackTemplate = config.FallbackCdnUrlTemplate;

            // 灰度命中只替换 CDN 模板，不改变环境、渠道、地区和包名。
            if (ShouldUseGrayRelease(config, channel, region))
            {
                mainTemplate = string.IsNullOrWhiteSpace(config.GrayMainCdnUrlTemplate)
                    ? mainTemplate
                    : config.GrayMainCdnUrlTemplate;
                fallbackTemplate = string.IsNullOrWhiteSpace(config.GrayFallbackCdnUrlTemplate)
                    ? fallbackTemplate
                    : config.GrayFallbackCdnUrlTemplate;
            }

            string mainOverride = ResolveOptionalOverride(mainUrlOverrideKey, mainUrlCommandLineKey);
            string fallbackOverride = ResolveOptionalOverride(fallbackUrlOverrideKey, fallbackUrlCommandLineKey);
            bool hasMainOverride = !string.IsNullOrWhiteSpace(mainOverride);
            bool hasFallbackOverride = !string.IsNullOrWhiteSpace(fallbackOverride);

            // 直接 URL 覆盖的优先级最高，方便不改代码、不重打包地应急切换 CDN。
            if (hasMainOverride)
            {
                mainTemplate = mainOverride;
            }

            if (hasFallbackOverride)
            {
                fallbackTemplate = fallbackOverride;
            }

            string mainUrl = NormalizeBaseUrl(ReplaceTokens(mainTemplate, environment, platform, channel, region, normalizedPackageName));
            string fallbackUrl = NormalizeBaseUrl(ReplaceTokens(fallbackTemplate, environment, platform, channel, region, normalizedPackageName));

            // 配置错误尽量在启动阶段暴露，避免进入下载流程后才发现远端地址不可用。
            if (!ValidateUrl(config, mainUrl, "main", hasMainOverride, out error) ||
                !ValidateUrl(config, fallbackUrl, "fallback", hasFallbackOverride, out error))
            {
                return false;
            }

            if (string.Equals(mainUrl, fallbackUrl, StringComparison.OrdinalIgnoreCase))
            {
                error = HotfixText.Get(HotfixTextKey.RemoteMainFallbackSame, mainUrl);
                return false;
            }

            address = new HotfixRemoteAddress
            {
                Environment = environment,
                Platform = platform,
                Channel = channel,
                Region = region,
                PackageName = normalizedPackageName,
                MainUrl = mainUrl,
                FallbackUrl = fallbackUrl,
                CertificatePinningEnabled = config.CertificatePinningEnabled,
                CertificatePublicKeyPin = config.CertificatePublicKeyPin
            };
            return true;
        }

        private HotfixRemoteEnvironment ResolveEnvironment()
        {
            if (!enableRuntimeOverride)
            {
                return defaultEnvironment;
            }

            string environmentName = GetCommandLineValue(environmentCommandLineKey);
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = PlayerPrefs.GetString(environmentOverrideKey, string.Empty);
            }

            return Enum.TryParse(environmentName, true, out HotfixRemoteEnvironment environment)
                ? environment
                : defaultEnvironment;
        }

        private string ResolveSelector(string defaultValue, string playerPrefsKey, string commandLineKey)
        {
            string value = enableRuntimeOverride ? GetCommandLineValue(commandLineKey) : string.Empty;
            if (enableRuntimeOverride && string.IsNullOrWhiteSpace(value))
            {
                value = PlayerPrefs.GetString(playerPrefsKey, string.Empty);
            }

            return string.IsNullOrWhiteSpace(value) ? NormalizeSelector(defaultValue) : NormalizeSelector(value);
        }

        private string ResolveOptionalOverride(string playerPrefsKey, string commandLineKey)
        {
            if (!enableRuntimeOverride)
            {
                return string.Empty;
            }

            string value = GetCommandLineValue(commandLineKey);
            if (string.IsNullOrWhiteSpace(value))
            {
                value = PlayerPrefs.GetString(playerPrefsKey, string.Empty);
            }

            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
        }

        private HotfixRemoteEnvironmentConfig FindEnvironmentConfig(HotfixRemoteEnvironment environment)
        {
            foreach (var config in environments)
            {
                if (config != null && config.Environment == environment)
                {
                    return config;
                }
            }

            return null;
        }

        private static string ReplaceTokens(
            string template,
            HotfixRemoteEnvironment environment,
            string platform,
            string channel,
            string region,
            string packageName)
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                return string.Empty;
            }

            return template
                .Replace("{Environment}", environment.ToString())
                .Replace("{Platform}", platform)
                .Replace("{Channel}", channel)
                .Replace("{Region}", region)
                .Replace("{PackageName}", packageName);
        }

        private static string NormalizeBaseUrl(string url)
        {
            return string.IsNullOrWhiteSpace(url) ? string.Empty : url.Trim().TrimEnd('/');
        }

        private static string NormalizeSelector(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "default" : value.Trim();
        }

        private static bool ValidateUrl(
            HotfixRemoteEnvironmentConfig config,
            string url,
            string label,
            bool skipAllowedDomainCheck,
            out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(url))
            {
                error = HotfixText.Get(HotfixTextKey.RemoteUrlEmpty, label);
                return false;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                error = HotfixText.Get(HotfixTextKey.RemoteUrlInvalid, label, url);
                return false;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                error = HotfixText.Get(HotfixTextKey.RemoteUrlSchemeUnsupported, label, url);
                return false;
            }

            if (config.RequireHttps && uri.Scheme != Uri.UriSchemeHttps)
            {
                error = HotfixText.Get(HotfixTextKey.RemoteUrlRequiresHttps, label, url);
                return false;
            }

            if (!skipAllowedDomainCheck && !IsAllowedDomain(config.AllowedDomains, uri.Host))
            {
                error = HotfixText.Get(HotfixTextKey.RemoteUrlDomainNotAllowed, label, uri.Host);
                return false;
            }

            return true;
        }

        private static bool IsAllowedDomain(string[] allowedDomains, string host)
        {
            if (allowedDomains == null || allowedDomains.Length == 0)
            {
                return true;
            }

            foreach (var allowedDomain in allowedDomains)
            {
                if (string.IsNullOrWhiteSpace(allowedDomain))
                {
                    continue;
                }

                if (string.Equals(host, allowedDomain.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsLoopbackUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uri) && uri.IsLoopback;
        }

        private static bool ShouldUseGrayRelease(HotfixRemoteEnvironmentConfig config, string channel, string region)
        {
            if (!config.EnableGrayRelease || config.GrayReleasePercent <= 0)
            {
                return false;
            }

            if (config.GrayReleasePercent >= 100)
            {
                return true;
            }

            string seed = $"{SystemInfo.deviceUniqueIdentifier}:{channel}:{region}:{config.GrayReleaseSalt}";
            return ComputeStableHash(seed) % 100 < config.GrayReleasePercent;
        }

        private static uint ComputeStableHash(string value)
        {
            // 不使用 string.GetHashCode()，因为不同运行时实现可能导致同一设备灰度命中结果漂移。
            unchecked
            {
                const uint offsetBasis = 2166136261;
                const uint prime = 16777619;
                uint hash = offsetBasis;
                for (int i = 0; i < value.Length; i++)
                {
                    hash ^= value[i];
                    hash *= prime;
                }

                return hash;
            }
        }

        private static string GetRuntimePlatformName()
        {
#if UNITY_ANDROID
            return "Android";
#elif UNITY_IOS
            return "iOS";
#elif UNITY_WEBGL
            return "WebGL";
#elif UNITY_STANDALONE_WIN
            return "Windows";
#elif UNITY_STANDALONE_OSX
            return "macOS";
#elif UNITY_STANDALONE_LINUX
            return "Linux";
#else
            return Application.platform.ToString();
#endif
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
}
