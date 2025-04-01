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
            LogKit.I("��ǰ״̬��ProcedureInitializePackage");
            CoroutineController.manager.StartCoroutine(InitPackage());
        }

        ResourcePackage _rawFilePackage = null;
        InitializationOperation initRawFileOperation = null;
        IEnumerator InitPackage()
        {
            var playMode = _manager._playMode;
            var packageName = _manager._packageName;

            // ��������Դ������
            var package = YooAssets.TryGetPackage(packageName) ?? YooAssets.CreatePackage(packageName);
            YooAssets.SetDefaultPackage(package);
            InitializationOperation initializationOperation = null;

            if(_manager._isIncludeRawFile)
                //����ԭ���ļ�������
                _rawFilePackage = YooAssets.TryGetPackage(_manager._rawfilwPkgName) ?? YooAssets.CreatePackage(_manager._rawfilwPkgName);
           

            // �༭���µ�ģ��ģʽ
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                //����Դ��
                var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
                var packageRoot = buildResult.PackageRootDirectory;
                var createParameters = new EditorSimulateModeParameters();
                createParameters.EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
                initializationOperation = package.InitializeAsync(createParameters);

                //ԭ����Դ��
                if (_manager._isIncludeRawFile)
                {
                    var rawfileBuildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
                    var rawfilePkgRoot = rawfileBuildResult.PackageRootDirectory;
                    var createParameters2 = new EditorSimulateModeParameters();
                    createParameters2.EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(rawfilePkgRoot);
                    initRawFileOperation = _rawFilePackage.InitializeAsync(createParameters2);
                }
            }
            // ��������ģʽ
            else if (playMode == EPlayMode.OfflinePlayMode)
            {
                //���ļ���ʼ��
                var createParameters = new OfflinePlayModeParameters();
                createParameters.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                initializationOperation = package.InitializeAsync(createParameters);

                //ԭ���ļ���ʼ��
                if (_manager._isIncludeRawFile)
                {
                    var createParameters2 = new OfflinePlayModeParameters();
                    createParameters2.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                    initRawFileOperation = _rawFilePackage.InitializeAsync(createParameters2);
                }
            }
            // ��������ģʽ
            else if (playMode == EPlayMode.HostPlayMode)
            {
                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                //����Դ��
                var createParameters = new HostPlayModeParameters();
                createParameters.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                createParameters.CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                initializationOperation = package.InitializeAsync(createParameters);

                //ԭ����Դ��
                if (_manager._isIncludeRawFile)
                {
                    var createParameters2 = new HostPlayModeParameters();
                    createParameters2.BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                    createParameters2.CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                    initRawFileOperation = _rawFilePackage.InitializeAsync(createParameters2);
                }
            }
            // WebGL����ģʽ
            else if (playMode == EPlayMode.WebPlayMode)
            {
//                var createParameters = new WebPlayModeParameters();
//#if UNITY_WEBGL && WEIXINMINIGAME && !UNITY_EDITOR
//			string defaultHostServer = GetHostServerURL();
//            string fallbackHostServer = GetHostServerURL();
//            string packageRoot = $"{WeChatWASM.WX.env.USER_DATA_PATH}/__GAME_FILE_CACHE"; //ע�⣺�������Ŀ¼�����޸Ĵ˴���
//            IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
//            createParameters.WebServerFileSystemParameters = WechatFileSystemCreater.CreateWechatFileSystemParameters(packageRoot, remoteServices);
//            LogKit.I("��Դ����ʼ���ɹ���");
//#else
//                createParameters.WebServerFileSystemParameters = FileSystemParameters.CreateDefaultWebServerFileSystemParameters(new WebDecryption());
//#endif
//                initializationOperation = package.InitializeAsync(createParameters);

                string defaultHostServer = GetHostServerURL();
                string fallbackHostServer = GetHostServerURL();
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                var webServerFileSystemParams = FileSystemParameters.CreateDefaultWebServerFileSystemParameters();
                var webRemoteFileSystemParams = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices); //֧�ֿ�������
                //����Դ��
                var initParameters = new WebPlayModeParameters();
                initParameters.WebServerFileSystemParameters = webServerFileSystemParams;
                initParameters.WebRemoteFileSystemParameters = webRemoteFileSystemParams;
                initializationOperation = package.InitializeAsync(initParameters);

                //ԭ����Դ(webgl,�ٷ���֧��ԭ�����������ο�bytes�������)
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

            // �����ʼ��ʧ�ܵ�����ʾ����
            if (initializationOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning($"{initializationOperation.Error}");
                UIPanelRoot.Instance.ShowMessage("��ʼ��ʧ�ܣ�");
            }
            else
            {
                Debug.Log("��Դ����ʼ���ɹ���");
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
        /// ��ȡ��Դ��������ַ
        /// </summary>
        private string GetHostServerURL()
        {
            //string hostServerIP = "http://10.0.2.2"; //��׿ģ������ַ
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
        /// Զ����Դ��ַ��ѯ������
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