using System;
using System.Collections;
using System.Threading.Tasks;
using QFramework;

namespace Framework.Procedure
{
    /// <summary>等待 IHotfixEntry.StartAsync 真正成功，再提交 LastGood 并完成启动流程。</summary>
    public class ProcedureStartGame : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureStartGame(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.PreloadHotfixResources;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureStartGame");
            CoroutineController.manager.StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            var entry = mTarget.HotfixEntry;
            var context = mTarget.HotfixContext;
            if (entry == null || context == null)
            {
                mTarget.SetFailed("Hotfix entry was not initialized before business startup.");
                yield break;
            }

            Task startupTask;
            try
            {
                startupTask = HotfixCodeEntryInvoker.StartAsync(
                    entry,
                    context,
                    mTarget.StartupCancellationToken);
            }
            catch (Exception exception)
            {
                mTarget.SetFailed(BuildStartupError(exception));
                yield break;
            }

            while (!startupTask.IsCompleted && !mTarget.IsDone)
            {
                yield return null;
            }

            if (mTarget.IsDone)
            {
                HotfixCodeEntryInvoker.ObserveFailure(startupTask);
                yield break;
            }

            if (startupTask.IsCanceled)
            {
                mTarget.SetFailed("Hotfix business startup was canceled.");
                yield break;
            }

            if (startupTask.IsFaulted)
            {
                mTarget.SetFailed(BuildStartupError(startupTask.Exception));
                yield break;
            }

            LogKit.I("Hotfix business startup completed.");
            if (mTarget.CommitLastGood(out var commitError))
            {
                mFSM.ChangeState(ResPackageStates.ClearCacheBundle);
                yield break;
            }

            // 业务已经启动成功时不因本地持久化或完整性检查失败而强制退出。
            // 跳过缓存清理，确保旧 LastGood 仍有机会在下次启动时使用。
            LogKit.W($"Skip cache cleanup because LastGood was not committed. {commitError}");
            mTarget.SetFinish();
        }

        private static string BuildStartupError(Exception exception)
        {
            Exception rootException = HotfixCodeEntryInvoker.GetRootException(exception);
            if (rootException is OperationCanceledException)
            {
                return "Hotfix business startup was canceled.";
            }

            return $"Hotfix business startup failed.\n{rootException}";
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }   

    }
}
