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
            YooAssetKit.SetDefaultPackage(mTarget._packageName);

            if (!InvokeEntryMethod(out var error))
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

        private bool InvokeEntryMethod(out string error)
        {
            error = string.Empty;

            string EntryTypeName = mTarget.EntryTypeName;
            string EntryMethodName = mTarget.EntryMethodName;
            if (string.IsNullOrWhiteSpace(EntryTypeName) ||
                string.IsNullOrWhiteSpace(EntryMethodName))
            {
                error = "Hotfix entry type or method name is null.";
                return false;
            }

            var entryType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(EntryTypeName))
                .FirstOrDefault(type => type != null) ?? Type.GetType(EntryTypeName);
            if (entryType == null)
            {
               
                error = $"Hotfix entry type not found: {EntryTypeName}";
                return false;
            }

            var entryMethod = entryType.GetMethod(
                EntryMethodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (entryMethod == null)
            {
               
                error = $"Hotfix entry method not found: {EntryTypeName}.{EntryMethodName}";
                return false;
            }

            try
            {
                entryMethod.Invoke(null, null);
                LogKit.I($"[HotfixCodeEntryInvoker] Invoked {EntryTypeName}.{EntryMethodName}().");
                return true;
            }
            catch (TargetInvocationException exception)
            {
                error = $"Invoke Hotfix CodeEntry failed: {EntryTypeName}.{EntryMethodName}().\n{exception.InnerException ?? exception}";
                return false;
            }
            catch (Exception exception)
            {
                error = $"Invoke Hotfix CodeEntry failed: {EntryTypeName}.{EntryMethodName}().\n{exception}";
                return false;
            }
        }

    }
}
