using System;
using System.IO;
using System.Linq;
using Framework;
using Framework.Assemblies;
using UnityEditor;
using UnityEngine;
using YooAsset;

namespace HybridCLR.Editor
{
    [CreateAssetMenu(fileName = "HotfixReleaseProfile", menuName = "Hotfix/Release Profile", order = 10)]
    public sealed class HotfixReleaseProfile : ScriptableObject
    {
        public const string DefaultAssetPath = "Assets/Editor/HybridCLR/HotfixReleaseProfile.asset";
        private const string SelectedProfileKeySuffix = ".HotfixBuildCenter.ReleaseProfile";

        [Header("发布标识")]
        [Tooltip("本次发布目标平台，需要和 Unity 当前切换的 BuildTarget 一致。不同平台的 DLL、AOT metadata 和 AssetBundle 不能混用。")]
        public BuildTarget BuildTarget = BuildTarget.StandaloneWindows64;

        [Tooltip("App 包体版本，会同步写入 PlayerSettings.bundleVersion。")]
        public string AppVersion = "1.0.0";

        [Tooltip("本次热更资源允许覆盖的最低 App 版本。热更包会写入 HotfixAssemblyManifest，用于阻断不兼容旧包。")]
        public string AppVersionMin = "1.0.0";

        [Tooltip("本次热更资源允许覆盖的最高 App 版本。通常和 AppVersion 一致，跨小版本兼容时再放宽。")]
        public string AppVersionMax = "1.0.0";

        [Tooltip("YooAsset PackageVersion。正式发布建议填写明确版本号；留空时构建流程会使用时间戳自动生成。")]
        public string ResourceVersion = string.Empty;

        [Tooltip("热更 DLL 版本号。通常留空由 DLL hash 自动生成；只有接入外部版本协议时才手动固定。")]
        public string HotfixVersion = string.Empty;

        [Tooltip("正式发布单调递增序号。每次发布新 ResourceVersion 必须递增，用于阻止合法旧签名包回滚。")]
        public long ReleaseSequence;

        [Header("Manifest 发布签名")]
        [Tooltip("包体可信 RSA 公钥的标识。正式发布必须填写；轮换密钥时请使用新 KeyId。")]
        public string ManifestSigningKeyId = string.Empty;

        [Tooltip("RSA 公钥 Modulus，Base64 编码。正式发布必须使用至少 2048 位密钥。")]
        [TextArea(2, 6)]
        public string ManifestPublicKeyModulus = string.Empty;

        [Tooltip("RSA 公钥 Exponent，Base64 编码。常见值为 AQAB。")]
        public string ManifestPublicKeyExponent = "AQAB";

        [Tooltip("保存私钥 XML 内容或仓库外私钥 XML 路径的环境变量名。私钥不会写入 Asset。")]
        public string ManifestPrivateKeyEnvironmentVariable = "HOTFIX_MANIFEST_PRIVATE_KEY";

        [Header("远端资源")]
        [Tooltip("发布使用的远端资源环境。正式发布应选择 Production。")]
        public HotfixRemoteEnvironment RemoteEnvironment = HotfixRemoteEnvironment.Development;

        [Tooltip("渠道标识，会替换 CDN 模板中的 {Channel}。没有渠道差异时保持 default。")]
        public string Channel = "default";

        [Tooltip("地区标识，会替换 CDN 模板中的 {Region}。没有地区差异时保持 global。")]
        public string Region = "global";

        [Tooltip("是否允许 Development CDN。正式发布应关闭；Production 环境会始终视为正式发布。")]
        public bool AllowDevelopmentCdn = true;

        [Tooltip("主 CDN 地址模板，支持 {Environment}/{Platform}/{Channel}/{Region}/{PackageName}。")]
        public string MainCdnUrlTemplate = string.Empty;

        [Tooltip("备用 CDN 地址模板，主 CDN 失败时使用；不能和主 CDN 完全相同。")]
        public string FallbackCdnUrlTemplate = string.Empty;

        [Tooltip("开启后，主/备 CDN 都必须使用 HTTPS。正式环境建议开启。")]
        public bool RequireHttps;

        [Tooltip("允许访问的 CDN 域名白名单。留空表示不限制；正式环境建议填写。")]
        public string[] AllowedDomains = Array.Empty<string>();

        [Tooltip("证书 pinning 预留开关，后续接入自定义下载器或证书校验时使用。")]
        public bool CertificatePinningEnabled;

        [Tooltip("证书公钥 pin 预留字段，用于记录允许的证书公钥摘要。")]
        public string CertificatePublicKeyPin = string.Empty;

        [Tooltip("是否启用灰度 CDN。开启后，命中灰度比例的设备会使用灰度 CDN 模板。")]
        public bool EnableGrayRelease;

        [Range(0, 100)]
        [Tooltip("灰度 CDN 命中比例，0 表示关闭灰度，100 表示全部使用灰度 CDN。")]
        public int GrayReleasePercent;

        [Tooltip("灰度主 CDN 地址模板。留空时继续使用普通主 CDN 模板。")]
        public string GrayMainCdnUrlTemplate = string.Empty;

        [Tooltip("灰度备用 CDN 地址模板。留空时继续使用普通备用 CDN 模板。")]
        public string GrayFallbackCdnUrlTemplate = string.Empty;

        [Tooltip("灰度分流盐值。调整该值会重新洗牌灰度命中用户。")]
        public string GrayReleaseSalt = "hotfix";

        [Header("启动策略")]
        [Tooltip("Player 下使用的 YooAsset 运行模式。FirstPackage/EmptyPackage 常用 HostPlayMode，WebGL 使用 WebPlayMode，OfflinePackage 使用 OfflinePlayMode。")]
        public EPlayMode PlayerPlayMode = EPlayMode.HostPlayMode;

        [Tooltip("启动包策略：FirstPackage 内置启动资源，EmptyPackage 首次启动拉远端，OfflinePackage 完整离线。")]
        public StartupPackageMode StartupPackageMode = StartupPackageMode.FirstPackage;

        [Tooltip("启动阶段下载模式：DownloadAll 下载全部差异资源，DownloadByTags 只下载指定 Tag，Skip 跳过启动下载。")]
        public StartupDownloadMode StartupDownloadMode = StartupDownloadMode.DownloadAll;

        [Tooltip("启动阶段远端失败时的处理策略，例如必须更新、允许缓存、Wi-Fi 优先或后台下载。")]
        public StartupUpdatePolicy StartupUpdatePolicy = StartupUpdatePolicy.AllowCached;

        [Tooltip("启动阶段按 Tag 下载时使用的 Tag 列表。StartupDownloadMode=DownloadByTags 时必须包含 startup。")]
        public string[] StartupDownloadTags = Array.Empty<string>();

        [Header("热更入口")]
        [Tooltip("实现 Framework.IHotfixEntry 且带公共无参构造函数的类型完整名。构建会写入 HotfixAssemblyManifest。")]
        public string EntryTypeName = "HotfixDemo.HotfixCodeEntry";

        [HideInInspector]
        public string EntryMethodName = string.Empty;

        public string DisplayName => string.IsNullOrWhiteSpace(name) ? "未命名 ReleaseProfile" : name;

        public bool AllowsDevelopmentCdnForBuild =>
            AllowDevelopmentCdn && RemoteEnvironment != HotfixRemoteEnvironment.Production;

        public bool IsFormalRelease => RemoteEnvironment == HotfixRemoteEnvironment.Production;

        public bool IsCompatibleWith(BuildTarget target)
        {
            return BuildTarget == target;
        }

        public void ApplyToEditorSettings()
        {
            EnsureEditorDefaults();

            if (!string.IsNullOrWhiteSpace(AppVersion))
            {
                PlayerSettings.bundleVersion = AppVersion.Trim();
            }

            var runtimeSettings = AssetDatabase.LoadAssetAtPath<HotfixRuntimeSettings>(HotfixBuildProfileUtility.RuntimeSettingsAssetPath);
            if (runtimeSettings != null)
            {
                runtimeSettings.SetPlayerPlayModeForEditor(PlayerPlayMode);
                runtimeSettings.SetStartupSettingsForEditor(
                    StartupPackageMode,
                    StartupDownloadMode,
                    StartupUpdatePolicy,
                    StartupDownloadTags);
                runtimeSettings.SetManifestTrustForEditor(
                    IsFormalRelease,
                    ManifestSigningKeyId,
                    ManifestPublicKeyModulus,
                    ManifestPublicKeyExponent);
                EditorUtility.SetDirty(runtimeSettings);
                AssetDatabase.SaveAssetIfDirty(runtimeSettings);
            }

            var remoteSettings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(HotfixBuildProfileUtility.RemoteSettingsAssetPath);
            if (remoteSettings != null)
            {
                FillMissingRemoteEnvironmentConfig(remoteSettings);
                remoteSettings.SetDefaultSelectorForEditor(RemoteEnvironment, Channel, Region);
                remoteSettings.SetRuntimeOverrideForEditor(RemoteEnvironment != HotfixRemoteEnvironment.Production);
                remoteSettings.SetEnvironmentConfigForEditor(
                    RemoteEnvironment,
                    MainCdnUrlTemplate,
                    FallbackCdnUrlTemplate,
                    RequireHttps,
                    AllowedDomains,
                    CertificatePinningEnabled,
                    CertificatePublicKeyPin,
                    EnableGrayRelease,
                    GrayReleasePercent,
                    GrayMainCdnUrlTemplate,
                    GrayFallbackCdnUrlTemplate,
                    GrayReleaseSalt);
                EditorUtility.SetDirty(remoteSettings);
                AssetDatabase.SaveAssetIfDirty(remoteSettings);
            }

            ApplyPlayerPlayModeToBuildProfile();

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

            var buildProfile = AssetDatabase.LoadAssetAtPath<HotfixBuildProfile>(HotfixBuildProfile.AssetPath);
            if (buildProfile != null)
            {
                PlayerPlayMode = buildProfile.GetPlayMode(BuildTarget);
            }

            var remoteSettings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(HotfixBuildProfileUtility.RemoteSettingsAssetPath);
            if (remoteSettings != null)
            {
                RemoteEnvironment = remoteSettings.DefaultEnvironment;
                Channel = remoteSettings.DefaultChannel;
                Region = remoteSettings.DefaultRegion;
                CaptureRemoteEnvironmentConfig(remoteSettings);
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
                EntryMethodName = string.Empty;
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

            manifest.EntryMethodName = string.Empty;
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

            if (IsFormalRelease)
            {
                if (RemoteEnvironment != HotfixRemoteEnvironment.Production)
                {
                    error = "Formal ReleaseProfile must use the Production remote environment.";
                    return false;
                }

                if (!RequireHttps)
                {
                    error = "Formal ReleaseProfile must require HTTPS.";
                    return false;
                }

                if (AllowedDomains == null ||
                    !AllowedDomains.Any(domain => !string.IsNullOrWhiteSpace(domain)) ||
                    AllowedDomains.Any(IsReservedExampleDomain))
                {
                    error = "Formal ReleaseProfile must configure a non-empty real CDN domain allowlist; RFC example domains are not allowed.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(ResourceVersion))
                {
                    error = "Formal ReleaseProfile must set an explicit ResourceVersion so the signed Manifest is bound to a stable PackageVersion.";
                    return false;
                }

                if (ReleaseSequence <= 0)
                {
                    error = "Formal ReleaseProfile ReleaseSequence must be greater than zero and increase for every release.";
                    return false;
                }

                if (!HotfixManifestSigningUtility.TryValidateSigningConfiguration(this, out error))
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(MainCdnUrlTemplate) ||
                string.IsNullOrWhiteSpace(FallbackCdnUrlTemplate))
            {
                error = "ReleaseProfile CDN templates can not be empty.";
                return false;
            }

            if (string.Equals(
                    NormalizeBaseUrl(MainCdnUrlTemplate),
                    NormalizeBaseUrl(FallbackCdnUrlTemplate),
                    StringComparison.OrdinalIgnoreCase))
            {
                error = "ReleaseProfile main CDN template and fallback CDN template must not be identical.";
                return false;
            }

            if (!ValidatePlayerPlayMode(PlayerPlayMode, context.BuildTarget, out error))
            {
                return false;
            }

            if (StartupDownloadMode == StartupDownloadMode.DownloadByTags &&
                !ContainsTag(StartupDownloadTags, HotfixRuntimeSettings.DefaultStartupTag))
            {
                error = $"ReleaseProfile DownloadByTags requires '{HotfixRuntimeSettings.DefaultStartupTag}' tag.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(EntryTypeName))
            {
                error = "ReleaseProfile IHotfixEntry type is required.";
                return false;
            }

            if (context.RemoteSettings != null &&
                !ValidateRemoteSettingsForStartupMode(context, out error))
            {
                return false;
            }

            return true;
        }

        private bool ValidateRemoteSettingsForStartupMode(HotfixBuildContext context, out string error)
        {
            if (!context.RemoteSettings.TryValidateForPlayerBuild(
                    AllowsDevelopmentCdnForBuild,
                    context.BuildTargetName,
                    RemoteEnvironment,
                    Channel,
                    Region,
                    out error))
            {
                return false;
            }

            return StartupPackageMode != StartupPackageMode.EmptyPackage ||
                   context.RemoteSettings.TryValidateForEmptyPackageBuild(
                       AllowsDevelopmentCdnForBuild,
                       context.BuildTargetName,
                       RemoteEnvironment,
                       Channel,
                       Region,
                       out error);
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
                profile.EnsureEditorDefaults();
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

        private void OnValidate()
        {
            EnsureEditorDefaults();
        }

        private void EnsureEditorDefaults()
        {
            if (string.IsNullOrWhiteSpace(Channel))
            {
                Channel = "default";
            }

            if (string.IsNullOrWhiteSpace(Region))
            {
                Region = "global";
            }

            if (AllowedDomains == null)
            {
                AllowedDomains = Array.Empty<string>();
            }

            if (StartupDownloadTags == null)
            {
                StartupDownloadTags = Array.Empty<string>();
            }

            GrayReleasePercent = Mathf.Clamp(GrayReleasePercent, 0, 100);
            if (string.IsNullOrWhiteSpace(GrayReleaseSalt))
            {
                GrayReleaseSalt = "hotfix";
            }

            if (string.IsNullOrWhiteSpace(ManifestPublicKeyExponent))
            {
                ManifestPublicKeyExponent = "AQAB";
            }

            if (string.IsNullOrWhiteSpace(ManifestPrivateKeyEnvironmentVariable))
            {
                ManifestPrivateKeyEnvironmentVariable = "HOTFIX_MANIFEST_PRIVATE_KEY";
            }
        }

        private void ApplyPlayerPlayModeToBuildProfile()
        {
            var buildProfile = AssetDatabase.LoadAssetAtPath<HotfixBuildProfile>(HotfixBuildProfile.AssetPath);
            if (buildProfile == null)
            {
                string directory = Path.GetDirectoryName(HotfixBuildProfile.AssetPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                buildProfile = CreateInstance<HotfixBuildProfile>();
                AssetDatabase.CreateAsset(buildProfile, HotfixBuildProfile.AssetPath);
            }

            buildProfile.SetPlayModeForEditor(BuildTarget, PlayerPlayMode);
            EditorUtility.SetDirty(buildProfile);
            AssetDatabase.SaveAssetIfDirty(buildProfile);
        }

        private void CaptureRemoteEnvironmentConfig(HotfixRemoteSettings remoteSettings)
        {
            if (remoteSettings == null ||
                !remoteSettings.TryGetEnvironmentConfigForEditor(
                    RemoteEnvironment,
                    out var mainCdnUrlTemplate,
                    out var fallbackCdnUrlTemplate,
                    out var requireHttps,
                    out var allowedDomains,
                    out var certificatePinningEnabled,
                    out var certificatePublicKeyPin,
                    out var enableGrayRelease,
                    out var grayReleasePercent,
                    out var grayMainCdnUrlTemplate,
                    out var grayFallbackCdnUrlTemplate,
                    out var grayReleaseSalt))
            {
                return;
            }

            MainCdnUrlTemplate = mainCdnUrlTemplate;
            FallbackCdnUrlTemplate = fallbackCdnUrlTemplate;
            RequireHttps = requireHttps;
            AllowedDomains = CloneArray(allowedDomains);
            CertificatePinningEnabled = certificatePinningEnabled;
            CertificatePublicKeyPin = certificatePublicKeyPin;
            EnableGrayRelease = enableGrayRelease;
            GrayReleasePercent = grayReleasePercent;
            GrayMainCdnUrlTemplate = grayMainCdnUrlTemplate;
            GrayFallbackCdnUrlTemplate = grayFallbackCdnUrlTemplate;
            GrayReleaseSalt = grayReleaseSalt;
        }

        private void FillMissingRemoteEnvironmentConfig(HotfixRemoteSettings remoteSettings)
        {
            if (remoteSettings == null ||
                !remoteSettings.TryGetEnvironmentConfigForEditor(
                    RemoteEnvironment,
                    out var mainCdnUrlTemplate,
                    out var fallbackCdnUrlTemplate,
                    out var requireHttps,
                    out var allowedDomains,
                    out var certificatePinningEnabled,
                    out var certificatePublicKeyPin,
                    out var enableGrayRelease,
                    out var grayReleasePercent,
                    out var grayMainCdnUrlTemplate,
                    out var grayFallbackCdnUrlTemplate,
                    out var grayReleaseSalt))
            {
                return;
            }

            bool missingMain = string.IsNullOrWhiteSpace(MainCdnUrlTemplate);
            bool missingFallback = string.IsNullOrWhiteSpace(FallbackCdnUrlTemplate);
            if (missingMain)
            {
                MainCdnUrlTemplate = mainCdnUrlTemplate;
            }

            if (missingFallback)
            {
                FallbackCdnUrlTemplate = fallbackCdnUrlTemplate;
            }

            if (AllowedDomains == null || AllowedDomains.Length == 0)
            {
                AllowedDomains = CloneArray(allowedDomains);
            }

            if (string.IsNullOrWhiteSpace(CertificatePublicKeyPin))
            {
                CertificatePublicKeyPin = certificatePublicKeyPin;
            }

            if (string.IsNullOrWhiteSpace(GrayMainCdnUrlTemplate))
            {
                GrayMainCdnUrlTemplate = grayMainCdnUrlTemplate;
            }

            if (string.IsNullOrWhiteSpace(GrayFallbackCdnUrlTemplate))
            {
                GrayFallbackCdnUrlTemplate = grayFallbackCdnUrlTemplate;
            }

            if (string.IsNullOrWhiteSpace(GrayReleaseSalt))
            {
                GrayReleaseSalt = grayReleaseSalt;
            }

            if (!missingMain || !missingFallback)
            {
                return;
            }

            RequireHttps = requireHttps;
            CertificatePinningEnabled = certificatePinningEnabled;
            EnableGrayRelease = enableGrayRelease;
            GrayReleasePercent = grayReleasePercent;
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

        private static bool IsReservedExampleDomain(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string host = value.Trim().TrimEnd('.');
            return IsDomainOrSubdomain(host, "example.com") ||
                   IsDomainOrSubdomain(host, "example.net") ||
                   IsDomainOrSubdomain(host, "example.org");
        }

        private static bool IsDomainOrSubdomain(string host, string domain)
        {
            return string.Equals(host, domain, StringComparison.OrdinalIgnoreCase) ||
                   host.EndsWith("." + domain, StringComparison.OrdinalIgnoreCase);
        }

        private static string[] CloneArray(string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return Array.Empty<string>();
            }

            var result = new string[values.Length];
            Array.Copy(values, result, values.Length);
            return result;
        }

        private static bool ValidatePlayerPlayMode(EPlayMode playMode, BuildTarget target, out string error)
        {
            error = string.Empty;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                error = $"ReleaseProfile PlayerPlayMode can not be {playMode} for player build.";
                return false;
            }

            if (target == BuildTarget.WebGL && playMode != EPlayMode.WebPlayMode)
            {
                error = $"ReleaseProfile PlayerPlayMode for WebGL must be {EPlayMode.WebPlayMode}. Current={playMode}.";
                return false;
            }

            if (target != BuildTarget.WebGL && playMode == EPlayMode.WebPlayMode)
            {
                error = $"ReleaseProfile PlayerPlayMode {EPlayMode.WebPlayMode} only supports WebGL. Current target={target}.";
                return false;
            }

            return true;
        }

        private static string NormalizeBaseUrl(string url)
        {
            return string.IsNullOrWhiteSpace(url) ? string.Empty : url.Trim().TrimEnd('/');
        }
    }
}
