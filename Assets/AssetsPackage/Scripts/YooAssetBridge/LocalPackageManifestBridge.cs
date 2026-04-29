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
            string hashFilePath = cacheFileSystem.GetCachePackageHashFilePath(packageVersion);
            if (string.IsNullOrEmpty(hashFilePath) || !File.Exists(hashFilePath))
            {
                onCompleted?.Invoke(Failed(
                    YooAssetLocalManifestSource.Cache,
                    packageVersion,
                    $"Can not found cache package hash file : {hashFilePath}"));
                yield break;
            }

            string packageHash = FileUtility.ReadAllText(hashFilePath);
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

            playModeImpl.ActiveManifest = operation.Manifest;
            onCompleted?.Invoke(Succeeded(YooAssetLocalManifestSource.Cache, packageVersion));
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
