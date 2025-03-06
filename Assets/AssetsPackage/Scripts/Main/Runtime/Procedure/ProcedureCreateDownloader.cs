using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;
using MsbFramework.UI;
using MsbFramework.Events;

namespace MsbFramework.Procedure
{
    public class ProcedureCreateDownloader : AbstractState<ResPackageStates, ProcedureManager>
    {
        public ProcedureCreateDownloader(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {

        }

        protected override bool OnCondition()
        {
            return mFSM.CurrentStateId == ResPackageStates.UpdatePackageManifest;
        }

        protected override void OnEnter()
        {
            LogKit.I("当前状态：创建资源下载器！");
            CreateDownloader();
        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }

        void CreateDownloader()
        {
           
            var packageName = mTarget._packageName;
            var package = YooAssets.GetPackage(packageName);
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            mTarget._downloaderOperation = downloader;

            if (downloader.TotalDownloadCount == 0)
            {
                //关闭loading界面
                UIPanelRoot.Instance.CloseLoadingPanel();
                Debug.Log("Not found any download files !");
                mFSM.ChangeState(ResPackageStates.LoadAssemblies);
            }
            else
            {
                // 发现新更新文件后，挂起流程系统
                // 注意：开发者需要在下载前检测磁盘空间不足
                OnDownloadInfoHandlerEvent onDownloadInfoHandler = new OnDownloadInfoHandlerEvent()
                { 
                    totalDownloadCount = downloader.TotalDownloadCount,
                    totalDownloadBytes = downloader.TotalDownloadBytes,
                    confirmCallBack = () => 
                    {
                        //切换为下载状态
                        mFSM.ChangeState(ResPackageStates.DownloadPackageFiles);    
                    }
                };
                //发送下载信息提示
                TypeEventSystem.Global.Send(onDownloadInfoHandler);
            }
        }
    }
}