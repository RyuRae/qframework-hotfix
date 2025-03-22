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
            #region ע���¼�
            TypeEventSystem.Global.Register<OnDownloadInfoHandlerEvent>(downloadInfo =>
            {
                float sizeMB = downloadInfo.totalDownloadBytes / 1048576f;
                sizeMB = Mathf.Clamp(sizeMB, 0.1f, float.MaxValue);
                string totalSizeMB = sizeMB.ToString("f1");
                ShowMessageBox($"���ֿɸ����ļ�, ������ {downloadInfo.totalDownloadCount}�� �ܴ�С {totalSizeMB}MB����ȷ���Ƿ����", downloadInfo.confirmCallBack);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //���ؿ�ʼ�¼�
            TypeEventSystem.Global.Register<OnDownloadFileBeginEvent>(downloadHandler =>
            {
                OnDownloadFileBeginHandler(downloadHandler.downloadFileData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //�ļ����ظ����¼�
            TypeEventSystem.Global.Register<OnDownloadUpdateEvent>(downloadHandler =>
            {
                OnDownloadUpdateHandler(downloadHandler.downloadUpdateData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //�ļ���������¼�
            TypeEventSystem.Global.Register<OnDownloadFinishEvent>(downloadHandler =>
            {
                OnDownloadFinishHandler(downloadHandler.downloaderFinishData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //���ش����¼�
            TypeEventSystem.Global.Register<OnDownloadErrorEvent>(downloadHandler =>
            {
                OnDownloadErrorHandler(downloadHandler.errorData);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //���������¼�
            TypeEventSystem.Global.Register<OnSceneloadUpdateEvent>(sceneLoadHandler =>
            {
                OnSceneLoadUpdateHandler(sceneLoadHandler.progress, sceneLoadHandler.desc);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //��Դ�����¼�
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
        public void OnSceneLoadUpdateHandler(float progress, string desc = "����������")
        {
            OpenLoadingPanel();
            UISceneLoading.OnUpdateProgressExcute(progress, desc);
        }


        public void OnAssetloadProgressHandler(float progress, string desc = "�ļ�������")
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
        /// �ر�loading���
        /// </summary>
        public void CloseLoadingPanel()
        {
            if (UISceneLoading.gameObject.activeSelf)
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

        /// <summary>
        /// �����Ļ
        /// </summary>
        public void ClearScreen()
        {
            if (Background.activeSelf)
                Background.SetActive(false);
        }
    }
}
