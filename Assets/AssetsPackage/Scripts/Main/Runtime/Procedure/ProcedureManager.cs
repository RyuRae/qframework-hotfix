using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Framework.Assemblies;
using Framework.Events;
using Framework.YooAssetBridge;
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
        private string _expectedFallbackHotfixVersion = string.Empty;
        private string _expectedFallbackAotVersion = string.Empty;
        private readonly CancellationTokenSource _startupCancellation = new CancellationTokenSource();
        private IUnRegister _downloadCancelRequestUnregister;

        // public string EntrySceneAddress { get; private set; } = HotfixUtility.DefaultEntrySceneAddress;
        public string EntryTypeName { get; private set; } = string.Empty;
        public HotfixAssemblyLoadContext AssemblyLoadContext { get; } = new HotfixAssemblyLoadContext();
        public bool IsDownloadCancelRequested => _downloadCancelRequested;
        public bool IsDownloadPaused => _downloadPaused;
        public bool IsUsingLocalManifestFallback => _useLocalManifestFallback;
        public bool CanUseLocalCacheFallback => _startupUpdatePolicy != StartupUpdatePolicy.MustUpdate;
        public bool LastGoodCommittedThisRun { get; private set; }

        public FSM<ResPackageStates> _mFSM = new FSM<ResPackageStates>();

        public string MainPackageName => _packageName;
        public CancellationToken StartupCancellationToken => _startupCancellation.Token;

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
            CancelStartup();
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
            if (IsDone)
            {
                return;
            }

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
            CancelStartup();
            ReleaseDownloadControlEvents();
        }

        public HotfixContext CreateHotfixContext()
        {
            var mainPackage = YooAssets.GetPackage(_packageName);
            var rawFilePackage = _isIncludeRawFile ? YooAssets.GetPackage(_rawfilwPkgName) : null;
            return new HotfixContext(
                mainPackage,
                rawFilePackage,
                _packageVersion,
                _isIncludeRawFile ? _rawfilePkgVersion : string.Empty,
                AssemblyLoadContext.HotfixManifest == null
                    ? string.Empty
                    : AssemblyLoadContext.HotfixManifest.HotfixVersion,
                AssemblyLoadContext.AotManifest == null
                    ? string.Empty
                    : AssemblyLoadContext.AotManifest.AotVersion,
                _useLocalManifestFallback,
                StartupCancellationToken);
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
                if (!HotfixLocalManifestUtility.TryGetLastGoodRecord(_packageName, out var lastGood))
                {
                    return false;
                }

                return !_isIncludeRawFile || IsRawFileLastGoodIdentityValid(lastGood, out _);
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

        public bool CommitLastGood(out string error)
        {
            LastGoodCommittedThisRun = false;
            error = string.Empty;
            var package = YooAssets.GetPackage(_packageName);
            if (!package.PackageValid)
            {
                error = $"Can not commit LastGood because package is invalid: {_packageName}";
                return false;
            }

            string activeMainVersion = package.GetPackageVersion();
            if (string.IsNullOrWhiteSpace(activeMainVersion) ||
                !string.Equals(activeMainVersion, _packageVersion, StringComparison.Ordinal))
            {
                error = $"Can not commit LastGood because main package version changed. " +
                        $"Expected={_packageVersion}, Active={activeMainVersion}";
                return false;
            }

            string activeRawFileVersion = string.Empty;
            if (_isIncludeRawFile)
            {
                var rawFilePackage = YooAssets.GetPackage(_rawfilwPkgName);
                if (!rawFilePackage.PackageValid)
                {
                    error = $"Can not commit LastGood because package is invalid: {_rawfilwPkgName}";
                    return false;
                }

                activeRawFileVersion = rawFilePackage.GetPackageVersion();
                if (string.IsNullOrWhiteSpace(activeRawFileVersion) ||
                    !string.Equals(activeRawFileVersion, _rawfilePkgVersion, StringComparison.Ordinal))
                {
                    error = $"Can not commit LastGood because raw file package version changed. " +
                            $"Expected={_rawfilePkgVersion}, Active={activeRawFileVersion}";
                    return false;
                }
            }

            if (!ValidateLoadedAssemblyCombination(out error))
            {
                return false;
            }

            if (!HasLocalStartupResources(package, _downloadTags, out error))
            {
                return false;
            }

            if (_isIncludeRawFile)
            {
                var rawFilePackage = YooAssets.GetPackage(_rawfilwPkgName);
                if (!HasLocalStartupResources(rawFilePackage, _rawfileDownloadTags, out error))
                {
                    return false;
                }
            }

            string hotfixVersion = AssemblyLoadContext.HotfixManifest == null
                ? string.Empty
                : AssemblyLoadContext.HotfixManifest.HotfixVersion;
            string aotVersion = AssemblyLoadContext.AotManifest == null
                ? string.Empty
                : AssemblyLoadContext.AotManifest.AotVersion;
            var runtimeSettings = HotfixRuntimeSettings.Load();
            var remoteSettings = HotfixRemoteSettings.Load();
            bool requireSignedManifests =
                runtimeSettings != null && runtimeSettings.RequireSignedAssemblyManifests ||
                remoteSettings != null && remoteSettings.IsProductionRuntimeEnvironment;
            if (requireSignedManifests)
            {
                var hotfixManifest = AssemblyLoadContext.HotfixManifest;
                string lastGoodError = string.Empty;
                if (hotfixManifest == null ||
                    !HotfixReleaseTrustStore.TryCommit(
                        hotfixManifest.ReleaseSequence,
                        hotfixManifest.ReleaseVersion,
                        () => HotfixLocalManifestUtility.SaveLastGoodRecord(
                            _packageName,
                            activeMainVersion,
                            _isIncludeRawFile ? _rawfilwPkgName : string.Empty,
                            activeRawFileVersion,
                            hotfixVersion,
                            aotVersion,
                            false,
                            out lastGoodError),
                        out error))
                {
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        error = lastGoodError;
                    }
                    return false;
                }
            }
            else if (!HotfixLocalManifestUtility.SaveLastGoodRecord(
                _packageName,
                activeMainVersion,
                _isIncludeRawFile ? _rawfilwPkgName : string.Empty,
                activeRawFileVersion,
                hotfixVersion,
                aotVersion,
                out error))
            {
                return false;
            }

            LastGoodCommittedThisRun = true;
            LogKit.I($"LastGood committed. Package={_packageName}:{activeMainVersion}, " +
                     $"RawFile={(_isIncludeRawFile ? $"{_rawfilwPkgName}:{activeRawFileVersion}" : "disabled")}, " +
                     $"Hotfix={hotfixVersion}, AOT={aotVersion}");
            return true;
        }

        public IEnumerator TryUseLocalManifestFallback(string reason, Action<bool, string> onCompleted)
        {
            ClearExpectedFallbackAssemblyCombination();
            var errors = new List<string>();
            if (HotfixLocalManifestUtility.TryGetLastGoodRecord(_packageName, out var lastGood))
            {
                if (_isIncludeRawFile && !IsRawFileLastGoodIdentityValid(lastGood, out var identityError))
                {
                    errors.Add($"LastGood failed: {identityError}");
                }
                else
                {
                    bool lastGoodSucceeded = false;
                    string lastGoodError = string.Empty;
                    yield return TryLoadManifestSet(
                        false,
                        lastGood.MainPackageVersion,
                        lastGood.RawFilePackageVersion,
                        (succeeded, error) =>
                        {
                            lastGoodSucceeded = succeeded;
                            lastGoodError = error;
                        });
                    if (lastGoodSucceeded)
                    {
                        _expectedFallbackHotfixVersion = lastGood.HotfixVersion;
                        _expectedFallbackAotVersion = lastGood.AotVersion;
                        MarkUseLocalManifestFallback(reason);
                        onCompleted?.Invoke(true, string.Empty);
                        yield break;
                    }

                    errors.Add($"LastGood failed: {lastGoodError}");
                }
            }
            else
            {
                errors.Add("LastGood record is missing or invalid.");
            }

            bool buildinSucceeded = false;
            string buildinError = string.Empty;
            yield return TryLoadManifestSet(
                true,
                string.Empty,
                string.Empty,
                (succeeded, error) =>
                {
                    buildinSucceeded = succeeded;
                    buildinError = error;
                });
            if (buildinSucceeded)
            {
                ClearExpectedFallbackAssemblyCombination();
                MarkUseLocalManifestFallback(reason);
                onCompleted?.Invoke(true, string.Empty);
                yield break;
            }

            errors.Add($"Build-in fallback failed: {buildinError}");
            onCompleted?.Invoke(false, string.Join(" | ", errors));
        }

        public bool ValidateLoadedAssemblyCombination(out string error)
        {
            error = string.Empty;
            if (string.IsNullOrEmpty(_expectedFallbackHotfixVersion) &&
                string.IsNullOrEmpty(_expectedFallbackAotVersion))
            {
                return true;
            }

            string actualHotfixVersion = AssemblyLoadContext.HotfixManifest == null
                ? string.Empty
                : AssemblyLoadContext.HotfixManifest.HotfixVersion;
            string actualAotVersion = AssemblyLoadContext.AotManifest == null
                ? string.Empty
                : AssemblyLoadContext.AotManifest.AotVersion;
            if (!string.Equals(actualHotfixVersion, _expectedFallbackHotfixVersion, StringComparison.Ordinal) ||
                !string.Equals(actualAotVersion, _expectedFallbackAotVersion, StringComparison.Ordinal))
            {
                error = $"LastGood assembly combination mismatch. " +
                        $"Expected=Hotfix:{_expectedFallbackHotfixVersion},AOT:{_expectedFallbackAotVersion}; " +
                        $"Actual=Hotfix:{actualHotfixVersion},AOT:{actualAotVersion}.";
                return false;
            }

            return true;
        }

        public bool ValidateRawFileManifestTrust(out string error)
        {
            error = string.Empty;
            if (!IsRawFileManifestTrustRequired())
            {
                return true;
            }

            var hotfixManifest = AssemblyLoadContext.HotfixManifest;
            if (!_isIncludeRawFile)
            {
                if (hotfixManifest != null &&
                    (!string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageName) ||
                     !string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageVersion) ||
                     !string.IsNullOrWhiteSpace(hotfixManifest.RawFileManifestSha256)))
                {
                    error = "Hotfix manifest declares a RawFile package but RuntimeSettings has it disabled.";
                    return false;
                }

                return true;
            }

            if (hotfixManifest == null)
            {
                error = "Hotfix manifest is unavailable while validating the RawFile package.";
                return false;
            }

            if (hotfixManifest.SignatureVersion < AssemblyManifestSignatureUtility.CurrentSignatureVersion)
            {
                error = $"Hotfix manifest signature protocol does not bind the RawFile package. " +
                        $"Required={AssemblyManifestSignatureUtility.CurrentSignatureVersion}, " +
                        $"Actual={hotfixManifest.SignatureVersion}";
                return false;
            }

            if (string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageName) ||
                string.IsNullOrWhiteSpace(hotfixManifest.RawFilePackageVersion) ||
                string.IsNullOrWhiteSpace(hotfixManifest.RawFileManifestSha256))
            {
                error = "Hotfix manifest does not contain a complete RawFile package trust binding.";
                return false;
            }

            if (!string.Equals(hotfixManifest.RawFilePackageName, _rawfilwPkgName, StringComparison.Ordinal) ||
                !string.Equals(hotfixManifest.RawFilePackageVersion, _rawfilePkgVersion, StringComparison.Ordinal))
            {
                error = $"RawFile release identity mismatch. " +
                        $"Expected={hotfixManifest.RawFilePackageName}:{hotfixManifest.RawFilePackageVersion}, " +
                        $"Active={_rawfilwPkgName}:{_rawfilePkgVersion}";
                return false;
            }

            var rawFilePackage = YooAssets.GetPackage(_rawfilwPkgName);
            if (!YooAssetLocalManifestBridge.TryGetActiveManifestFingerprint(rawFilePackage, out var fingerprint))
            {
                error = fingerprint.Error;
                return false;
            }

            if (!string.Equals(fingerprint.PackageName, _rawfilwPkgName, StringComparison.Ordinal) ||
                !string.Equals(fingerprint.PackageVersion, _rawfilePkgVersion, StringComparison.Ordinal) ||
                !string.Equals(fingerprint.Sha256, hotfixManifest.RawFileManifestSha256, StringComparison.OrdinalIgnoreCase))
            {
                error = $"RawFile YooAsset manifest trust validation failed. " +
                        $"Expected={hotfixManifest.RawFilePackageName}:{hotfixManifest.RawFilePackageVersion}:{hotfixManifest.RawFileManifestSha256}, " +
                        $"Actual={fingerprint.PackageName}:{fingerprint.PackageVersion}:{fingerprint.Sha256}";
                return false;
            }

            return true;
        }

        private static bool IsRawFileManifestTrustRequired()
        {
            var runtimeSettings = HotfixRuntimeSettings.Load();
            var remoteSettings = HotfixRemoteSettings.Load();
            return runtimeSettings != null && runtimeSettings.RequireSignedAssemblyManifests ||
                   remoteSettings != null && remoteSettings.IsProductionRuntimeEnvironment;
        }

        private IEnumerator TryLoadManifestSet(
            bool useBuildin,
            string mainPackageVersion,
            string rawFilePackageVersion,
            Action<bool, string> onCompleted)
        {
            if (!useBuildin && _isIncludeRawFile && string.IsNullOrWhiteSpace(rawFilePackageVersion))
            {
                onCompleted?.Invoke(false, $"LastGood record has no version for raw file package: {_rawfilwPkgName}");
                yield break;
            }

            var package = YooAssets.GetPackage(_packageName);
            HotfixLocalManifestResult packageResult = default;
            yield return LoadFallbackManifest(
                package,
                useBuildin,
                mainPackageVersion,
                result => packageResult = result);
            if (!packageResult.Succeeded)
            {
                onCompleted?.Invoke(false, HotfixText.Get(HotfixTextKey.MainPackageLocalCacheUnavailable, packageResult.Error));
                yield break;
            }

            HotfixLocalManifestResult rawFileResult = default;
            if (_isIncludeRawFile)
            {
                var rawFilePackage = YooAssets.GetPackage(_rawfilwPkgName);
                yield return LoadFallbackManifest(
                    rawFilePackage,
                    useBuildin,
                    rawFilePackageVersion,
                    result => rawFileResult = result);
                if (!rawFileResult.Succeeded)
                {
                    onCompleted?.Invoke(false, HotfixText.Get(HotfixTextKey.RawFilePackageLocalCacheUnavailable, rawFileResult.Error));
                    yield break;
                }
            }

            if (!HasLocalStartupResources(package, _downloadTags, out var availabilityError))
            {
                onCompleted?.Invoke(false, availabilityError);
                yield break;
            }

            if (_isIncludeRawFile)
            {
                var rawFilePackage = YooAssets.GetPackage(_rawfilwPkgName);
                if (!HasLocalStartupResources(rawFilePackage, _rawfileDownloadTags, out availabilityError))
                {
                    onCompleted?.Invoke(false, availabilityError);
                    yield break;
                }
            }

            _packageVersion = packageResult.PackageVersion;
            if (_isIncludeRawFile)
            {
                _rawfilePkgVersion = rawFileResult.PackageVersion;
            }

            onCompleted?.Invoke(true, string.Empty);
        }

        private bool IsRawFileLastGoodIdentityValid(HotfixLastGoodRecord lastGood, out string error)
        {
            error = string.Empty;
            if (!_isIncludeRawFile)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(lastGood.RawFilePackageName))
            {
                error = $"LastGood record does not bind a RawFile package name. Expected: {_rawfilwPkgName}";
                return false;
            }

            if (!string.Equals(lastGood.RawFilePackageName, _rawfilwPkgName, StringComparison.Ordinal))
            {
                error = $"LastGood RawFile package identity mismatch. " +
                        $"Expected={_rawfilwPkgName}, Recorded={lastGood.RawFilePackageName}";
                return false;
            }

            if (string.IsNullOrWhiteSpace(lastGood.RawFilePackageVersion))
            {
                error = $"LastGood record has no version for raw file package: {_rawfilwPkgName}";
                return false;
            }

            return true;
        }

        private static IEnumerator LoadFallbackManifest(
            ResourcePackage package,
            bool useBuildin,
            string lastGoodVersion,
            Action<HotfixLocalManifestResult> onCompleted)
        {
            if (useBuildin)
            {
                yield return HotfixLocalManifestUtility.TryLoadBuildinManifest(package, onCompleted);
            }
            else
            {
                yield return HotfixLocalManifestUtility.TryLoadLastGoodManifest(package, lastGoodVersion, onCompleted);
            }
        }

        private bool HasLocalStartupResources(ResourcePackage package, string[] tags, out string error)
        {
            error = string.Empty;
            if (package == null || !package.PackageValid)
            {
                error = $"Can not validate local startup resources because package is invalid: {package?.PackageName ?? "null"}";
                return false;
            }

            ResourceDownloaderOperation downloader;
            try
            {
                if (_startupDownloadMode == StartupDownloadMode.DownloadByTags)
                {
                    if (tags == null || tags.Length == 0)
                    {
                        error = $"Can not validate local startup resources because download tags are empty. Package: {package.PackageName}";
                        return false;
                    }

                    downloader = package.CreateResourceDownloader(tags, 1, 0);
                }
                else
                {
                    // DownloadAll 和 Skip 都必须完整可用。Skip 可以正常启动，但不完整时不能成为 LastGood。
                    downloader = package.CreateResourceDownloader(1, 0);
                }
            }
            catch (Exception exception)
            {
                error = $"Validate local startup resources failed. Package: {package.PackageName}. {exception.Message}";
                return false;
            }

            if (downloader == null)
            {
                error = $"Can not create local resource validator. Package: {package.PackageName}";
                return false;
            }

            if (downloader.TotalDownloadCount > 0)
            {
                error = $"Fallback manifest is not locally complete. Package: {package.PackageName}, " +
                        $"MissingFiles={downloader.TotalDownloadCount}, MissingBytes={downloader.TotalDownloadBytes}.";
                return false;
            }

            return true;
        }

        private void ClearExpectedFallbackAssemblyCombination()
        {
            _expectedFallbackHotfixVersion = string.Empty;
            _expectedFallbackAotVersion = string.Empty;
        }



        public void SetHotfixEntryType(string typeName)
        {
            EntryTypeName = typeName ?? string.Empty;
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

        private void CancelStartup()
        {
            try
            {
                if (!_startupCancellation.IsCancellationRequested)
                {
                    _startupCancellation.Cancel();
                }
            }
            catch (Exception exception)
            {
                LogKit.W($"Cancel hotfix business startup failed. {exception}");
            }
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
