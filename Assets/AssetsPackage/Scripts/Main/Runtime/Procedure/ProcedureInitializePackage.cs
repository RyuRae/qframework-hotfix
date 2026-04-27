using System;
using System.Collections;
using Framework.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    public class ProcedureInitializePackage : AbstractState<ResPackageStates, ProcedureManager>
    {
        private readonly ProcedureManager manager;
        private readonly FSM<ResPackageStates> fsm;
        private ResourcePackage rawFilePackage;
        private InitializationOperation initRawFileOperation;

        public ProcedureInitializePackage(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
            this.fsm = fsm;
            this.manager = manager;
        }

        protected override bool OnCondition()
        {
            return true;
        }

        protected override void OnEnter()
        {
            LogKit.I("Current state: ProcedureInitializePackage");
            CoroutineController.manager.StartCoroutine(InitPackage());
        }

        private IEnumerator InitPackage()
        {
            var playMode = manager._playMode;
            var package = YooAssets.TryGetPackage(manager._packageName) ?? YooAssets.CreatePackage(manager._packageName);
            YooAssets.SetDefaultPackage(package);

            if (manager._isIncludeRawFile)
            {
                rawFilePackage = YooAssets.TryGetPackage(manager._rawfilwPkgName) ?? YooAssets.CreatePackage(manager._rawfilwPkgName);
            }

            InitializationOperation initializationOperation = null;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                initializationOperation = InitEditorSimulatePackage(package, manager._packageName);
                if (manager._isIncludeRawFile)
                {
                    initRawFileOperation = InitEditorSimulatePackage(rawFilePackage, manager._rawfilwPkgName);
                }
            }
            else if (playMode == EPlayMode.OfflinePlayMode)
            {
                initializationOperation = InitOfflinePackage(package);
                if (manager._isIncludeRawFile)
                {
                    initRawFileOperation = InitOfflinePackage(rawFilePackage);
                }
            }
            else if (playMode == EPlayMode.HostPlayMode)
            {
                var remoteServices = new RemoteServices(GetHostServerURL(), GetHostServerURL());
                initializationOperation = InitHostPackage(package, remoteServices);
                if (manager._isIncludeRawFile)
                {
                    initRawFileOperation = InitHostPackage(rawFilePackage, remoteServices);
                }
            }
            else if (playMode == EPlayMode.WebPlayMode)
            {
                var remoteServices = new RemoteServices(GetHostServerURL(), GetHostServerURL());
                initializationOperation = InitWebPackage(package, remoteServices);
                if (manager._isIncludeRawFile)
                {
                    initRawFileOperation = InitWebPackage(rawFilePackage, remoteServices);
                }
            }

            if (initializationOperation == null)
            {
                manager.SetFailed($"Unsupported YooAsset play mode: {playMode}");
                yield break;
            }

            yield return initializationOperation;
            if (manager._isIncludeRawFile)
            {
                yield return initRawFileOperation;
            }

            if (initializationOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(initializationOperation.Error);
                UIPanelRoot.Instance.ShowMessage("资源包初始化失败！");
                manager.SetFailed(initializationOperation.Error);
                yield break;
            }

            if (manager._isIncludeRawFile && initRawFileOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(initRawFileOperation.Error);
                UIPanelRoot.Instance.ShowMessage("原生资源包初始化失败！");
                manager.SetFailed(initRawFileOperation.Error);
                yield break;
            }

            Debug.Log("资源包初始化成功！");
            fsm.ChangeState(ResPackageStates.LoadAOTMetadata);
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        private static InitializationOperation InitEditorSimulatePackage(ResourcePackage package, string packageName)
        {
            var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
            var createParameters = new EditorSimulateModeParameters
            {
                EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(buildResult.PackageRootDirectory)
            };
            return package.InitializeAsync(createParameters);
        }

        private static InitializationOperation InitOfflinePackage(ResourcePackage package)
        {
            var createParameters = new OfflinePlayModeParameters
            {
                BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters()
            };
            return package.InitializeAsync(createParameters);
        }

        private static InitializationOperation InitHostPackage(ResourcePackage package, IRemoteServices remoteServices)
        {
            var createParameters = new HostPlayModeParameters
            {
                BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters(),
                CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices)
            };
            return package.InitializeAsync(createParameters);
        }

        private static InitializationOperation InitWebPackage(ResourcePackage package, IRemoteServices remoteServices)
        {
            var createParameters = new WebPlayModeParameters
            {
                WebServerFileSystemParameters = FileSystemParameters.CreateDefaultWebServerFileSystemParameters(),
                WebRemoteFileSystemParameters = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices)
            };
            return package.InitializeAsync(createParameters);
        }

        private string GetHostServerURL()
        {
            return "http://127.0.0.1:8080/TestProject/PC";
        }

        private class RemoteServices : IRemoteServices
        {
            private readonly string defaultHostServer;
            private readonly string fallbackHostServer;

            public RemoteServices(string defaultHostServer, string fallbackHostServer)
            {
                this.defaultHostServer = defaultHostServer;
                this.fallbackHostServer = fallbackHostServer;
            }

            string IRemoteServices.GetRemoteMainURL(string fileName)
            {
                return $"{defaultHostServer}/{fileName}";
            }

            string IRemoteServices.GetRemoteFallbackURL(string fileName)
            {
                return $"{fallbackHostServer}/{fileName}";
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

                return new WebDecryptResult
                {
                    Result = AssetBundle.LoadFromMemory(copyData)
                };
            }
        }
    }
}
