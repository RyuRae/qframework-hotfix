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
            return mFSM.CurrentStateId == ResPackageStates.UpdatePackageManifest
                   || mFSM.CurrentStateId == ResPackageStates.CreateDownloader
                   || mFSM.CurrentStateId == ResPackageStates.DownloadPackageOver;
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

                TypeEventSystem.Global.Send(new OnAssetloadProgressEvent { progress = displayProgress, desc = "加载热更程序集" });
            }).UnRegisterWhenGameObjectDestroyed(CoroutineController.manager);

            CoroutineController.manager.StartCoroutine(LoadAssemblies());
        }

        private IEnumerator LoadAssemblies()
        {
            isLoading = true;

            var package = YooAssets.GetPackage(mTarget._packageName);
            var loader = new HybridCLRAssemblyLoader();
            yield return loader.LoadHotUpdateAssemblies(package, progress => rawProgress = progress);

            isLoading = false;
            if (!loader.Succeeded)
            {
                var error = string.IsNullOrEmpty(loader.Error) ? "代码加载失败！" : loader.Error;
                UIPanelRoot.Instance.ShowMessage(error);
                mTarget.SetFailed(error);
                yield break;
            }

            mTarget.SetHotfixEntry(loader.EntrySceneAddress, loader.EntryTypeName, loader.EntryMethodName);
            rawProgress = 1f;
            displayProgress = 1f;
            TypeEventSystem.Global.Send(new OnAssetloadProgressEvent { progress = 1f, desc = "热更程序集加载完成" });
            LogKit.I("Hot update assemblies loaded.");
            mFSM.ChangeState(ResPackageStates.ClearCacheBundle);
        }

        protected override void OnExit()
        {
            isLoading = false;
        }
    }
}
