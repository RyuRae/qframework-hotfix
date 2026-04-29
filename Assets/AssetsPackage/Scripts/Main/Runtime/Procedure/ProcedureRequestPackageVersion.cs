using System;
using System.Collections;
using Framework.UI;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureRequestPackageVersion : AbstractState<ResPackageStates, ProcedureManager>
    {
        private enum StartupFailureDecision
        {
            Retry,
            UseLocalCache,
            Exit
        }

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
                    yield return TryUseLocalManifestFallback(HotfixText.Get(HotfixTextKey.StartupLocalCacheOnlyReason));
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
                yield return WaitForStartupFailureDecision(
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
                    yield return TryUseLocalManifestFallback(HotfixText.Get(
                        HotfixTextKey.RequestRemoteVersionFailedUseCacheReason,
                        GetFailureHint(error)));
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

        private IEnumerator TryUseLocalManifestFallback(string reason)
        {
            bool succeeded = false;
            string error = string.Empty;
            yield return mTarget.TryUseLocalManifestFallback(reason, (result, resultError) =>
            {
                succeeded = result;
                error = resultError;
            });

            if (succeeded)
            {
                mFSM.ChangeState(ResPackageStates.CreateDownloader);
                yield break;
            }

            mTarget.SetFailed(HotfixText.Get(HotfixTextKey.LocalCacheStartupFailed, error));
        }

        private IEnumerator WaitForStartupFailureDecision(
            string title,
            string error,
            bool canUseLocalCache,
            Action<StartupFailureDecision> onDecision)
        {
            bool hasDecision = false;
            StartupFailureDecision decision = StartupFailureDecision.Retry;
            string fallbackText = canUseLocalCache
                ? HotfixText.Get(HotfixTextKey.RetryOrUseCachePrompt)
                : HotfixText.Get(HotfixTextKey.RetryOrExitPrompt);

            UIPanelRoot.Instance.CloseLoadingPanel();
            UIPanelRoot.Instance.ShowMessageBox(
                HotfixText.Get(HotfixTextKey.StartupFailurePrompt, title, GetFailureHint(error), error, fallbackText),
                () =>
                {
                    decision = StartupFailureDecision.Retry;
                    hasDecision = true;
                },
                () =>
                {
                    decision = canUseLocalCache ? StartupFailureDecision.UseLocalCache : StartupFailureDecision.Exit;
                    hasDecision = true;
                },
                true);

            while (!hasDecision && !mTarget.IsDone)
            {
                yield return null;
            }

            onDecision?.Invoke(decision);
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

        private static string GetFailureHint(string error)
        {
            string lowerError = (error ?? string.Empty).ToLowerInvariant();
            if (lowerError.Contains("404") || lowerError.Contains("not found"))
            {
                return HotfixText.Get(HotfixTextKey.VersionFileNotFoundHint);
            }

            if (lowerError.Contains("resolve") || lowerError.Contains("dns"))
            {
                return HotfixText.Get(HotfixTextKey.DnsResolveFailedHint);
            }

            if (lowerError.Contains("timeout") || lowerError.Contains("connect") || lowerError.Contains("unreachable"))
            {
                return HotfixText.Get(HotfixTextKey.ServerUnreachableHint);
            }

            if (lowerError.Contains("manifest") || lowerError.Contains("verify") || lowerError.Contains("deserialize"))
            {
                return HotfixText.Get(HotfixTextKey.RemoteManifestInvalidHint);
            }

            return HotfixText.Get(HotfixTextKey.NetworkOrRemoteResourceErrorHint);
        }
    }
}
