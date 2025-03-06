using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace MsbFramework.Procedure
{
    public class ProcedureDownloadPackageOver : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureDownloadPackageOver(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {

        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.DownloadPackageFiles;
        }

        protected override void OnEnter()
        {
            LogKit.I("��Դ�ļ�������ɣ�");
            mFSM.ChangeState(ResPackageStates.LoadAssemblies);
        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }
    }
}
