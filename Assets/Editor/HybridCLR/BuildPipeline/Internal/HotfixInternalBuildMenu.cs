using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    public static class HotfixInternalBuildMenu
    {
        [MenuItem("Build/热更新/一键构建/构建首包", false, HotfixBuildMenuPriority.OneClickInitialPackage)]
        public static void BuildInitialPackage()
        {
            HotfixBuildRunner.Build(HotfixBuildMode.InitialPackage);
        }

        [MenuItem("Build/热更新/一键构建/构建热更包", false, HotfixBuildMenuPriority.OneClickHotfixPackage)]
        public static void BuildHotfixPackage()
        {
            HotfixBuildRunner.Build(HotfixBuildMode.HotfixPackage);
        }

        [MenuItem("Build/热更新/高级/构建 AOT 元数据补丁", false, HotfixBuildMenuPriority.AdvancedAOTMetadataPatch)]
        public static void BuildAOTMetadataPatch()
        {
            HotfixBuildRunner.Build(HotfixBuildMode.AOTMetadataPatch);
        }

        [MenuItem("Build/热更新/内部工具/校验运行时设置", false, HotfixBuildMenuPriority.InternalValidateRuntimeSettings)]
        public static void ValidateRuntimeSettings()
        {
            var report = HotfixBuildRunner.ValidateOnly(HotfixBuildMode.InitialPackage);
            if (report.HasErrors)
            {
                throw new InvalidOperationException(report.BuildErrorSummary());
            }

            Debug.Log("[HotfixBuild] 运行时设置校验通过。");
        }

        [MenuItem("Build/热更新/ReleaseProfile/创建或绑定默认 Profile", false, HotfixBuildMenuPriority.ReleaseProfile)]
        public static void CreateOrBindDefaultReleaseProfile()
        {
            var profile = HotfixReleaseProfile.GetOrCreateDefault();
            HotfixReleaseProfile.SaveSelectedProfile(profile);
            Selection.activeObject = profile;
            Debug.Log($"[HotfixReleaseProfile] Bound profile: {AssetDatabase.GetAssetPath(profile)}");
        }

        [MenuItem("Build/热更新/ReleaseProfile/保存当前配置到 Profile", false, HotfixBuildMenuPriority.ReleaseProfile + 1)]
        public static void SaveCurrentSettingsToReleaseProfile()
        {
            var profile = HotfixReleaseProfile.LoadSelectedOrDefault() ?? HotfixReleaseProfile.GetOrCreateDefault();
            profile.CaptureCurrentEditorSettings();
            EditorUtility.SetDirty(profile);
            AssetDatabase.SaveAssetIfDirty(profile);
            HotfixReleaseProfile.SaveSelectedProfile(profile);
            Selection.activeObject = profile;
            Debug.Log($"[HotfixReleaseProfile] Saved current settings to {AssetDatabase.GetAssetPath(profile)}");
        }

        [MenuItem("Build/热更新/ReleaseProfile/复制当前 Profile", false, HotfixBuildMenuPriority.ReleaseProfile + 2)]
        public static void DuplicateReleaseProfile()
        {
            var profile = HotfixReleaseProfile.LoadSelectedOrDefault() ?? HotfixReleaseProfile.GetOrCreateDefault();
            string sourcePath = AssetDatabase.GetAssetPath(profile);
            string targetPath = EditorUtility.SaveFilePanelInProject(
                "复制 ReleaseProfile",
                $"{profile.name}_Copy",
                "asset",
                "选择新的 ReleaseProfile 保存位置",
                Path.GetDirectoryName(sourcePath));
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                return;
            }

            AssetDatabase.CopyAsset(sourcePath, targetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            var copied = AssetDatabase.LoadAssetAtPath<HotfixReleaseProfile>(targetPath);
            HotfixReleaseProfile.SaveSelectedProfile(copied);
            Selection.activeObject = copied;
        }

        [MenuItem("Build/热更新/ReleaseProfile/导出当前 Profile JSON", false, HotfixBuildMenuPriority.ReleaseProfile + 3)]
        public static void ExportReleaseProfileJson()
        {
            var profile = HotfixReleaseProfile.LoadSelectedOrDefault() ?? HotfixReleaseProfile.GetOrCreateDefault();
            string path = EditorUtility.SaveFilePanel(
                "导出 ReleaseProfile JSON",
                Application.dataPath,
                $"{profile.name}.json",
                "json");
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            File.WriteAllText(path, profile.ToJson());
            Debug.Log($"[HotfixReleaseProfile] Exported JSON: {path}");
        }
    }
}
