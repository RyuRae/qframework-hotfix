using Framework.Events;
using QFramework;
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
            return mFSM.CurrentStateId == ResPackageStates.UpdatePackageManifest;
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
            var package = YooAssets.GetPackage(mTarget._packageName);
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;

            var downloader = CreateDownloader(package, mTarget._downloadTags, downloadingMaxNum, failedTryAgain);
            mTarget._downloaderOperation = downloader;

            ResourceDownloaderOperation downloaderRawfile = null;
            if (mTarget._isIncludeRawFile)
            {
                var rawfilePkg = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                downloaderRawfile = CreateDownloader(rawfilePkg, mTarget._rawfileDownloadTags, downloadingMaxNum, failedTryAgain);
                mTarget._downloaderRawfile = downloaderRawfile;
            }

            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;
            if (downloaderRawfile != null)
            {
                totalDownloadCount += downloaderRawfile.TotalDownloadCount;
                totalDownloadBytes += downloaderRawfile.TotalDownloadBytes;
            }

            if (totalDownloadCount == 0)
            {
                Debug.Log("Not found any download files.");
                mFSM.ChangeState(ResPackageStates.LoadAssemblies);
                return;
            }

            TypeEventSystem.Global.Send(new OnDownloadInfoHandlerEvent
            {
                totalDownloadCount = totalDownloadCount,
                totalDownloadBytes = totalDownloadBytes,
                confirmCallBack = () => mFSM.ChangeState(ResPackageStates.DownloadPackageFiles)
            });
        }

        private static ResourceDownloaderOperation CreateDownloader(
            ResourcePackage package,
            string[] tags,
            int downloadingMaxNum,
            int failedTryAgain)
        {
            if (tags != null && tags.Length > 0)
            {
                Debug.Log($"Create resource downloader by tags: {string.Join(",", tags)}");
                return package.CreateResourceDownloader(tags, downloadingMaxNum, failedTryAgain);
            }

            Debug.Log("Create resource downloader by all changed resources.");
            return package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
        }
    }
}
