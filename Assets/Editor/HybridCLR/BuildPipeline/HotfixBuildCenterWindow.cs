using System;
using System.IO;
using System.Linq;
using Framework;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    /// <summary>
    /// 面向日常发布的热更新构建中心，统一 Profile 编辑、只读校验、风险确认、构建进度与产物交付。
    /// </summary>
    public sealed class HotfixBuildCenterWindow : EditorWindow
    {
        private static readonly string[] TaskLabels =
        {
            "构建首包资源",
            "构建热更资源"
        };

        private static readonly string[] FlavorLabels =
        {
            "开发",
            "测试",
            "预发",
            "正式"
        };

        private HotfixBuildMode mMode = HotfixBuildMode.InitialPackage;
        private HotfixReleaseProfile mReleaseProfile;
        private SerializedObject mSerializedProfile;
        private HotfixBuildReport mReport;
        private Vector2 mScrollPosition;
        private bool mShowCompatibility;
        private bool mShowStartupAdvanced;
        private bool mShowRequiredFixes = true;
        private bool mShowRecommendedFixes = true;
        private bool mShowBuildInfo;
        private bool mShowAdvancedTools;
        private bool mIsBuilding;
        private string mBuildStage = string.Empty;
        private string mBuildOutputDirectory = string.Empty;
        private DateTime mBuildStartedAt;
        private double mLastBuildDurationSeconds;
        private HotfixBuildExecutionResult mLastExecutionResult;

        /// <summary>打开构建中心并刷新当前 ReleaseProfile 的只读校验报告。</summary>
        [MenuItem("Build/热更新/构建中心...", false, HotfixBuildMenuPriority.BuildCenter)]
        public static void Open()
        {
            var window = GetWindow<HotfixBuildCenterWindow>("热更新构建中心");
            window.minSize = new Vector2(680, 640);
            window.RefreshReport();
            window.Show();
        }

        private void OnEnable()
        {
            HotfixBuildRunner.ProgressChanged += OnBuildProgressChanged;
            LoadSelectedReleaseProfile();
            EnsureResourceVersionPrefilled();
            RefreshReport();
        }

        private void OnDisable()
        {
            HotfixBuildRunner.ProgressChanged -= OnBuildProgressChanged;
            EditorUtility.ClearProgressBar();
        }

        private void OnGUI()
        {
            DrawHeader();
            DrawReleaseProfile();
            if (mReleaseProfile == null)
            {
                return;
            }

            EnsureSerializedProfile();
            mSerializedProfile.Update();
            mScrollPosition = EditorGUILayout.BeginScrollView(mScrollPosition);
            DrawBuildTask();
            DrawFlavorPreset();
            DrawCoreSettings();
            DrawActions();
            DrawReport();
            DrawAdvancedTools();
            EditorGUILayout.EndScrollView();

            if (mSerializedProfile.ApplyModifiedProperties())
            {
                EditorUtility.SetDirty(mReleaseProfile);
                AssetDatabase.SaveAssetIfDirty(mReleaseProfile);
                mReport = null;
                Repaint();
            }
        }

        private void DrawHeader()
        {
            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("热更新构建中心", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "按 ①任务 → ②环境 → ③核心配置 → ④检查并构建 完成发布。" +
                "“只读检查”不会修改 RuntimeSettings、RemoteSettings、Manifest 或 PlayerSettings。",
                MessageType.Info);
            DrawReportSummary();
            DrawBuildStatus();
        }

        private void DrawReleaseProfile()
        {
            EditorGUILayout.Space(4);
            EditorGUI.BeginChangeCheck();
            var selected = (HotfixReleaseProfile)EditorGUILayout.ObjectField(
                "发布配置",
                mReleaseProfile,
                typeof(HotfixReleaseProfile),
                false);
            if (EditorGUI.EndChangeCheck())
            {
                mReleaseProfile = selected;
                mSerializedProfile = null;
                HotfixReleaseProfile.SaveSelectedProfile(mReleaseProfile);
                EnsureResourceVersionPrefilled();
                RefreshReport();
            }

            if (mReleaseProfile != null)
            {
                return;
            }

            EditorGUILayout.HelpBox("尚未绑定发布配置。先创建默认 Profile 即可开始。", MessageType.Warning);
            if (GUILayout.Button("创建并绑定默认发布配置", GUILayout.Height(34)))
            {
                mReleaseProfile = HotfixReleaseProfile.GetOrCreateDefault();
                HotfixReleaseProfile.SaveSelectedProfile(mReleaseProfile);
                EnsureResourceVersionPrefilled();
                EnsureSerializedProfile();
                RefreshReport();
            }
        }

        private void DrawBuildTask()
        {
            DrawSectionTitle("① 选择构建任务");
            if (mMode == HotfixBuildMode.AOTMetadataPatch)
            {
                EditorGUILayout.HelpBox(
                    "当前为高级任务：AOT 元数据补丁。只允许在同一 App 基线下补充泛型元数据。",
                    MessageType.Warning);
                if (GUILayout.Button("返回日常资源构建"))
                {
                    mMode = HotfixBuildMode.HotfixPackage;
                    RefreshReport();
                }
                return;
            }

            int selectedTask = mMode == HotfixBuildMode.HotfixPackage ? 1 : 0;
            EditorGUI.BeginChangeCheck();
            selectedTask = GUILayout.Toolbar(selectedTask, TaskLabels, GUILayout.Height(30));
            if (EditorGUI.EndChangeCheck())
            {
                mMode = selectedTask == 0
                    ? HotfixBuildMode.InitialPackage
                    : HotfixBuildMode.HotfixPackage;
                RefreshReport();
            }

            EditorGUILayout.HelpBox(
                mMode == HotfixBuildMode.InitialPackage
                    ? "用于新 App 基线：生成 AOT/热更 DLL、Manifest 和 YooAsset 资源；根据启动包策略决定是否复制到 StreamingAssets。本操作不构建 Player。"
                    : "用于已有 App 基线：复用现有 AOT Manifest，重新编译热更 DLL并生成可上传到 CDN 的差异资源。本操作不构建 Player。",
                MessageType.None);
        }

        private void DrawFlavorPreset()
        {
            DrawSectionTitle("② 选择发布环境");
            int selectedFlavor = Mathf.Clamp((int)mReleaseProfile.BuildFlavor, 0, FlavorLabels.Length - 1);
            EditorGUI.BeginChangeCheck();
            selectedFlavor = GUILayout.Toolbar(selectedFlavor, FlavorLabels, GUILayout.Height(28));
            if (EditorGUI.EndChangeCheck())
            {
                mSerializedProfile.ApplyModifiedProperties();
                Undo.RecordObject(mReleaseProfile, "Apply Hotfix Build Flavor Preset");
                mReleaseProfile.ApplyBuildFlavorPreset((HotfixBuildFlavor)selectedFlavor);
                EditorUtility.SetDirty(mReleaseProfile);
                AssetDatabase.SaveAssetIfDirty(mReleaseProfile);
                mSerializedProfile.Update();
                mReport = null;
            }

            string flavorHint;
            MessageType flavorMessageType = MessageType.Info;
            switch ((HotfixBuildFlavor)selectedFlavor)
            {
                case HotfixBuildFlavor.Development:
                    flavorHint = "开发：允许开发 CDN；Development Build/Editor 打印完整 LogKit 日志。";
                    break;
                case HotfixBuildFlavor.Testing:
                    flavorHint = "测试：使用 Testing CDN；适合 QA 和自动化包，日志由 Player 的 Development Build 决定。";
                    break;
                case HotfixBuildFlavor.Staging:
                    flavorHint = "预发：关闭 Development CDN，要求 HTTPS，建议使用接近正式环境的配置。";
                    break;
                default:
                    flavorHint = "正式：强制 Production、HTTPS、固定资源版本、递增序号和 Manifest 签名；正式包仅输出 Error/Exception。";
                    flavorMessageType = MessageType.Warning;
                    break;
            }

            EditorGUILayout.HelpBox(flavorHint, flavorMessageType);
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextField(
                    "Player 日志策略",
                    mReleaseProfile.UsesDevelopmentBuild ? "完整日志（Development Build）" : "仅 Error / Exception");
            }
        }

        private void DrawCoreSettings()
        {
            DrawSectionTitle("③ 确认核心配置");

            DrawProperty("BuildTarget", "目标平台");
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.EnumPopup("Unity 当前平台", EditorUserBuildSettings.activeBuildTarget);
            }

            var buildTargetProperty = FindProperty("BuildTarget");
            if (buildTargetProperty.intValue != (int)EditorUserBuildSettings.activeBuildTarget)
            {
                EditorGUILayout.HelpBox("Profile 目标平台与 Unity 当前平台不一致，构建会被阻断。", MessageType.Error);
                if (GUILayout.Button("将 Profile 目标平台设为当前平台"))
                {
                    buildTargetProperty.intValue = (int)EditorUserBuildSettings.activeBuildTarget;
                }
            }

            DrawProperty("AppVersion", "App 版本");
            DrawResourceVersion();

            mShowCompatibility = EditorGUILayout.Foldout(mShowCompatibility, "App 兼容范围", true);
            if (mShowCompatibility)
            {
                DrawProperty("AppVersionMin", "最低兼容 App");
                DrawProperty("AppVersionMax", "最高兼容 App");
            }

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("CDN", EditorStyles.boldLabel);
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.EnumPopup("远端环境", mReleaseProfile.RemoteEnvironment);
            }
            DrawProperty("Channel", "渠道");
            DrawProperty("Region", "地区");
            DrawProperty("MainCdnUrlTemplate", "主 CDN");
            DrawProperty("FallbackCdnUrlTemplate", "备用 CDN");

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("启动", EditorStyles.boldLabel);
            DrawProperty("StartupPackageMode", "启动包策略");
            DrawProperty("StartupDownloadMode", "启动下载模式");
            DrawProperty("StartupUpdatePolicy", "更新失败策略");
            DrawProperty("EntryTypeName", "热更入口类型");

            var downloadMode = FindProperty("StartupDownloadMode");
            if (downloadMode.enumValueIndex == (int)StartupDownloadMode.DownloadByTags)
            {
                DrawProperty("StartupDownloadTags", "主包启动 Tags", true);
                DrawProperty("RawFileStartupDownloadTags", "RawFile 启动 Tags", true);
            }

            mShowStartupAdvanced = EditorGUILayout.Foldout(mShowStartupAdvanced, "启动高级选项", true);
            if (mShowStartupAdvanced)
            {
                DrawProperty("PlayerPlayMode", "Player YooAsset 模式");
                DrawProperty("HotfixVersion", "热更版本覆盖（通常留空）");
            }

            var flavor = mReleaseProfile.BuildFlavor;
            if (flavor == HotfixBuildFlavor.Production)
            {
                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("正式发布保护", EditorStyles.boldLabel);
                DrawProperty("ReleaseSequence", "发布序号");
                DrawProperty("RequireHttps", "强制 HTTPS");
                DrawProperty("AllowedDomains", "CDN 域名白名单", true);
                DrawProperty("ManifestSigningKeyId", "签名 KeyId");
                DrawProperty("ManifestPublicKeyModulus", "RSA 公钥 Modulus");
                DrawProperty("ManifestPublicKeyExponent", "RSA 公钥 Exponent");
                DrawProperty("ManifestPrivateKeyEnvironmentVariable", "私钥环境变量名");
            }
        }

        private void DrawActions()
        {
            DrawSectionTitle("④ 检查并构建");
            if (mReport == null)
            {
                EditorGUILayout.HelpBox("核心配置已修改。请先执行只读检查。", MessageType.Warning);
            }
            else
            {
                MessageType summaryType = mReport.HasErrors
                    ? MessageType.Error
                    : mReport.WarningCount > 0 ? MessageType.Warning : MessageType.Info;
                EditorGUILayout.HelpBox(
                    $"检查结果：{mReport.ErrorCount} 个错误，{mReport.WarningCount} 个警告，{mReport.InfoCount} 项信息。",
                    summaryType);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("只读检查", GUILayout.Height(34)))
            {
                SaveProfileEdits();
                RefreshReport();
            }

            if (GUILayout.Button("应用并自动修复", GUILayout.Height(34)))
            {
                SaveProfileEdits();
                if (ConfirmApplySettings())
                {
                    RunAction(() => mReport = HotfixBuildRunner.FixAll(mMode), "配置已应用并完成自动修复。");
                }
            }

            bool canBuild = mReport != null && !mReport.HasErrors && !mIsBuilding;
            using (new EditorGUI.DisabledScope(!canBuild))
            {
                if (GUILayout.Button(GetBuildButtonLabel(), GUILayout.Height(34)))
                {
                    SaveProfileEdits();
                    if (ConfirmBuild())
                    {
                        RunAction(
                            () =>
                            {
                                mReport = HotfixBuildRunner.Build(mMode);
                                mLastExecutionResult = HotfixBuildRunner.LastExecutionResult;
                                if (mLastExecutionResult != null)
                                {
                                    mLastBuildDurationSeconds = mLastExecutionResult.DurationSeconds;
                                }
                            },
                            "资源构建完成。");
                    }
                }
            }

            EditorGUILayout.EndHorizontal();
            if (!canBuild)
            {
                EditorGUILayout.LabelField("构建按钮将在只读检查无错误后启用。", EditorStyles.miniLabel);
            }

            if (GUILayout.Button("打开 Unity Build Settings（构建 Player）"))
            {
                EditorApplication.ExecuteMenuItem("File/Build Settings...");
            }
        }

        private void DrawReport()
        {
            if (mReport == null)
            {
                return;
            }

            DrawReportGroup(
                "必须修复",
                mReport.ErrorCount,
                HotfixBuildReportSeverity.Error,
                ref mShowRequiredFixes,
                MessageType.Error);
            DrawReportGroup(
                "建议修复",
                mReport.WarningCount,
                HotfixBuildReportSeverity.Warning,
                ref mShowRecommendedFixes,
                MessageType.Warning);
            DrawReportGroup(
                "构建信息",
                mReport.InfoCount,
                HotfixBuildReportSeverity.Info,
                ref mShowBuildInfo,
                MessageType.Info);

            DrawBuildArtifacts();

            if (mReport.HasErrors && GUILayout.Button("在 Inspector 中打开高级 Profile 定位"))
            {
                Selection.activeObject = mReleaseProfile;
                EditorGUIUtility.PingObject(mReleaseProfile);
            }
        }

        private void DrawAdvancedTools()
        {
            EditorGUILayout.Space(8);
            mShowAdvancedTools = EditorGUILayout.Foldout(mShowAdvancedTools, "高级工具（框架维护/特殊发布）", true);
            if (!mShowAdvancedTools)
            {
                return;
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("打开完整 Profile Inspector"))
            {
                Selection.activeObject = mReleaseProfile;
                EditorGUIUtility.PingObject(mReleaseProfile);
            }

            if (GUILayout.Button("复制 Profile"))
            {
                SaveProfileEdits();
                DuplicateReleaseProfile();
            }

            if (GUILayout.Button("导出 JSON"))
            {
                SaveProfileEdits();
                ExportReleaseProfileJson();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.HelpBox("AOT 元数据补丁只用于同一 App 基线补充泛型元数据。修改 AOT 逻辑、公共接口、原生 SDK 或 PlayerSettings 时必须发布新 App。", MessageType.Warning);
            if (GUILayout.Button("切换到 AOT 元数据补丁任务"))
            {
                mMode = HotfixBuildMode.AOTMetadataPatch;
                RefreshReport();
            }
        }

        private bool ConfirmApplySettings()
        {
            return EditorUtility.DisplayDialog(
                "应用发布配置",
                "将把当前 Profile 同步到：\n" +
                "• PlayerSettings.bundleVersion\n" +
                "• HotfixRuntimeSettings\n" +
                "• HotfixRemoteSettings\n" +
                "• HotfixBuildProfile\n" +
                "• HotfixAssemblyManifest 入口与兼容字段\n\n" +
                "同时会从 YooAsset Collector 同步包名。是否继续？",
                "应用并修复",
                "取消");
        }

        private bool ConfirmBuild()
        {
            string resourceVersion = string.IsNullOrWhiteSpace(mReleaseProfile.ResourceVersion)
                ? "自动时间戳"
                : mReleaseProfile.ResourceVersion.Trim();
            string message =
                $"任务：{HotfixBuildModeUtility.GetDisplayName(mMode)}\n" +
                $"环境：{mReleaseProfile.BuildFlavor} / {mReleaseProfile.RemoteEnvironment}\n" +
                $"平台：{mReleaseProfile.BuildTarget}\n" +
                $"App：{mReleaseProfile.AppVersion}\n" +
                $"资源版本：{resourceVersion}\n" +
                $"主 CDN：{mReleaseProfile.MainCdnUrlTemplate}\n" +
                $"备用 CDN：{mReleaseProfile.FallbackCdnUrlTemplate}\n\n" +
                "构建会先应用 Profile，然后编译 DLL、更新 Manifest 并构建 YooAsset 资源。是否继续？";
            if (!EditorUtility.DisplayDialog("确认资源构建", message, "继续", "取消"))
            {
                return false;
            }

            if (!mReleaseProfile.IsFormalRelease)
            {
                return true;
            }

            string signingKey = string.IsNullOrWhiteSpace(mReleaseProfile.ManifestSigningKeyId)
                ? "缺失"
                : mReleaseProfile.ManifestSigningKeyId.Trim();
            string signingPrivateKey = string.IsNullOrWhiteSpace(mReleaseProfile.ManifestPrivateKeyEnvironmentVariable)
                ? "缺失"
                : mReleaseProfile.ManifestPrivateKeyEnvironmentVariable.Trim();
            string sequence = mReleaseProfile.ReleaseSequence.ToString();
            string formalMessage =
                "正式发布最终确认\n\n" +
                $"环境：{mReleaseProfile.RemoteEnvironment}（正式）\n" +
                $"App 版本：{mReleaseProfile.AppVersion}\n" +
                $"ResourceVersion / PackageVersion：{resourceVersion}\n" +
                $"主 CDN：{mReleaseProfile.MainCdnUrlTemplate}\n" +
                $"备用 CDN：{mReleaseProfile.FallbackCdnUrlTemplate}\n" +
                $"签名：KeyId={signingKey} / 私钥变量={signingPrivateKey}\n" +
                $"ReleaseSequence：{sequence}\n\n" +
                "请确认环境、版本、CDN、签名配置和发布序号均正确。正式构建产物将用于发布，是否继续？";
            return EditorUtility.DisplayDialog("正式发布最终确认", formalMessage, "确认并构建", "返回修改");
        }

        private string GetBuildButtonLabel()
        {
            switch (mMode)
            {
                case HotfixBuildMode.InitialPackage:
                    return "构建首包资源";
                case HotfixBuildMode.HotfixPackage:
                    return "构建热更资源";
                case HotfixBuildMode.AOTMetadataPatch:
                    return "构建 AOT 元数据补丁";
                default:
                    return "开始构建";
            }
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
                mReport.AddError("构建中心", "检查失败", exception.Message);
                Debug.LogException(exception);
            }
        }

        private void LoadSelectedReleaseProfile()
        {
            if (mReleaseProfile == null)
            {
                mReleaseProfile = HotfixReleaseProfile.LoadSelectedOrDefault();
                mSerializedProfile = null;
            }
        }

        private void EnsureSerializedProfile()
        {
            if (mSerializedProfile == null || mSerializedProfile.targetObject != mReleaseProfile)
            {
                mSerializedProfile = new SerializedObject(mReleaseProfile);
            }
        }

        private void EnsureResourceVersionPrefilled()
        {
            if (mReleaseProfile == null ||
                !string.IsNullOrWhiteSpace(mReleaseProfile.ResourceVersion))
            {
                return;
            }

            Undo.RecordObject(mReleaseProfile, "Prefill YooAsset Resource Version");
            mReleaseProfile.ResourceVersion = HotfixReleaseProfile.CreateSuggestedResourceVersion();
            EditorUtility.SetDirty(mReleaseProfile);
            AssetDatabase.SaveAssetIfDirty(mReleaseProfile);
            mSerializedProfile = null;
        }

        private SerializedProperty FindProperty(string propertyName)
        {
            return mSerializedProfile.FindProperty(propertyName);
        }

        private void DrawProperty(string propertyName, string label, bool includeChildren = false)
        {
            var property = FindProperty(propertyName);
            if (property == null)
            {
                EditorGUILayout.HelpBox($"Profile 字段缺失：{propertyName}", MessageType.Error);
                return;
            }

            EditorGUILayout.PropertyField(
                property,
                new GUIContent(label, property.tooltip),
                includeChildren);
        }

        private void DrawResourceVersion()
        {
            var property = FindProperty("ResourceVersion");
            if (property == null)
            {
                EditorGUILayout.HelpBox("Profile 字段缺失：ResourceVersion", MessageType.Error);
                return;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(
                property,
                new GUIContent("资源版本 / PackageVersion", property.tooltip));
            if (GUILayout.Button("重新生成", GUILayout.Width(82)))
            {
                property.stringValue = HotfixReleaseProfile.CreateSuggestedResourceVersion();
                mReport = null;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField(
                "将作为 YooAsset 主包与 RawFile 包的共同 PackageVersion；可直接修改。",
                EditorStyles.miniLabel);
        }

        private static void DrawSectionTitle(string title)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
        }

        private static void DrawReportItem(HotfixBuildReportItem item, MessageType typeOverride = MessageType.None)
        {
            MessageType type = typeOverride != MessageType.None
                ? typeOverride
                : item.Severity == HotfixBuildReportSeverity.Error
                ? MessageType.Error
                : item.Severity == HotfixBuildReportSeverity.Warning
                    ? MessageType.Warning
                    : MessageType.Info;
            string value = string.IsNullOrWhiteSpace(item.Value) ? string.Empty : $"：{item.Value}";
            string message = string.IsNullOrWhiteSpace(item.Message) ? string.Empty : $"\n{item.Message}";
            EditorGUILayout.HelpBox($"{item.Label}{value}{message}", type);
        }

        private void SaveProfileEdits()
        {
            EnsureSerializedProfile();
            mSerializedProfile.ApplyModifiedProperties();
            EditorUtility.SetDirty(mReleaseProfile);
            AssetDatabase.SaveAssetIfDirty(mReleaseProfile);
            HotfixReleaseProfile.SaveSelectedProfile(mReleaseProfile);
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
            mSerializedProfile = new SerializedObject(mReleaseProfile);
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
            bool isBuildAction = notification == "资源构建完成。";
            if (isBuildAction)
            {
                mIsBuilding = true;
                mBuildStage = "准备构建";
                mBuildOutputDirectory = string.Empty;
                mBuildStartedAt = DateTime.UtcNow;
                mLastExecutionResult = null;
                mLastBuildDurationSeconds = 0d;
            }

            try
            {
                action();
                ShowNotification(new GUIContent(notification));
                if (isBuildAction &&
                    mLastExecutionResult != null &&
                    mLastExecutionResult.IsAOTMetadataPatch)
                {
                    ShowAOTMetadataPatchResult(mLastExecutionResult);
                }
            }
            catch (Exception exception)
            {
                RefreshReport();
                Debug.LogException(exception);
                EditorUtility.DisplayDialog("热更新构建", exception.Message, "确定");
            }
            finally
            {
                if (isBuildAction)
                {
                    mIsBuilding = false;
                    EditorUtility.ClearProgressBar();
                    Repaint();
                }
            }
        }

        private void OnBuildProgressChanged(string stage, float progress, string outputDirectory)
        {
            mBuildStage = stage ?? string.Empty;
            mBuildOutputDirectory = outputDirectory ?? string.Empty;
            if (mIsBuilding)
            {
                EditorUtility.DisplayProgressBar(
                    "热更新资源构建",
                    string.IsNullOrWhiteSpace(mBuildOutputDirectory)
                        ? $"{mBuildStage}\n耗时：{(DateTime.UtcNow - mBuildStartedAt).TotalSeconds:F1}s"
                        : $"{mBuildStage}\n耗时：{(DateTime.UtcNow - mBuildStartedAt).TotalSeconds:F1}s\n{mBuildOutputDirectory}",
                    progress);
            }
            Repaint();
        }

        private void DrawReportSummary()
        {
            EditorGUILayout.Space(4);
            EditorGUILayout.BeginHorizontal();
            if (mReport == null)
            {
                EditorGUILayout.LabelField("校验报告：尚未生成", EditorStyles.miniLabel);
            }
            else
            {
                EditorGUILayout.LabelField($"通过 {mReport.PassedCount}", EditorStyles.miniLabel);
                EditorGUILayout.LabelField($"警告 {mReport.WarningCount}", EditorStyles.miniLabel);
                EditorGUILayout.LabelField($"错误 {mReport.ErrorCount}", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawBuildStatus()
        {
            if (!mIsBuilding && mLastExecutionResult == null)
            {
                return;
            }

            double elapsed = mIsBuilding
                ? (DateTime.UtcNow - mBuildStartedAt).TotalSeconds
                : mLastBuildDurationSeconds;
            MessageType type = mIsBuilding ? MessageType.Info : MessageType.None;
            string output = string.IsNullOrWhiteSpace(mBuildOutputDirectory)
                ? string.Empty
                : $"\n输出目录：{mBuildOutputDirectory}";
            EditorGUILayout.HelpBox(
                $"{(mIsBuilding ? "构建中" : "最近一次构建")}：{mBuildStage}\n耗时：{elapsed:F1}s{output}",
                type);
        }

        private void DrawReportGroup(
            string title,
            int count,
            HotfixBuildReportSeverity severity,
            ref bool expanded,
            MessageType messageType)
        {
            expanded = EditorGUILayout.Foldout(expanded, $"{title}（{count}）", true);
            if (!expanded)
            {
                return;
            }

            var items = mReport.Items.Where(item => item.Severity == severity);
            foreach (var item in items)
            {
                DrawReportItem(item, messageType);
            }
        }

        private void DrawBuildArtifacts()
        {
            var result = mLastExecutionResult;
            if (result == null)
            {
                return;
            }

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("构建产物", EditorStyles.boldLabel);
            DrawReadOnlyPath("主包输出目录", result.OutputPackageDirectory);
            DrawReadOnlyPath("主包 CDN 上传目录", result.CdnUploadDirectory);
            if (!string.IsNullOrWhiteSpace(result.RawFileOutputPackageDirectory))
            {
                DrawReadOnlyPath("RawFile 输出目录", result.RawFileOutputPackageDirectory);
                DrawReadOnlyPath("RawFile CDN 上传目录", result.RawFileCdnUploadDirectory);
            }
            if (!string.IsNullOrWhiteSpace(result.PlayerBaselinePath))
            {
                DrawReadOnlyPath("Player AOT 基线", result.PlayerBaselinePath);
                DrawReadOnlyPath("Player AOT 基线指纹", result.PlayerBaselineFingerprint);
            }
            DrawReadOnlyPath("构建报告", result.ReportPath);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("打开输出目录"))
            {
                RevealDirectory(result.OutputPackageDirectory);
            }
            if (GUILayout.Button("复制 CDN 上传目录"))
            {
                EditorGUIUtility.systemCopyBuffer = result.CdnUploadDirectory ?? string.Empty;
                ShowNotification(new GUIContent("CDN 上传目录已复制。"));
            }
            if (GUILayout.Button("查看报告"))
            {
                OpenReport(result.ReportPath);
            }
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawReadOnlyPath(string label, string path)
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextField(label, string.IsNullOrWhiteSpace(path) ? "未生成" : path);
            }
        }

        private static void RevealDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                EditorUtility.DisplayDialog("打开输出目录", "输出目录不存在或尚未生成。", "确定");
                return;
            }
            EditorUtility.RevealInFinder(path);
        }

        private static void OpenReport(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                EditorUtility.DisplayDialog("查看报告", "构建报告不存在或尚未生成。", "确定");
                return;
            }
            EditorUtility.OpenWithDefaultApp(path);
        }

        internal static void ShowAOTMetadataPatchResult(HotfixBuildExecutionResult result)
        {
            string message =
                "Player AOT 基线校验：通过\n" +
                $"基线路径：{result.PlayerBaselinePath}\n" +
                $"基线指纹：{result.PlayerBaselineFingerprint}\n\n" +
                $"AotVersion：{result.PreviousAotVersion} -> {result.AotVersion}\n" +
                $"Manifest 指纹：{result.PreviousManifestBaselineFingerprint} -> " +
                $"{result.CurrentManifestBaselineFingerprint}\n" +
                $"新增 DLL：{result.FormatAOTChanges(result.AotAddedFiles)}\n" +
                $"变化 DLL：{result.FormatAOTChanges(result.AotChangedFiles)}\n" +
                $"移除 DLL：{result.FormatAOTChanges(result.AotRemovedFiles)}\n\n" +
                $"PackageVersion：{result.PackageVersion}\n" +
                $"输出目录：{result.OutputPackageDirectory}\n" +
                $"构建报告：{result.ReportPath}\n\n" +
                "该补丁不会复制到 StreamingAssets，只能部署给同一 Player AOT 基线的 App。";

            int option = EditorUtility.DisplayDialogComplex(
                "AOT 元数据补丁构建成功",
                message,
                "打开输出目录",
                "关闭",
                "查看报告");
            if (option == 0)
            {
                RevealDirectory(result.OutputPackageDirectory);
            }
            else if (option == 2)
            {
                OpenReport(result.ReportPath);
            }
        }
    }
}
