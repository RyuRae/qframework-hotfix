using System;
using System.Collections;
using System.Collections.Generic;
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
        public const string DefaultEntrySceneAddress = "main";

        public readonly string _packageName;
        public readonly string _rawfilwPkgName;
        public readonly EPlayMode _playMode;
        public string _packageVersion;
        public bool _isIncludeRawFile;
        public string _rawfilePkgVersion;
        public ResourceDownloaderOperation _downloaderOperation;
        public ResourceDownloaderOperation _downloaderRawfile;
        public readonly StartupDownloadMode _startupDownloadMode;
        public readonly StartupUpdatePolicy _startupUpdatePolicy;
        public readonly string[] _downloadTags;
        public readonly string[] _rawfileDownloadTags;
        private bool _downloadCancelRequested;
        private bool _downloadPaused;
        private bool _useLocalManifestFallback;
        private IUnRegister _downloadCancelRequestUnregister;

        public string EntrySceneAddress { get; private set; } = DefaultEntrySceneAddress;
        public string EntryTypeName { get; private set; } = string.Empty;
        public string EntryMethodName { get; private set; } = string.Empty;
        public bool IsDownloadCancelRequested => _downloadCancelRequested;
        public bool IsDownloadPaused => _downloadPaused;
        public bool IsUsingLocalManifestFallback => _useLocalManifestFallback;
        public bool CanUseLocalCacheFallback => _startupUpdatePolicy != StartupUpdatePolicy.MustUpdate;

        public FSM<ResPackageStates> _mFSM = new FSM<ResPackageStates>();

        public ProcedureManager(
            string packageName,
            EPlayMode playMode,
            bool IsIncludeRawFile = false,
            StartupDownloadMode startupDownloadMode = StartupDownloadMode.DownloadAll,
            StartupUpdatePolicy startupUpdatePolicy = StartupUpdatePolicy.AllowCached,
            string[] downloadTags = null,
            string[] rawfileDownloadTags = null,
            string rawfilePackageName = null)
        {
            _packageName = NormalizePackageName(packageName, HotfixRuntimeSettings.DefaultMainPackageName);
            _playMode = playMode;
            _isIncludeRawFile = IsIncludeRawFile;
            _startupDownloadMode = startupDownloadMode;
            _startupUpdatePolicy = startupUpdatePolicy;
            _downloadTags = NormalizeTags(downloadTags);
            _rawfileDownloadTags = NormalizeTags(rawfileDownloadTags);
            _rawfilwPkgName = NormalizePackageName(rawfilePackageName, HotfixRuntimeSettings.DefaultRawFilePackageName);

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

            string cancelReason = string.IsNullOrWhiteSpace(reason) ? "用户取消资源更新。" : reason.Trim();
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

            LogKit.I(handled ? "资源下载已暂停。" : "当前没有可暂停的下载任务。");
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

            LogKit.I(handled ? "资源下载已继续。" : "当前没有可继续的下载任务。");
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
                onCompleted?.Invoke(false, $"主资源包本地缓存不可用：{packageResult.Error}");
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
                    onCompleted?.Invoke(false, $"RawFile 资源包本地缓存不可用：{rawfileResult.Error}");
                    yield break;
                }

                _rawfilePkgVersion = rawfileResult.PackageVersion;
            }

            MarkUseLocalManifestFallback(reason);
            SaveUsablePackageVersions();
            onCompleted?.Invoke(true, string.Empty);
        }

        public void SetHotfixEntry(string sceneAddress, string typeName, string methodName)
        {
            EntrySceneAddress = string.IsNullOrWhiteSpace(sceneAddress) ? DefaultEntrySceneAddress : sceneAddress;
            EntryTypeName = typeName ?? string.Empty;
            EntryMethodName = methodName ?? string.Empty;
        }

        private static string[] NormalizeTags(string[] tags)
        {
            if (tags == null || tags.Length == 0)
            {
                return Array.Empty<string>();
            }

            var results = new List<string>();
            var exists = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                {
                    continue;
                }

                var normalizedTag = tag.Trim();
                if (exists.Add(normalizedTag))
                {
                    results.Add(normalizedTag);
                }
            }

            return results.ToArray();
        }

        private static string NormalizePackageName(string packageName, string fallback)
        {
            return string.IsNullOrWhiteSpace(packageName) ? fallback : packageName.Trim();
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
