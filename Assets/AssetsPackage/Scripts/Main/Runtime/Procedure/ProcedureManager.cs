using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;

namespace MsbFramework.Procedure
{
    /// <summary>
    /// ��Դ״̬
    /// </summary>
    public enum ResPackageStates
    { 
        /// <summary>
        /// ��ʼ����Դ��
        /// </summary>
        InitializePackage,
        /// <summary>
        /// �����Դ�汾
        /// </summary>
        RequestPackageVersion,
        /// <summary>
        /// ������Դ���嵥
        /// </summary>
        UpdatePackageManifest,
        /// <summary>
        /// ����������
        /// </summary>
        CreateDownloader,
        /// <summary>
        /// �����ļ�
        /// </summary>
        DownloadPackageFiles,
        /// <summary>
        /// ���ؽ���
        /// </summary>
        DownloadPackageOver,
        /// <summary>
        /// ���س���
        /// </summary>
        LoadAssemblies,
        /// <summary>
        /// �������ػ���
        /// </summary>
        ClearCacheBundle,
        /// <summary>
        /// ��ʼ��Ϸ
        /// </summary>
        StartGame
    }

    public class ProcedureManager : GameAsyncOperation
    {
        /// <summary>
        /// ��Դ����
        /// </summary>
        public readonly string _packageName;
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public readonly EPlayMode _playMode;
        /// <summary>
        /// ��Դ���汾��Ϣ
        /// </summary>
        public string _packageVersion;
        /// <summary>
        /// ������
        /// </summary>
        public ResourceDownloaderOperation _downloaderOperation;

        public FSM<ResPackageStates> _mFSM = new FSM<ResPackageStates>();
        public ProcedureManager(string packageName, EPlayMode playMode)
        {
            _packageName = packageName;
            _playMode = playMode;

            _mFSM.AddState(ResPackageStates.InitializePackage, new ProcedureInitializePackage(_mFSM, this));
            _mFSM.AddState(ResPackageStates.RequestPackageVersion, new ProcedureRequestPackageVersion(_mFSM, this));
            _mFSM.AddState(ResPackageStates.UpdatePackageManifest, new ProcedureUpdatePackageManifest(_mFSM, this));
            _mFSM.AddState(ResPackageStates.CreateDownloader, new ProcedureCreateDownloader(_mFSM, this));
            _mFSM.AddState(ResPackageStates.DownloadPackageFiles, new ProcedureDownloadPackageFiles(_mFSM, this));
            _mFSM.AddState(ResPackageStates.DownloadPackageOver, new ProcedureDownloadPackageOver(_mFSM, this));
            _mFSM.AddState(ResPackageStates.LoadAssemblies, new ProcedureLoadAssembly(_mFSM, this));
            _mFSM.AddState(ResPackageStates.ClearCacheBundle, new ProcedureClearCacheBundle(_mFSM, this));
            _mFSM.AddState(ResPackageStates.StartGame, new ProcedureStartGame(_mFSM, this));

        }

        protected override void OnAbort()
        {
           
        }

        protected override void OnStart()
        {
            _mFSM.StartState(ResPackageStates.InitializePackage);
        }

        protected override void OnUpdate()
        {
            _mFSM.Update();
        }


        public void SetFinish()
        { 
            Status = EOperationStatus.Succeed;
        }
    }
}
