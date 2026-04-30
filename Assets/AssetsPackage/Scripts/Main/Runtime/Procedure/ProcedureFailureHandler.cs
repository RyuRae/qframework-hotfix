using System;
using System.Collections;
using Framework.UI;
using QFramework;

namespace Framework.Procedure
{
    /// <summary>
    /// 启动阶段失败决策，统一管理三个 Procedure 中共用的重试/本地缓存/退出逻辑。
    /// </summary>
    public enum StartupFailureDecision
    {
        Retry,
        UseLocalCache,
        Exit
    }

    /// <summary>
    /// 启动阶段失败处理工具，提取 ProcedureRequestPackageVersion、
    /// ProcedureUpdatePackageManifest、ProcedureDownloadPackageFiles 中共用的
    /// 决策等待、错误分类提示和本地缓存降级逻辑。
    /// </summary>
    public static class ProcedureFailureHandler
    {
        /// <summary>
        /// 弹出失败决策对话框，等待用户选择重试、使用本地缓存或退出。
        /// </summary>
        public static IEnumerator WaitForStartupFailureDecision(
            ProcedureManager procedureManager,
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

            while (!hasDecision && !procedureManager.IsDone)
            {
                yield return null;
            }

            onDecision?.Invoke(decision);
        }

        /// <summary>
        /// 尝试使用本地缓存降级启动。
        /// </summary>
        public static IEnumerator TryUseLocalManifestFallback(
            ProcedureManager procedureManager,
            string reason,
            Action<bool> onCompleted)
        {
            bool succeeded = false;
            string error = string.Empty;
            yield return procedureManager.TryUseLocalManifestFallback(reason, (result, resultError) =>
            {
                succeeded = result;
                error = resultError;
            });

            if (succeeded)
            {
                onCompleted?.Invoke(true);
                yield break;
            }

            procedureManager.SetFailed(HotfixText.Get(HotfixTextKey.LocalCacheStartupFailed, error));
            onCompleted?.Invoke(false);
        }

        /// <summary>
        /// 根据错误信息关键词分类，返回用户可读的失败原因提示。
        /// </summary>
        public static string GetFailureHint(string error)
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

            if (lowerError.Contains("crc") || lowerError.Contains("hash"))
            {
                return HotfixText.Get(HotfixTextKey.DownloadVerifyFailedHint);
            }

            return HotfixText.Get(HotfixTextKey.NetworkOrRemoteResourceErrorHint);
        }
    }
}
