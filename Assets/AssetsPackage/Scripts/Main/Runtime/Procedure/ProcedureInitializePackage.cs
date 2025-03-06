using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using YooAsset;
using System;
using MsbFramework.UI;

namespace MsbFramework.Procedure
{
    public class ProcedureInitializePackage : AbstractState<ResPackageStates, ProcedureManager>
    {
        private ProcedureManager _manager;
        private FSM<ResPackageStates> _fsm;
        public ProcedureInitializePackage(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
            _fsm = fsm;
            _manager = manager;
        }

        protected override bool OnCondition()
        {
            return true;
        }

        protected override void OnEnter()
        {
            LogKit.I("当前状态：ProcedureInitializePackage");
            CoroutineController.manager.StartCoroutine(InitPackage());
        }

        IEnumerator InitPackage()
        {
            var playMode = _manager._playMode;
            var packageName = _manager._packageName;

            // 创建资源包裹类
            var package = YooAssets.TryGetPackage(packageName) ?? YooAssets.CreatePackage(packageName);
            YooAssets.SetDefaultPackage(package);
            InitializationOperation initializationOperation = null;

            // 编辑器下的模拟模式
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
                var packageRoot = buildResult.PackageRootDirectory;
                var createParameters = new EditorSimulateModeParameters();
                createParameters.EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
                initializationOperation = package.InitializeAsync(createParameters);
            }
            // 单机运行模式
            else if (playMode == EPlayMode.OfflinePlayMode)
            {
                var createParameters = new OfflinePlayModeParameters();
                createParameters.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                initializationOperation = package.InitializeAsync(createParameters);
            }
            // 联机运行模式
            else if (playMode == EPlayMode.HostPlayMode)
            {
                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                var createParameters = new HostPlayModeParameters();
                createParameters.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                createParameters.CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                initializationOperation = package.InitializeAsync(createParameters);
            }
            // WebGL运行模式
            else if (playMode == EPlayMode.WebPlayMode)
            {
                var createParameters = new WebPlayModeParameters();
#if UNITY_WEBGL && WEIXINMINIGAME && !UNITY_EDITOR
			string defaultHostServer = GetHostServerURL();
            string fallbackHostServer = GetHostServerURL();
            string packageRoot = $"{WeChatWASM.WX.env.USER_DATA_PATH}/__GAME_FILE_CACHE"; //注意：如果有子目录，请修改此处！
            IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
            createParameters.WebServerFileSystemParameters = WechatFileSystemCreater.CreateWechatFileSystemParameters(packageRoot, remoteServices);
#else
                createParameters.WebServerFileSystemParameters = FileSystemParameters.CreateDefaultWebServerFileSystemParameters(new WebDecryption());
#endif
                initializationOperation = package.InitializeAsync(createParameters);
            }

            yield return initializationOperation;

            // 如果初始化失败弹出提示界面
            if (initializationOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning($"{initializationOperation.Error}");
                UIPanelRoot.Instance.ShowMessage("初始化失败！");
            }
            else
            {
                _fsm.ChangeState(ResPackageStates.RequestPackageVersion);
            }
        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }

        /// <summary>
        /// 获取资源服务器地址
        /// </summary>
        private string GetHostServerURL()
        {
            //string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
            string hostServerIP = "http://127.0.0.1:8080/TestProject/PC";
            //            string appVersion = "v1.0";

            //#if UNITY_EDITOR
            //            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
            //                return $"{hostServerIP}/CDN/Android/{appVersion}";
            //            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
            //                return $"{hostServerIP}/CDN/IPhone/{appVersion}";
            //            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
            //                return $"{hostServerIP}/CDN/WebGL/{appVersion}";
            //            else
            //                return $"{hostServerIP}/CDN/PC/{appVersion}";
            //#else
            //        if (Application.platform == RuntimePlatform.Android)
            //            return $"{hostServerIP}/CDN/Android/{appVersion}";
            //        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            //            return $"{hostServerIP}/CDN/IPhone/{appVersion}";
            //        else if (Application.platform == RuntimePlatform.WebGLPlayer)
            //            return $"{hostServerIP}/CDN/WebGL/{appVersion}";
            //        else
            //            return $"{hostServerIP}/CDN/PC/{appVersion}";
            //#endif

            return hostServerIP;
        }

        /// <summary>
        /// 远端资源地址查询服务类
        /// </summary>
        private class RemoteServices : IRemoteServices
        {
            private readonly string _defaultHostServer;
            private readonly string _fallbackHostServer;

            public RemoteServices(string defaultHostServer, string fallbackHostServer)
            {
                _defaultHostServer = defaultHostServer;
                _fallbackHostServer = fallbackHostServer;
            }
            string IRemoteServices.GetRemoteMainURL(string fileName)
            {
                return $"{_defaultHostServer}/{fileName}";
            }
            string IRemoteServices.GetRemoteFallbackURL(string fileName)
            {
                return $"{_fallbackHostServer}/{fileName}";
            }
        }

        private class WebDecryption : IWebDecryptionServices
        {
            public const byte KEY = 64;

            public WebDecryptResult LoadAssetBundle(WebDecryptFileInfo fileInfo)
            {
                byte[] copyData = new byte[fileInfo.FileData.Length];
                Buffer.BlockCopy(fileInfo.FileData, 0, copyData, 0, fileInfo.FileData.Length);

                for (int i = 0; i < copyData.Length; i++)
                {
                    copyData[i] ^= KEY;
                }

                WebDecryptResult decryptResult = new WebDecryptResult();
                decryptResult.Result = AssetBundle.LoadFromMemory(copyData);
                return decryptResult;
            }
        }
    }
}