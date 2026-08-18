using System.Collections;
using Framework.Localization;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    /// <summary>在当前 Manifest 上加载最新语言目录和业务文本；失败仅降级到 Bootstrap。</summary>
    public sealed class ProcedureLoadLocalization : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureLoadLocalization(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager) { }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.RequestPackageVersion ||
                   mFSM.CurrentStateId == ResPackageStates.UpdatePackageManifest;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureLoadLocalization");
            CoroutineController.manager.StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            bool succeeded = false;
            string error = string.Empty;
            var package = YooAssets.GetPackage(mTarget.MainPackageName);
            yield return LocalizationService.Instance.UpgradeFromPackage(
                package,
                (result, message) => { succeeded = result; error = message; });

            if (!succeeded)
            {
                LogKit.W($"Localization runtime upgrade skipped; Bootstrap remains active. {error}");
            }

            mFSM.ChangeState(ResPackageStates.CreateDownloader);
        }

        protected override void OnExit() { }
        protected override void OnUpdate() { }
    }
}
