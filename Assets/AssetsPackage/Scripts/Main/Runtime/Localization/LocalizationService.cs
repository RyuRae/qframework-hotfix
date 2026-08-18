using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Framework.Localization.Generated;
using Luban;
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
        public const string LocaleTextAddress = "tblocaletext";

        private Dictionary<string, string> bootstrapTexts = new Dictionary<string, string>(StringComparer.Ordinal);
        private Dictionary<string, string> runtimeTexts = new Dictionary<string, string>(StringComparer.Ordinal);
        private Dictionary<string, LanguageCatalogRow> catalog = new Dictionary<string, LanguageCatalogRow>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static LocalizationService Instance { get; } = new LocalizationService();

        public string RequestedLocale { get; private set; } = DefaultLocale;
        public string ActiveLocale { get; private set; } = DefaultLocale;
        public bool IsBootstrapInitialized { get; private set; }
        public bool HasRuntimeCatalog { get; private set; }
        public event Action<string> LocaleChanged;

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
            AssetHandle textHandle = null;
            try
            {
                catalogHandle = package.LoadAssetAsync<TextAsset>(CatalogAddress);
                aliasHandle = package.LoadAssetAsync<TextAsset>(AliasAddress);
                textHandle = package.LoadAssetAsync<TextAsset>(LocaleTextAddress);
                yield return catalogHandle;
                yield return aliasHandle;
                yield return textHandle;

                if (catalogHandle.Status != EOperationStatus.Succeed ||
                    aliasHandle.Status != EOperationStatus.Succeed ||
                    textHandle.Status != EOperationStatus.Succeed)
                {
                    completed?.Invoke(false, $"Catalog={catalogHandle.LastError}; Alias={aliasHandle.LastError}; Text={textHandle.LastError}");
                    yield break;
                }

                try
                {
                    var nextCatalogTable = new TbLanguageCatalog(new ByteBuf(((TextAsset)catalogHandle.AssetObject).bytes));
                    var nextAliasTable = new TbLanguageAlias(new ByteBuf(((TextAsset)aliasHandle.AssetObject).bytes));
                    var nextTextTable = new TbLocaleText(new ByteBuf(((TextAsset)textHandle.AssetObject).bytes));
                    var nextCatalog = BuildCatalog(nextCatalogTable.DataList);
                    var nextAliases = BuildAliases(nextAliasTable.DataList);
                    ValidateCatalog(nextCatalog);
                    var nextTexts = BuildTextLookup(nextTextTable.DataList);
                    string nextLocale = ResolveRuntimeLocale(RequestedLocale, nextCatalog, nextAliases);

                    catalog = nextCatalog;
                    aliases = nextAliases;
                    runtimeTexts = nextTexts;
                    HasRuntimeCatalog = true;
                    CommitActiveLocale(nextLocale);
                    completed?.Invoke(true, string.Empty);
                }
                catch (Exception exception)
                {
                    completed?.Invoke(false, exception.Message);
                }
            }
            finally
            {
                catalogHandle?.Release();
                aliasHandle?.Release();
                textHandle?.Release();
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
            if (HasRuntimeCatalog && !catalog.ContainsKey(normalized)) return false;
            RequestedLocale = normalized;
            PlayerPrefs.SetString(PreferenceKey, normalized);
            if (HasRuntimeCatalog) CommitActiveLocale(ResolveRuntimeLocale(normalized, catalog, aliases));
            else CommitActiveLocale(ResolveBootstrapLocale(normalized));
            return true;
        }

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

        private void CommitActiveLocale(string locale)
        {
            locale = NormalizeLocale(locale);
            if (string.Equals(ActiveLocale, locale, StringComparison.OrdinalIgnoreCase)) return;
            ActiveLocale = locale;
            LocaleChanged?.Invoke(locale);
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
    }
}
