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
            return mFSM.CurrentStateId == ResPackageStates.ClearCacheBundle;
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
