using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;
using MsbFramework.UI;

namespace MsbFramework.Procedure
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

        private IEnumerator UpdatePackageVersion()
        {
            var packageName = mTarget._packageName;
            var package = YooAssets.GetPackage(packageName);
            var operation = package.RequestPackageVersionAsync();
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(operation.Error);
                UIPanelRoot.Instance.ShowMessageBox(operation.Error);
            }
            else
            {
                LogKit.I($"Request package version : {operation.PackageVersion}");
                mTarget._packageVersion = operation.PackageVersion;
                mFSM.ChangeState(ResPackageStates.UpdatePackageManifest);
            }
        }
    }
}
