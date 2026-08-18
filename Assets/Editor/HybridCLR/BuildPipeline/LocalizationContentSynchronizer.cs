using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using YooAsset.Editor;
using Debug = UnityEngine.Debug;

namespace HybridCLR.Editor
{
    /// <summary>按 Catalog 拆分语言二进制，并同步 YooAsset Collector/Tag。</summary>
    public sealed class LocalizationContentSynchronizer : IPreprocessBuildWithReport
    {
        private const string SourceRoot = "LubanConfig/Localization";
        private const string OutputRoot = "Assets/AssetsPackage/AssetsHotFix/Localization/Locales";
        private const string GroupName = "localization-locales";
        public int callbackOrder => -950;

        public void OnPreprocessBuild(UnityEditor.Build.Reporting.BuildReport report) => SyncOrThrow();

        [MenuItem("Build/热更新/内部工具/同步 Luban 语言包与 Collector", false, 224)]
        public static void SyncMenu()
        {
            SyncOrThrow();
            Debug.Log("Localization locale packages and YooAsset collectors synchronized.");
        }

        public static void SyncOrThrow()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string sourceRoot = Path.Combine(projectRoot, SourceRoot);
            var catalogRows = ReadCsv(Path.Combine(sourceRoot, "Datas/language_catalog.csv"));
            var textRows = ReadCsv(Path.Combine(sourceRoot, "Datas/locale_text.csv"));
            var locales = catalogRows.Select(row => Required(row, "locale")).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            if (locales.Length == 0) throw new BuildFailedException("Language catalog is empty.");

            string tempRoot = Path.Combine(projectRoot, "Temp/LocalizationSplit");
            Directory.CreateDirectory(tempRoot);
            foreach (string locale in locales)
            {
                string address = "l10n_text_" + locale;
                var rows = textRows.Where(row => string.Equals(Get(row, "locale"), locale, StringComparison.OrdinalIgnoreCase)).ToList();
                if (rows.Count == 0) throw new BuildFailedException($"Enabled locale has no text rows: {locale}");
                GenerateLocaleBinary(projectRoot, tempRoot, locale, address, rows);
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            SyncCollectors(locales);
            AssetDatabase.SaveAssets();
        }

        private static void GenerateLocaleBinary(string projectRoot, string tempRoot, string locale, string address, List<Dictionary<string, string>> rows)
        {
            string safe = locale.Replace('/', '-').Replace('\\', '-');
            string workspace = Path.Combine(tempRoot, safe);
            string dataDir = Path.Combine(workspace, "Datas");
            Directory.CreateDirectory(dataDir);
            File.WriteAllText(Path.Combine(workspace, "luban.conf"),
                "{\"groups\":[{\"names\":[\"c\"],\"default\":true}],\"schemaFiles\":[{\"fileName\":\"schema.xml\",\"type\":\"\"}],\"dataDir\":\"Datas\",\"targets\":[{\"name\":\"client\",\"manager\":\"LocaleTables\",\"groups\":[\"c\"],\"topModule\":\"Framework.Localization.Generated\"}]}", Encoding.UTF8);
            File.WriteAllText(Path.Combine(workspace, "schema.xml"),
                "<module name=\"\"><bean name=\"LocalizedTextRow\"><var name=\"id\" type=\"string\"/><var name=\"key\" type=\"string\"/><var name=\"locale\" type=\"string\"/><var name=\"text\" type=\"string\"/></bean><table name=\"TbLocaleText\" value=\"LocalizedTextRow\" input=\"locale_text.csv\" index=\"id\" mode=\"map\" group=\"c\"/></module>", Encoding.UTF8);
            WriteCsv(Path.Combine(dataDir, "locale_text.csv"), rows);

            string outputDirectory = Path.Combine(projectRoot, OutputRoot, safe);
            Directory.CreateDirectory(outputDirectory);
            string lubanDll = Path.Combine(projectRoot, "LubanConfig/DataTables/Luban/Luban.dll");
            var startInfo = new ProcessStartInfo("dotnet")
            {
                WorkingDirectory = workspace,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                Arguments = $"\"{lubanDll}\" -t client -d bin --conf \"{Path.Combine(workspace, "luban.conf")}\" --validationFailAsError -x bin.outputDataDir=\"{outputDirectory}\""
            };
            using (var process = Process.Start(startInfo))
            {
                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode != 0) throw new BuildFailedException($"Luban locale generation failed ({locale}).\n{stdout}\n{stderr}");
            }
            string generated = Path.Combine(outputDirectory, "tblocaletext.bytes");
            string target = Path.Combine(outputDirectory, address + ".bytes");
            if (File.Exists(target)) File.Delete(target);
            File.Move(generated, target);
        }

        private static void SyncCollectors(IEnumerable<string> locales)
        {
            var setting = AssetBundleCollectorSettingData.Setting;
            if (setting == null) throw new BuildFailedException("YooAsset Collector settings are missing.");
            var package = setting.Packages.FirstOrDefault(item => item != null && item.PackageName == BuildAssetsCommand.GetConfiguredMainPackageName());
            if (package == null) throw new BuildFailedException("Main YooAsset package is missing.");
            var group = package.Groups.FirstOrDefault(item => item != null && item.GroupName == GroupName);
            if (group == null)
            {
                group = new AssetBundleCollectorGroup { GroupName = GroupName, GroupDesc = "Luban 自动生成的独立语言包", ActiveRuleName = nameof(EnableGroup) };
                package.Groups.Add(group);
            }

            var expected = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (string locale in locales)
            {
                string path = $"{OutputRoot}/{locale}";
                expected.Add(path);
                var collector = group.Collectors.FirstOrDefault(item => item != null && string.Equals(item.CollectPath, path, StringComparison.OrdinalIgnoreCase));
                if (collector == null)
                {
                    collector = new AssetBundleCollector { CollectPath = path, CollectorType = ECollectorType.MainAssetCollector };
                    group.Collectors.Add(collector);
                }
                collector.CollectorGUID = AssetDatabase.AssetPathToGUID(path);
                collector.AddressRuleName = nameof(AddressByFileName);
                collector.PackRuleName = nameof(PackDirectory);
                collector.FilterRuleName = nameof(CollectAll);
                collector.AssetTags = "l10n." + locale;
            }
            group.Collectors.RemoveAll(item => item == null || !expected.Contains(item.CollectPath));
            EditorUtility.SetDirty(setting);
        }

        private static void WriteCsv(string path, List<Dictionary<string, string>> rows)
        {
            var builder = new StringBuilder();
            builder.AppendLine("##var,id,key,locale,text");
            builder.AppendLine("##type,string,string,string,string");
            builder.AppendLine("##,联合ID,Key,语言,文本");
            foreach (var row in rows) builder.Append(',').Append(Escape(Get(row, "id"))).Append(',').Append(Escape(Get(row, "key"))).Append(',').Append(Escape(Get(row, "locale"))).Append(',').Append(Escape(Get(row, "text"))).AppendLine();
            File.WriteAllText(path, builder.ToString(), new UTF8Encoding(false));
        }
        private static string Escape(string value) => value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0 ? value : "\"" + value.Replace("\"", "\"\"") + "\"";
        private static List<Dictionary<string, string>> ReadCsv(string path)
        {
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            string[] headers = ParseLine(lines[0]);
            var result = new List<Dictionary<string, string>>();
            for (int line = 3; line < lines.Length; line++)
            {
                if (string.IsNullOrWhiteSpace(lines[line])) continue;
                string[] values = ParseLine(lines[line]);
                var row = new Dictionary<string, string>(StringComparer.Ordinal);
                for (int i = 1; i < headers.Length; i++) row[headers[i]] = i < values.Length ? values[i] : string.Empty;
                result.Add(row);
            }
            return result;
        }
        private static string[] ParseLine(string line)
        {
            var result = new List<string>(); var value = new StringBuilder(); bool quoted = false;
            for (int i = 0; i < line.Length; i++) { char c = line[i]; if (c == '"') { if (quoted && i + 1 < line.Length && line[i + 1] == '"') { value.Append('"'); i++; } else quoted = !quoted; } else if (c == ',' && !quoted) { result.Add(value.ToString()); value.Length = 0; } else value.Append(c); }
            result.Add(value.ToString()); return result.ToArray();
        }
        private static string Required(Dictionary<string, string> row, string key) { string value = Get(row, key); if (string.IsNullOrWhiteSpace(value)) throw new BuildFailedException($"Localization field is empty: {key}"); return value.Trim(); }
        private static string Get(Dictionary<string, string> row, string key) => row.TryGetValue(key, out var value) ? value : string.Empty;
    }
}
