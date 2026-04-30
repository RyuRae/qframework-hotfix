using System;
using System.Collections;
using Framework.UI;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureRequestPackageVersion : AbstractState<ResPackageStates, ProcedureManager>
    {
        private struct PackageVersionRequestResult
        {
            public bool Succeeded;
            public string PackageName;
            public string PackageVersion;
            public string Error;
        }

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
            while (!mTarget.IsDone)
            {
                if (mTarget.ShouldUseLocalCacheOnlyAtStartup())
                {
                    yield return ProcedureFailureHandler.TryUseLocalManifestFallback(
                        mTarget,
                        HotfixText.Get(HotfixTextKey.StartupLocalCacheOnlyReason),
                        succeeded =>
                        {
                            if (succeeded)
                            {
                                mFSM.ChangeState(ResPackageStates.CreateDownloader);
                            }
                        });
                    yield break;
                }

                PackageVersionRequestResult packageResult = default;
                yield return RequestPackageVersion(mTarget._packageName, result => packageResult = result);

                PackageVersionRequestResult rawfileResult = default;
                if (mTarget._isIncludeRawFile)
                {
                    yield return RequestPackageVersion(mTarget._rawfilwPkgName, result => rawfileResult = result);
                }

                if (packageResult.Succeeded && (!mTarget._isIncludeRawFile || rawfileResult.Succeeded))
                {
                    LogKit.I($"Request package version: {packageResult.PackageVersion}");
                    mTarget._packageVersion = packageResult.PackageVersion;
                    if (mTarget._isIncludeRawFile)
                    {
                        mTarget._rawfilePkgVersion = rawfileResult.PackageVersion;
                    }

                    mFSM.ChangeState(ResPackageStates.UpdatePackageManifest);
                    yield break;
                }

                string title = HotfixText.Get(HotfixTextKey.RequestRemoteVersionFailedTitle);
                string error = BuildPackageError(title, packageResult, rawfileResult);
                LogKit.W(error);

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
                        HotfixText.Get(HotfixTextKey.RequestRemoteVersionFailedUseCacheReason,
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

        private IEnumerator RequestPackageVersion(string packageName, Action<PackageVersionRequestResult> onCompleted)
        {
            var package = YooAssets.GetPackage(packageName);
            var operation = package.RequestPackageVersionAsync();
            yield return operation;

            onCompleted?.Invoke(new PackageVersionRequestResult
            {
                Succeeded = operation.Status == EOperationStatus.Succeed,
                PackageName = packageName,
                PackageVersion = operation.PackageVersion,
                Error = operation.Error
            });
        }

        private string BuildPackageError(
            string prefix,
            PackageVersionRequestResult packageResult,
            PackageVersionRequestResult rawfileResult)
        {
            string error = packageResult.Succeeded
                ? string.Empty
                : $"{packageResult.PackageName}: {packageResult.Error}";

            if (mTarget._isIncludeRawFile && !rawfileResult.Succeeded)
            {
                error = string.IsNullOrEmpty(error)
                    ? $"{rawfileResult.PackageName}: {rawfileResult.Error}"
                    : $"{error}\n{rawfileResult.PackageName}: {rawfileResult.Error}";
            }

            return $"{prefix}\n{error}";
        }
    }
}
