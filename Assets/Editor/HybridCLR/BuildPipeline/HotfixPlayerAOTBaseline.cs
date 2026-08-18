using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Framework;
using Framework.Assemblies;
using HybridCLR.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    public sealed class HotfixPlayerAOTBaseline : ScriptableObject
    {
        public const int CurrentFormatVersion = 1;
        public const string AssetPath = "Assets/Editor/HybridCLR/PlayerAOTBaseline.asset";

        public int FormatVersion = CurrentFormatVersion;
        public string BuildTarget = string.Empty;
        public string AppVersion = string.Empty;
        public string UnityVersion = string.Empty;
        public string PackageVersion = string.Empty;
        public string AotVersion = string.Empty;
        public string ManifestBaselineFingerprint = string.Empty;
        public string PlayerSettingsFingerprint = string.Empty;
        [TextArea(4, 16)]
        public string PlayerSettingsIdentity = string.Empty;
        public string BaselineFingerprint = string.Empty;
        public string GeneratedAtUtc = string.Empty;
        public List<AssemblyFileRecord> StrippedAOTAssemblies = new List<AssemblyFileRecord>();
    }

    public static class HotfixPlayerAOTBaselineUtility
    {
        private const string PackagesLockPath = "Packages/packages-lock.json";

        public static HotfixPlayerAOTBaseline Load()
        {
            return AssetDatabase.LoadAssetAtPath<HotfixPlayerAOTBaseline>(HotfixPlayerAOTBaseline.AssetPath);
        }

        public static HotfixPlayerAOTBaseline CaptureAfterInitialPackage(
            BuildTarget target,
            string appVersion,
            AOTAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            var strippedAssemblies = CreateStrippedAOTAssemblyRecords(target);
            string identity = CreatePlayerSettingsIdentity(target, appVersion);
            string identityFingerprint = ComputeTextSha256(identity);
            string generatedAtUtc = DateTime.UtcNow.ToString("O");
            string baselineFingerprint = CreateBaselineFingerprint(
                target,
                appVersion,
                manifest,
                identityFingerprint,
                strippedAssemblies);

            var baseline = Load();
            if (baseline == null)
            {
                string directory = Path.GetDirectoryName(HotfixPlayerAOTBaseline.AssetPath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                baseline = ScriptableObject.CreateInstance<HotfixPlayerAOTBaseline>();
                baseline.name = "PlayerAOTBaseline";
                AssetDatabase.CreateAsset(baseline, HotfixPlayerAOTBaseline.AssetPath);
            }

            baseline.FormatVersion = HotfixPlayerAOTBaseline.CurrentFormatVersion;
            baseline.BuildTarget = GetBuildTargetName(target);
            baseline.AppVersion = appVersion ?? string.Empty;
            baseline.UnityVersion = Application.unityVersion;
            baseline.PackageVersion = manifest.ReleaseVersion ?? string.Empty;
            baseline.AotVersion = manifest.AotVersion ?? string.Empty;
            baseline.ManifestBaselineFingerprint = manifest.BaselineFingerprint ?? string.Empty;
            baseline.PlayerSettingsFingerprint = identityFingerprint;
            baseline.PlayerSettingsIdentity = identity;
            baseline.BaselineFingerprint = baselineFingerprint;
            baseline.GeneratedAtUtc = generatedAtUtc;
            baseline.StrippedAOTAssemblies = CloneRecords(strippedAssemblies);

            EditorUtility.SetDirty(baseline);
            AssetDatabase.SaveAssetIfDirty(baseline);
            Debug.Log(
                $"[HotfixBuild] Player AOT 基线已建立。Target={baseline.BuildTarget}, " +
                $"AppVersion={baseline.AppVersion}, Fingerprint={baseline.BaselineFingerprint}, " +
                $"Assemblies={baseline.StrippedAOTAssemblies.Count}");
            return baseline;
        }

        public static bool TryValidateIdentity(
            HotfixPlayerAOTBaseline baseline,
            BuildTarget target,
            string appVersion,
            out string message)
        {
            if (baseline == null)
            {
                message =
                    $"缺少独立 Player AOT 基线：{HotfixPlayerAOTBaseline.AssetPath}。" +
                    "旧项目必须先通过构建中心重新构建首包资源。";
                return false;
            }

            if (baseline.FormatVersion != HotfixPlayerAOTBaseline.CurrentFormatVersion)
            {
                message =
                    $"Player AOT 基线格式不受支持。Baseline={baseline.FormatVersion}, " +
                    $"Current={HotfixPlayerAOTBaseline.CurrentFormatVersion}。请重新构建首包资源。";
                return false;
            }

            string targetName = GetBuildTargetName(target);
            if (!string.Equals(baseline.BuildTarget, targetName, StringComparison.OrdinalIgnoreCase))
            {
                message = $"Player AOT 基线平台不匹配。Baseline={baseline.BuildTarget}, Current={targetName}。";
                return false;
            }

            if (!string.Equals(baseline.AppVersion, appVersion, StringComparison.OrdinalIgnoreCase))
            {
                message = $"Player AOT 基线 AppVersion 不匹配。Baseline={baseline.AppVersion}, Current={appVersion}。";
                return false;
            }

            if (!string.Equals(baseline.UnityVersion, Application.unityVersion, StringComparison.Ordinal))
            {
                message =
                    $"Player AOT 基线 Unity 版本不匹配。Baseline={baseline.UnityVersion}, " +
                    $"Current={Application.unityVersion}。";
                return false;
            }

            string currentIdentity = CreatePlayerSettingsIdentity(target, appVersion);
            string currentFingerprint = ComputeTextSha256(currentIdentity);
            if (!string.Equals(
                    baseline.PlayerSettingsFingerprint,
                    currentFingerprint,
                    StringComparison.OrdinalIgnoreCase))
            {
                message =
                    "Player/HybridCLR 构建身份已经变化，AOT 元数据补丁被阻断。" +
                    $"Baseline={baseline.PlayerSettingsFingerprint}, Current={currentFingerprint}。" +
                    "请发布新 App 并重新建立首包基线。";
                return false;
            }

            if (baseline.StrippedAOTAssemblies == null || baseline.StrippedAOTAssemblies.Count == 0)
            {
                message = "Player AOT 基线没有记录裁剪后的 AOT DLL。请重新构建首包资源。";
                return false;
            }

            if (string.IsNullOrWhiteSpace(baseline.BaselineFingerprint))
            {
                message = "Player AOT 基线指纹缺失。请重新构建首包资源。";
                return false;
            }

            string storedFingerprint = CreateBaselineFingerprint(
                baseline.BuildTarget,
                baseline.AppVersion,
                baseline.PackageVersion,
                baseline.AotVersion,
                baseline.ManifestBaselineFingerprint,
                baseline.PlayerSettingsFingerprint,
                baseline.StrippedAOTAssemblies);
            if (!string.Equals(
                    baseline.BaselineFingerprint,
                    storedFingerprint,
                    StringComparison.OrdinalIgnoreCase))
            {
                message =
                    "Player AOT 基线内容与自身指纹不一致，可能已被手工修改或损坏。" +
                    "请重新构建首包资源。";
                return false;
            }

            message =
                $"Player AOT 基线身份有效。Fingerprint={baseline.BaselineFingerprint}, " +
                $"Assemblies={baseline.StrippedAOTAssemblies.Count}";
            return true;
        }

        public static void ValidateIdentityOrThrow(
            HotfixPlayerAOTBaseline baseline,
            BuildTarget target,
            string appVersion)
        {
            if (!TryValidateIdentity(baseline, target, appVersion, out string message))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void ValidateGeneratedAssembliesOrThrow(
            HotfixPlayerAOTBaseline baseline,
            BuildTarget target)
        {
            if (baseline == null)
            {
                throw new ArgumentNullException(nameof(baseline));
            }

            var current = CreateStrippedAOTAssemblyRecords(target);
            BuildAssemblyDiff(
                baseline.StrippedAOTAssemblies,
                current,
                out var added,
                out var changed,
                out var removed);
            if (added.Count == 0 && changed.Count == 0 && removed.Count == 0)
            {
                return;
            }

            throw new InvalidOperationException(
                "裁剪后的 Player AOT DLL 与首包基线不一致，AOT 元数据补丁已在改写 Manifest 前阻断。\n" +
                $"新增：{FormatNames(added)}\n" +
                $"变化：{FormatNames(changed)}\n" +
                $"移除：{FormatNames(removed)}\n" +
                "这表示当前输出不再属于原 Player，请发布新 App 并重新构建首包资源。");
        }

        public static List<AssemblyFileRecord> CreateStrippedAOTAssemblyRecords(BuildTarget target)
        {
            string directory = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(
                    $"裁剪后的 AOT DLL 目录不存在：{directory}。请先执行 HybridCLR Generate All。");
            }

            var records = Directory.GetFiles(directory, "*.dll", SearchOption.TopDirectoryOnly)
                .OrderBy(path => Path.GetFileName(path), StringComparer.OrdinalIgnoreCase)
                .Select(path =>
                {
                    var info = new FileInfo(path);
                    string fileName = Path.GetFileName(path);
                    return new AssemblyFileRecord
                    {
                        FileName = fileName,
                        AssemblyName = fileName,
                        Size = info.Length,
                        Sha256 = ComputeFileSha256(path)
                    };
                })
                .ToList();

            if (records.Count == 0)
            {
                throw new InvalidOperationException($"裁剪后的 AOT DLL 目录为空：{directory}");
            }

            return records;
        }

        public static void BuildAssemblyDiff(
            IEnumerable<AssemblyFileRecord> previous,
            IEnumerable<AssemblyFileRecord> current,
            out List<string> added,
            out List<string> changed,
            out List<string> removed)
        {
            var previousMap = ToRecordMap(previous);
            var currentMap = ToRecordMap(current);
            added = currentMap.Keys
                .Where(name => !previousMap.ContainsKey(name))
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
            removed = previousMap.Keys
                .Where(name => !currentMap.ContainsKey(name))
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
            changed = currentMap.Keys
                .Where(previousMap.ContainsKey)
                .Where(name => !RecordsEqual(previousMap[name], currentMap[name]))
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static string CreatePlayerSettingsIdentity(BuildTarget target, string appVersion)
        {
            var settings = SettingsUtil.HybridCLRSettings;
            BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(target);
            var builder = new StringBuilder();
            builder.AppendLine($"UnityVersion={Application.unityVersion}");
            builder.AppendLine($"BuildTarget={GetBuildTargetName(target)}");
            builder.AppendLine($"BuildTargetGroup={targetGroup}");
            builder.AppendLine($"AppVersion={appVersion ?? string.Empty}");
            builder.AppendLine($"ApplicationIdentifier={PlayerSettings.GetApplicationIdentifier(targetGroup)}");
            builder.AppendLine($"CompanyName={PlayerSettings.companyName}");
            builder.AppendLine($"ProductName={PlayerSettings.productName}");
            builder.AppendLine($"DevelopmentBuild={EditorUserBuildSettings.development}");
            builder.AppendLine($"AllowDebugging={EditorUserBuildSettings.allowDebugging}");
            builder.AppendLine($"ScriptingBackend={PlayerSettings.GetScriptingBackend(targetGroup)}");
            builder.AppendLine($"ApiCompatibilityLevel={PlayerSettings.GetApiCompatibilityLevel(targetGroup)}");
            builder.AppendLine($"ManagedStrippingLevel={PlayerSettings.GetManagedStrippingLevel(targetGroup)}");
            builder.AppendLine($"Il2CppCompilerConfiguration={PlayerSettings.GetIl2CppCompilerConfiguration(targetGroup)}");
            builder.AppendLine($"Architecture={PlayerSettings.GetArchitecture(targetGroup)}");
            builder.AppendLine($"AdditionalIl2CppArgs={PlayerSettings.GetAdditionalIl2CppArgs()}");
            builder.AppendLine($"StripEngineCode={PlayerSettings.stripEngineCode}");
            builder.AppendLine($"ScriptingDefineSymbols={PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup)}");
            AppendArray(
                builder,
                "AdditionalCompilerArguments",
                PlayerSettings.GetAdditionalCompilerArgumentsForGroup(targetGroup));
            builder.AppendLine($"PackagesLockSha256={ComputeProjectFileSha256(PackagesLockPath)}");
            builder.AppendLine($"NativePluginSourcesSha256={CreateNativePluginSourcesFingerprint()}");
            builder.AppendLine($"HybridCLR.enable={settings.enable}");
            builder.AppendLine($"HybridCLR.useGlobalIl2cpp={settings.useGlobalIl2cpp}");
            builder.AppendLine($"HybridCLR.hybridclrRepoURL={settings.hybridclrRepoURL ?? string.Empty}");
            builder.AppendLine($"HybridCLR.il2cppPlusRepoURL={settings.il2cppPlusRepoURL ?? string.Empty}");
            AppendArray(builder, "HybridCLR.hotUpdateAssemblyDefinitions", GetAssemblyDefinitionGuids(settings));
            AppendArray(builder, "HybridCLR.hotUpdateAssemblies", settings.hotUpdateAssemblies);
            AppendArray(builder, "HybridCLR.preserveHotUpdateAssemblies", settings.preserveHotUpdateAssemblies);
            AppendArray(builder, "HybridCLR.externalHotUpdateAssembliyDirs", settings.externalHotUpdateAssembliyDirs);
            builder.AppendLine($"HybridCLR.hotUpdateDllCompileOutputRootDir={settings.hotUpdateDllCompileOutputRootDir ?? string.Empty}");
            builder.AppendLine($"HybridCLR.strippedAOTDllOutputRootDir={settings.strippedAOTDllOutputRootDir ?? string.Empty}");
            builder.AppendLine($"HybridCLR.outputLinkFile={settings.outputLinkFile ?? string.Empty}");
            builder.AppendLine($"HybridCLR.outputAOTGenericReferenceFile={settings.outputAOTGenericReferenceFile ?? string.Empty}");
            builder.AppendLine($"HybridCLR.maxGenericReferenceIteration={settings.maxGenericReferenceIteration}");
            builder.AppendLine($"HybridCLR.maxMethodBridgeGenericIteration={settings.maxMethodBridgeGenericIteration}");
            // patchAOTAssemblies is intentionally excluded: changing the metadata selection is the purpose of this task.
            return builder.ToString();
        }

        private static IEnumerable<string> GetAssemblyDefinitionGuids(HybridCLRSettings settings)
        {
            return (settings.hotUpdateAssemblyDefinitions ?? Array.Empty<UnityEditorInternal.AssemblyDefinitionAsset>())
                .Select(asset => asset == null
                    ? string.Empty
                    : AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset)));
        }

        private static void AppendArray(StringBuilder builder, string name, IEnumerable<string> values)
        {
            builder.Append(name).Append('=');
            builder.AppendLine(string.Join(",", values ?? Enumerable.Empty<string>()));
        }

        private static string CreateBaselineFingerprint(
            BuildTarget target,
            string appVersion,
            AOTAssemblyManifest manifest,
            string identityFingerprint,
            IEnumerable<AssemblyFileRecord> records)
        {
            return CreateBaselineFingerprint(
                GetBuildTargetName(target),
                appVersion,
                manifest.ReleaseVersion,
                manifest.AotVersion,
                manifest.BaselineFingerprint,
                identityFingerprint,
                records);
        }

        private static string CreateBaselineFingerprint(
            string buildTarget,
            string appVersion,
            string packageVersion,
            string aotVersion,
            string manifestBaselineFingerprint,
            string identityFingerprint,
            IEnumerable<AssemblyFileRecord> records)
        {
            var builder = new StringBuilder();
            builder.AppendLine(buildTarget ?? string.Empty);
            builder.AppendLine(appVersion ?? string.Empty);
            builder.AppendLine(packageVersion ?? string.Empty);
            builder.AppendLine(aotVersion ?? string.Empty);
            builder.AppendLine(manifestBaselineFingerprint ?? string.Empty);
            builder.AppendLine(identityFingerprint ?? string.Empty);
            foreach (var record in records ?? Enumerable.Empty<AssemblyFileRecord>())
            {
                builder.Append(GetRecordName(record)).Append('|')
                    .Append(record == null ? 0 : record.Size).Append('|')
                    .AppendLine(record == null ? string.Empty : record.Sha256 ?? string.Empty);
            }

            return ComputeTextSha256(builder.ToString());
        }

        private static Dictionary<string, AssemblyFileRecord> ToRecordMap(IEnumerable<AssemblyFileRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyFileRecord>())
                .Where(record => record != null && !string.IsNullOrWhiteSpace(GetRecordName(record)))
                .GroupBy(GetRecordName, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);
        }

        private static bool RecordsEqual(AssemblyFileRecord left, AssemblyFileRecord right)
        {
            return left != null && right != null &&
                   left.Size == right.Size &&
                   string.Equals(left.Sha256, right.Sha256, StringComparison.OrdinalIgnoreCase);
        }

        private static string GetRecordName(AssemblyFileRecord record)
        {
            if (record == null)
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(record.FileName) ? record.AssemblyName : record.FileName;
        }

        private static string FormatNames(IEnumerable<string> names)
        {
            var values = (names ?? Enumerable.Empty<string>()).ToList();
            return values.Count == 0 ? "无" : string.Join(", ", values);
        }

        private static List<AssemblyFileRecord> CloneRecords(IEnumerable<AssemblyFileRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyFileRecord>())
                .Where(record => record != null)
                .Select(record => new AssemblyFileRecord
                {
                    FileName = record.FileName ?? string.Empty,
                    AssemblyName = record.AssemblyName ?? string.Empty,
                    Size = record.Size,
                    Sha256 = record.Sha256 ?? string.Empty
                })
                .ToList();
        }

        private static string GetBuildTargetName(BuildTarget target)
        {
            return HotfixUtility.GetPlatformNameForBuildTarget(target);
        }

        private static string ComputeProjectFileSha256(string relativePath)
        {
            string projectRoot = Directory.GetParent(Application.dataPath)?.FullName ?? string.Empty;
            string fullPath = Path.Combine(projectRoot, relativePath);
            return File.Exists(fullPath) ? ComputeFileSha256(fullPath) : "missing";
        }

        private static string CreateNativePluginSourcesFingerprint()
        {
            string projectRoot = Directory.GetParent(Application.dataPath)?.FullName ?? string.Empty;
            var builder = new StringBuilder();
            AppendNativePluginFiles(builder, projectRoot, Path.Combine(projectRoot, "Assets"));
            AppendNativePluginFiles(builder, projectRoot, Path.Combine(projectRoot, "Packages"));
            AppendImportedNativePlugins(builder, projectRoot);
            return ComputeTextSha256(builder.ToString());
        }

        private static void AppendNativePluginFiles(StringBuilder builder, string projectRoot, string searchRoot)
        {
            if (!Directory.Exists(searchRoot))
            {
                return;
            }

            foreach (string path in Directory.EnumerateFiles(searchRoot, "*", SearchOption.AllDirectories)
                         .Where(IsNativePluginSource)
                         .OrderBy(value => value, StringComparer.OrdinalIgnoreCase))
            {
                string relativePath = path.Substring(projectRoot.Length)
                    .TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    .Replace(Path.DirectorySeparatorChar, '/');
                var info = new FileInfo(path);
                builder.Append(relativePath).Append('|')
                    .Append(info.Length).Append('|')
                    .Append(ComputeFileSha256(path)).Append('|')
                    .AppendLine(ComputeOptionalFileSha256(path + ".meta"));
            }
        }

        private static bool IsNativePluginSource(string path)
        {
            string normalizedPath = path.Replace('\\', '/');
            if (normalizedPath.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (normalizedPath.IndexOf(".framework/", StringComparison.OrdinalIgnoreCase) >= 0 ||
                normalizedPath.IndexOf(".xcframework/", StringComparison.OrdinalIgnoreCase) >= 0 ||
                normalizedPath.IndexOf(".bundle/", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            switch (Path.GetExtension(normalizedPath).ToLowerInvariant())
            {
                case ".aar":
                case ".jar":
                case ".so":
                case ".a":
                case ".dylib":
                case ".lib":
                case ".winmd":
                case ".c":
                case ".cc":
                case ".cpp":
                case ".h":
                case ".hpp":
                case ".m":
                case ".mm":
                    return true;
                default:
                    return false;
            }
        }

        private static void AppendImportedNativePlugins(StringBuilder builder, string projectRoot)
        {
            foreach (string assetPath in AssetDatabase.GetAllAssetPaths()
                         .Where(path => path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase) ||
                                        path.StartsWith("Packages/", StringComparison.OrdinalIgnoreCase))
                         .OrderBy(path => path, StringComparer.OrdinalIgnoreCase))
            {
                if (!(AssetImporter.GetAtPath(assetPath) is PluginImporter importer) || !importer.isNativePlugin)
                {
                    continue;
                }

                string fullPath = Path.Combine(projectRoot, assetPath);
                if (!File.Exists(fullPath) || IsNativePluginSource(fullPath))
                {
                    continue;
                }

                var info = new FileInfo(fullPath);
                builder.Append(assetPath).Append('|')
                    .Append(info.Length).Append('|')
                    .Append(ComputeFileSha256(fullPath)).Append('|')
                    .AppendLine(AssetDatabase.GetAssetDependencyHash(assetPath).ToString());
            }
        }

        private static string ComputeOptionalFileSha256(string path)
        {
            return File.Exists(path) ? ComputeFileSha256(path) : "missing";
        }

        private static string ComputeTextSha256(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value ?? string.Empty));
                return BytesToHex(bytes);
            }
        }

        private static string ComputeFileSha256(string path)
        {
            using (var sha256 = SHA256.Create())
            using (var stream = File.OpenRead(path))
            {
                return BytesToHex(sha256.ComputeHash(stream));
            }
        }

        private static string BytesToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}
