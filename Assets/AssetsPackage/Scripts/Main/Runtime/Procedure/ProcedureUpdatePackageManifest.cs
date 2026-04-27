using System.Collections;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureUpdatePackageManifest : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureUpdatePackageManifest(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.RequestPackageVersion;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureUpdatePackageManifest");
            CoroutineController.manager.StartCoroutine(UpdateManifest());
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private IEnumerator UpdateManifest()
        {
            var package = YooAssets.GetPackage(mTarget._packageName);
            var operation = package.UpdatePackageManifestAsync(mTarget._packageVersion);

            UpdatePackageManifestOperation rawfileOperation = null;
            if (mTarget._isIncludeRawFile)
            {
                var rawfilePackage = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                rawfileOperation = rawfilePackage.UpdatePackageManifestAsync(mTarget._rawfilePkgVersion);
                yield return rawfileOperation;
            }

            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(operation.Error);
                mTarget.SetFailed(operation.Error);
                yield break;
            }

            if (mTarget._isIncludeRawFile && rawfileOperation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(rawfileOperation.Error);
                mTarget.SetFailed(rawfileOperation.Error);
                yield break;
            }

            mFSM.ChangeState(ResPackageStates.CreateDownloader);
        }
    }
}
