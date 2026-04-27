using System.Collections;
using Framework.Events;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
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
            LogKit.I("Current state: ProcedureDownloadPackageFiles");
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
            yield return DownloadPackage(mTarget._downloaderOperation, "DefaultPackage");
            if (mTarget.IsDone)
            {
                yield break;
            }

            if (mTarget._isIncludeRawFile)
            {
                yield return DownloadPackage(mTarget._downloaderRawfile, "RawFilePackage");
                if (mTarget.IsDone)
                {
                    yield break;
                }
            }

            mFSM.ChangeState(ResPackageStates.DownloadPackageOver);
        }

        private IEnumerator DownloadPackage(ResourceDownloaderOperation downloader, string packageName)
        {
            if (downloader == null)
            {
                mTarget.SetFailed($"{packageName} downloader is null.");
                yield break;
            }

            downloader.DownloadErrorCallback = OnDownloadErrorHandler;
            downloader.DownloadFileBeginCallback = OnStartDownloadFileHandler;
            downloader.DownloadUpdateCallback = OnDownloadProgressUpdateHandler;
            downloader.DownloadFinishCallback = OnDownloadFinishHandler;

            downloader.BeginDownload();
            yield return downloader;

            if (downloader.Status != EOperationStatus.Succeed)
            {
                mTarget.SetFailed($"{packageName} download failed: {downloader.Error}");
            }
        }

        private void OnStartDownloadFileHandler(DownloadFileData downloadFileData)
        {
            TypeEventSystem.Global.Send(new OnDownloadFileBeginEvent { downloadFileData = downloadFileData });
        }

        private void OnDownloadFinishHandler(DownloaderFinishData downloaderFinishData)
        {
            TypeEventSystem.Global.Send(new OnDownloadFinishEvent { downloaderFinishData = downloaderFinishData });
        }

        private void OnDownloadProgressUpdateHandler(DownloadUpdateData downloadUpdateData)
        {
            TypeEventSystem.Global.Send(new OnDownloadUpdateEvent { downloadUpdateData = downloadUpdateData });
        }

        private void OnDownloadErrorHandler(DownloadErrorData errorData)
        {
            Debug.Log($"Download error. Package:{errorData.PackageName}, File:{errorData.FileName}, Error:{errorData.ErrorInfo}");
            TypeEventSystem.Global.Send(new OnDownloadErrorEvent { errorData = errorData });
        }
    }
}
