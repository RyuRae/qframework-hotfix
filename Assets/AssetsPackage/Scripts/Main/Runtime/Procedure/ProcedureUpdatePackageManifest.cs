using System;
using System.Collections;
using Framework.UI;
using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureUpdatePackageManifest : AbstractState<ResPackageStates, ProcedureManager>
    {
        private enum StartupFailureDecision
        {
            Retry,
            UseLocalCache,
            Exit
        }

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

                if (operation.Status == EOperationStatus.Succeed &&
                    (!mTarget._isIncludeRawFile || rawfileOperation.Status == EOperationStatus.Succeed))
                {
                    mFSM.ChangeState(ResPackageStates.CreateDownloader);
                    yield break;
                }

                string error = BuildPackageError(operation, rawfileOperation);
                LogKit.W(error);

                StartupFailureDecision decision = StartupFailureDecision.Retry;
                yield return WaitForStartupFailureDecision(
                    "更新远端 manifest 失败",
                    error,
                    mTarget.CanUseLocalCacheFallback,
                    value => decision = value);

                if (decision == StartupFailureDecision.Retry)
                {
                    continue;
                }

                if (decision == StartupFailureDecision.UseLocalCache)
                {
                    yield return TryUseLocalManifestFallback($"更新远端 manifest 失败，使用本地缓存启动。{GetFailureHint(error)}");
                    yield break;
                }

                mTarget.SetFailed(error);
                yield break;
            }
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

            mTarget.SetFailed($"本地缓存启动失败：{error}");
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
                ? "点击确定重试，点击取消使用本地缓存启动。"
                : "点击确定重试，点击取消退出更新。";

            UIPanelRoot.Instance.CloseLoadingPanel();
            UIPanelRoot.Instance.ShowMessageBox(
                $"{title}：\n{GetFailureHint(error)}\n{error}\n{fallbackText}",
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

        private string BuildPackageError(UpdatePackageManifestOperation operation, UpdatePackageManifestOperation rawfileOperation)
        {
            string error = operation.Status == EOperationStatus.Succeed
                ? string.Empty
                : $"{mTarget._packageName}: {operation.Error}";

            if (mTarget._isIncludeRawFile && rawfileOperation.Status != EOperationStatus.Succeed)
            {
                error = string.IsNullOrEmpty(error)
                    ? $"{mTarget._rawfilwPkgName}: {rawfileOperation.Error}"
                    : $"{error}\n{mTarget._rawfilwPkgName}: {rawfileOperation.Error}";
            }

            return error;
        }

        private static string GetFailureHint(string error)
        {
            string lowerError = (error ?? string.Empty).ToLowerInvariant();
            if (lowerError.Contains("404") || lowerError.Contains("not found"))
            {
                return "CDN manifest 或 hash 文件不存在。";
            }

            if (lowerError.Contains("resolve") || lowerError.Contains("dns"))
            {
                return "域名解析失败，请检查网络或 CDN 域名。";
            }

            if (lowerError.Contains("timeout") || lowerError.Contains("connect") || lowerError.Contains("unreachable"))
            {
                return "服务器不可达或网络超时。";
            }

            if (lowerError.Contains("manifest") || lowerError.Contains("verify") || lowerError.Contains("deserialize"))
            {
                return "远端 manifest 可能损坏或与 hash 不匹配。";
            }

            return "网络或远端资源异常。";
        }
    }
}
