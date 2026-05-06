using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    public sealed class HotfixBuildCenterWindow : EditorWindow
    {
        private static readonly string[] ModeLabels =
        {
            "首包构建",
            "热更包构建",
            "AOT 元数据补丁"
        };

        private HotfixBuildMode mMode = HotfixBuildMode.InitialPackage;
        private HotfixReleaseProfile mReleaseProfile;
        private HotfixBuildReport mReport;
        private Vector2 mScrollPosition;

        [MenuItem("Build/热更新/构建中心...", false, HotfixBuildMenuPriority.BuildCenter)]
        public static void Open()
        {
            var window = GetWindow<HotfixBuildCenterWindow>("热更新构建中心");
            window.minSize = new Vector2(560, 480);
            window.RefreshReport();
            window.Show();
        }

        private void OnEnable()
        {
            RefreshReport();
        }

        private void OnGUI()
        {
            DrawHeader();
            DrawReleaseProfile();
            DrawActions();
            DrawReport();
        }

        private void DrawHeader()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("热更新构建中心", EditorStyles.boldLabel);
            EditorGUILayout.Space(4);

            EditorGUI.BeginChangeCheck();
            int selected = GUILayout.Toolbar((int)mMode, ModeLabels);
            if (EditorGUI.EndChangeCheck())
            {
                mMode = (HotfixBuildMode)selected;
                RefreshReport();
            }

            EditorGUILayout.Space(6);
            EditorGUILayout.HelpBox(
                "推荐优先使用构建中心或一键构建入口。内部工具只用于框架维护、排障和分步验证。",
                MessageType.Info);
        }

        private void DrawReleaseProfile()
        {
            EditorGUILayout.Space(6);
            EditorGUI.BeginChangeCheck();
            mReleaseProfile = (HotfixReleaseProfile)EditorGUILayout.ObjectField(
                "ReleaseProfile",
                mReleaseProfile,
                typeof(HotfixReleaseProfile),
                false);
            if (EditorGUI.EndChangeCheck())
            {
                HotfixReleaseProfile.SaveSelectedProfile(mReleaseProfile);
                RefreshReport();
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("创建/绑定默认 Profile"))
            {
                mReleaseProfile = HotfixReleaseProfile.GetOrCreateDefault();
                HotfixReleaseProfile.SaveSelectedProfile(mReleaseProfile);
                RefreshReport();
            }

            using (new EditorGUI.DisabledScope(mReleaseProfile == null))
            {
                if (GUILayout.Button("保存当前配置"))
                {
                    mReleaseProfile.CaptureCurrentEditorSettings();
                    EditorUtility.SetDirty(mReleaseProfile);
                    AssetDatabase.SaveAssetIfDirty(mReleaseProfile);
                    RefreshReport();
                }

                if (GUILayout.Button("复制 Profile"))
                {
                    DuplicateReleaseProfile();
                }

                if (GUILayout.Button("导出 JSON"))
                {
                    ExportReleaseProfileJson();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawActions()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("仅校验", GUILayout.Height(30)))
            {
                RefreshReport();
            }

            if (GUILayout.Button("一键修复", GUILayout.Height(30)))
            {
                RunAction(() => mReport = HotfixBuildRunner.FixAll(mMode), "修复完成。");
            }

            using (new EditorGUI.DisabledScope(mReport != null && mReport.HasErrors))
            {
                if (GUILayout.Button("开始构建", GUILayout.Height(30)))
                {
                    RunAction(() => mReport = HotfixBuildRunner.Build(mMode), "构建完成。");
                }
            }

            EditorGUILayout.EndHorizontal();

            if (mReport != null && mReport.HasErrors)
            {
                EditorGUILayout.HelpBox(
                    "存在红色错误项，构建已被阻断。请修复后再执行构建。",
                    MessageType.Error);
            }
        }

        private void DrawReport()
        {
            if (mReport == null)
            {
                RefreshReport();
            }

            EditorGUILayout.Space(6);
            mScrollPosition = EditorGUILayout.BeginScrollView(mScrollPosition);
            foreach (var item in mReport.Items)
            {
                DrawReportItem(item);
            }

            EditorGUILayout.EndScrollView();
        }

        private static void DrawReportItem(HotfixBuildReportItem item)
        {
            MessageType messageType;
            switch (item.Severity)
            {
                case HotfixBuildReportSeverity.Error:
                    messageType = MessageType.Error;
                    break;
                case HotfixBuildReportSeverity.Warning:
                    messageType = MessageType.Warning;
                    break;
                default:
                    messageType = MessageType.Info;
                    break;
            }

            string value = string.IsNullOrWhiteSpace(item.Value) ? string.Empty : $": {item.Value}";
            string message = string.IsNullOrWhiteSpace(item.Message) ? string.Empty : $"\n{item.Message}";
            EditorGUILayout.HelpBox($"{item.Label}{value}{message}", messageType);
        }

        private void RefreshReport()
        {
            try
            {
                LoadSelectedReleaseProfile();
                mReport = HotfixBuildRunner.ValidateOnly(mMode);
            }
            catch (Exception exception)
            {
                mReport = new HotfixBuildReport();
                mReport.AddError("构建中心", "刷新失败", exception.Message);
                Debug.LogException(exception);
            }
        }

        private void LoadSelectedReleaseProfile()
        {
            if (mReleaseProfile != null)
            {
                return;
            }

            mReleaseProfile = HotfixReleaseProfile.LoadSelectedOrDefault();
        }

        private void DuplicateReleaseProfile()
        {
            string sourcePath = AssetDatabase.GetAssetPath(mReleaseProfile);
            string targetPath = EditorUtility.SaveFilePanelInProject(
                "复制 ReleaseProfile",
                $"{mReleaseProfile.name}_Copy",
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
            mReleaseProfile = AssetDatabase.LoadAssetAtPath<HotfixReleaseProfile>(targetPath);
            HotfixReleaseProfile.SaveSelectedProfile(mReleaseProfile);
            RefreshReport();
        }

        private void ExportReleaseProfileJson()
        {
            string path = EditorUtility.SaveFilePanel(
                "导出 ReleaseProfile JSON",
                Application.dataPath,
                $"{mReleaseProfile.name}.json",
                "json");
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            File.WriteAllText(path, mReleaseProfile.ToJson());
            ShowNotification(new GUIContent("ReleaseProfile 已导出。"));
        }

        private void RunAction(Action action, string notification)
        {
            try
            {
                action();
                ShowNotification(new GUIContent(notification));
            }
            catch (Exception exception)
            {
                RefreshReport();
                Debug.LogException(exception);
                EditorUtility.DisplayDialog("热更新构建", exception.Message, "确定");
            }
        }
    }
}
