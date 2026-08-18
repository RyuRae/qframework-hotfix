using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace Framework.Luban.Editor
{
    public static class LubanProfileUtility
    {
        public static LubanBuildProfile LoadOrCreateDefault()
        {
            var profile = AssetDatabase.LoadAssetAtPath<LubanBuildProfile>(LubanBuildProfile.DefaultAssetPath);
            if (profile != null) return profile;
            Directory.CreateDirectory(Path.GetDirectoryName(LubanBuildProfile.DefaultAssetPath));
            profile = ScriptableObject.CreateInstance<LubanBuildProfile>();
            profile.Tasks.Add(CreateGameTask());
            profile.Tasks.Add(CreateLocalizationTask());
            AssetDatabase.CreateAsset(profile, LubanBuildProfile.DefaultAssetPath);
            AssetDatabase.SaveAssets();
            return profile;
        }

        public static LubanBuildTask CreateGameTask() => new LubanBuildTask();
        public static LubanBuildTask CreateLocalizationTask() => new LubanBuildTask
        {
            Name = "Localization",
            Category = LubanTaskCategory.Localization,
            ConfigPath = "LubanConfig/Localization/luban.conf",
            DataSourceRoot = "LubanConfig/Localization/Datas",
            Target = "client",
            CodeTarget = "cs-bin",
            DataTarget = "bin",
            CodeOutputDirectory = "Assets/AssetsPackage/Scripts/Main/Runtime/Localization/Generated",
            DataOutputDirectory = "Assets/AssetsPackage/AssetsHotFix/Datas/Localization"
        };

        public static void ValidateOrThrow(string root, LubanBuildProfile profile)
        {
            RequireFile(LubanTableScanner.Resolve(root, profile.LubanDllPath), "Luban DLL");
            foreach (var task in profile.Tasks)
            {
                if (!task.Enabled) continue;
                RequireFile(LubanTableScanner.Resolve(root, task.ConfigPath), $"{task.Name} config");
                string dataRoot = LubanTableScanner.Resolve(root, task.DataSourceRoot);
                if (!Directory.Exists(dataRoot)) throw new BuildFailedException($"Luban data source directory not found: {dataRoot}");
                if (task.GenerateCode) ValidateOutput(root, task.CodeOutputDirectory);
                if (task.GenerateData) ValidateOutput(root, task.DataOutputDirectory);
                if (!task.GenerateCode && !task.GenerateData) throw new BuildFailedException($"Task '{task.Name}' generates neither code nor data.");
                ValidateGeneratedCodeIsolation(root, task);
            }
        }

        private static void ValidateGeneratedCodeIsolation(string root, LubanBuildTask task)
        {
            if (!task.GenerateCode || string.Equals(task.CodeTarget, "cs-bin", StringComparison.OrdinalIgnoreCase)) return;
            string output = LubanTableScanner.Resolve(root, task.CodeOutputDirectory);
            if (!task.CleanCodeOutputBeforeGenerate && Directory.Exists(output) && Directory.GetFiles(output, "*.cs", SearchOption.AllDirectories).Length > 0)
            {
                throw new BuildFailedException(
                    $"Task '{task.Name}' is switching to code target '{task.CodeTarget}', but the output directory already contains generated C# files. " +
                    "Enable 'Clean Code Output Before Generate' and use the same runtime code directory, or disable code generation when exporting JSON data.");
            }

            string parent = Directory.GetParent(output)?.FullName;
            if (string.IsNullOrWhiteSpace(parent) || !Directory.Exists(parent)) return;
            foreach (string sibling in Directory.GetDirectories(parent))
            {
                if (string.Equals(Path.GetFullPath(sibling), Path.GetFullPath(output), StringComparison.OrdinalIgnoreCase)) continue;
                if (Directory.GetFiles(sibling, "*.cs", SearchOption.AllDirectories).Length > 0)
                {
                    throw new BuildFailedException(
                        $"Another generated C# directory exists beside the selected output: {sibling}. " +
                        "Bin and JSON generated C# types use the same namespace/type names and cannot coexist in one Unity assembly. " +
                        "Remove the old directory, generate data only, or isolate it with a separate asmdef and namespace.");
                }
            }
        }

        private static void ValidateOutput(string root, string path)
        {
            string full = LubanTableScanner.Resolve(root, path);
            string assets = Path.GetFullPath(Application.dataPath);
            if (!full.StartsWith(assets, StringComparison.OrdinalIgnoreCase))
                throw new BuildFailedException($"For safety, Luban output must be under Assets: {path}");
            if (full.IndexOf(Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) >= 0)
                throw new BuildFailedException($"Invalid Luban output: {path}");
        }
        private static void RequireFile(string path, string label) { if (!File.Exists(path)) throw new BuildFailedException($"{label} not found: {path}"); }
    }
}
