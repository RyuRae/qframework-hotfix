using System;
using System.Collections;
using Framework.UI;
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
            while (!mTarget.IsDone)
            {
                var package = YooAssets.GetPackage(mTarget.MainPackageName);
                var operation = package.UpdatePackageManifestAsync(mTarget._packageVersion);

                UpdatePackageManifestOperation rawfileOperation = null;
                if (mTarget._isIncludeRawFile)
                {
                    var rawfilePackage = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                    rawfileOperation = rawfilePackage.UpdatePackageManifestAsync(mTarget._rawfilePkgVersion);
                    yield return rawfileOperation;
                }

                yield return operation;

                if (operation.Status == EOperationStatus.Succeed &&
                    (!mTarget._isIncludeRawFile || rawfileOperation.Status == EOperationStatus.Succeed))
                {
                    mFSM.ChangeState(ResPackageStates.CreateDownloader);
                    yield break;
                }

                string error = BuildPackageError(operation, rawfileOperation);
                LogKit.W(error);

                string title = HotfixText.Get(HotfixTextKey.UpdateManifestFailedTitle);
                StartupFailureDecision decision = StartupFailureDecision.Retry;
                yield return ProcedureFailureHandler.WaitForStartupFailureDecision(
                    mTarget,
                    title,
                    error,
                    mTarget.CanUseLocalCacheFallback,
                    value => decision = value);

                if (decision == StartupFailureDecision.Retry)
                {
                    continue;
                }

                if (decision == StartupFailureDecision.UseLocalCache)
                {
                    yield return ProcedureFailureHandler.TryUseLocalManifestFallback(
                        mTarget,
                        HotfixText.Get(HotfixTextKey.UpdateManifestFailedUseCacheReason,
                            ProcedureFailureHandler.GetFailureHint(error)),
                        succeeded =>
                        {
                            if (succeeded)
                            {
                                mFSM.ChangeState(ResPackageStates.CreateDownloader);
                            }
                        });
                    yield break;
                }

                mTarget.SetFailed(error);
                yield break;
            }
        }

        private string BuildPackageError(UpdatePackageManifestOperation operation, UpdatePackageManifestOperation rawfileOperation)
        {
            string error = operation.Status == EOperationStatus.Succeed
                ? string.Empty
                : $"{mTarget.MainPackageName}: {operation.Error}";

            if (mTarget._isIncludeRawFile && rawfileOperation.Status != EOperationStatus.Succeed)
            {
                error = string.IsNullOrEmpty(error)
                    ? $"{mTarget._rawfilwPkgName}: {rawfileOperation.Error}"
                    : $"{error}\n{mTarget._rawfilwPkgName}: {rawfileOperation.Error}";
            }

            return error;
        }
    }
}
