using Framework.Assemblies;
using Framework.Events;
using Framework.UI;
using QFramework;
using System.Collections;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    /// <summary>
    /// 加载热更代码资源
    /// </summary>
    public class ProcedureLoadAssembly : AbstractState<ResPackageStates, ProcedureManager>
    {
        private float rawProgress;
        private float displayProgress;
        private bool isLoading;

        public ProcedureLoadAssembly(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.LoadAOTMetadata;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureLoadAssembly");
            rawProgress = 0f;
            displayProgress = 0f;

            ActionKit.OnUpdate.Register(() =>
            {
                if (!isLoading) return;

                if (displayProgress < 0.98f)
                    displayProgress = Mathf.Lerp(displayProgress, rawProgress, Time.deltaTime * 10f);
                else
                    displayProgress = 1f;

                TypeEventSystem.Global.Send(new OnAssetloadProgressEvent
                {
                    progress = displayProgress,
                    desc = HotfixText.Get(HotfixTextKey.LoadingHotUpdateAssemblies)
                });
            }).UnRegisterWhenGameObjectDestroyed(CoroutineController.manager);

            CoroutineController.manager.StartCoroutine(LoadAssemblies());
        }

        private IEnumerator LoadAssemblies()
        {
            isLoading = true;

            var package = YooAssets.GetPackage(mTarget.MainPackageName);
            var loader = new HybridCLRAssemblyLoader();
            yield return loader.LoadHotUpdateAssemblies(package, mTarget.AssemblyLoadContext, progress => rawProgress = progress);

            isLoading = false;
            if (!loader.Succeeded)
            {
                var error = string.IsNullOrEmpty(loader.Error)
                    ? HotfixText.Get(HotfixTextKey.HotUpdateAssemblyLoadFailed)
                    : loader.Error;
                UIPanelRoot.Instance.ShowMessage(error);
                mTarget.SetFailed(error);
                yield break;
            }

            mTarget.SetHotfixEntryType(loader.EntryTypeName);
            if (!mTarget.ValidateLoadedAssemblyCombination(out var combinationError))
            {
                UIPanelRoot.Instance.ShowMessage(combinationError);
                mTarget.SetFailed(combinationError);
                yield break;
            }

            if (!mTarget.ValidateRawFileManifestTrust(out var rawFileTrustError))
            {
                UIPanelRoot.Instance.ShowMessage(rawFileTrustError);
                mTarget.SetFailed(rawFileTrustError);
                yield break;
            }

            rawProgress = 1f;
            displayProgress = 1f;
            TypeEventSystem.Global.Send(new OnAssetloadProgressEvent
            {
                progress = 1f,
                desc = HotfixText.Get(HotfixTextKey.HotUpdateAssembliesLoaded)
            });
            LogKit.I("Hot update assemblies loaded.");
            mFSM.ChangeState(ResPackageStates.StartGame);
        }

        protected override void OnExit()
        {
            isLoading = false;
        }
    }
}
