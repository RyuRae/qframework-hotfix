using System;
using Framework.Events;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.UI
{
    public partial class UIPanelRoot : ViewController, ISingleton
    {
        private void Awake()
        {
            TypeEventSystem.Global.Register<OnDownloadInfoHandlerEvent>(downloadInfo =>
            {
                float sizeMB = Mathf.Clamp(downloadInfo.totalDownloadBytes / 1048576f, 0.1f, float.MaxValue);
                string totalSizeMB = sizeMB.ToString("f1");
                ShowMessageBox(
                    $"发现可更新文件：{downloadInfo.totalDownloadCount} 个，总大小 {totalSizeMB} MB，是否开始下载？",
                    downloadInfo.confirmCallBack);
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
            UISceneHint.ShowMessage($"下载失败：{data.PackageName}\n{data.FileName}\n{data.ErrorInfo}");
        }

        public void OnDownloadUpdateHandler(DownloadUpdateData data)
        {
            LogKit.I($"资源下载中：{data.Progress:P0}");
            UISceneLoading.OnUpdateProgressExcute(data);
        }

        public void OnDownloadFileBeginHandler(DownloadFileData data)
        {
            LogKit.I($"开始下载文件：{data.FileName}");
        }

        public void OnDownloadFinishHandler(DownloaderFinishData data)
        {
            LogKit.I("文件下载完成。");
            ActionKit.Delay(1, CloseLoadingPanel).Start(this);
        }

        public void OnSceneLoadUpdateHandler(float progress, string desc = "场景加载中")
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc);
        }

        public void OnAssetloadProgressHandler(float progress, string desc = "资源加载中")
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc);
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

        public void ShowMessageBox(string msg, Action action = null)
        {
            UISceneMessageBox.Show();
            UISceneMessageBox.ShowMessageBox(msg, action);
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
