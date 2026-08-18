using System.Collections;
using Framework.Assemblies;
using Framework.Events;
using Framework.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    /// <summary>验证 AOT/Hotfix 清单组合，并向 HybridCLR 补充当前热更代码所需的 AOT 元数据。</summary>
    public class ProcedureLoadAOTMetadata : AbstractState<ResPackageStates, ProcedureManager>
    {
        private float rawProgress;
        private float displayProgress;
        private bool isLoading;

        public ProcedureLoadAOTMetadata(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.CreateDownloader
                   || mFSM.CurrentStateId == ResPackageStates.DownloadPackageOver;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureLoadAOTMetadata");
            rawProgress = 0f;
            displayProgress = 0f;

            ActionKit.OnUpdate.Register(() =>
            {
                if (!isLoading)
                {
                    return;
                }

                displayProgress = Mathf.Lerp(displayProgress, rawProgress, Time.deltaTime * 10f);
                TypeEventSystem.Global.Send(new OnAssetloadProgressEvent
                {
                    progress = displayProgress,
                    desc = HotfixText.Get(HotfixTextKey.LoadingAotMetadata)
                });
            }).UnRegisterWhenGameObjectDestroyed(CoroutineController.manager);

            CoroutineController.manager.StartCoroutine(LoadAOTMetadata());
        }

        private IEnumerator LoadAOTMetadata()
        {
            isLoading = true;

            var package = YooAssets.GetPackage(mTarget.MainPackageName);
            var loader = new HybridCLRAssemblyLoader();
            yield return loader.LoadAotMetadata(package, mTarget.AssemblyLoadContext, progress => rawProgress = progress);

            isLoading = false;
            if (!loader.Succeeded)
            {
                var error = string.IsNullOrEmpty(loader.Error)
                    ? HotfixText.Get(HotfixTextKey.AotMetadataLoadFailed)
                    : loader.Error;
                UIPanelRoot.Instance.ShowMessage(error);
                mTarget.SetFailed(error);
                yield break;
            }

            // LastGood 不只固定包版本，也固定 Hotfix/AOT 组合。
            // 在加载热更 DLL 之前尽早阻断“同版本号内容被替换”的异常缓存。
            if (!mTarget.ValidateLoadedAssemblyCombination(out var combinationError))
            {
                UIPanelRoot.Instance.ShowMessage(combinationError);
                mTarget.SetFailed(combinationError);
                yield break;
            }

            TypeEventSystem.Global.Send(new OnAssetloadProgressEvent
            {
                progress = 1f,
                desc = HotfixText.Get(HotfixTextKey.AotMetadataLoaded)
            });
            mFSM.ChangeState(ResPackageStates.LoadAssemblies);
        }

        protected override void OnExit()
        {
            isLoading = false;
        }
    }
}
