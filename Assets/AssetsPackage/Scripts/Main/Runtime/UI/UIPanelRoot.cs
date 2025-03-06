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
                ShowMessageBox($"���ֿɸ����ļ�, ������ {downloadInfo.totalDownloadCount}�� �ܴ�С {totalSizeMB}MB����ȷ���Ƿ����", downloadInfo.confirmCallBack);
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
        /// ���ش���ص��¼�
        /// </summary>
        /// <param name="data">���ش�������</param>
        public void OnDownloadErrorHandler(DownloadErrorData data)
        {
            UISceneHint.ShowMessage("���س���" + data.PackageName + "\n" + data.FileName + "\n" + data.ErrorInfo);
        }

        /// <summary>
        /// ���ظ��»ص��¼�
        /// </summary>
        /// <param name="data">���ظ�������</param>
        public void OnDownloadUpdateHandler(DownloadUpdateData data)
        {
            LogKit.I("��Դ�����У�" + data.Progress);
            UISceneLoading.OnUpdateProgressExcute(data);
        }

        /// <summary>
        /// ��ʼ�����ļ��ص��¼�
        /// </summary>
        /// <param name="data">�ļ����ݣ�����/�ļ���/��С��</param>
        public void OnDownloadFileBeginHandler(DownloadFileData data) 
        {
            LogKit.I("��ʼ�����ļ���" + data.FileName);
        }

        /// <summary>
        /// ������ɻص��¼�
        /// </summary>
        /// <param name="data">����������ݣ�����/�Ƿ����سɹ���</param>
        public void OnDownloadFinishHandler(DownloaderFinishData data)
        {
            LogKit.I("�ļ�������ɣ�");
            ActionKit.Delay(1, () => 
            {
                CloseLoadingPanel();
            }).Start(this);
        }

        /// <summary>
        /// �������ؽ���
        /// </summary>
        /// <param name="progress">���ؽ���</param>
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
        /// ��ʾ��ʾ����
        /// </summary>
        public void ShowMessage(string msg, float seconds = -1) 
        {
            UISceneHint.Show();
            UISceneHint.ShowMessage(msg, seconds);
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="action">ȷ�Ϻ�ص�</param>
        public void ShowMessageBox(string msg, Action action = null)
        {
            UISceneMessageBox.Show();
            UISceneMessageBox.ShowMessageBox(msg, action);
        }
	}
}
