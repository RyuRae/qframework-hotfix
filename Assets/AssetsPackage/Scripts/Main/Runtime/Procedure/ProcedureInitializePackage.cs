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
                // Host/Web 模式需要远端服务。主包和 RawFile 包分别解析，避免不同包名目录被拼错。
                if (!TryCreateRemoteServices(manager._packageName, out var remoteServices, out var error))
                {
                    FailRemoteConfig(error);
                    yield break;
                }

                initializationOperation = InitHostPackage(package, remoteServices);
                if (manager._isIncludeRawFile)
                {
                    if (!TryCreateRemoteServices(manager._rawfilwPkgName, out var rawFileRemoteServices, out error))
                    {
                        FailRemoteConfig(error);
                        yield break;
                    }

                    initRawFileOperation = InitHostPackage(rawFilePackage, rawFileRemoteServices);
                }
            }
            else if (playMode == EPlayMode.WebPlayMode)
            {
                // WebGL 场景也复用同一份远端配置，保证各平台 CDN 地址规则一致。
                if (!TryCreateRemoteServices(manager._packageName, out var remoteServices, out var error))
                {
                    FailRemoteConfig(error);
                    yield break;
                }

                initializationOperation = InitWebPackage(package, remoteServices);
                if (manager._isIncludeRawFile)
                {
                    if (!TryCreateRemoteServices(manager._rawfilwPkgName, out var rawFileRemoteServices, out error))
                    {
                        FailRemoteConfig(error);
                        yield break;
                    }

                    initRawFileOperation = InitWebPackage(rawFilePackage, rawFileRemoteServices);
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
                UIPanelRoot.Instance.ShowMessage(HotfixText.Get(HotfixTextKey.ResourcePackageInitializeFailed));
                manager.SetFailed(initializationOperation.Error);
                yield break;
            }

            if (manager._isIncludeRawFile && initRawFileOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(initRawFileOperation.Error);
                UIPanelRoot.Instance.ShowMessage(HotfixText.Get(HotfixTextKey.RawFilePackageInitializeFailed));
                manager.SetFailed(initRawFileOperation.Error);
                yield break;
            }

            Debug.Log(HotfixText.Get(HotfixTextKey.ResourcePackageInitializeSucceed));
            fsm.ChangeState(ResPackageStates.RequestPackageVersion);
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
            var buildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
#if !UNITY_EDITOR
            buildinFileSystemParameters.AddParameter(FileSystemParametersDefine.COPY_BUILDIN_PACKAGE_MANIFEST, true);
#endif

            var createParameters = new HostPlayModeParameters
            {
                BuildinFileSystemParameters = buildinFileSystemParameters,
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

        private bool TryCreateRemoteServices(string packageName, out RemoteServices remoteServices, out string error)
        {
            remoteServices = null;
            error = string.Empty;

            // 远端配置放在 Resources 中，包体启动后可直接加载，无需额外异步流程。
            var settings = HotfixRemoteSettings.Load();
            if (settings == null)
            {
                error = HotfixText.Get(HotfixTextKey.RemoteSettingsMissing, HotfixRemoteSettings.AssetName);
                return false;
            }

            if (!settings.TryResolve(packageName, out var address, out error))
            {
                return false;
            }

            LogKit.I($"Hotfix remote resolved. Env:{address.Environment}, Platform:{address.Platform}, Channel:{address.Channel}, Region:{address.Region}, Package:{address.PackageName}");
            remoteServices = new RemoteServices(address);
            return true;
        }

        private void FailRemoteConfig(string error)
        {
            Debug.LogError(error);
            UIPanelRoot.Instance.ShowMessage(error);
            manager.SetFailed(error);
        }

        private class RemoteServices : IRemoteServices
        {
            private readonly HotfixRemoteAddress address;

            public RemoteServices(HotfixRemoteAddress address)
            {
                this.address = address;
            }

            string IRemoteServices.GetRemoteMainURL(string fileName)
            {
                return CombineUrl(address.MainUrl, fileName);
            }

            string IRemoteServices.GetRemoteFallbackURL(string fileName)
            {
                return CombineUrl(address.FallbackUrl, fileName);
            }

            private static string CombineUrl(string rootUrl, string fileName)
            {
                // 统一处理斜杠，避免配置末尾带 "/" 或 YooAsset 文件名前带 "/" 时拼出双斜杠。
                return $"{rootUrl.TrimEnd('/')}/{fileName.TrimStart('/')}";
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
