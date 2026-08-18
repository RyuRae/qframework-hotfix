using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Framework.Localization.Generated;
using Luban;
using TMPro;
using UnityEngine;
using YooAsset;

namespace Framework.Localization
{
    /// <summary>首包和热更业务共享的唯一多语言服务。</summary>
    public sealed class LocalizationService
    {
        public const string DefaultLocale = "en";
        public const string PreferenceKey = "Localization.Locale";
        public const string BootstrapResourcePath = "Localization/bootstrap";
        public const string CatalogAddress = "tblanguagecatalog";
        public const string AliasAddress = "tblanguagealias";
        public const string FontGroupAddress = "tbfontgroup";
        public const string LocalizedAssetAddress = "tblocalizedasset";

        private Dictionary<string, string> bootstrapTexts = new Dictionary<string, string>(StringComparer.Ordinal);
        private Dictionary<string, string> runtimeTexts = new Dictionary<string, string>(StringComparer.Ordinal);
        private Dictionary<string, LanguageCatalogRow> catalog = new Dictionary<string, LanguageCatalogRow>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, FontGroupRow> fontGroups = new Dictionary<string, FontGroupRow>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> localizedAssets = new Dictionary<string, string>(StringComparer.Ordinal);
        private ResourcePackage runtimePackage;
        private AssetHandle activeFontHandle;
        private int localeRequestVersion;

        public static LocalizationService Instance { get; } = new LocalizationService();

        public string RequestedLocale { get; private set; } = DefaultLocale;
        public string ActiveLocale { get; private set; } = DefaultLocale;
        public bool IsBootstrapInitialized { get; private set; }
        public bool HasRuntimeCatalog { get; private set; }
        public TMP_FontAsset ActiveFont { get; private set; }
        public bool IsChangingLocale { get; private set; }
        public string LastChangeError { get; private set; } = string.Empty;
        public event Action<string> LocaleChanged;
        public event Action<float> LocaleDownloadProgress;

        private LocalizationService() { }

        /// <summary>同步初始化 Resources 兜底；此方法不依赖 YooAsset。</summary>
        public void InitializeBootstrap()
        {
            RequestedLocale = NormalizeLocale(PlayerPrefs.GetString(PreferenceKey, ResolveSystemLocale()));
            var asset = Resources.Load<TextAsset>(BootstrapResourcePath);
            if (asset != null)
            {
                try
                {
                    bootstrapTexts = BuildTextLookup(new TbBootstrapText(new ByteBuf(asset.bytes)).DataList);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"[Localization] Bootstrap parse failed: {exception}");
                }
            }

            ActiveLocale = ResolveBootstrapLocale(RequestedLocale);
            IsBootstrapInitialized = true;
        }

        /// <summary>Manifest 可用后加载动态 Catalog、Alias 和业务文本，并原子提交。</summary>
        public IEnumerator UpgradeFromPackage(ResourcePackage package, Action<bool, string> completed = null)
        {
            if (package == null)
            {
                completed?.Invoke(false, "Localization package is null.");
                yield break;
            }

            AssetHandle catalogHandle = null;
            AssetHandle aliasHandle = null;
            AssetHandle fontGroupHandle = null;
            AssetHandle localizedAssetHandle = null;
            try
            {
                catalogHandle = package.LoadAssetAsync<TextAsset>(CatalogAddress);
                aliasHandle = package.LoadAssetAsync<TextAsset>(AliasAddress);
                fontGroupHandle = package.LoadAssetAsync<TextAsset>(FontGroupAddress);
                localizedAssetHandle = package.LoadAssetAsync<TextAsset>(LocalizedAssetAddress);
                yield return catalogHandle;
                yield return aliasHandle;
                yield return fontGroupHandle;
                yield return localizedAssetHandle;

                if (catalogHandle.Status != EOperationStatus.Succeed ||
                    aliasHandle.Status != EOperationStatus.Succeed ||
                    fontGroupHandle.Status != EOperationStatus.Succeed ||
                    localizedAssetHandle.Status != EOperationStatus.Succeed)
                {
                    completed?.Invoke(false, $"Catalog={catalogHandle.LastError}; Alias={aliasHandle.LastError}; Font={fontGroupHandle.LastError}; Asset={localizedAssetHandle.LastError}");
                    yield break;
                }

                bool parsed = false;
                string parseError = string.Empty;
                try
                {
                    var nextCatalogTable = new TbLanguageCatalog(new ByteBuf(((TextAsset)catalogHandle.AssetObject).bytes));
                    var nextAliasTable = new TbLanguageAlias(new ByteBuf(((TextAsset)aliasHandle.AssetObject).bytes));
                    var nextFontTable = new TbFontGroup(new ByteBuf(((TextAsset)fontGroupHandle.AssetObject).bytes));
                    var nextAssetTable = new TbLocalizedAsset(new ByteBuf(((TextAsset)localizedAssetHandle.AssetObject).bytes));
                    var nextCatalog = BuildCatalog(nextCatalogTable.DataList);
                    var nextAliases = BuildAliases(nextAliasTable.DataList);
                    ValidateCatalog(nextCatalog);
                    var nextFonts = BuildFontGroups(nextFontTable.DataList);
                    var nextAssets = BuildLocalizedAssets(nextAssetTable.DataList);
                    string nextLocale = ResolveRuntimeLocale(RequestedLocale, nextCatalog, nextAliases);

                    catalog = nextCatalog;
                    aliases = nextAliases;
                    fontGroups = nextFonts;
                    localizedAssets = nextAssets;
                    runtimePackage = package;
                    HasRuntimeCatalog = true;
                    parsed = true;
                }
                catch (Exception exception)
                {
                    parseError = exception.Message;
                }

                if (!parsed)
                {
                    completed?.Invoke(false, parseError);
                    yield break;
                }

                bool activated = false;
                string activationError = string.Empty;
                yield return ChangeLocale(RequestedLocale, (result, error) => { activated = result; activationError = error; });
                completed?.Invoke(activated, activationError);
            }
            finally
            {
                catalogHandle?.Release();
                aliasHandle?.Release();
                fontGroupHandle?.Release();
                localizedAssetHandle?.Release();
            }
        }

        public string Get(string key, params object[] args)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            string template = FindText(runtimeTexts, key, ActiveLocale) ??
                              FindText(bootstrapTexts, key, ActiveLocale) ??
                              FindFallbackText(key) ?? key;
            if (args == null || args.Length == 0) return template;
            try { return string.Format(CultureInfo.InvariantCulture, template, args); }
            catch (FormatException) { return template; }
        }

        public bool TryRequestLocale(string locale)
        {
            string normalized = NormalizeLocale(locale);
            // Catalog 可用后必须走异步事务，避免只改 Locale 却继续使用旧语言文本/字体。
            if (HasRuntimeCatalog) return false;
            RequestedLocale = normalized;
            PlayerPrefs.SetString(PreferenceKey, normalized);
            CommitActiveLocale(ResolveBootstrapLocale(normalized));
            return true;
        }

        /// <summary>下载并准备目标语言快照，全部成功后才提交语言和字体。</summary>
        public IEnumerator ChangeLocale(string locale, Action<bool, string> completed = null)
        {
            int requestVersion = ++localeRequestVersion;
            string normalized = NormalizeLocale(locale);
            if (!HasRuntimeCatalog || runtimePackage == null)
            {
                bool accepted = TryRequestLocale(normalized);
                completed?.Invoke(accepted, accepted ? string.Empty : $"Unsupported locale: {normalized}");
                yield break;
            }

            string targetLocale = ResolveRuntimeLocale(normalized, catalog, aliases);
            if (!catalog.TryGetValue(targetLocale, out var language) || !language.Enabled)
            {
                completed?.Invoke(false, $"Unsupported locale: {normalized}");
                yield break;
            }

            IsChangingLocale = true;
            LastChangeError = string.Empty;
            AssetHandle nextFontHandle = null;
            AssetHandle nextTextHandle = null;
            try
            {
                var locations = new List<string>();
                if (!string.IsNullOrWhiteSpace(language.TextTableAddress)) locations.Add(language.TextTableAddress);
                if (fontGroups.TryGetValue(language.FontGroup, out var fontInfo) && !string.IsNullOrWhiteSpace(fontInfo.PrimaryFontAddress))
                    locations.Add(fontInfo.PrimaryFontAddress);

                if (locations.Count > 0)
                {
                    var downloader = runtimePackage.CreateBundleDownloader(locations.ToArray(), 4, 2);
                    if (downloader.TotalDownloadCount > 0)
                    {
                        downloader.DownloadUpdateCallback = data => LocaleDownloadProgress?.Invoke(data.Progress);
                        downloader.BeginDownload();
                        yield return downloader;
                        if (downloader.Status != EOperationStatus.Succeed)
                        {
                            LastChangeError = downloader.Error;
                            completed?.Invoke(false, LastChangeError);
                            yield break;
                        }
                    }
                }

                TMP_FontAsset nextFont = null;
                nextTextHandle = runtimePackage.LoadAssetAsync<TextAsset>(language.TextTableAddress);
                yield return nextTextHandle;
                if (nextTextHandle.Status != EOperationStatus.Succeed)
                {
                    LastChangeError = nextTextHandle.LastError;
                    completed?.Invoke(false, LastChangeError);
                    yield break;
                }
                Dictionary<string, string> nextTexts;
                try
                {
                    var table = new TbLocaleText(new ByteBuf(((TextAsset)nextTextHandle.AssetObject).bytes));
                    nextTexts = BuildTextLookup(table.DataList);
                    if (!HasLocale(nextTexts, targetLocale))
                        throw new InvalidOperationException($"Locale text asset '{language.TextTableAddress}' does not contain '{targetLocale}'.");
                }
                catch (Exception exception)
                {
                    LastChangeError = exception.Message;
                    completed?.Invoke(false, LastChangeError);
                    yield break;
                }

                if (fontInfo != null && !string.IsNullOrWhiteSpace(fontInfo.PrimaryFontAddress))
                {
                    nextFontHandle = runtimePackage.LoadAssetAsync<TMP_FontAsset>(fontInfo.PrimaryFontAddress);
                    yield return nextFontHandle;
                    if (nextFontHandle.Status != EOperationStatus.Succeed)
                    {
                        LastChangeError = nextFontHandle.LastError;
                        completed?.Invoke(false, LastChangeError);
                        yield break;
                    }
                    nextFont = nextFontHandle.AssetObject as TMP_FontAsset;
                }

                if (requestVersion != localeRequestVersion)
                {
                    completed?.Invoke(false, "Locale request was superseded.");
                    yield break;
                }

                var previousFontHandle = activeFontHandle;
                activeFontHandle = nextFontHandle;
                nextFontHandle = null;
                ActiveFont = nextFont;
                runtimeTexts = nextTexts;
                RequestedLocale = normalized;
                PlayerPrefs.SetString(PreferenceKey, normalized);
                CommitActiveLocale(targetLocale, true);
                previousFontHandle?.Release();
                LocaleDownloadProgress?.Invoke(1f);
                completed?.Invoke(true, string.Empty);
            }
            finally
            {
                nextFontHandle?.Release();
                nextTextHandle?.Release();
                if (requestVersion == localeRequestVersion) IsChangingLocale = false;
            }
        }

        public bool TryGetLocalizedAssetAddress(string key, string assetType, out string address)
        {
            address = null;
            if (string.IsNullOrWhiteSpace(key)) return false;
            string locale = ActiveLocale;
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            while (visited.Add(locale))
            {
                if (localizedAssets.TryGetValue(ComposeAssetId(key, locale, assetType), out address) && !string.IsNullOrWhiteSpace(address)) return true;
                if (!catalog.TryGetValue(locale, out var info) || string.IsNullOrWhiteSpace(info.FallbackLocale)) break;
                locale = NormalizeLocale(info.FallbackLocale);
            }
            return localizedAssets.TryGetValue(ComposeAssetId(key, DefaultLocale, assetType), out address) && !string.IsNullOrWhiteSpace(address);
        }

        public ResourcePackage RuntimePackage => runtimePackage;

        private string FindFallbackText(string key)
        {
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string locale = ActiveLocale;
            while (catalog.TryGetValue(locale, out var info) && !string.IsNullOrWhiteSpace(info.FallbackLocale) && visited.Add(locale))
            {
                locale = NormalizeLocale(info.FallbackLocale);
                string value = FindText(runtimeTexts, key, locale) ?? FindText(bootstrapTexts, key, locale);
                if (!string.IsNullOrEmpty(value)) return value;
            }
            return FindText(runtimeTexts, key, DefaultLocale) ?? FindText(bootstrapTexts, key, DefaultLocale);
        }

        private void CommitActiveLocale(string locale, bool forceNotify = false)
        {
            locale = NormalizeLocale(locale);
            bool changed = !string.Equals(ActiveLocale, locale, StringComparison.OrdinalIgnoreCase);
            ActiveLocale = locale;
            if (changed || forceNotify) LocaleChanged?.Invoke(locale);
        }

        private string ResolveBootstrapLocale(string requested)
        {
            if (HasLocale(bootstrapTexts, requested)) return requested;
            string neutral = GetNeutralLocale(requested);
            return HasLocale(bootstrapTexts, neutral) ? neutral : DefaultLocale;
        }

        private static string ResolveRuntimeLocale(string requested, Dictionary<string, LanguageCatalogRow> rows, Dictionary<string, string> aliasMap)
        {
            if (rows.TryGetValue(requested, out var exact) && exact.Enabled) return exact.Locale;
            if (aliasMap.TryGetValue(requested, out var mapped) && rows.TryGetValue(mapped, out var alias) && alias.Enabled) return alias.Locale;
            string neutral = GetNeutralLocale(requested);
            if (rows.TryGetValue(neutral, out var baseRow) && baseRow.Enabled) return baseRow.Locale;
            if (aliasMap.TryGetValue(neutral, out mapped) && rows.TryGetValue(mapped, out alias) && alias.Enabled) return alias.Locale;
            return rows.TryGetValue(DefaultLocale, out var fallback) && fallback.Enabled ? fallback.Locale : DefaultLocale;
        }

        private static Dictionary<string, string> BuildTextLookup(IEnumerable<LocalizedTextRow> rows)
        {
            var result = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row.Key) || string.IsNullOrWhiteSpace(row.Locale)) continue;
                result[ComposeTextId(row.Key, row.Locale)] = row.Text ?? string.Empty;
            }
            return result;
        }

        private static Dictionary<string, LanguageCatalogRow> BuildCatalog(IEnumerable<LanguageCatalogRow> rows)
        {
            var result = new Dictionary<string, LanguageCatalogRow>(StringComparer.OrdinalIgnoreCase);
            foreach (var row in rows) result.Add(NormalizeLocale(row.Locale), row);
            return result;
        }

        private static Dictionary<string, string> BuildAliases(IEnumerable<LanguageAliasRow> rows)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var row in rows) result[NormalizeLocale(row.SourceCode)] = NormalizeLocale(row.Locale);
            return result;
        }

        private static Dictionary<string, FontGroupRow> BuildFontGroups(IEnumerable<FontGroupRow> rows)
        {
            var result = new Dictionary<string, FontGroupRow>(StringComparer.OrdinalIgnoreCase);
            foreach (var row in rows) result.Add(row.Id, row);
            return result;
        }

        private static Dictionary<string, string> BuildLocalizedAssets(IEnumerable<LocalizedAssetRow> rows)
        {
            var result = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var row in rows)
                result[ComposeAssetId(row.Key, row.Locale, row.AssetType)] = row.AssetAddress;
            return result;
        }

        private static void ValidateCatalog(Dictionary<string, LanguageCatalogRow> rows)
        {
            if (!rows.ContainsKey(DefaultLocale)) throw new InvalidOperationException("Language catalog must contain enabled fallback locale 'en'.");
            foreach (var pair in rows)
            {
                var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                string current = pair.Key;
                while (rows.TryGetValue(current, out var row) && !string.IsNullOrWhiteSpace(row.FallbackLocale))
                {
                    if (!visited.Add(current)) throw new InvalidOperationException($"Language fallback cycle detected at '{current}'.");
                    current = NormalizeLocale(row.FallbackLocale);
                    if (!rows.ContainsKey(current)) throw new InvalidOperationException($"Language '{pair.Key}' references missing fallback '{current}'.");
                }
            }
        }

        private static string FindText(Dictionary<string, string> texts, string key, string locale) =>
            texts.TryGetValue(ComposeTextId(key, locale), out var value) && !string.IsNullOrEmpty(value) ? value : null;
        private static bool HasLocale(Dictionary<string, string> texts, string locale)
        {
            string suffix = "|" + NormalizeLocale(locale);
            foreach (var key in texts.Keys) if (key.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        private static string ComposeTextId(string key, string locale) => key.Trim() + "|" + NormalizeLocale(locale);
        private static string ComposeAssetId(string key, string locale, string type) => key.Trim() + "|" + NormalizeLocale(locale) + "|" + (type ?? string.Empty).Trim();
        private static string GetNeutralLocale(string locale) { int index = locale.IndexOf('-'); return index > 0 ? locale.Substring(0, index) : locale; }
        private static string NormalizeLocale(string locale) => string.IsNullOrWhiteSpace(locale) ? DefaultLocale : locale.Trim().Replace('_', '-');
        private static string ResolveSystemLocale()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified: return "zh-CN";
                case SystemLanguage.ChineseTraditional: return "zh-TW";
                case SystemLanguage.Japanese: return "ja-JP";
                case SystemLanguage.Korean: return "ko-KR";
                default: return "en";
            }
        }
    }

    /// <summary>首包和 HybridCLR 共同调用的稳定静态门面。</summary>
    public static class L10n
    {
        public static string Get(string key, params object[] args) => LocalizationService.Instance.Get(key, args);
        public static string RequestedLocale => LocalizationService.Instance.RequestedLocale;
        public static string ActiveLocale => LocalizationService.Instance.ActiveLocale;
        public static IEnumerator ChangeLocale(string locale, Action<bool, string> completed = null) => LocalizationService.Instance.ChangeLocale(locale, completed);
    }
}
