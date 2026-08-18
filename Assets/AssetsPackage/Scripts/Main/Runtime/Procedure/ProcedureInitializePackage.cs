using System;
using System.Collections;
using Framework.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    /// <summary>
    /// 创建并初始化主包和可选 RawFile 包，根据运行模式配置编辑器、离线、Host 或 Web 参数。
    /// </summary>
    public class ProcedureInitializePackage : AbstractState<ResPackageStates, ProcedureManager>
    {
        private ResourcePackage rawFilePackage;
        private InitializationOperation initRawFileOperation;

        public ProcedureInitializePackage(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
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
            var playMode = mTarget._playMode;
            if (mTarget._startupPackageMode == StartupPackageMode.EmptyPackage &&
                playMode == EPlayMode.OfflinePlayMode)
            {
                mTarget.SetFailed("Startup package mode EmptyPackage requires HostPlayMode or WebPlayMode so the client can request the remote package version and manifest before downloading AOT metadata, hotfix DLLs, and entry resources.");
                yield break;
            }

            if (mTarget._startupPackageMode == StartupPackageMode.OfflinePackage &&
                playMode != EPlayMode.OfflinePlayMode)
            {
                mTarget.SetFailed($"Startup package mode OfflinePackage requires OfflinePlayMode, current play mode is {playMode}.");
                yield break;
            }

            var package = YooAssets.TryGetPackage(mTarget.MainPackageName) ?? YooAssets.CreatePackage(mTarget.MainPackageName);
            YooAssets.SetDefaultPackage(package);

            if (mTarget._isIncludeRawFile)
            {
                rawFilePackage = YooAssets.TryGetPackage(mTarget._rawfilwPkgName) ?? YooAssets.CreatePackage(mTarget._rawfilwPkgName);
            }

            InitializationOperation initializationOperation = null;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                initializationOperation = InitEditorSimulatePackage(package, mTarget.MainPackageName);
                if (mTarget._isIncludeRawFile)
                {
                    initRawFileOperation = InitEditorSimulatePackage(rawFilePackage, mTarget._rawfilwPkgName);
                }
            }
            else if (playMode == EPlayMode.OfflinePlayMode)
            {
                initializationOperation = InitOfflinePackage(package);
                if (mTarget._isIncludeRawFile)
                {
                    initRawFileOperation = InitOfflinePackage(rawFilePackage);
                }
            }
            else if (playMode == EPlayMode.HostPlayMode)
            {
                // Host/Web 模式需要远端服务。主包和 RawFile 包分别解析，避免不同包名目录被拼错。
                if (!TryCreateRemoteServices(mTarget.MainPackageName, out var remoteServices, out var error))
                {
                    FailRemoteConfig(error);
                    yield break;
                }

                initializationOperation = InitHostPackage(package, remoteServices);
                if (mTarget._isIncludeRawFile)
                {
                    if (!TryCreateRemoteServices(mTarget._rawfilwPkgName, out var rawFileRemoteServices, out error))
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
                if (!TryCreateRemoteServices(mTarget.MainPackageName, out var remoteServices, out var error))
                {
                    FailRemoteConfig(error);
                    yield break;
                }

                initializationOperation = InitWebPackage(package, remoteServices);
                if (mTarget._isIncludeRawFile)
                {
                    if (!TryCreateRemoteServices(mTarget._rawfilwPkgName, out var rawFileRemoteServices, out error))
                    {
                        FailRemoteConfig(error);
                        yield break;
                    }

                    initRawFileOperation = InitWebPackage(rawFilePackage, rawFileRemoteServices);
                }
            }

            if (initializationOperation == null)
            {
                mTarget.SetFailed($"Unsupported YooAsset play mode: {playMode}");
                yield break;
            }

            yield return initializationOperation;
            if (mTarget._isIncludeRawFile)
            {
                yield return initRawFileOperation;
            }

            if (initializationOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(initializationOperation.Error);
                UIPanelRoot.Instance.ShowMessage(HotfixText.Get(HotfixTextKey.ResourcePackageInitializeFailed));
                mTarget.SetFailed(initializationOperation.Error);
                yield break;
            }

            if (mTarget._isIncludeRawFile && initRawFileOperation.Status != EOperationStatus.Succeed)
            {
                Debug.LogWarning(initRawFileOperation.Error);
                UIPanelRoot.Instance.ShowMessage(HotfixText.Get(HotfixTextKey.RawFilePackageInitializeFailed));
                mTarget.SetFailed(initRawFileOperation.Error);
                yield break;
            }

            Debug.Log(HotfixText.Get(HotfixTextKey.ResourcePackageInitializeSucceed));
            mFSM.ChangeState(ResPackageStates.RequestPackageVersion);
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
            mTarget.SetFailed(error);
        }

        /// <summary>将解析后的主备 CDN 根地址适配为 YooAsset 远端服务。</summary>
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

    }
}
