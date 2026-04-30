using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Assemblies;
using Framework.Events;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    public enum ResPackageStates
    {
        InitializePackage,
        RequestPackageVersion,
        UpdatePackageManifest,
        CreateDownloader,
        DownloadPackageFiles,
        DownloadPackageOver,
        LoadAOTMetadata,
        LoadAssemblies,
        ClearCacheBundle,
        StartGame
    }

    public class ProcedureManager : GameAsyncOperation
    {
        private readonly string _packageName;
        public readonly string _rawfilwPkgName;
        public readonly EPlayMode _playMode;
        public string _packageVersion;
        public bool _isIncludeRawFile;
        public string _rawfilePkgVersion;
        public ResourceDownloaderOperation _downloaderOperation;
        public ResourceDownloaderOperation _downloaderRawfile;
        public readonly StartupDownloadMode _startupDownloadMode;
        public readonly StartupUpdatePolicy _startupUpdatePolicy;
        public readonly StartupPackageMode _startupPackageMode;
        public readonly string[] _downloadTags;
        public readonly string[] _rawfileDownloadTags;
        private bool _downloadCancelRequested;
        private bool _downloadPaused;
        private bool _useLocalManifestFallback;
        private IUnRegister _downloadCancelRequestUnregister;

        // public string EntrySceneAddress { get; private set; } = HotfixUtility.DefaultEntrySceneAddress;
        public string EntryTypeName { get; private set; } = string.Empty;
        public string EntryMethodName { get; private set; } = string.Empty;
        public HotfixAssemblyLoadContext AssemblyLoadContext { get; } = new HotfixAssemblyLoadContext();
        public bool IsDownloadCancelRequested => _downloadCancelRequested;
        public bool IsDownloadPaused => _downloadPaused;
        public bool IsUsingLocalManifestFallback => _useLocalManifestFallback;
        public bool CanUseLocalCacheFallback => _startupUpdatePolicy != StartupUpdatePolicy.MustUpdate;

        public FSM<ResPackageStates> _mFSM = new FSM<ResPackageStates>();

        public string MainPackageName => _packageName;

        public ProcedureManager(
            string packageName,
            EPlayMode playMode,
            bool IsIncludeRawFile = false,
            StartupDownloadMode startupDownloadMode = StartupDownloadMode.DownloadAll,
            StartupUpdatePolicy startupUpdatePolicy = StartupUpdatePolicy.AllowCached,
            StartupPackageMode startupPackageMode = StartupPackageMode.FirstPackage,
            string[] downloadTags = null,
            string[] rawfileDownloadTags = null,
            string rawfilePackageName = null)
        {
            _packageName = HotfixUtility.NormalizePackageName(packageName, HotfixRuntimeSettings.DefaultMainPackageName);
            _playMode = playMode;
            _isIncludeRawFile = IsIncludeRawFile;
            _startupDownloadMode = startupDownloadMode;
            _startupUpdatePolicy = startupUpdatePolicy;
            _startupPackageMode = startupPackageMode;
            _downloadTags = HotfixUtility.NormalizeTags(downloadTags);
            _rawfileDownloadTags = HotfixUtility.NormalizeTags(rawfileDownloadTags);
            _rawfilwPkgName = HotfixUtility.NormalizePackageName(rawfilePackageName, HotfixRuntimeSettings.DefaultRawFilePackageName);

            _mFSM.AddState(ResPackageStates.InitializePackage, new ProcedureInitializePackage(_mFSM, this));
            _mFSM.AddState(ResPackageStates.LoadAOTMetadata, new ProcedureLoadAOTMetadata(_mFSM, this));
            _mFSM.AddState(ResPackageStates.RequestPackageVersion, new ProcedureRequestPackageVersion(_mFSM, this));
            _mFSM.AddState(ResPackageStates.UpdatePackageManifest, new ProcedureUpdatePackageManifest(_mFSM, this));
            _mFSM.AddState(ResPackageStates.CreateDownloader, new ProcedureCreateDownloader(_mFSM, this));
            _mFSM.AddState(ResPackageStates.DownloadPackageFiles, new ProcedureDownloadPackageFiles(_mFSM, this));
            _mFSM.AddState(ResPackageStates.DownloadPackageOver, new ProcedureDownloadPackageOver(_mFSM, this));
            _mFSM.AddState(ResPackageStates.LoadAssemblies, new ProcedureLoadAssembly(_mFSM, this));
            _mFSM.AddState(ResPackageStates.ClearCacheBundle, new ProcedureClearCacheBundle(_mFSM, this));
            _mFSM.AddState(ResPackageStates.StartGame, new ProcedureStartGame(_mFSM, this));

            RegisterDownloadControlEvents();
        }

        protected override void OnAbort()
        {
            ReleaseDownloadControlEvents();
        }

        protected override void OnStart()
        {
            _mFSM.StartState(ResPackageStates.InitializePackage);
        }

        protected override void OnUpdate()
        {
            if (IsDone)
            {
                return;
            }

            _mFSM.Update();
        }

        public void SetFinish()
        {
            Status = EOperationStatus.Succeed;
            ReleaseDownloadControlEvents();
        }

        public void SetFailed(string error)
        {
            if (IsDone)
            {
                return;
            }

            Error = string.IsNullOrEmpty(error) ? "Hot update procedure failed." : error;
            Status = EOperationStatus.Failed;
            LogKit.E(Error);
            ReleaseDownloadControlEvents();
        }

        public void CancelDownload(string reason = null)
        {
            if (IsDone)
            {
                return;
            }

            string cancelReason = string.IsNullOrWhiteSpace(reason)
                ? HotfixText.Get(HotfixTextKey.UserCanceledResourceUpdate)
                : reason.Trim();
            _downloadCancelRequested = true;
            _downloadPaused = false;

            TryCancelDownloader(_downloaderOperation);
            TryCancelDownloader(_downloaderRawfile);

            LogKit.I(cancelReason);
            TypeEventSystem.Global.Send(new OnDownloadCanceledEvent { reason = cancelReason });
            SetFailed(cancelReason);
        }

        public bool TryPauseDownload()
        {
            if (IsDone || _downloadCancelRequested)
            {
                return false;
            }

            bool handled = TryPauseDownloader(_downloaderOperation);
            handled |= TryPauseDownloader(_downloaderRawfile);
            _downloadPaused = handled;

            LogKit.I(handled
                ? HotfixText.Get(HotfixTextKey.DownloadPaused)
                : HotfixText.Get(HotfixTextKey.NoDownloadTaskToPause));
            return handled;
        }

        public bool TryResumeDownload()
        {
            if (IsDone || _downloadCancelRequested)
            {
                return false;
            }

            bool handled = TryResumeDownloader(_downloaderOperation);
            handled |= TryResumeDownloader(_downloaderRawfile);
            _downloadPaused = handled ? false : _downloadPaused;

            LogKit.I(handled
                ? HotfixText.Get(HotfixTextKey.DownloadResumed)
                : HotfixText.Get(HotfixTextKey.NoDownloadTaskToResume));
            return handled;
        }

        public bool ShouldUseLocalCacheOnlyAtStartup()
        {
            if (!CanUseLocalCacheFallback)
            {
                return false;
            }

            if (_startupUpdatePolicy == StartupUpdatePolicy.WifiOnly)
            {
                return Application.internetReachability != NetworkReachability.ReachableViaLocalAreaNetwork;
            }

            if (_startupUpdatePolicy == StartupUpdatePolicy.BackgroundDownload)
            {
                if (string.IsNullOrEmpty(HotfixLocalManifestUtility.GetLastUsablePackageVersion(_packageName)))
                {
                    return false;
                }

                return !_isIncludeRawFile ||
                       !string.IsNullOrEmpty(HotfixLocalManifestUtility.GetLastUsablePackageVersion(_rawfilwPkgName));
            }

            return false;
        }

        public void MarkUseLocalManifestFallback(string reason)
        {
            _useLocalManifestFallback = true;
            if (!string.IsNullOrWhiteSpace(reason))
            {
                LogKit.W(reason);
                TypeEventSystem.Global.Send(new OnStartupUsingLocalCacheEvent { reason = reason.Trim() });
            }
        }

        public void SaveUsablePackageVersions()
        {
            HotfixLocalManifestUtility.SaveLastUsablePackageVersion(_packageName, _packageVersion);
            if (_isIncludeRawFile)
            {
                HotfixLocalManifestUtility.SaveLastUsablePackageVersion(_rawfilwPkgName, _rawfilePkgVersion);
            }
        }

        public void SaveUsableAssemblyVersions()
        {
            HotfixLocalManifestUtility.SaveLastUsableAssemblyVersions(
                _packageName,
                AssemblyLoadContext.HotfixManifest == null ? string.Empty : AssemblyLoadContext.HotfixManifest.HotfixVersion,
                AssemblyLoadContext.AotManifest == null ? string.Empty : AssemblyLoadContext.AotManifest.AotVersion);
        }

        public IEnumerator TryUseLocalManifestFallback(string reason, Action<bool, string> onCompleted)
        {
            var package = YooAssets.GetPackage(_packageName);
            HotfixLocalManifestResult packageResult = default;
            yield return HotfixLocalManifestUtility.TryLoadLocalManifest(
                package,
                _packageVersion,
                result => packageResult = result);

            if (!packageResult.Succeeded)
            {
                onCompleted?.Invoke(false, HotfixText.Get(HotfixTextKey.MainPackageLocalCacheUnavailable, packageResult.Error));
                yield break;
            }

            _packageVersion = packageResult.PackageVersion;

            if (_isIncludeRawFile)
            {
                var rawfilePackage = YooAssets.GetPackage(_rawfilwPkgName);
                HotfixLocalManifestResult rawfileResult = default;
                yield return HotfixLocalManifestUtility.TryLoadLocalManifest(
                    rawfilePackage,
                    _rawfilePkgVersion,
                    result => rawfileResult = result);

                if (!rawfileResult.Succeeded)
                {
                    onCompleted?.Invoke(false, HotfixText.Get(HotfixTextKey.RawFilePackageLocalCacheUnavailable, rawfileResult.Error));
                    yield break;
                }

                _rawfilePkgVersion = rawfileResult.PackageVersion;
            }

            MarkUseLocalManifestFallback(reason);
            SaveUsablePackageVersions();
            onCompleted?.Invoke(true, string.Empty);
        }



        public void SetHotfixEntry(string typeName, string methodName)
        {
            EntryTypeName = typeName ?? string.Empty;
            EntryMethodName = methodName ?? string.Empty;
        }

        private void RegisterDownloadControlEvents()
        {
            _downloadCancelRequestUnregister = TypeEventSystem.Global.Register<OnDownloadCancelRequestEvent>(downloadCancelRequest =>
            {
                CancelDownload(downloadCancelRequest.reason);
            });
        }

        private void ReleaseDownloadControlEvents()
        {
            _downloadCancelRequestUnregister?.UnRegister();
            _downloadCancelRequestUnregister = null;
        }

        private static bool TryCancelDownloader(ResourceDownloaderOperation downloader)
        {
            if (downloader == null || downloader.IsDone)
            {
                return false;
            }

            downloader.CancelDownload();
            return true;
        }

        private static bool TryPauseDownloader(ResourceDownloaderOperation downloader)
        {
            if (downloader == null || downloader.IsDone)
            {
                return false;
            }

            downloader.PauseDownload();
            return true;
        }

        private static bool TryResumeDownloader(ResourceDownloaderOperation downloader)
        {
            if (downloader == null || downloader.IsDone)
            {
                return false;
            }

            downloader.ResumeDownload();
            return true;
        }
    }
}
