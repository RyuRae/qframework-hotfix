using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;

namespace MsbFramework.Procedure
{
    /// <summary>
    /// 资源状态
    /// </summary>
    public enum ResPackageStates
    { 
        /// <summary>
        /// 初始化资源包
        /// </summary>
        InitializePackage,
        /// <summary>
        /// 检查资源版本
        /// </summary>
        RequestPackageVersion,
        /// <summary>
        /// 更新资源包清单
        /// </summary>
        UpdatePackageManifest,
        /// <summary>
        /// 创建下载器
        /// </summary>
        CreateDownloader,
        /// <summary>
        /// 下载文件
        /// </summary>
        DownloadPackageFiles,
        /// <summary>
        /// 下载结束
        /// </summary>
        DownloadPackageOver,
        /// <summary>
        /// 加载程序集
        /// </summary>
        LoadAssemblies,
        /// <summary>
        /// 清理下载缓存
        /// </summary>
        ClearCacheBundle,
        /// <summary>
        /// 开始游戏
        /// </summary>
        StartGame
    }

    public class ProcedureManager : GameAsyncOperation
    {
        /// <summary>
        /// 资源包名
        /// </summary>
        public readonly string _packageName;
        /// <summary>
        /// 原生文件资源包名
        /// </summary>
        public readonly string _rawfilwPkgName;
        /// <summary>
        /// 播放模式
        /// </summary>
        public readonly EPlayMode _playMode;
        /// <summary>
        /// 资源包版本信息
        /// </summary>
        public string _packageVersion;
        //是否包含原生资源
        public bool _isIncludeRawFile;

        /// <summary>
        /// 资源包版本信息
        /// </summary>
        public string _rawfilePkgVersion;
        /// <summary>
        /// 下载器
        /// </summary>
        public ResourceDownloaderOperation _downloaderOperation;
        /// <summary>
        /// 原生文件下载器
        /// </summary>
        public ResourceDownloaderOperation _downloaderRawfile;

        public FSM<ResPackageStates> _mFSM = new FSM<ResPackageStates>();
        public ProcedureManager(string packageName, EPlayMode playMode, bool IsIncludeRawFile = false)
        {
            _packageName = packageName;
            _playMode = playMode;
            _isIncludeRawFile = IsIncludeRawFile;
            if (_isIncludeRawFile)
                _rawfilwPkgName = Boot.rawfilePackageName;

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
