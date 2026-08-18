using System.Collections;
using UnityEngine;
using QFramework;
using YooAsset;
using Framework.Procedure;
using UnityEngine.SceneManagement;
using Framework.Events;
using Framework.UI;
using Framework.Localization;

namespace Framework
{
    /// <summary>
    /// 热更新框架的 Unity 启动入口，负责初始化日志与 YooAsset，并启动完整的 Procedure 更新流程。
    /// </summary>
    public class Boot : MonoBehaviour
    {
        [Header("游戏运行帧率")]
        [SerializeField]
        private int targetFrame = 45;

        void Awake()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            LogKit.Level = LogKit.LogLevel.Normal;
#else
            LogKit.Level = LogKit.LogLevel.Error;
#endif
            Application.targetFrameRate = targetFrame;//设置目标帧率
            Application.runInBackground = true;//设置后台运行
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 读取运行时配置、创建资源更新流程，并等待热更业务真正启动完成。
        /// </summary>
        IEnumerator Start()
        {
            // 在任何 YooAsset 操作之前同步准备启动安全文案。
            LocalizationService.Instance.InitializeBootstrap();
            var settings = HotfixRuntimeSettings.Load();
            if (settings == null)
            {
                ShowStartupError(HotfixText.Get(HotfixTextKey.StartupRuntimeConfigMissing, HotfixRuntimeSettings.AssetName));
                yield break;
            }

            var playMode = settings.PlayMode;
            var mainPackageName = settings.MainPackageName;
            var rawfilePackageName = settings.RawFilePackageName;
            if (!ValidatePlayModeForRuntime(playMode))
            {
                yield break;
            }

            //初始化资源系统
            YooAssets.Initialize();

            //进入资源检查及更新状态
            var operation = new ProcedureManager(
                mainPackageName,
                playMode,
                settings.IncludeRawFilePackage,
                settings.StartupDownloadMode,
                settings.StartupUpdatePolicy,
                settings.StartupPackageMode,
                settings.StartupDownloadTags,
                settings.RawFileStartupDownloadTags,
                rawfilePackageName);
            YooAssets.StartOperation(operation);
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                var error = string.IsNullOrEmpty(operation.Error)
                    ? HotfixText.Get(HotfixTextKey.HotUpdateProcedureInitializeFailed)
                    : operation.Error;
                Debug.LogError(error);
                UIPanelRoot.Instance.ShowMessage(error);
                yield break;
            }

        }

        /// <summary>
        /// 校验当前 Player 平台与 YooAsset 运行模式是否匹配，避免把编辑器模拟模式带入正式包。
        /// </summary>
        private bool ValidatePlayModeForRuntime(EPlayMode playMode)
        {
#if !UNITY_EDITOR
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                ShowStartupError(HotfixText.Get(HotfixTextKey.InvalidEditorSimulateInPlayer));
                return false;
            }

#if UNITY_WEBGL
            if (playMode != EPlayMode.WebPlayMode)
            {
                ShowStartupError(HotfixText.Get(HotfixTextKey.InvalidWebGLPlayMode, playMode));
                return false;
            }
#else
            if (playMode == EPlayMode.WebPlayMode)
            {
                ShowStartupError(HotfixText.Get(HotfixTextKey.InvalidNonWebGLPlayMode, playMode));
                return false;
            }
#endif
#endif
            return true;
        }

        private void ShowStartupError(string error)
        {
            Debug.LogError(error);
            var panelRoot = FindObjectOfType<UIPanelRoot>();
            if (panelRoot != null)
            {
                panelRoot.ShowMessage(error);
            }
        }
    }
}
