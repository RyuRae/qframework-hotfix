using Cysharp.Threading.Tasks;
using Main.Editor;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MsbFramework.Procedure
{
    public class ProcedureLoadConfig : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureLoadConfig(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {

        }

        protected override void OnEnter()
        {
            base.OnEnter();
            LogKit.I("加载配置文件");
            CoroutineController.manager.StartCoroutine(LoadConfigAssets());
        }


        IEnumerator LoadConfigAssets()
        {
            LoadConfigAsset().Forget();
            //加载Asset配置
            yield return new WaitUntil(() => YooAssetHybridCLRSetting.Instance != null);
            mFSM.ChangeState(ResPackageStates.LoadAssemblies);
        }

        //初始化配置
        private async UniTask LoadConfigAsset()
        {
            if(YooAssetHybridCLRSetting.Instance == null)
            await YooAssetHybridCLRSetting.GetInstanceAsync();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

        }


        protected override void OnExit()
        {
            base.OnExit();

        }
    }
}