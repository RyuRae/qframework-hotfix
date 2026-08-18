using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Framework.Luban.Editor
{
    public sealed class LubanBuildCenterWindow : EditorWindow
    {
        private static readonly string[] DataTargets = { "bin", "json", "bson", "msgpack", "lua" };
        private static readonly string[] CodeTargets = { "cs-bin", "cs-simple-json", "cs-newtonsoft-json", "cs-editor-json" };
        private LubanBuildProfile profile;
        private SerializedObject serializedProfile;
        private Vector2 scroll;
        private Vector2 logScroll;
        private int selectedTask;
        private bool showCommand = true;
        private bool showLog = true;

        [MenuItem("Build/Luban/数据构建中心...", false, 100)]
        public static void Open()
        {
            var window = GetWindow<LubanBuildCenterWindow>("Luban 数据构建中心");
            window.minSize = new Vector2(760, 650);
            window.Show();
        }

        private void OnEnable()
        {
            LubanBuildPipeline.Changed += OnPipelineChanged;
            profile = LubanProfileUtility.LoadOrCreateDefault();
            EnsureSerialized();
        }
        private void OnDisable() => LubanBuildPipeline.Changed -= OnPipelineChanged;
        private void OnPipelineChanged() => Repaint();

        private void OnGUI()
        {
            DrawHeader();
            if (profile == null) return;
            EnsureSerialized();
            serializedProfile.Update();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            DrawProfileSettings();
            DrawTaskTabs();
            if (profile.Tasks.Count > 0)
            {
                selectedTask = Mathf.Clamp(selectedTask, 0, profile.Tasks.Count - 1);
                DrawTask(profile.Tasks[selectedTask]);
            }
            DrawActions();
            DrawReport();
            EditorGUILayout.EndScrollView();
            if (serializedProfile.ApplyModifiedProperties()) SaveProfile();
        }

        private void DrawHeader()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("Luban 数据构建中心", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("选择生成任务和表格，配置格式与输出路径，然后校验或一键转换。配置保存在可提交 Git 的 Profile 中。", MessageType.Info);
            EditorGUI.BeginChangeCheck();
            profile = (LubanBuildProfile)EditorGUILayout.ObjectField("构建 Profile", profile, typeof(LubanBuildProfile), false);
            if (EditorGUI.EndChangeCheck()) { serializedProfile = null; EnsureSerialized(); }
        }

        private void DrawProfileSettings()
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("全局设置", EditorStyles.boldLabel);
            profile.LubanDllPath = DrawPath("Luban DLL", profile.LubanDllPath, false);
            profile.ValidateBeforeGenerate = EditorGUILayout.Toggle("生成前校验", profile.ValidateBeforeGenerate);
            profile.RefreshAssetDatabase = EditorGUILayout.Toggle("生成后刷新 Assets", profile.RefreshAssetDatabase);
            profile.SyncLocalizationCollectors = EditorGUILayout.Toggle("同步多语言 Collector", profile.SyncLocalizationCollectors);
            profile.CleanupStaleOutputs = EditorGUILayout.Toggle("清理过期产物", profile.CleanupStaleOutputs);
        }

        private void DrawTaskTabs()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.BeginHorizontal();
            string[] labels = profile.Tasks.Select(task => (task.Enabled ? "● " : "○ ") + task.Name).ToArray();
            if (labels.Length > 0) selectedTask = GUILayout.Toolbar(selectedTask, labels);
            if (GUILayout.Button("+", GUILayout.Width(28))) { profile.Tasks.Add(LubanProfileUtility.CreateGameTask()); selectedTask = profile.Tasks.Count - 1; SaveProfile(); }
            if (profile.Tasks.Count > 1 && GUILayout.Button("-", GUILayout.Width(28))) { profile.Tasks.RemoveAt(selectedTask); selectedTask = Mathf.Max(0, selectedTask - 1); SaveProfile(); }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTask(LubanBuildTask task)
        {
            EditorGUILayout.BeginVertical("box");
            task.Enabled = EditorGUILayout.Toggle("启用任务", task.Enabled);
            task.Name = EditorGUILayout.TextField("名称", task.Name);
            task.Category = (LubanTaskCategory)EditorGUILayout.EnumPopup("类别", task.Category);
            task.ConfigPath = DrawPath("Luban Config", task.ConfigPath, false);
            task.DataSourceRoot = DrawPath("数据源目录", task.DataSourceRoot, true);
            task.Target = EditorGUILayout.TextField("Target", task.Target);
            task.GenerateCode = EditorGUILayout.Toggle("生成代码", task.GenerateCode);
            using (new EditorGUI.DisabledScope(!task.GenerateCode))
            {
                task.CodeTarget = DrawPopupOrCustom("代码格式", task.CodeTarget, CodeTargets);
                task.CodeOutputDirectory = DrawPath("代码输出目录", task.CodeOutputDirectory, true);
                task.CleanCodeOutputBeforeGenerate = EditorGUILayout.Toggle("生成前清理旧代码", task.CleanCodeOutputBeforeGenerate);
            }
            task.GenerateData = EditorGUILayout.Toggle("生成数据", task.GenerateData);
            using (new EditorGUI.DisabledScope(!task.GenerateData))
            {
                task.DataTarget = DrawPopupOrCustom("数据格式", task.DataTarget, DataTargets);
                task.DataOutputDirectory = DrawPath("数据输出目录", task.DataOutputDirectory, true);
            }
            task.ValidationFailAsError = EditorGUILayout.Toggle("校验失败即停止", task.ValidationFailAsError);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("设为运行时 Bin", GUILayout.Width(110)))
            {
                task.GenerateCode = true;
                task.GenerateData = true;
                task.CodeTarget = "cs-bin";
                task.DataTarget = "bin";
                task.CleanCodeOutputBeforeGenerate = false;
            }
            if (GUILayout.Button("设为 JSON 数据导出", GUILayout.Width(140)))
            {
                task.GenerateCode = false;
                task.GenerateData = true;
                task.DataTarget = "json";
                task.DataOutputDirectory = "Assets/AssetsPackage/AssetsHotFix/Datas/json";
                task.CleanCodeOutputBeforeGenerate = false;
            }
            EditorGUILayout.EndHorizontal();
            if (task.GenerateCode && !string.Equals(task.CodeTarget, "cs-bin", StringComparison.OrdinalIgnoreCase))
            {
                EditorGUILayout.HelpBox(
                    "注意：不同 C# Target 默认生成相同命名空间和类型名。若工程中已存在 cs-bin 代码，" +
                    "请启用“生成前清理旧代码”并输出到同一运行时代码目录；如果只是需要查看 JSON，请关闭“生成代码”，仅生成 JSON 数据。",
                    MessageType.Warning);
            }
            DrawTables(task);
            if (task.Category == LubanTaskCategory.Localization) DrawLocalizationSummary(task);
            DrawCommandPreview(task);
            EditorGUILayout.EndVertical();
        }

        private void DrawTables(LubanBuildTask task)
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"输入表（{task.Tables.Count}）", EditorStyles.boldLabel);
            if (GUILayout.Button("扫描/刷新", GUILayout.Width(80))) Scan(task);
            if (GUILayout.Button("全选", GUILayout.Width(48))) task.Tables.ForEach(item => item.Enabled = true);
            if (GUILayout.Button("反选", GUILayout.Width(48))) task.Tables.ForEach(item => item.Enabled = !item.Enabled);
            EditorGUILayout.EndHorizontal();
            foreach (var table in task.Tables)
            {
                EditorGUILayout.BeginHorizontal();
                table.Enabled = EditorGUILayout.Toggle(table.Enabled, GUILayout.Width(18));
                EditorGUILayout.LabelField(table.Name, GUILayout.Width(150));
                EditorGUILayout.LabelField(table.InputPath);
                table.OutputTable = EditorGUILayout.TextField(table.OutputTable, GUILayout.Width(170));
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.HelpBox("OutputTable 留空时由当前任务一次生成全部表；填写 Luban 表名后会使用 -o 仅生成该表。", MessageType.None);
        }

        private void DrawLocalizationSummary(LubanBuildTask task)
        {
            string root = ProjectRoot;
            string catalog = Path.Combine(root, task.DataSourceRoot, "language_catalog.csv");
            string texts = Path.Combine(root, task.DataSourceRoot, "locale_text.csv");
            int locales = File.Exists(catalog) ? Math.Max(0, File.ReadAllLines(catalog).Count(line => line.StartsWith(","))) : 0;
            int rows = File.Exists(texts) ? Math.Max(0, File.ReadAllLines(texts).Count(line => line.StartsWith(","))) : 0;
            EditorGUILayout.HelpBox($"多语言状态：{locales} 个 Locale，{rows} 条翻译记录。生成后将按 Locale 拆包并同步 YooAsset Collector/Tag。", MessageType.Info);
        }

        private void DrawCommandPreview(LubanBuildTask task)
        {
            showCommand = EditorGUILayout.Foldout(showCommand, "命令预览", true);
            if (!showCommand) return;
            try
            {
                foreach (var command in LubanCommandBuilder.Build(ProjectRoot, profile, task))
                    EditorGUILayout.SelectableLabel(command.DisplayText, EditorStyles.textArea, GUILayout.MinHeight(38));
            }
            catch (Exception exception) { EditorGUILayout.HelpBox(exception.Message, MessageType.Error); }
        }

        private void DrawActions()
        {
            EditorGUILayout.Space(8);
            using (new EditorGUI.DisabledScope(LubanBuildPipeline.IsRunning))
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("只校验", GUILayout.Height(30))) ValidateProfile();
                if (GUILayout.Button("生成当前任务", GUILayout.Height(30))) Run(new[] { profile.Tasks[selectedTask] });
                if (GUILayout.Button("一键生成全部", GUILayout.Height(30))) Run(profile.Tasks.Where(task => task.Enabled).ToArray());
                EditorGUILayout.EndHorizontal();
            }
            if (LubanBuildPipeline.IsRunning && GUILayout.Button("取消生成", GUILayout.Height(26))) LubanBuildPipeline.Cancel();
        }

        private void DrawReport()
        {
            var report = LubanBuildPipeline.LastReport;
            if (report == null) return;
            showLog = EditorGUILayout.Foldout(showLog, $"最近结果：{(report.Succeeded ? "成功" : report.Canceled ? "已取消" : "失败")} / {report.DurationSeconds:F2}s", true);
            if (!showLog) return;
            logScroll = EditorGUILayout.BeginScrollView(logScroll, "box", GUILayout.MinHeight(130), GUILayout.MaxHeight(280));
            foreach (var result in report.Tasks)
            {
                EditorGUILayout.LabelField($"[{result.TaskName}] Exit={result.ExitCode}, {result.DurationSeconds:F2}s", result.Succeeded ? EditorStyles.boldLabel : EditorStyles.label);
                EditorGUILayout.SelectableLabel(result.Command ?? string.Empty, EditorStyles.textField, GUILayout.Height(32));
                EditorGUILayout.SelectableLabel(result.Output ?? string.Empty, EditorStyles.textArea, GUILayout.MinHeight(50));
            }
            EditorGUILayout.EndScrollView();
        }

        private string DrawPath(string label, string value, bool folder)
        {
            EditorGUILayout.BeginHorizontal();
            value = EditorGUILayout.TextField(label, value);
            if (GUILayout.Button("选择", GUILayout.Width(48)))
            {
                string selected = folder ? EditorUtility.OpenFolderPanel(label, LubanTableScanner.Resolve(ProjectRoot, value), string.Empty) : EditorUtility.OpenFilePanel(label, Path.GetDirectoryName(LubanTableScanner.Resolve(ProjectRoot, value)), string.Empty);
                if (!string.IsNullOrEmpty(selected)) value = LubanTableScanner.MakeRelative(ProjectRoot, selected).Replace('\\', '/');
            }
            if (GUILayout.Button("打开", GUILayout.Width(48))) EditorUtility.RevealInFinder(LubanTableScanner.Resolve(ProjectRoot, value));
            EditorGUILayout.EndHorizontal();
            return value;
        }

        private static string DrawPopupOrCustom(string label, string value, string[] options)
        {
            int index = Array.IndexOf(options, value);
            int selected = EditorGUILayout.Popup(label, index < 0 ? options.Length : index, options.Concat(new[] { "自定义" }).ToArray());
            return selected < options.Length ? options[selected] : EditorGUILayout.TextField(label + "（自定义）", value);
        }
        private void Scan(LubanBuildTask task) { task.Tables = LubanTableScanner.Scan(ProjectRoot, task); SaveProfile(); }
        private void ValidateProfile()
        {
            try
            {
                LubanProfileUtility.ValidateOrThrow(ProjectRoot, profile);
                if (profile.Tasks.Any(task => task.Enabled && task.Category == LubanTaskCategory.Localization))
                {
                    HybridCLR.Editor.LocalizationContentSynchronizer.SyncOrThrow();
                    HybridCLR.Editor.LocalizationBuildValidator.ValidateOrThrow();
                }
                EditorUtility.DisplayDialog("Luban", "Profile 和专项数据校验通过。", "确定");
            }
            catch (Exception e) { EditorUtility.DisplayDialog("Luban 校验失败", e.Message, "确定"); }
        }
        private void Run(LubanBuildTask[] tasks) { try { SaveProfile(); LubanBuildPipeline.Generate(profile, tasks); } catch (Exception e) { EditorUtility.DisplayDialog("Luban 生成失败", e.Message, "确定"); } }
        private void EnsureSerialized() { if (profile != null && (serializedProfile == null || serializedProfile.targetObject != profile)) serializedProfile = new SerializedObject(profile); }
        private void SaveProfile() { if (profile == null) return; EditorUtility.SetDirty(profile); AssetDatabase.SaveAssetIfDirty(profile); }
        private string ProjectRoot => Directory.GetParent(Application.dataPath).FullName;
    }
}
