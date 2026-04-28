using System.Collections;
using Framework.Assemblies;
using Framework.Events;
using Framework.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
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
                    desc = "加载AOT元数据"
                });
            }).UnRegisterWhenGameObjectDestroyed(CoroutineController.manager);

            CoroutineController.manager.StartCoroutine(LoadAOTMetadata());
        }

        private IEnumerator LoadAOTMetadata()
        {
            isLoading = true;

            var package = YooAssets.GetPackage(mTarget._packageName);
            var loader = new HybridCLRAssemblyLoader();
            yield return loader.LoadAotMetadata(package, progress => rawProgress = progress);

            isLoading = false;
            if (!loader.Succeeded)
            {
                var error = string.IsNullOrEmpty(loader.Error) ? "AOT元数据加载失败！" : loader.Error;
                UIPanelRoot.Instance.ShowMessage(error);
                mTarget.SetFailed(error);
                yield break;
            }

            TypeEventSystem.Global.Send(new OnAssetloadProgressEvent
            {
                progress = 1f,
                desc = "AOT元数据加载完成"
            });
            mFSM.ChangeState(ResPackageStates.LoadAssemblies);
        }

        protected override void OnExit()
        {
            isLoading = false;
        }
    }
}
