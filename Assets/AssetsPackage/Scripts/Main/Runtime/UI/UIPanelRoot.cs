using UnityEngine;
using QFramework;
using MsbFramework.Events;
using YooAsset;
using System;

namespace MsbFramework.UI
{
    public partial class UIPanelRoot : ViewController, ISingleton
    {
        void Awake()
        {
            #region 注册事件
            TypeEventSystem.Global.Register<OnDownloadInfoHandlerEvent>(downloadInfo =>
            {
                float sizeMB = downloadInfo.totalDownloadBytes / 1048576f;
                sizeMB = Mathf.Clamp(sizeMB, 0.1f, float.MaxValue);
                string totalSizeMB = sizeMB.ToString("f1");
                ShowMessageBox($"发现可更新文件, 总数量 {downloadInfo.totalDownloadCount}， 总大小 {totalSizeMB}MB，请确认是否更新", downloadInfo.confirmCallBack);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //下载开始事件
            TypeEventSystem.Global.Register<OnDownloadFileBeginEvent>(downloadHandler =>
            {
                OnDownloadFileBeginHandler(downloadHandler.downloadFileData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //文件下载更新事件
            TypeEventSystem.Global.Register<OnDownloadUpdateEvent>(downloadHandler =>
            {
                OnDownloadUpdateHandler(downloadHandler.downloadUpdateData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //文件下载完成事件
            TypeEventSystem.Global.Register<OnDownloadFinishEvent>(downloadHandler =>
            {
                OnDownloadFinishHandler(downloadHandler.downloaderFinishData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //下载错误事件
            TypeEventSystem.Global.Register<OnDownloadErrorEvent>(downloadHandler =>
            {
                OnDownloadErrorHandler(downloadHandler.errorData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //场景加载事件
            TypeEventSystem.Global.Register<OnSceneloadUpdateEvent>(sceneLoadHandler =>
            {
                OnSceneLoadUpdateHandler(sceneLoadHandler.progress, sceneLoadHandler.desc);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //资源加载事件
            TypeEventSystem.Global.Register<OnAssetloadProgressEvent>(assetloadHandler =>
            {
                OnAssetloadProgressHandler(assetloadHandler.progress, assetloadHandler.desc);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            #endregion
        }

        public static UIPanelRoot Instance
        {
            get
            {
                return MonoSingletonProperty<UIPanelRoot>.Instance;
            }
        }

        public void OnSingletonInit()
        {
        }

        void Start()
        {
            ActionKit.OnUpdate.Register(() =>
            {
                if (Application.platform == RuntimePlatform.WindowsPlayer && Input.GetKeyDown(KeyCode.Escape))
                    Application.Quit();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 下载错误回调事件
        /// </summary>
        /// <param name="data">下载错误数据</param>
        public void OnDownloadErrorHandler(DownloadErrorData data)
        {
            UISceneHint.ShowMessage("下载出错：" + data.PackageName + "\n" + data.FileName + "\n" + data.ErrorInfo);
        }

        /// <summary>
        /// 下载更新回调事件
        /// </summary>
        /// <param name="data">下载更新数据</param>
        public void OnDownloadUpdateHandler(DownloadUpdateData data)
        {
            LogKit.I("资源更新中：" + data.Progress);
            UISceneLoading.OnUpdateProgressExcute(data);
        }

        /// <summary>
        /// 开始下载文件回调事件
        /// </summary>
        /// <param name="data">文件数据（包名/文件名/大小）</param>
        public void OnDownloadFileBeginHandler(DownloadFileData data)
        {
            LogKit.I("开始下载文件：" + data.FileName);
        }

        /// <summary>
        /// 下载完成回调事件
        /// </summary>
        /// <param name="data">下载完成数据（包名/是否下载成功）</param>
        public void OnDownloadFinishHandler(DownloaderFinishData data)
        {
            LogKit.I("文件下载完成！");
            ActionKit.Delay(1, () =>
            {
                CloseLoadingPanel();
            }).Start(this);
        }

        /// <summary>
        /// 场景加载进度
        /// </summary>
        /// <param name="progress">加载进度</param>
        public void OnSceneLoadUpdateHandler(float progress, string desc = "场景加载中")
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc);
        }


        public void OnAssetloadProgressHandler(float progress, string desc = "文件下载中")
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc);
        }

        public void OpenLoadingPanel()
        {
            if (!UISceneLoading.gameObject.activeSelf)
                UISceneLoading.Show();
        }

        /// <summary>
        /// 关闭loading面版
        /// </summary>
        public void CloseLoadingPanel()
        {
            if (UISceneLoading.gameObject.activeSelf)
                UISceneLoading.Hide();
        }

        /// <summary>
        /// 显示提示内容
        /// </summary>
        public void ShowMessage(string msg, float seconds = -1)
        {
            UISceneHint.Show();
            UISceneHint.ShowMessage(msg, seconds);
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="action">确认后回调</param>
        public void ShowMessageBox(string msg, Action action = null)
        {
            UISceneMessageBox.Show();
            UISceneMessageBox.ShowMessageBox(msg, action);
        }

        /// <summary>
        /// 清空屏幕
        /// </summary>
        public void ClearScreen()
        {
            if (Background.activeSelf)
                Background.SetActive(false);
        }
    }
}
