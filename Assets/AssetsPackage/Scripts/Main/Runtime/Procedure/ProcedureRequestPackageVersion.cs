using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;
using Framework.UI;

namespace Framework.Procedure
{
    public class ProcedureRequestPackageVersion : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureRequestPackageVersion(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {

        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.InitializePackage;
        }

        protected override void OnEnter()
        {
            LogKit.I("µ±Ç°×´Ì¬£ºProcedureRequestPackageVersion");
            CoroutineController.manager.StartCoroutine(UpdatePackageVersion());
        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }

        ResourcePackage rawfilePackage;
        RequestPackageVersionOperation rawfileOperation;
        private IEnumerator UpdatePackageVersion()
        {
            var packageName = mTarget._packageName;
            var package = YooAssets.GetPackage(packageName);
            var operation = package.RequestPackageVersionAsync();
            if (mTarget._isIncludeRawFile)
            {
                rawfilePackage = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                rawfileOperation = rawfilePackage.RequestPackageVersionAsync();
                yield return rawfileOperation;
            }
            yield return operation;


            if (operation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(operation.Error);
                UIPanelRoot.Instance.ShowMessageBox(operation.Error);
                mTarget.SetFailed(operation.Error);
            }
            else if (mTarget._isIncludeRawFile && rawfileOperation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(rawfileOperation.Error);
                UIPanelRoot.Instance.ShowMessageBox(rawfileOperation.Error);
                mTarget.SetFailed(rawfileOperation.Error);
            }
            else
            {
                LogKit.I($"Request package version : {operation.PackageVersion}");
                if (mTarget._isIncludeRawFile)
                {
                    mTarget._rawfilePkgVersion = rawfileOperation.PackageVersion;
                }
                mTarget._packageVersion = operation.PackageVersion;
                mFSM.ChangeState(ResPackageStates.UpdatePackageManifest);
            }
        }
    }
}
