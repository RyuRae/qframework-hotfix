using System;
using System.Linq;
using System.Reflection;
using QFramework;

namespace Framework.Procedure
{
    public class ProcedureStartGame : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureStartGame(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.LoadAssemblies;
        }

        protected override void OnEnter()
        {
           LogKit.I("Current state: ProcedureStartGame");

            // 关键：在调用 CodeEntry 前设置默认资源包
            YooAssetKit.SetDefaultPackage(mTarget.MainPackageName);

            if (!HotfixCodeEntryInvoker.InvokeEntryMethod(mTarget.EntryTypeName, mTarget.EntryMethodName, out var error))
            {
                mTarget.SetFailed(error);
                return;
            }

            LogKit.I("Hotfix CodeEntry started.");
            if (mTarget.CommitLastGood(out var commitError))
            {
                mFSM.ChangeState(ResPackageStates.ClearCacheBundle);
                return;
            }

            // 业务已经启动成功时不因本地持久化或完整性检查失败而强制退出。
            // 跳过缓存清理，确保旧 LastGood 仍有机会在下次启动时使用。
            LogKit.W($"Skip cache cleanup because LastGood was not committed. {commitError}");
            mTarget.SetFinish();
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }   

    }
}
