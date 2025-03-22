using QFramework;
using System.Collections;
using YooAsset;

namespace MsbFramework.Procedure
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
            LogKit.I("当前状态：请求资源版本！");
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
            var packageName = mTarget._packageName;
            var packageVersion = mTarget._packageVersion;
            var package = YooAssets.GetPackage(packageName);//待优化
            var operation = package.UpdatePackageManifestAsync(packageVersion);
            string rawfilePkgVersion = null;
            ResourcePackage rawfilePackage = null;
            UpdatePackageManifestOperation rawfileOperation = null;
            if (mTarget._isIncludeRawFile)
            {
                rawfilePkgVersion = mTarget._rawfilePkgVersion;
                rawfilePackage = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                rawfileOperation = rawfilePackage.UpdatePackageManifestAsync(rawfilePkgVersion);
                yield return rawfileOperation;
            }
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                LogKit.W(operation.Error);
                yield break;
            }
            else
            {
                mFSM.ChangeState(ResPackageStates.CreateDownloader);
            }
        }
    }
}