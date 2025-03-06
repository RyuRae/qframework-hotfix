using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;
using MsbFramework.Events;

namespace MsbFramework.Procedure
{
    public class ProcedureDownloadPackageFiles : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureDownloadPackageFiles(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {

        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.CreateDownloader;
        }

        protected override void OnEnter()
        {
            LogKit.I("��ǰ״̬���ļ�������");
            CoroutineController.manager.StartCoroutine(BeginDownload());
        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }

        private IEnumerator BeginDownload()
        {
            var downloader = mTarget._downloaderOperation;
            downloader.DownloadErrorCallback = OnDownloadErrorHandler;
            downloader.DownloadFileBeginCallback = OnStartDownloadFileHandler;
            downloader.DownloadUpdateCallback = OnDownloadProgressUpdateHandler;
            downloader.DownloadFinishCallback = OnDownloadOverHandler;

            downloader.BeginDownload();
            yield return downloader;

            // ������ؽ��
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;

            mFSM.ChangeState(ResPackageStates.DownloadPackageOver);
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private void OnStartDownloadFileHandler(DownloadFileData downloadFileData)
        {
            TypeEventSystem.Global.Send(new OnDownloadFileBeginEvent() { downloadFileData = downloadFileData });
        }

        /// <summary>
        /// �������
        /// </summary>
        private void OnDownloadOverHandler(DownloaderFinishData downloaderFinishData)
        {
            TypeEventSystem.Global.Send(new OnDownloadFinishEvent() { downloaderFinishData = downloaderFinishData });
        }

        /// <summary>
        /// ������
        /// </summary>
        private void OnDownloadProgressUpdateHandler(DownloadUpdateData downloadUpdateData)
        {
            TypeEventSystem.Global.Send(new OnDownloadUpdateEvent() { downloadUpdateData = downloadUpdateData });
        }

        /// <summary>
        /// ���س���
        /// </summary>
        /// <param name="errorData"></param>
        private void OnDownloadErrorHandler(DownloadErrorData errorData)
        {
            Debug.Log($"���س�������:{errorData.PackageName} �ļ�����{errorData.FileName}��������Ϣ��{errorData.ErrorInfo}");
            TypeEventSystem.Global.Send(new OnDownloadErrorEvent() { errorData = errorData });
        }
    }
}
