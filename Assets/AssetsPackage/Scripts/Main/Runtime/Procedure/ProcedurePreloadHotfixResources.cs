using System;
using System.Collections;
using System.Threading.Tasks;
using Framework.Events;
using QFramework;
using UnityEngine;

namespace Framework.Procedure
{
    /// <summary>
    /// 在热更程序集加载完成后、业务启动前，调度热更层声明的关键资源预加载。
    /// </summary>
    public sealed class ProcedurePreloadHotfixResources : AbstractState<ResPackageStates, ProcedureManager>
    {
        private bool _isPreloading;
        private float _reportedProgress;

        public ProcedurePreloadHotfixResources(
            FSM<ResPackageStates> fsm,
            ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.LoadAssemblies;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedurePreloadHotfixResources");
            CoroutineController.manager.StartCoroutine(PreloadHotfixResources());
        }

        private IEnumerator PreloadHotfixResources()
        {
            var entry = mTarget.HotfixEntry;
            var context = mTarget.HotfixContext;
            if (entry == null || context == null)
            {
                mTarget.SetFailed("Hotfix entry was not initialized before resource preload.");
                yield break;
            }

            if (!(entry is IHotfixResourcePreloader preloader))
            {
                LogKit.I($"Hotfix entry does not implement {typeof(IHotfixResourcePreloader).FullName}; skip resource preload.");
                SendProgress(1f, HotfixText.Get(HotfixTextKey.HotfixResourcesPreloaded));
                mFSM.ChangeState(ResPackageStates.StartGame);
                yield break;
            }

            _reportedProgress = 0f;
            _isPreloading = true;
            SendProgress(0f, HotfixText.Get(HotfixTextKey.PreloadingHotfixResources));
            IProgress<HotfixPreloadProgress> progress =
                new Progress<HotfixPreloadProgress>(OnProgressReported);

            Task preloadTask;
            try
            {
                preloadTask = HotfixCodeEntryInvoker.PreloadAsync(
                    preloader,
                    context,
                    progress,
                    mTarget.StartupCancellationToken);
            }
            catch (Exception exception)
            {
                _isPreloading = false;
                mTarget.SetFailed(BuildPreloadError(exception));
                yield break;
            }

            while (!preloadTask.IsCompleted && !mTarget.IsDone)
            {
                yield return null;
            }

            _isPreloading = false;
            if (mTarget.IsDone)
            {
                HotfixCodeEntryInvoker.ObserveFailure(preloadTask);
                yield break;
            }

            if (preloadTask.IsCanceled)
            {
                mTarget.SetFailed(HotfixText.Get(HotfixTextKey.HotfixResourcePreloadCanceled));
                yield break;
            }

            if (preloadTask.IsFaulted)
            {
                mTarget.SetFailed(BuildPreloadError(preloadTask.Exception));
                yield break;
            }

            SendProgress(1f, HotfixText.Get(HotfixTextKey.HotfixResourcesPreloaded));
            LogKit.I("Hotfix resource preload completed.");
            mFSM.ChangeState(ResPackageStates.StartGame);
        }

        private void OnProgressReported(HotfixPreloadProgress progress)
        {
            if (!_isPreloading || mTarget.IsDone)
            {
                return;
            }

            float normalizedProgress = Mathf.Clamp01(progress.Progress);
            _reportedProgress = Mathf.Max(_reportedProgress, normalizedProgress);
            string description = string.IsNullOrWhiteSpace(progress.Description)
                ? HotfixText.Get(HotfixTextKey.PreloadingHotfixResources)
                : progress.Description;
            SendProgress(_reportedProgress, description);
        }

        private static void SendProgress(float progress, string description)
        {
            TypeEventSystem.Global.Send(new OnAssetloadProgressEvent
            {
                progress = Mathf.Clamp01(progress),
                desc = description
            });
        }

        private static string BuildPreloadError(Exception exception)
        {
            Exception rootException = HotfixCodeEntryInvoker.GetRootException(exception);
            if (rootException is OperationCanceledException)
            {
                return HotfixText.Get(HotfixTextKey.HotfixResourcePreloadCanceled);
            }

            return HotfixText.Get(HotfixTextKey.HotfixResourcePreloadFailed, rootException);
        }

        protected override void OnExit()
        {
            _isPreloading = false;
        }
    }
}
