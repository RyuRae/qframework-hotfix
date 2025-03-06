using MsbFramework.Procedure;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using YooAsset;

namespace MsbFramework.Procedure
{
    public class ProcedureStartGame : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureStartGame(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {
            if(mTarget._playMode == EPlayMode.OfflinePlayMode)
                return mFSM.CurrentStateId == ResPackageStates.CreateDownloader;
            return mFSM.CurrentStateId == ResPackageStates.ClearCacheBundle;
        }

        protected override void OnEnter()
        {
            LogKit.I("开始游戏！");
            LogKit.I("处理开始逻辑！");
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