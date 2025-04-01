using System.Collections;
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

        ResourcePackage _rawFilePackage = null;
        InitializationOperation initRawFileOperation = null;
        IEnumerator InitPackage()
        {
            var playMode = _manager._playMode;
            var packageName = _manager._packageName;

            // 创建主资源包裹类
            var package = YooAssets.TryGetPackage(packageName) ?? YooAssets.CreatePackage(packageName);
            YooAssets.SetDefaultPackage(package);
            InitializationOperation initializationOperation = null;

            if(_manager._isIncludeRawFile)
                //创建原生文件包裹类
                _rawFilePackage = YooAssets.TryGetPackage(_manager._rawfilwPkgName) ?? YooAssets.CreatePackage(_manager._rawfilwPkgName);
           

            // 编辑器下的模拟模式
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                //主资源包
                var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
                var packageRoot = buildResult.PackageRootDirectory;
                var createParameters = new EditorSimulateModeParameters();
                createParameters.EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
                initializationOperation = package.InitializeAsync(createParameters);

                //原生资源包
                if (_manager._isIncludeRawFile)
                {
                    var rawfileBuildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
                    var rawfilePkgRoot = rawfileBuildResult.PackageRootDirectory;
                    var createParameters2 = new EditorSimulateModeParameters();
                    createParameters2.EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(rawfilePkgRoot);
                    initRawFileOperation = _rawFilePackage.InitializeAsync(createParameters2);
                }
            }
            // 单机运行模式
            else if (playMode == EPlayMode.OfflinePlayMode)
            {
                //主文件初始化
                var createParameters = new OfflinePlayModeParameters();
                createParameters.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                initializationOperation = package.InitializeAsync(createParameters);

                //原生文件初始化
                if (_manager._isIncludeRawFile)
                {
                    var createParameters2 = new OfflinePlayModeParameters();
                    createParameters2.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                    initRawFileOperation = _rawFilePackage.InitializeAsync(createParameters2);
                }
            }
            // 联机运行模式
            else if (playMode == EPlayMode.HostPlayMode)
            {
                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                //主资源包
                var createParameters = new HostPlayModeParameters();
                createParameters.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                createParameters.CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                initializationOperation = package.InitializeAsync(createParameters);

                //原生资源包
                if (_manager._isIncludeRawFile)
                {
                    var createParameters2 = new HostPlayModeParameters();
                    createParameters2.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                    createParameters2.CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                    initRawFileOperation = _rawFilePackage.InitializeAsync(createParameters2);
                }
            }
            // WebGL运行模式
            else if (playMode == EPlayMode.WebPlayMode)
            {
//                var createParameters = new WebPlayModeParameters();
//#if UNITY_WEBGL && WEIXINMINIGAME && !UNITY_EDITOR
//			string defaultHostServer = GetHostServerURL();
//            string fallbackHostServer = GetHostServerURL();
//            string packageRoot = $"{WeChatWASM.WX.env.USER_DATA_PATH}/__GAME_FILE_CACHE"; //注意：如果有子目录，请修改此处！
//            IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
//            createParameters.WebServerFileSystemParameters = WechatFileSystemCreater.CreateWechatFileSystemParameters(packageRoot, remoteServices);
//            LogKit.I("资源包初始化成功！");
//#else
//                createParameters.WebServerFileSystemParameters = FileSystemParameters.CreateDefaultWebServerFileSystemParameters(new WebDecryption());
//#endif
//                initializationOperation = package.InitializeAsync(createParameters);

                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                var webServerFileSystemParams = FileSystemParameters.CreateDefaultWebServerFileSystemParameters();
                var webRemoteFileSystemParams = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices); //支持跨域下载
                //主资源包
                var initParameters = new WebPlayModeParameters();
                initParameters.WebServerFileSystemParameters = webServerFileSystemParams;
                initParameters.WebRemoteFileSystemParameters = webRemoteFileSystemParams;
                initializationOperation = package.InitializeAsync(initParameters);

                //原生资源(webgl,官方不支持原生包构建，参考bytes解决方案)
                if (_manager._isIncludeRawFile)
                {
                    var initParameters2 = new WebPlayModeParameters();
                    initParameters2.WebServerFileSystemParameters = webServerFileSystemParams;
                    initParameters2.WebRemoteFileSystemParameters = webRemoteFileSystemParams;
                    //initParameters2.WebServerFileSystemParameters = WechatFileSystemCreater.CreateWechatFileSystemParameters(packageRoot, remoteServices);
                    initRawFileOperation = _rawFilePackage.InitializeAsync(initParameters2);
                }
            }

            yield return initializationOperation;
            if (_manager._isIncludeRawFile)
                yield return initRawFileOperation;

            // 如果初始化失败弹出提示界面
            if (initializationOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning($"{initializationOperation.Error}");
                UIPanelRoot.Instance.ShowMessage("初始化失败！");
            }
            else
            {
                Debug.Log("资源包初始化成功！");
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
            string hostServerIP = "http://127.0.0.1:8080/TestProject/PC";//192.168.125.148
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