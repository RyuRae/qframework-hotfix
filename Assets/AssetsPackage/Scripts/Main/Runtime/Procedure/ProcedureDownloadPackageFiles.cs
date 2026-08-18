using System;
using System.Collections;
using Framework.Events;
using Framework.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    /// <summary>执行资源下载，处理进度、重试、暂停、取消和本地缓存降级。</summary>
    public class ProcedureDownloadPackageFiles : AbstractState<ResPackageStates, ProcedureManager>
    {
        private const int DownloadingMaxNum = 10;
        private const int FailedTryAgain = 3;

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
            if (mTarget._downloaderOperation != null)
            {
                yield return DownloadPackage(mTarget._downloaderOperation, mTarget.MainPackageName, false);
                if (mTarget.IsDone || mTarget.IsUsingLocalManifestFallback)
                {
                    yield break;
                }
            }

            if (mTarget._isIncludeRawFile && mTarget._downloaderRawfile != null)
            {
                yield return DownloadPackage(mTarget._downloaderRawfile, mTarget._rawfilwPkgName, true);
                if (mTarget.IsDone || mTarget.IsUsingLocalManifestFallback)
                {
                    yield break;
                }
            }

            UIPanelRoot.Instance.CloseLoadingPanel();
            mFSM.ChangeState(ResPackageStates.DownloadPackageOver);
        }

        private IEnumerator DownloadPackage(ResourceDownloaderOperation downloader, string packageName, bool isRawFilePackage)
        {
            while (!mTarget.IsDone)
            {
                if (downloader == null)
                {
                    mTarget.SetFailed($"{packageName} downloader is null.");
                    yield break;
                }

                BindDownloaderCallbacks(downloader);
                downloader.BeginDownload();

                while (!downloader.IsDone)
                {
                    if (mTarget.IsDone || mTarget.IsDownloadCancelRequested)
                    {
                        ClearDownloaderCallbacks(downloader);
                        yield break;
                    }

                    yield return null;
                }

                ClearDownloaderCallbacks(downloader);

                if (mTarget.IsDone || mTarget.IsDownloadCancelRequested)
                {
                    yield break;
                }

                if (downloader.Status == EOperationStatus.Succeed)
                {
                    yield break;
                }

                string error = $"{packageName} download failed: {downloader.Error}";
                LogKit.E(error);
                StartupFailureDecision decision = StartupFailureDecision.Retry;
                yield return ProcedureFailureHandler.WaitForStartupFailureDecision(
                    mTarget,
                    HotfixText.Get(HotfixTextKey.PackageDownloadFailedPrompt),
                    error,
                    mTarget.CanUseLocalCacheFallback,
                    value => decision = value);

                if (mTarget.IsDone || mTarget.IsDownloadCancelRequested)
                {
                    yield break;
                }

                if (decision == StartupFailureDecision.UseLocalCache)
                {
                    yield return ProcedureFailureHandler.TryUseLocalManifestFallback(
                        mTarget,
                        HotfixText.Get(HotfixTextKey.DownloadFailedUseLocalCacheReason,
                            ProcedureFailureHandler.GetFailureHint(error)),
                        succeeded =>
                        {
                            if (succeeded)
                            {
                                UIPanelRoot.Instance.CloseLoadingPanel();
                                mFSM.ChangeState(ResPackageStates.DownloadPackageOver);
                            }
                        });
                    yield break;
                }

                if (decision == StartupFailureDecision.Exit)
                {
                    mTarget.CancelDownload(HotfixText.Get(HotfixTextKey.DownloadFailedExitReason, error));
                    yield break;
                }

                downloader = CreateDownloader(packageName, isRawFilePackage);
                SetCurrentDownloader(downloader, isRawFilePackage);
            }
        }

        private void BindDownloaderCallbacks(ResourceDownloaderOperation downloader)
        {
            downloader.DownloadErrorCallback = OnDownloadErrorHandler;
            downloader.DownloadFileBeginCallback = OnStartDownloadFileHandler;
            downloader.DownloadUpdateCallback = OnDownloadProgressUpdateHandler;
            downloader.DownloadFinishCallback = OnDownloadFinishHandler;
        }

        private static void ClearDownloaderCallbacks(ResourceDownloaderOperation downloader)
        {
            if (downloader == null)
            {
                return;
            }

            downloader.DownloadErrorCallback = null;
            downloader.DownloadFileBeginCallback = null;
            downloader.DownloadUpdateCallback = null;
            downloader.DownloadFinishCallback = null;
        }

        private ResourceDownloaderOperation CreateDownloader(string packageName, bool isRawFilePackage)
        {
            var package = YooAssets.GetPackage(packageName);
            if (mTarget._startupDownloadMode == StartupDownloadMode.DownloadAll)
            {
                Debug.Log($"Retry resource downloader by all changed resources. Package: {packageName}");
                return package.CreateResourceDownloader(DownloadingMaxNum, FailedTryAgain);
            }

            var tags = isRawFilePackage ? mTarget._rawfileDownloadTags : mTarget._downloadTags;
            if (mTarget._startupDownloadMode == StartupDownloadMode.DownloadByTags && tags != null && tags.Length > 0)
            {
                Debug.Log($"Retry resource downloader by tags: {string.Join(",", tags)}. Package: {packageName}");
                return package.CreateResourceDownloader(tags, DownloadingMaxNum, FailedTryAgain);
            }

            throw new InvalidOperationException($"No startup download tags configured for retry. Package: {packageName}");
        }

        private void SetCurrentDownloader(ResourceDownloaderOperation downloader, bool isRawFilePackage)
        {
            if (isRawFilePackage)
            {
                mTarget._downloaderRawfile = downloader;
                return;
            }

            mTarget._downloaderOperation = downloader;
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
