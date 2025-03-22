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
            LogKit.I("当前状态：文件下载中");
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
        /// 下载默认包（主包）
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
            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;
            yield return new WaitUntil(() => IsMainPkgFinish);
        }

        bool IsRawFileFinish = false;
        /// <summary>
        /// 下载原生文件包
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
            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;
            yield return new WaitUntil(() => IsRawFileFinish);
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        private void OnStartDownloadFileHandler(DownloadFileData downloadFileData)
        {
            TypeEventSystem.Global.Send(new OnDownloadFileBeginEvent() { downloadFileData = downloadFileData });
        }

        /// <summary>
        /// 下载完成
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
        /// 更新中
        /// </summary>
        private void OnDownloadProgressUpdateHandler(DownloadUpdateData downloadUpdateData)
        {
            TypeEventSystem.Global.Send(new OnDownloadUpdateEvent() { downloadUpdateData = downloadUpdateData });
        }

        /// <summary>
        /// 下载出错
        /// </summary>
        /// <param name="errorData"></param>
        private void OnDownloadErrorHandler(DownloadErrorData errorData)
        {
            Debug.Log($"下载出错：包名:{errorData.PackageName} 文件名：{errorData.FileName}，错误信息：{errorData.ErrorInfo}");
            TypeEventSystem.Global.Send(new OnDownloadErrorEvent() { errorData = errorData });
        }
    }
}
