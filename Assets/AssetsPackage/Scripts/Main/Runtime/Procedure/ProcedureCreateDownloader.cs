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
            LogKit.I("��ǰ״̬��������Դ��������");
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
            ResourcePackage rawfilePkg = null;
            ResourceDownloaderOperation downloaderRawfile = null;
            if (mTarget._isIncludeRawFile)
            {
                rawfilePkg = YooAssets.GetPackage(mTarget._rawfilwPkgName);
                downloaderRawfile = rawfilePkg.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
                mTarget._downloaderRawfile = downloaderRawfile;
            }

            if (downloader.TotalDownloadCount == 0)
            {
                //�ر�loading����
                UIPanelRoot.Instance.CloseLoadingPanel();
                Debug.Log("Not found any download files !");
                mFSM.ChangeState(ResPackageStates.LoadAssemblies);
            }
            else
            {
                // �����¸����ļ��󣬹�������ϵͳ
                // ע�⣺��������Ҫ������ǰ�����̿ռ䲻��
                int totalDownloadCount = mTarget._isIncludeRawFile ? downloader.TotalDownloadCount + downloaderRawfile.TotalDownloadCount : downloader.TotalDownloadCount;
                long totalDownloadBytes = mTarget._isIncludeRawFile ? downloader.TotalDownloadBytes + downloaderRawfile.TotalDownloadBytes : downloader.TotalDownloadBytes;
                OnDownloadInfoHandlerEvent onDownloadInfoHandler = new OnDownloadInfoHandlerEvent()
                { 
                    totalDownloadCount = totalDownloadCount,
                    totalDownloadBytes = totalDownloadBytes,
                    confirmCallBack = () => 
                    {
                        //�л�Ϊ����״̬
                        mFSM.ChangeState(ResPackageStates.DownloadPackageFiles);    
                    }
                };
                //����������Ϣ��ʾ
                TypeEventSystem.Global.Send(onDownloadInfoHandler);
            }
        }
    }
}