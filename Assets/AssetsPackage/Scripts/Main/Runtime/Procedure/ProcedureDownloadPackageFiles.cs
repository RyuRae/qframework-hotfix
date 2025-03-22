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
            //var downloader = mTarget._downloaderOperation;
            //downloader.DownloadErrorCallback = OnDownloadErrorHandler;
            //downloader.DownloadFileBeginCallback = OnStartDownloadFileHandler;
            //downloader.DownloadUpdateCallback = OnDownloadProgressUpdateHandler;
            //downloader.DownloadFinishCallback = OnDownloadOverHandler;

            //downloader.BeginDownload();
            //yield return downloader;

            yield return DownloadDefaultPackage();

            if (mTarget._isIncludeRawFile)
                yield return DownloadRawFilePackage();

            mFSM.ChangeState(ResPackageStates.DownloadPackageOver);
        }

        bool IsMainPkgFinish = false;
        /// <summary>
        /// ����Ĭ�ϰ���������
        /// </summary>
        /// <returns></returns>
        IEnumerator DownloadDefaultPackage()
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
            yield return new WaitUntil(() => IsMainPkgFinish);
        }

        bool IsRawFileFinish = false;
        /// <summary>
        /// ����ԭ���ļ���
        /// </summary>
        /// <returns></returns>
        IEnumerator DownloadRawFilePackage()
        {
            var downloader = mTarget._downloaderRawfile;
            downloader.DownloadErrorCallback = OnDownloadErrorHandler;
            downloader.DownloadFileBeginCallback = OnStartDownloadFileHandler;
            downloader.DownloadUpdateCallback = OnDownloadProgressUpdateHandler;
            downloader.DownloadFinishCallback = OnDownloadFinishHandler;

            downloader.BeginDownload();
            yield return downloader;
            // ������ؽ��
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;
            yield return new WaitUntil(() => IsRawFileFinish);
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
            IsMainPkgFinish = true;
        }


        private void OnDownloadFinishHandler(DownloaderFinishData downloaderFinishData)
        {
            TypeEventSystem.Global.Send(new OnDownloadFinishEvent() { downloaderFinishData = downloaderFinishData });
            IsRawFileFinish = true;
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
