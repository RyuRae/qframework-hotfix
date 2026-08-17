using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using YooAsset;

namespace Framework.YooAssetBridge
{
    public enum YooAssetLocalManifestSource
    {
        None,
        Cache,
        Buildin,
        WebServer,
        Editor
    }

    public struct YooAssetLocalManifestResult
    {
        public bool Succeeded;
        public string PackageName;
        public string PackageVersion;
        public string Error;
        public YooAssetLocalManifestSource Source;
    }

    public sealed class YooAssetLocalManifestBridge
    {
        private const int LocalManifestTimeout = 60;
        private const BindingFlags InstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly IPlayMode playModeImpl;

        private YooAssetLocalManifestBridge(ResourcePackage package, IPlayMode playModeImpl)
        {
            PackageName = package.PackageName;
            this.playModeImpl = playModeImpl;
        }

        public string PackageName { get; }

        public static bool TryCreate(ResourcePackage package, out YooAssetLocalManifestBridge bridge, out string error)
        {
            bridge = null;
            error = string.Empty;

            if (package == null)
            {
                error = "Resource package is null.";
                return false;
            }

            var fieldInfo = typeof(ResourcePackage).GetField("_playModeImpl", InstanceFlags);
            if (!(fieldInfo?.GetValue(package) is IPlayMode playModeImpl))
            {
                error = $"Can not find YooAsset play mode implementation. Package: {package.PackageName}";
                return false;
            }

            bridge = new YooAssetLocalManifestBridge(package, playModeImpl);
            return true;
        }

        public IEnumerator TryCollectLocalPackageVersions(List<string> candidates)
        {
            foreach (var entry in GetLocalFileSystems())
            {
                yield return TryRequestLocalPackageVersion(entry.FileSystem, candidates);
            }
        }

        public IEnumerator TryLoadLocalManifest(string packageVersion, Action<YooAssetLocalManifestResult> onCompleted)
        {
            YooAssetLocalManifestResult lastResult = default;
            if (TryGetDefaultCacheFileSystem(out var cacheFileSystem))
            {
                YooAssetLocalManifestResult result = default;
                yield return TryLoadCacheManifest(cacheFileSystem, packageVersion, value => result = value);
                if (result.Succeeded)
                {
                    onCompleted?.Invoke(result);
                    yield break;
                }

                lastResult = result;
            }

            foreach (var entry in GetLocalFileSystems())
            {
                yield return TryLoadFileSystemManifest(
                    entry.FileSystem,
                    entry.Source,
                    packageVersion,
                    value => lastResult = value);

                if (lastResult.Succeeded)
                {
                    onCompleted?.Invoke(lastResult);
                    yield break;
                }
            }

            if (string.IsNullOrEmpty(lastResult.Error))
            {
                lastResult = Failed(
                    YooAssetLocalManifestSource.None,
                    packageVersion,
                    $"No local manifest file system available. Package: {PackageName}");
            }

            onCompleted?.Invoke(lastResult);
        }

        /// <summary>
        /// 仅从 CacheFileSystem 加载指定版本，用于严格回退已提交的 LastGood。
        /// </summary>
        public IEnumerator TryLoadCacheManifest(
            string packageVersion,
            Action<YooAssetLocalManifestResult> onCompleted)
        {
            if (!TryGetDefaultCacheFileSystem(out var cacheFileSystem))
            {
                onCompleted?.Invoke(Failed(
                    YooAssetLocalManifestSource.Cache,
                    packageVersion,
                    $"Cache file system is unavailable. Package: {PackageName}"));
                yield break;
            }

            yield return TryLoadCacheManifest(cacheFileSystem, packageVersion, onCompleted);
        }

        /// <summary>
        /// 直接加载包体内置（WebGL 为 WebServer、编辑器为模拟目录）的 manifest。
        /// 不经过 CacheFileSystem，避免缓存中的失败版本遮蔽首包基线。
        /// </summary>
        public IEnumerator TryLoadBuildinManifest(Action<YooAssetLocalManifestResult> onCompleted)
        {
            YooAssetLocalManifestResult lastResult = default;
            bool foundFileSystem = false;
            foreach (var entry in GetLocalFileSystems())
            {
                foundFileSystem = true;
                string packageVersion = string.Empty;
                string versionError = string.Empty;
                yield return TryRequestLocalPackageVersion(
                    entry.FileSystem,
                    (succeeded, version, error) =>
                    {
                        if (succeeded)
                        {
                            packageVersion = version;
                        }
                        else
                        {
                            versionError = error;
                        }
                    });

                if (string.IsNullOrWhiteSpace(packageVersion))
                {
                    lastResult = Failed(
                        entry.Source,
                        string.Empty,
                        string.IsNullOrWhiteSpace(versionError)
                            ? $"Build-in package version is unavailable. Package: {PackageName}, Source: {entry.Source}"
                            : versionError);
                    continue;
                }

                yield return TryLoadFileSystemManifest(
                    entry.FileSystem,
                    entry.Source,
                    packageVersion,
                    value => lastResult = value);
                if (lastResult.Succeeded)
                {
                    onCompleted?.Invoke(lastResult);
                    yield break;
                }
            }

            if (!foundFileSystem)
            {
                lastResult = Failed(
                    YooAssetLocalManifestSource.None,
                    string.Empty,
                    $"No build-in package file system available. Package: {PackageName}");
            }

            onCompleted?.Invoke(lastResult);
        }

        private IEnumerator TryRequestLocalPackageVersion(IFileSystem fileSystem, List<string> candidates)
        {
            if (fileSystem == null)
            {
                yield break;
            }

            var operation = fileSystem.RequestPackageVersionAsync(false, LocalManifestTimeout);
            yield return operation;
            if (operation.Status == EOperationStatus.Succeed)
            {
                AddVersionCandidate(candidates, operation.PackageVersion);
            }
        }

        private IEnumerator TryRequestLocalPackageVersion(
            IFileSystem fileSystem,
            Action<bool, string, string> onCompleted)
        {
            if (fileSystem == null)
            {
                onCompleted?.Invoke(false, string.Empty, "Local package file system is null.");
                yield break;
            }

            var operation = fileSystem.RequestPackageVersionAsync(false, LocalManifestTimeout);
            yield return operation;
            bool succeeded = operation.Status == EOperationStatus.Succeed &&
                             !string.IsNullOrWhiteSpace(operation.PackageVersion);
            onCompleted?.Invoke(succeeded, operation.PackageVersion, operation.Error);
        }

        private IEnumerator TryLoadFileSystemManifest(
            IFileSystem fileSystem,
            YooAssetLocalManifestSource source,
            string packageVersion,
            Action<YooAssetLocalManifestResult> onCompleted)
        {
            if (fileSystem == null)
            {
                onCompleted?.Invoke(Failed(source, packageVersion, $"{source} file system is null."));
                yield break;
            }

            var operation = fileSystem.LoadPackageManifestAsync(packageVersion, LocalManifestTimeout);
            yield return operation;
            CompleteManifestOperation(source, packageVersion, operation, onCompleted);
        }

        private IEnumerator TryLoadCacheManifest(
            DefaultCacheFileSystem cacheFileSystem,
            string packageVersion,
            Action<YooAssetLocalManifestResult> onCompleted)
        {
            string hashFilePath = string.Empty;
            string packageHash;
            try
            {
                hashFilePath = cacheFileSystem.GetCachePackageHashFilePath(packageVersion);
                if (string.IsNullOrEmpty(hashFilePath) || !File.Exists(hashFilePath))
                {
                    onCompleted?.Invoke(Failed(
                        YooAssetLocalManifestSource.Cache,
                        packageVersion,
                        $"Can not found cache package hash file : {hashFilePath}"));
                    yield break;
                }

                packageHash = FileUtility.ReadAllText(hashFilePath);
            }
            catch (Exception exception)
            {
                onCompleted?.Invoke(Failed(
                    YooAssetLocalManifestSource.Cache,
                    packageVersion,
                    $"Read cache package hash failed. Path={hashFilePath ?? string.Empty}. {exception.Message}"));
                yield break;
            }

            if (string.IsNullOrWhiteSpace(packageHash))
            {
                onCompleted?.Invoke(Failed(
                    YooAssetLocalManifestSource.Cache,
                    packageVersion,
                    $"Cache package hash file content is empty : {hashFilePath}"));
                yield break;
            }

            var operation = new LoadCachePackageManifestOperation(cacheFileSystem, packageVersion, packageHash.Trim());
            OperationSystem.StartOperation(PackageName, operation);
            yield return operation;
            CompleteCacheManifestOperation(packageVersion, operation, onCompleted);
        }

        private void CompleteManifestOperation(
            YooAssetLocalManifestSource source,
            string packageVersion,
            FSLoadPackageManifestOperation operation,
            Action<YooAssetLocalManifestResult> onCompleted)
        {
            if (operation.Status != EOperationStatus.Succeed)
            {
                onCompleted?.Invoke(Failed(source, packageVersion, operation.Error));
                return;
            }

            if (operation.Manifest == null)
            {
                onCompleted?.Invoke(Failed(source, packageVersion, "Local manifest is null."));
                return;
            }

            if (!ValidateManifestIdentity(operation.Manifest, packageVersion, out var identityError))
            {
                onCompleted?.Invoke(Failed(source, packageVersion, identityError));
                return;
            }

            playModeImpl.ActiveManifest = operation.Manifest;
            onCompleted?.Invoke(Succeeded(source, packageVersion));
        }

        private void CompleteCacheManifestOperation(
            string packageVersion,
            LoadCachePackageManifestOperation operation,
            Action<YooAssetLocalManifestResult> onCompleted)
        {
            if (operation.Status != EOperationStatus.Succeed)
            {
                onCompleted?.Invoke(Failed(YooAssetLocalManifestSource.Cache, packageVersion, operation.Error));
                return;
            }

            if (operation.Manifest == null)
            {
                onCompleted?.Invoke(Failed(YooAssetLocalManifestSource.Cache, packageVersion, "Cache manifest is null."));
                return;
            }

            if (!ValidateManifestIdentity(operation.Manifest, packageVersion, out var identityError))
            {
                onCompleted?.Invoke(Failed(YooAssetLocalManifestSource.Cache, packageVersion, identityError));
                return;
            }

            playModeImpl.ActiveManifest = operation.Manifest;
            onCompleted?.Invoke(Succeeded(YooAssetLocalManifestSource.Cache, packageVersion));
        }

        private bool ValidateManifestIdentity(PackageManifest manifest, string requestedVersion, out string error)
        {
            error = string.Empty;
            if (!string.Equals(manifest.PackageName, PackageName, StringComparison.Ordinal) ||
                !string.Equals(manifest.PackageVersion, requestedVersion, StringComparison.Ordinal))
            {
                error = $"Local manifest identity mismatch. " +
                        $"Expected={PackageName}:{requestedVersion}, " +
                        $"Actual={manifest.PackageName}:{manifest.PackageVersion}.";
                return false;
            }

            return true;
        }

        private IEnumerable<LocalFileSystemEntry> GetLocalFileSystems()
        {
            if (playModeImpl is HostPlayModeImpl hostPlayModeImpl)
            {
                if (hostPlayModeImpl.BuildinFileSystem != null)
                {
                    yield return new LocalFileSystemEntry(hostPlayModeImpl.BuildinFileSystem, YooAssetLocalManifestSource.Buildin);
                }

                yield break;
            }

            if (playModeImpl is OfflinePlayModeImpl offlinePlayModeImpl)
            {
                if (offlinePlayModeImpl.BuildinFileSystem != null)
                {
                    yield return new LocalFileSystemEntry(offlinePlayModeImpl.BuildinFileSystem, YooAssetLocalManifestSource.Buildin);
                }

                yield break;
            }

            if (playModeImpl is WebPlayModeImpl webPlayModeImpl)
            {
                if (webPlayModeImpl.WebServerFileSystem != null)
                {
                    yield return new LocalFileSystemEntry(webPlayModeImpl.WebServerFileSystem, YooAssetLocalManifestSource.WebServer);
                }

                yield break;
            }

            if (playModeImpl is EditorSimulateModeImpl editorSimulateModeImpl &&
                editorSimulateModeImpl.EditorFileSystem != null)
            {
                yield return new LocalFileSystemEntry(editorSimulateModeImpl.EditorFileSystem, YooAssetLocalManifestSource.Editor);
            }
        }

        private bool TryGetDefaultCacheFileSystem(out DefaultCacheFileSystem cacheFileSystem)
        {
            cacheFileSystem = null;
            if (playModeImpl is HostPlayModeImpl hostPlayModeImpl)
            {
                cacheFileSystem = hostPlayModeImpl.CacheFileSystem as DefaultCacheFileSystem;
            }

            return cacheFileSystem != null;
        }

        private YooAssetLocalManifestResult Succeeded(YooAssetLocalManifestSource source, string packageVersion)
        {
            return new YooAssetLocalManifestResult
            {
                Succeeded = true,
                PackageName = PackageName,
                PackageVersion = packageVersion,
                Source = source
            };
        }

        private YooAssetLocalManifestResult Failed(YooAssetLocalManifestSource source, string packageVersion, string error)
        {
            return new YooAssetLocalManifestResult
            {
                Succeeded = false,
                PackageName = PackageName,
                PackageVersion = packageVersion,
                Source = source,
                Error = error
            };
        }

        private static void AddVersionCandidate(List<string> candidates, string packageVersion)
        {
            if (candidates == null || string.IsNullOrWhiteSpace(packageVersion))
            {
                return;
            }

            string normalizedVersion = packageVersion.Trim();
            if (!candidates.Contains(normalizedVersion))
            {
                candidates.Add(normalizedVersion);
            }
        }

        private readonly struct LocalFileSystemEntry
        {
            public LocalFileSystemEntry(IFileSystem fileSystem, YooAssetLocalManifestSource source)
            {
                FileSystem = fileSystem;
                Source = source;
            }

            public IFileSystem FileSystem { get; }
            public YooAssetLocalManifestSource Source { get; }
        }
    }
}
