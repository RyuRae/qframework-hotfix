using System;
using Framework.Events;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.UI
{
    /// <summary>热更新启动 UI 的全局协调器，统一转发下载事件并管理 Loading、提示和确认框。</summary>
    public partial class UIPanelRoot : ViewController, ISingleton
    {
        private void Awake()
        {
            TypeEventSystem.Global.Register<OnDownloadInfoHandlerEvent>(downloadInfo =>
            {
                float sizeMB = Mathf.Clamp(downloadInfo.totalDownloadBytes / 1048576f, 0.1f, float.MaxValue);
                string totalSizeMB = sizeMB.ToString("f1");
                ShowMessageBox(
                    HotfixText.Get(HotfixTextKey.DownloadInfoPrompt, downloadInfo.totalDownloadCount, totalSizeMB),
                    downloadInfo.confirmCallBack,
                    downloadInfo.cancelCallBack,
                    true);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnDownloadFileBeginEvent>(downloadHandler =>
            {
                OnDownloadFileBeginHandler(downloadHandler.downloadFileData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnDownloadUpdateEvent>(downloadHandler =>
            {
                OnDownloadUpdateHandler(downloadHandler.downloadUpdateData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnDownloadFinishEvent>(downloadHandler =>
            {
                OnDownloadFinishHandler(downloadHandler.downloaderFinishData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnDownloadErrorEvent>(downloadHandler =>
            {
                OnDownloadErrorHandler(downloadHandler.errorData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnDownloadCanceledEvent>(downloadHandler =>
            {
                OnDownloadCanceledHandler(downloadHandler.reason);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnStartupUsingLocalCacheEvent>(downloadHandler =>
            {
                OnStartupUsingLocalCacheHandler(downloadHandler.reason);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnSceneloadUpdateEvent>(sceneLoadHandler =>
            {
                OnSceneLoadUpdateHandler(sceneLoadHandler.progress, sceneLoadHandler.desc);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            TypeEventSystem.Global.Register<OnAssetloadProgressEvent>(assetloadHandler =>
            {
                OnAssetloadProgressHandler(assetloadHandler.progress, assetloadHandler.desc);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public static UIPanelRoot Instance => MonoSingletonProperty<UIPanelRoot>.Instance;

        public void OnSingletonInit()
        {
        }

        private void Start()
        {
            ActionKit.OnUpdate.Register(() =>
            {
                if (Application.platform == RuntimePlatform.WindowsPlayer && Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public void OnDownloadErrorHandler(DownloadErrorData data)
        {
            UISceneHint.ShowMessage(HotfixText.Get(HotfixTextKey.DownloadFailed, data.PackageName, data.FileName, data.ErrorInfo));
        }

        public void OnDownloadCanceledHandler(string reason)
        {
            CloseLoadingPanel();
            ShowMessage(string.IsNullOrEmpty(reason) ? HotfixText.Get(HotfixTextKey.DownloadCanceledDefault) : reason);
        }

        public void OnStartupUsingLocalCacheHandler(string reason)
        {
            CloseLoadingPanel();
            ShowMessage(string.IsNullOrEmpty(reason) ? HotfixText.Get(HotfixTextKey.StartupUsingLocalCacheDefault) : reason);
        }

        public void OnDownloadUpdateHandler(DownloadUpdateData data)
        {
            LogKit.I(HotfixText.Get(HotfixTextKey.ResourceDownloadingProgress, data.Progress));
            UISceneLoading.OnUpdateProgressExcute(data);
        }

        public void OnDownloadFileBeginHandler(DownloadFileData data)
        {
            LogKit.I(HotfixText.Get(HotfixTextKey.DownloadFileBegin, data.FileName));
        }

        public void OnDownloadFinishHandler(DownloaderFinishData data)
        {
            LogKit.I(data.Succeed
                ? HotfixText.Get(HotfixTextKey.DownloadFileCompleted, data.PackageName)
                : HotfixText.Get(HotfixTextKey.DownloadFileFailed, data.PackageName));
        }

        public void OnSceneLoadUpdateHandler(float progress, string desc = null)
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc ?? HotfixText.Get(HotfixTextKey.SceneLoading));
        }

        public void OnAssetloadProgressHandler(float progress, string desc = null)
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc ?? HotfixText.Get(HotfixTextKey.AssetLoading));
        }

        public void OpenLoadingPanel()
        {
            if (!UISceneLoading.gameObject.activeSelf)
            {
                UISceneLoading.Show();
            }
        }

        public void CloseLoadingPanel()
        {
            if (UISceneLoading.gameObject.activeSelf)
            {
                UISceneLoading.Hide();
            }
        }

        public void ShowMessage(string msg, float seconds = -1)
        {
            UISceneHint.Show();
            UISceneHint.ShowMessage(msg, seconds);
        }

        public void ShowMessageBox(string msg, Action action = null, Action cancelAction = null, bool openLoadingOnConfirm = false)
        {
            UISceneMessageBox.Show();
            UISceneMessageBox.ShowMessageBox(msg, action, cancelAction, openLoadingOnConfirm);
        }

        public void RequestCancelDownload()
        {
            RequestCancelDownloadWithReason(HotfixText.Get(HotfixTextKey.UserCanceledResourceDownload));
        }

        public void RequestCancelDownloadWithReason(string reason)
        {
            TypeEventSystem.Global.Send(new OnDownloadCancelRequestEvent { reason = reason });
        }

        public void ClearScreen()
        {
            if (Background.activeSelf)
            {
                Background.SetActive(false);
            }
        }
    }
}
