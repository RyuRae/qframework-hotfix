using Framework.Events;
using QFramework;
using System;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureCreateDownloader : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureCreateDownloader(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.UpdatePackageManifest ||
                   mFSM.CurrentStateId == ResPackageStates.RequestPackageVersion;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureCreateDownloader");
            CreateDownloader();
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private void CreateDownloader()
        {
            if (mTarget._startupDownloadMode == StartupDownloadMode.DownloadByTags &&
                (mTarget._downloadTags == null || mTarget._downloadTags.Length == 0))
            {
                mTarget.SetFailed($"Startup download tags are empty. Package: {mTarget.MainPackageName}");
                return;
            }

            if (mTarget._startupDownloadMode == StartupDownloadMode.DownloadByTags &&
                mTarget._isIncludeRawFile &&
                (mTarget._rawfileDownloadTags == null || mTarget._rawfileDownloadTags.Length == 0))
            {
                mTarget.SetFailed($"RawFile startup download tags are empty. Package: {mTarget._rawfilwPkgName}");
                return;
            }

            if (mTarget.IsUsingLocalManifestFallback)
            {
                Debug.Log("Using local manifest fallback, skip startup resource download.");
                mFSM.ChangeState(ResPackageStates.LoadAOTMetadata);
                return;
            }

            if (mTarget._startupDownloadMode == StartupDownloadMode.Skip)
            {
                Debug.Log("Skip startup resource download.");
                mFSM.ChangeState(ResPackageStates.LoadAOTMetadata);
                return;
            }

            var package = YooAssets.GetPackage(mTarget.MainPackageName);
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;

            var downloader = CreateDownloader(
                package,
                mTarget.MainPackageName,
                mTarget._startupDownloadMode,
                mTarget._downloadTags,
                downloadingMaxNum,
                failedTryAgain);
            mTarget._downloaderOperation = downloader;

            ResourceDownloaderOperation downloaderRawfile = null;
            if (mTarget._isIncludeRawFile)
            {
                var rawfilePkg = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                downloaderRawfile = CreateDownloader(
                    rawfilePkg,
                    mTarget._rawfilwPkgName,
                    mTarget._startupDownloadMode,
                    mTarget._rawfileDownloadTags,
                    downloadingMaxNum,
                    failedTryAgain);
                mTarget._downloaderRawfile = downloaderRawfile;
            }

            int totalDownloadCount = GetDownloadCount(downloader);
            long totalDownloadBytes = GetDownloadBytes(downloader);
            if (downloaderRawfile != null)
            {
                totalDownloadCount += GetDownloadCount(downloaderRawfile);
                totalDownloadBytes += GetDownloadBytes(downloaderRawfile);
            }

            if (totalDownloadCount == 0)
            {
                Debug.Log("Not found any download files.");
                mFSM.ChangeState(ResPackageStates.LoadAOTMetadata);
                return;
            }

            TypeEventSystem.Global.Send(new OnDownloadInfoHandlerEvent
            {
                totalDownloadCount = totalDownloadCount,
                totalDownloadBytes = totalDownloadBytes,
                confirmCallBack = () => mFSM.ChangeState(ResPackageStates.DownloadPackageFiles),
                cancelCallBack = () => TypeEventSystem.Global.Send(new OnDownloadCancelRequestEvent
                {
                    reason = HotfixText.Get(HotfixTextKey.UserCanceledStartupUpdate)
                })
            });
        }

        private static ResourceDownloaderOperation CreateDownloader(
            ResourcePackage package,
            string packageName,
            StartupDownloadMode startupDownloadMode,
            string[] tags,
            int downloadingMaxNum,
            int failedTryAgain)
        {
            if (startupDownloadMode == StartupDownloadMode.DownloadAll)
            {
                Debug.Log($"Create resource downloader by all changed resources. Package: {packageName}");
                return package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            }

            if (startupDownloadMode == StartupDownloadMode.DownloadByTags && tags != null && tags.Length > 0)
            {
                Debug.Log($"Create resource downloader by tags: {string.Join(",", tags)}. Package: {packageName}");
                return package.CreateResourceDownloader(tags, downloadingMaxNum, failedTryAgain);
            }

            throw new InvalidOperationException($"No startup download tags configured. Package: {packageName}");
        }

        private static int GetDownloadCount(ResourceDownloaderOperation downloader)
        {
            return downloader == null ? 0 : downloader.TotalDownloadCount;
        }

        private static long GetDownloadBytes(ResourceDownloaderOperation downloader)
        {
            return downloader == null ? 0 : downloader.TotalDownloadBytes;
        }
    }
}
