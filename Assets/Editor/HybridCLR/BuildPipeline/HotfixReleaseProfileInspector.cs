using System;
using System.IO;
using System.Linq;
using Framework;
using Framework.Assemblies;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    [CustomEditor(typeof(HotfixReleaseProfile))]
    public sealed class HotfixReleaseProfileInspector : UnityEditor.Editor
    {
        private static readonly string[] ModeLabels =
        {
            "首包",
            "热更",
            "AOT补丁"
        };

        private HotfixBuildMode mMode = HotfixBuildMode.InitialPackage;
        private HotfixBuildReport mReport;

        public override void OnInspectorGUI()
        {
            var profile = (HotfixReleaseProfile)target;

            serializedObject.Update();
            DrawHeader(profile);
            DrawRequiredSettings();
            DrawOptionalSettings();
            ApplyProfileProperties(profile);
            DrawGeneratedState(profile);
            DrawActions(profile);
            DrawReport();
        }

        private void DrawHeader(HotfixReleaseProfile profile)
        {
            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("发布配置入口", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "带 * 的字段为发布前需要确认的配置。灰色区域为构建时自动生成或从底层 asset 派生的状态，不在这里直接编辑。底层 RuntimeSettings / RemoteSettings 仍会保留给运行时加载，但日常发布优先在这里完成。",
                MessageType.Info);

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextField("Profile Path", AssetDatabase.GetAssetPath(profile));
            }
        }

        private void DrawRequiredSettings()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("必填配置", EditorStyles.boldLabel);

            DrawRequiredProperty("BuildTarget", "BuildTarget");
            DrawRequiredProperty("AppVersion", "AppVersion");
            DrawRequiredProperty("AppVersionMin", "AppVersionMin");
            DrawRequiredProperty("AppVersionMax", "AppVersionMax");
            DrawRequiredProperty("RemoteEnvironment", "RemoteEnvironment");
            DrawRequiredProperty("Channel", "Channel");
            DrawRequiredProperty("Region", "Region");
            DrawRequiredProperty("AllowDevelopmentCdn", "AllowDevelopmentCdn");
            DrawRequiredProperty("MainCdnUrlTemplate", "MainCdnUrlTemplate");
            DrawRequiredProperty("FallbackCdnUrlTemplate", "FallbackCdnUrlTemplate");
            DrawRequiredProperty("RequireHttps", "RequireHttps");
            if (IsProductionSelected())
            {
                EditorGUILayout.LabelField("正式发布签名", EditorStyles.boldLabel);
                DrawRequiredProperty("ManifestSigningKeyId", "ManifestSigningKeyId");
                DrawRequiredProperty("ManifestPublicKeyModulus", "ManifestPublicKeyModulus");
                DrawRequiredProperty("ManifestPublicKeyExponent", "ManifestPublicKeyExponent");
                DrawRequiredProperty("ManifestPrivateKeyEnvironmentVariable", "ManifestPrivateKeyEnvironmentVariable");
            }
            DrawRequiredProperty("PlayerPlayMode", "PlayerPlayMode");
            DrawRequiredProperty("StartupPackageMode", "StartupPackageMode");
            DrawRequiredProperty("StartupDownloadMode", "StartupDownloadMode");
            DrawRequiredProperty("StartupUpdatePolicy", "StartupUpdatePolicy");

            var downloadMode = serializedObject.FindProperty("StartupDownloadMode");
            bool requiresTags = downloadMode != null &&
                                downloadMode.enumValueIndex == (int)StartupDownloadMode.DownloadByTags;
            DrawProperty(
                "StartupDownloadTags",
                requiresTags ? "* StartupDownloadTags" : "StartupDownloadTags",
                true);
            DrawProperty(
                "RawFileStartupDownloadTags",
                requiresTags ? "* RawFileStartupDownloadTags" : "RawFileStartupDownloadTags",
                true);

            DrawRequiredProperty("EntryTypeName", "EntryTypeName");
        }

        private void DrawOptionalSettings()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("可选覆盖 / 高级配置", EditorStyles.boldLabel);
            DrawProperty("ResourceVersion", IsProductionSelected() ? "* ResourceVersion" : "ResourceVersion");
            DrawProperty("ReleaseSequence", IsProductionSelected() ? "* ReleaseSequence" : "ReleaseSequence");
            DrawProperty("HotfixVersion", "HotfixVersion");
            DrawProperty("AllowedDomains", "AllowedDomains", true);
            DrawProperty("CertificatePinningEnabled", "CertificatePinningEnabled");
            DrawProperty("CertificatePublicKeyPin", "CertificatePublicKeyPin");
            DrawProperty("EnableGrayRelease", "EnableGrayRelease");
            DrawProperty("GrayReleasePercent", "GrayReleasePercent");
            DrawProperty("GrayMainCdnUrlTemplate", "GrayMainCdnUrlTemplate");
            DrawProperty("GrayFallbackCdnUrlTemplate", "GrayFallbackCdnUrlTemplate");
            DrawProperty("GrayReleaseSalt", "GrayReleaseSalt");
        }

        private void DrawGeneratedState(HotfixReleaseProfile profile)
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("自动生成 / 派生状态", EditorStyles.boldLabel);

            var context = HotfixBuildContext.Create(mMode);
            var runtimeSettings = context.RuntimeSettings;
            var aotManifest = context.AOTManifest;
            var hotfixManifest = context.HotfixManifest;

            DrawReadOnly("Active BuildTarget", $"{EditorUserBuildSettings.activeBuildTarget} / {context.BuildTargetName}");
            DrawReadOnly("Selected Build Mode", HotfixBuildModeUtility.GetDisplayName(mMode));
            DrawReadOnly("Synced PlayerPlayMode", ResolveSyncedPlayerPlayMode(profile));
            DrawReadOnly("MainPackageName", runtimeSettings == null ? "未创建 HotfixRuntimeSettings.asset" : runtimeSettings.MainPackageName);
            DrawReadOnly("RawFile Package", runtimeSettings == null
                ? "未创建 HotfixRuntimeSettings.asset"
                : $"{runtimeSettings.IncludeRawFilePackage} / {runtimeSettings.RawFilePackageName}");
            DrawReadOnly("RawFile StartupDownloadTags", runtimeSettings == null
                ? "未创建 HotfixRuntimeSettings.asset"
                : string.Join(", ", runtimeSettings.RawFileStartupDownloadTags));
            DrawReadOnly("Synced StartupPackageMode", runtimeSettings == null ? "未创建 HotfixRuntimeSettings.asset" : runtimeSettings.StartupPackageMode.ToString());
            DrawReadOnly("Synced StartupDownloadMode", runtimeSettings == null ? "未创建 HotfixRuntimeSettings.asset" : runtimeSettings.StartupDownloadMode.ToString());
            DrawReadOnly("Synced Remote Selector", ResolveSyncedRemoteSelector(profile));
            DrawReadOnly("Generated PackageVersion", FormatPackageVersion(profile));
            DrawReadOnly("Generated HotfixVersion", FormatHotfixVersion(profile, hotfixManifest));
            DrawReadOnly("Formal Protection", profile.IsFormalRelease ? "开启" : "未开启");
            DrawReadOnly("Resolved Main CDN", ResolveRemoteUrl(profile, runtimeSettings, true));
            DrawReadOnly("Resolved Fallback CDN", ResolveRemoteUrl(profile, runtimeSettings, false));
            DrawReadOnly("AotVersion", aotManifest == null ? "未生成" : aotManifest.AotVersion);
            DrawReadOnly("RequiredAotVersion", hotfixManifest == null ? "未生成" : hotfixManifest.RequiredAotVersion);
            DrawReadOnly("AOT Metadata Files", aotManifest == null ? "未生成" : CountFiles(aotManifest.AotMetadataFiles));
            DrawReadOnly("Hotfix DLL Files", hotfixManifest == null ? "未生成" : CountFiles(hotfixManifest.HotUpdateFiles));
            DrawReadOnly("Latest BuildReport", FindLatestBuildReport());
        }

        private void DrawActions(HotfixReleaseProfile profile)
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("操作", EditorStyles.boldLabel);

            mMode = (HotfixBuildMode)GUILayout.Toolbar((int)mMode, ModeLabels);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("绑定当前 Profile"))
            {
                SavePendingProfileEdits(profile);
                HotfixReleaseProfile.SaveSelectedProfile(profile);
                ShowNotification("已绑定当前 Profile。");
            }

            if (GUILayout.Button("应用 Profile 到底层 Settings"))
            {
                RunAction(
                    profile,
                    () =>
                    {
                        profile.ApplyToEditorSettings();
                        AssetDatabase.SaveAssets();
                    },
                    "已应用到 Settings。");
            }

            if (GUILayout.Button("从底层 Settings 覆盖 Profile"))
            {
                SavePendingProfileEdits(profile);
                Undo.RecordObject(profile, "Capture Hotfix Release Profile");
                profile.CaptureCurrentEditorSettings();
                EditorUtility.SetDirty(profile);
                AssetDatabase.SaveAssetIfDirty(profile);
                ShowNotification("已保存当前配置。");
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("只读检查"))
            {
                RefreshReport(profile);
            }

            if (GUILayout.Button("应用并自动修复"))
            {
                RunAction(profile, () => mReport = HotfixBuildRunner.FixAll(mMode), "修复完成。");
            }

            using (new EditorGUI.DisabledScope(mReport != null && mReport.HasErrors))
            {
                if (GUILayout.Button("开始构建"))
                {
                    RunAction(profile, () => mReport = HotfixBuildRunner.Build(mMode), "构建完成。");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("打开构建中心"))
            {
                HotfixBuildCenterWindow.Open();
            }

            if (GUILayout.Button("复制 Profile"))
            {
                SavePendingProfileEdits(profile);
                DuplicateProfile(profile);
            }

            if (GUILayout.Button("导出 JSON"))
            {
                SavePendingProfileEdits(profile);
                ExportJson(profile);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawReport()
        {
            if (mReport == null)
            {
                return;
            }

            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("校验 / 构建报告", EditorStyles.boldLabel);
            foreach (var item in mReport.Items)
            {
                string value = string.IsNullOrWhiteSpace(item.Value) ? string.Empty : $": {item.Value}";
                string message = string.IsNullOrWhiteSpace(item.Message) ? string.Empty : $"\n{item.Message}";
                EditorGUILayout.HelpBox($"{item.Label}{value}{message}", ToMessageType(item.Severity));
            }
        }

        private void DrawRequiredProperty(string propertyName, string label)
        {
            DrawProperty(propertyName, "* " + label);
        }

        private bool IsProductionSelected()
        {
            var environment = serializedObject.FindProperty("RemoteEnvironment");
            return environment != null &&
                   environment.enumValueIndex == (int)HotfixRemoteEnvironment.Production;
        }

        private void DrawProperty(string propertyName, string label, bool includeChildren = false)
        {
            var property = serializedObject.FindProperty(propertyName);
            if (property == null)
            {
                EditorGUILayout.HelpBox($"Serialized property missing: {propertyName}", MessageType.Warning);
                return;
            }

            var content = new GUIContent(
                label,
                string.IsNullOrWhiteSpace(property.tooltip) ? string.Empty : property.tooltip);
            EditorGUILayout.PropertyField(property, content, includeChildren);
        }

        private static void DrawReadOnly(string label, string value)
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextField(label, string.IsNullOrWhiteSpace(value) ? "空" : value);
            }
        }

        private void RefreshReport(HotfixReleaseProfile profile)
        {
            RunAction(
                profile,
                () =>
                {
                    mReport = HotfixBuildRunner.ValidateOnly(mMode);
                },
                "校验完成。");
        }

        private void RunAction(HotfixReleaseProfile profile, Action action, string message)
        {
            try
            {
                SavePendingProfileEdits(profile);
                HotfixReleaseProfile.SaveSelectedProfile(profile);
                action();
                ShowNotification(message);
            }
            catch (Exception exception)
            {
                mReport = new HotfixBuildReport();
                mReport.AddError("ReleaseProfile", "执行失败", exception.Message);
                Debug.LogException(exception);
                EditorUtility.DisplayDialog("Hotfix ReleaseProfile", exception.Message, "确定");
            }
        }

        private void ApplyProfileProperties(HotfixReleaseProfile profile)
        {
            if (serializedObject.ApplyModifiedProperties())
            {
                EditorUtility.SetDirty(profile);
                mReport = null;
            }
        }

        private void SavePendingProfileEdits(HotfixReleaseProfile profile)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(profile);
            AssetDatabase.SaveAssetIfDirty(profile);
        }

        private static void DuplicateProfile(HotfixReleaseProfile profile)
        {
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

        private static void ExportJson(HotfixReleaseProfile profile)
        {
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

        private void ShowNotification(string message)
        {
            if (EditorWindow.focusedWindow != null)
            {
                EditorWindow.focusedWindow.ShowNotification(new GUIContent(message));
            }
        }

        private static MessageType ToMessageType(HotfixBuildReportSeverity severity)
        {
            switch (severity)
            {
                case HotfixBuildReportSeverity.Error:
                    return MessageType.Error;
                case HotfixBuildReportSeverity.Warning:
                    return MessageType.Warning;
                default:
                    return MessageType.Info;
            }
        }

        private static string ResolveSyncedPlayerPlayMode(HotfixReleaseProfile profile)
        {
            var buildProfile = AssetDatabase.LoadAssetAtPath<HotfixBuildProfile>(HotfixBuildProfile.AssetPath);
            if (buildProfile == null)
            {
                return $"未创建 HotfixBuildProfile.asset / Profile={profile.PlayerPlayMode}";
            }

            var syncedPlayMode = buildProfile.GetPlayMode(profile.BuildTarget);
            return syncedPlayMode == profile.PlayerPlayMode
                ? syncedPlayMode.ToString()
                : $"{syncedPlayMode} / 等待应用 Profile={profile.PlayerPlayMode}";
        }

        private static string FormatPackageVersion(HotfixReleaseProfile profile)
        {
            return string.IsNullOrWhiteSpace(profile.ResourceVersion)
                ? $"建议值 {HotfixReleaseProfile.CreateSuggestedResourceVersion()}"
                : profile.ResourceVersion.Trim();
        }

        private static string FormatHotfixVersion(
            HotfixReleaseProfile profile,
            HotfixAssemblyManifest hotfixManifest)
        {
            if (!string.IsNullOrWhiteSpace(profile.HotfixVersion))
            {
                return profile.HotfixVersion.Trim();
            }

            return hotfixManifest == null || string.IsNullOrWhiteSpace(hotfixManifest.HotfixVersion)
                ? "构建时按 DLL hash 自动生成"
                : hotfixManifest.HotfixVersion;
        }

        private static string ResolveRemoteUrl(
            HotfixReleaseProfile profile,
            HotfixRuntimeSettings runtimeSettings,
            bool mainUrl)
        {
            string template = mainUrl ? profile.MainCdnUrlTemplate : profile.FallbackCdnUrlTemplate;
            if (string.IsNullOrWhiteSpace(template))
            {
                return $"未配置 {profile.RemoteEnvironment} {(mainUrl ? "主" : "备用")} CDN";
            }

            string packageName = runtimeSettings == null
                ? HotfixRuntimeSettings.DefaultMainPackageName
                : runtimeSettings.MainPackageName;
            return ReplaceTokens(
                template,
                profile.RemoteEnvironment,
                HotfixUtility.GetPlatformNameForBuildTarget(profile.BuildTarget),
                NormalizeSelector(profile.Channel),
                NormalizeSelector(profile.Region),
                packageName).Trim().TrimEnd('/');
        }

        private static string ReplaceTokens(
            string template,
            HotfixRemoteEnvironment environment,
            string platform,
            string channel,
            string region,
            string packageName)
        {
            return (template ?? string.Empty)
                .Replace("{Environment}", environment.ToString())
                .Replace("{Platform}", platform)
                .Replace("{Channel}", channel)
                .Replace("{Region}", region)
                .Replace("{PackageName}", packageName);
        }

        private static string NormalizeSelector(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "default" : value.Trim();
        }

        private static string ResolveSyncedRemoteSelector(HotfixReleaseProfile profile)
        {
            var remoteSettings = AssetDatabase.LoadAssetAtPath<HotfixRemoteSettings>(
                HotfixBuildProfileUtility.RemoteSettingsAssetPath);
            if (remoteSettings == null)
            {
                return "未创建 HotfixRemoteSettings.asset";
            }

            string selector = $"{remoteSettings.DefaultEnvironment} / {remoteSettings.DefaultChannel} / {remoteSettings.DefaultRegion}";
            string profileSelector = $"{profile.RemoteEnvironment} / {NormalizeSelector(profile.Channel)} / {NormalizeSelector(profile.Region)}";
            return string.Equals(selector, profileSelector, StringComparison.OrdinalIgnoreCase)
                ? selector
                : $"{selector} / 等待应用 Profile={profileSelector}";
        }

        private static string CountFiles<T>(System.Collections.Generic.ICollection<T> files)
        {
            return files == null ? "0" : files.Count.ToString();
        }

        private static string FindLatestBuildReport()
        {
            string directory = Path.Combine(Application.dataPath, "..", "BuildReports", "Hotfix");
            if (!Directory.Exists(directory))
            {
                return "未生成";
            }

            string path = Directory.GetFiles(directory, "*.txt")
                .OrderByDescending(File.GetLastWriteTimeUtc)
                .FirstOrDefault();
            return string.IsNullOrWhiteSpace(path) ? "未生成" : path;
        }
    }
}
