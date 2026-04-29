using System;
using System.Collections;
using Framework.Events;
using Framework.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureDownloadPackageFiles : AbstractState<ResPackageStates, ProcedureManager>
    {
        private const int DownloadingMaxNum = 10;
        private const int FailedTryAgain = 3;

        private enum DownloadFailureDecision
        {
            Retry,
            UseLocalCache,
            Exit
        }

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
                yield return DownloadPackage(mTarget._downloaderOperation, mTarget._packageName, false);
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
                DownloadFailureDecision decision = DownloadFailureDecision.Retry;
                yield return WaitForDownloadFailureDecision(packageName, error, value => decision = value);

                if (mTarget.IsDone || mTarget.IsDownloadCancelRequested)
                {
                    yield break;
                }

                if (decision == DownloadFailureDecision.UseLocalCache)
                {
                    yield return TryUseLocalManifestFallback($"资源下载失败，使用本地缓存启动。{GetFailureHint(error)}");
                    yield break;
                }

                if (decision == DownloadFailureDecision.Exit)
                {
                    mTarget.CancelDownload($"下载失败，用户选择退出更新。{error}");
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

        private IEnumerator WaitForDownloadFailureDecision(string packageName, string error, Action<DownloadFailureDecision> onDecision)
        {
            bool hasDecision = false;
            DownloadFailureDecision decision = DownloadFailureDecision.Retry;
            string fallbackText = mTarget.CanUseLocalCacheFallback
                ? "点击确定重试，点击取消使用本地缓存启动。"
                : "点击确定重试，点击取消退出更新。";

            LogKit.E(error);
            UIPanelRoot.Instance.CloseLoadingPanel();
            UIPanelRoot.Instance.ShowMessageBox(
                $"资源包 {packageName} 下载失败：\n{GetFailureHint(error)}\n{error}\n{fallbackText}",
                () =>
                {
                    decision = DownloadFailureDecision.Retry;
                    hasDecision = true;
                },
                () =>
                {
                    decision = mTarget.CanUseLocalCacheFallback
                        ? DownloadFailureDecision.UseLocalCache
                        : DownloadFailureDecision.Exit;
                    hasDecision = true;
                },
                true);

            while (!hasDecision && !mTarget.IsDone)
            {
                yield return null;
            }

            onDecision?.Invoke(decision);
        }

        private IEnumerator TryUseLocalManifestFallback(string reason)
        {
            bool succeeded = false;
            string error = string.Empty;
            yield return mTarget.TryUseLocalManifestFallback(reason, (result, resultError) =>
            {
                succeeded = result;
                error = resultError;
            });

            if (succeeded)
            {
                UIPanelRoot.Instance.CloseLoadingPanel();
                mFSM.ChangeState(ResPackageStates.DownloadPackageOver);
                yield break;
            }

            mTarget.SetFailed($"本地缓存启动失败：{error}");
        }

        private static string GetFailureHint(string error)
        {
            string lowerError = (error ?? string.Empty).ToLowerInvariant();
            if (lowerError.Contains("404") || lowerError.Contains("not found"))
            {
                return "CDN bundle 不存在，可能是资源未上传或 manifest 指向了错误版本。";
            }

            if (lowerError.Contains("resolve") || lowerError.Contains("dns"))
            {
                return "域名解析失败，请检查网络或 CDN 域名。";
            }

            if (lowerError.Contains("timeout") || lowerError.Contains("connect") || lowerError.Contains("unreachable"))
            {
                return "服务器不可达或网络超时。";
            }

            if (lowerError.Contains("verify") || lowerError.Contains("crc") || lowerError.Contains("hash"))
            {
                return "下载文件校验失败，可能是 CDN 文件损坏或缓存污染。";
            }

            return "网络或远端资源异常。";
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

            Debug.Log($"No startup download tags configured for retry. Package: {packageName}");
            return null;
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
