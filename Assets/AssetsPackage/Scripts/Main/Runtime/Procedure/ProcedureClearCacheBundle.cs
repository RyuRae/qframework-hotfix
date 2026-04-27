using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureClearCacheBundle : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureClearCacheBundle(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.LoadAssemblies;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureClearCacheBundle");
            var package = YooAssets.GetPackage(mTarget._packageName);
            var operation = package.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles);
            operation.Completed += OnClearCacheFilesCompleted;
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private void OnClearCacheFilesCompleted(AsyncOperationBase obj)
        {
            if (obj.Status != EOperationStatus.Succeed)
            {
                mTarget.SetFailed(obj.Error);
                return;
            }

            LogKit.I("Cache cleanup completed.");
            mFSM.ChangeState(ResPackageStates.StartGame);
        }
    }
}
