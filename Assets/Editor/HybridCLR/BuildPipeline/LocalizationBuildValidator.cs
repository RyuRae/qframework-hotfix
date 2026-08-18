using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace HybridCLR.Editor
{
    /// <summary>Luban 多语言源表的商业发布前门禁。</summary>
    public sealed class LocalizationBuildValidator : IPreprocessBuildWithReport
    {
        private const string Root = "LubanConfig/Localization/Datas";
        private static readonly Regex PlaceholderRegex = new Regex(@"\{\d+(?:[^}]*)?\}", RegexOptions.Compiled);
        public int callbackOrder => -900;

        public void OnPreprocessBuild(BuildReport report)
        {
            ValidateOrThrow();
        }

        [MenuItem("Build/热更新/内部工具/校验 Luban 多语言", false, 225)]
        public static void ValidateMenu()
        {
            ValidateOrThrow();
            Debug.Log("Luban localization validation succeeded.");
        }

        public static void ValidateOrThrow()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string dataRoot = Path.Combine(projectRoot, Root);
            var catalog = ReadCsv(Path.Combine(dataRoot, "language_catalog.csv"));
            var bootstrap = ReadCsv(Path.Combine(dataRoot, "bootstrap_text.csv"));
            var localeText = ReadCsv(Path.Combine(dataRoot, "locale_text.csv"));
            var fontGroups = ReadCsv(Path.Combine(dataRoot, "font_group.csv"));
            var localizedAssets = ReadCsv(Path.Combine(dataRoot, "localized_asset.csv"), true);

            ValidateCatalog(catalog);
            ValidateTexts("BootstrapText", bootstrap);
            ValidateTexts("LocaleText", localeText);
            ValidateUnique("FontGroup", fontGroups, "id");
            ValidateUnique("LocalizedAsset", localizedAssets, "id");

            RequireFile(Path.Combine(projectRoot, "Assets/AssetsPackage/Resources/Localization/bootstrap.bytes"));
            RequireFile(Path.Combine(projectRoot, "Assets/AssetsPackage/AssetsHotFix/Datas/Localization/tblanguagecatalog.bytes"));
            RequireFile(Path.Combine(projectRoot, "Assets/AssetsPackage/AssetsHotFix/Datas/Localization/tblanguagealias.bytes"));
            RequireFile(Path.Combine(projectRoot, "Assets/AssetsPackage/AssetsHotFix/Datas/Localization/tbfontgroup.bytes"));
            RequireFile(Path.Combine(projectRoot, "Assets/AssetsPackage/AssetsHotFix/Datas/Localization/tblocalizedasset.bytes"));
            foreach (var row in catalog)
            {
                string locale = Required(row, "locale", "LanguageCatalog");
                string address = Required(row, "textTableAddress", "LanguageCatalog");
                RequireFile(Path.Combine(projectRoot, $"Assets/AssetsPackage/AssetsHotFix/Localization/Locales/{locale}/{address}.bytes"));
            }
        }

        private static void ValidateCatalog(List<Dictionary<string, string>> rows)
        {
            var locales = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var fallback = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var row in rows)
            {
                string locale = Required(row, "locale", "LanguageCatalog");
                if (!locales.Add(locale)) throw new BuildFailedException($"Duplicate localization locale: {locale}");
                fallback[locale] = Get(row, "fallbackLocale");
            }
            if (!locales.Contains("en")) throw new BuildFailedException("LanguageCatalog must contain fallback locale 'en'.");
            foreach (string start in locales)
            {
                var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                string current = start;
                while (fallback.TryGetValue(current, out var next) && !string.IsNullOrWhiteSpace(next))
                {
                    if (!locales.Contains(next)) throw new BuildFailedException($"Locale '{current}' references missing fallback '{next}'.");
                    if (!visited.Add(current)) throw new BuildFailedException($"Localization fallback cycle detected at '{current}'.");
                    current = next;
                }
            }
        }

        private static void ValidateTexts(string label, List<Dictionary<string, string>> rows)
        {
            var ids = new HashSet<string>(StringComparer.Ordinal);
            var placeholders = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
            foreach (var row in rows)
            {
                string id = Required(row, "id", label);
                string key = Required(row, "key", label);
                Required(row, "locale", label);
                string text = Required(row, "text", label);
                if (!ids.Add(id)) throw new BuildFailedException($"Duplicate {label} id: {id}");
                var set = new HashSet<string>(PlaceholderRegex.Matches(text).Cast<Match>().Select(NormalizePlaceholder), StringComparer.Ordinal);
                if (placeholders.TryGetValue(key, out var expected) && !expected.SetEquals(set))
                    throw new BuildFailedException($"{label} format placeholders differ between locales for key '{key}'.");
                placeholders[key] = set;
            }
        }

        private static string NormalizePlaceholder(Match match)
        {
            string value = match.Value;
            int colon = value.IndexOf(':');
            int comma = value.IndexOf(',');
            int end = colon < 0 ? comma : comma < 0 ? colon : Math.Min(colon, comma);
            return end < 0 ? value : value.Substring(0, end) + "}";
        }

        private static void ValidateUnique(string label, List<Dictionary<string, string>> rows, string key)
        {
            var values = new HashSet<string>(StringComparer.Ordinal);
            foreach (var row in rows)
            {
                string value = Required(row, key, label);
                if (!values.Add(value)) throw new BuildFailedException($"Duplicate {label}.{key}: {value}");
            }
        }

        private static List<Dictionary<string, string>> ReadCsv(string path, bool allowEmpty = false)
        {
            RequireFile(path);
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            if (lines.Length < 3 || (!allowEmpty && lines.Length < 4))
                throw new BuildFailedException($"Localization CSV has no data: {path}");
            string[] headers = ParseLine(lines[0]).Select(value => value == "##var" ? string.Empty : value).ToArray();
            var result = new List<Dictionary<string, string>>();
            for (int line = 3; line < lines.Length; line++)
            {
                if (string.IsNullOrWhiteSpace(lines[line])) continue;
                string[] values = ParseLine(lines[line]);
                var row = new Dictionary<string, string>(StringComparer.Ordinal);
                for (int i = 1; i < headers.Length; i++) row[headers[i]] = i < values.Length ? values[i] : string.Empty;
                result.Add(row);
            }
            if (!allowEmpty && result.Count == 0) throw new BuildFailedException($"Localization CSV has no data: {path}");
            return result;
        }

        private static string[] ParseLine(string line)
        {
            var values = new List<string>();
            var value = new StringBuilder();
            bool quoted = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (quoted && i + 1 < line.Length && line[i + 1] == '"') { value.Append('"'); i++; }
                    else quoted = !quoted;
                }
                else if (c == ',' && !quoted) { values.Add(value.ToString()); value.Length = 0; }
                else value.Append(c);
            }
            values.Add(value.ToString());
            return values.ToArray();
        }

        private static string Required(Dictionary<string, string> row, string key, string label)
        {
            string value = Get(row, key);
            if (string.IsNullOrWhiteSpace(value)) throw new BuildFailedException($"{label}.{key} must not be empty.");
            return value.Trim();
        }
        private static string Get(Dictionary<string, string> row, string key) => row.TryGetValue(key, out var value) ? value : string.Empty;
        private static void RequireFile(string path) { if (!File.Exists(path)) throw new BuildFailedException($"Required localization file not found: {path}"); }
    }
}
