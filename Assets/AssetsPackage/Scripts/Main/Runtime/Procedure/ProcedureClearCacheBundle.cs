using System.Collections;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    /// <summary>仅在新版本成功提交 LastGood 后清理未使用缓存，避免提前删除回滚资源。</summary>
    public class ProcedureClearCacheBundle : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureClearCacheBundle(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.StartGame;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureClearCacheBundle");
            CoroutineController.manager.StartCoroutine(ClearCacheFiles());
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private IEnumerator ClearCacheFiles()
        {
            if (!mTarget.LastGoodCommittedThisRun)
            {
                LogKit.W("Skip cache cleanup because LastGood was not committed in this startup.");
                mTarget.SetFinish();
                yield break;
            }

            yield return ClearPackageCache(mTarget.MainPackageName);
            if (mTarget._isIncludeRawFile)
            {
                yield return ClearPackageCache(mTarget._rawfilwPkgName);
            }

            mTarget.SetFinish();
        }

        private static IEnumerator ClearPackageCache(string packageName)
        {
            var package = YooAssets.TryGetPackage(packageName);
            if (package == null || !package.PackageValid)
            {
                LogKit.W($"Cache cleanup skipped because package is invalid: {packageName}");
                yield break;
            }

            var operation = package.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles);
            yield return operation;
            if (operation.Status != EOperationStatus.Succeed)
            {
                LogKit.W($"Cache cleanup failed, continue startup. Package={packageName}. {operation.Error}");
                yield break;
            }

            LogKit.I($"Cache cleanup completed. Package={packageName}");
        }
    }
}
