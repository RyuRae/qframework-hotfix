using System.Collections;
using Framework.UI;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureRequestPackageVersion : AbstractState<ResPackageStates, ProcedureManager>
    {
        private RequestPackageVersionOperation rawfileOperation;

        public ProcedureRequestPackageVersion(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.InitializePackage;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureRequestPackageVersion");
            CoroutineController.manager.StartCoroutine(UpdatePackageVersion());
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private IEnumerator UpdatePackageVersion()
        {
            var package = YooAssets.GetPackage(mTarget._packageName);
            var operation = package.RequestPackageVersionAsync();
            if (mTarget._isIncludeRawFile)
            {
                var rawfilePackage = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                rawfileOperation = rawfilePackage.RequestPackageVersionAsync();
                yield return rawfileOperation;
            }

            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(operation.Error);
                UIPanelRoot.Instance.ShowMessageBox(operation.Error);
                mTarget.SetFailed(operation.Error);
                yield break;
            }

            if (mTarget._isIncludeRawFile && rawfileOperation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(rawfileOperation.Error);
                UIPanelRoot.Instance.ShowMessageBox(rawfileOperation.Error);
                mTarget.SetFailed(rawfileOperation.Error);
                yield break;
            }

            LogKit.I($"Request package version: {operation.PackageVersion}");
            if (mTarget._isIncludeRawFile)
            {
                mTarget._rawfilePkgVersion = rawfileOperation.PackageVersion;
            }

            mTarget._packageVersion = operation.PackageVersion;
            mFSM.ChangeState(ResPackageStates.UpdatePackageManifest);
        }
    }
}
