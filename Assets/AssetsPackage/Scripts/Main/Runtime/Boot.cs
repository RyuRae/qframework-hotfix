using System.Collections;
using UnityEngine;
using QFramework;
using YooAsset;
using Framework.Procedure;
using UnityEngine.SceneManagement;
using Framework.Events;
using Framework.UI;

namespace Framework
{
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

        IEnumerator Start()
        {
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
