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
            TypeEventSystem.Global.Register<OnDownloadInfoHandlerEvent>(downloadInfo =>
            {
                float sizeMB = downloadInfo.totalDownloadBytes / 1048576f;
                sizeMB = Mathf.Clamp(sizeMB, 0.1f, float.MaxValue);
                string totalSizeMB = sizeMB.ToString("f1");
                ShowMessageBox($"发现可更新文件, 总数量 {downloadInfo.totalDownloadCount}， 总大小 {totalSizeMB}MB，请确认是否更新", downloadInfo.confirmCallBack);
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
                OnSceneLoadUpdateHandler(sceneLoadHandler.progress);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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
        public void OnSceneLoadUpdateHandler(float progress)
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress);
        }

        public void OpenLoadingPanel()
        { 
            UISceneLoading.Show();
        }

        public void CloseLoadingPanel()
        {
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
	}
}
