using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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

    public struct YooAssetManifestFingerprintResult
    {
        public bool Succeeded;
        public string PackageName;
        public string PackageVersion;
        public string Sha256;
        public string Error;
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

        public static bool TryGetActiveManifestFingerprint(
            ResourcePackage package,
            out YooAssetManifestFingerprintResult result)
        {
            result = default;
            if (!TryCreate(package, out var bridge, out var error))
            {
                result.Error = error;
                return false;
            }

            PackageManifest manifest = bridge.playModeImpl.ActiveManifest;
            if (manifest == null)
            {
                result.PackageName = package.PackageName;
                result.Error = $"Active YooAsset manifest is null. Package: {package.PackageName}";
                return false;
            }

            return TryCreateFingerprint(manifest, out result);
        }

#if UNITY_EDITOR
        public static bool TryGetBuiltManifestFingerprint(
            string outputPackageDirectory,
            string packageName,
            string packageVersion,
            out YooAssetManifestFingerprintResult result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(outputPackageDirectory) ||
                string.IsNullOrWhiteSpace(packageName) ||
                string.IsNullOrWhiteSpace(packageVersion))
            {
                result.Error = "Built manifest fingerprint requires output directory, package name, and package version.";
                return false;
            }

            string normalizedPackageName = packageName.Trim();
            string normalizedPackageVersion = packageVersion.Trim();
            string manifestPath = Path.Combine(
                outputPackageDirectory,
                YooAssetSettingsData.GetManifestBinaryFileName(normalizedPackageName, normalizedPackageVersion));
            if (!File.Exists(manifestPath))
            {
                result.PackageName = normalizedPackageName;
                result.PackageVersion = normalizedPackageVersion;
                result.Error = $"Built YooAsset manifest not found: {manifestPath}";
                return false;
            }

            try
            {
                var manifest = ManifestTools.DeserializeFromBinary(File.ReadAllBytes(manifestPath));
                if (!TryCreateFingerprint(manifest, out result))
                {
                    return false;
                }

                if (!string.Equals(result.PackageName, normalizedPackageName, StringComparison.Ordinal) ||
                    !string.Equals(result.PackageVersion, normalizedPackageVersion, StringComparison.Ordinal))
                {
                    result.Succeeded = false;
                    result.Error = $"Built manifest identity mismatch. " +
                                   $"Expected={normalizedPackageName}:{normalizedPackageVersion}, " +
                                   $"Actual={result.PackageName}:{result.PackageVersion}";
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                result.PackageName = normalizedPackageName;
                result.PackageVersion = normalizedPackageVersion;
                result.Error = $"Calculate built YooAsset manifest fingerprint failed. Path={manifestPath}. {exception.Message}";
                return false;
            }
        }

        public static bool TryCopyBuiltPackageToBuildin(
            string outputPackageDirectory,
            string buildinRootDirectory,
            string packageName,
            string packageVersion,
            out string error)
        {
            error = string.Empty;
            if (!TryLoadBuiltManifest(
                    outputPackageDirectory,
                    packageName,
                    packageVersion,
                    out var manifest,
                    out error))
            {
                return false;
            }

            try
            {
                Directory.CreateDirectory(buildinRootDirectory);
                CopyBuiltFile(
                    outputPackageDirectory,
                    buildinRootDirectory,
                    YooAssetSettingsData.GetManifestBinaryFileName(packageName.Trim(), packageVersion.Trim()));
                CopyBuiltFile(
                    outputPackageDirectory,
                    buildinRootDirectory,
                    YooAssetSettingsData.GetPackageHashFileName(packageName.Trim(), packageVersion.Trim()));
                CopyBuiltFile(
                    outputPackageDirectory,
                    buildinRootDirectory,
                    YooAssetSettingsData.GetPackageVersionFileName(packageName.Trim()));
                foreach (var bundle in manifest.BundleList)
                {
                    CopyBuiltFile(outputPackageDirectory, buildinRootDirectory, bundle.FileName);
                }

                return true;
            }
            catch (Exception exception)
            {
                error = $"Copy built YooAsset package to build-in directory failed. " +
                        $"Package={packageName}:{packageVersion}. {exception.Message}";
                return false;
            }
        }

        private static bool TryLoadBuiltManifest(
            string outputPackageDirectory,
            string packageName,
            string packageVersion,
            out PackageManifest manifest,
            out string error)
        {
            manifest = null;
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(outputPackageDirectory) ||
                string.IsNullOrWhiteSpace(packageName) ||
                string.IsNullOrWhiteSpace(packageVersion))
            {
                error = "Built YooAsset manifest requires output directory, package name, and package version.";
                return false;
            }

            string manifestPath = Path.Combine(
                outputPackageDirectory,
                YooAssetSettingsData.GetManifestBinaryFileName(packageName.Trim(), packageVersion.Trim()));
            if (!File.Exists(manifestPath))
            {
                error = $"Built YooAsset manifest not found: {manifestPath}";
                return false;
            }

            manifest = ManifestTools.DeserializeFromBinary(File.ReadAllBytes(manifestPath));
            if (!string.Equals(manifest.PackageName, packageName.Trim(), StringComparison.Ordinal) ||
                !string.Equals(manifest.PackageVersion, packageVersion.Trim(), StringComparison.Ordinal))
            {
                error = $"Built manifest identity mismatch. " +
                        $"Expected={packageName.Trim()}:{packageVersion.Trim()}, " +
                        $"Actual={manifest.PackageName}:{manifest.PackageVersion}";
                manifest = null;
                return false;
            }

            return true;
        }

        private static void CopyBuiltFile(string sourceDirectory, string destinationDirectory, string fileName)
        {
            string sourcePath = Path.Combine(sourceDirectory, fileName);
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"Built YooAsset file not found: {sourcePath}", sourcePath);
            }

            File.Copy(sourcePath, Path.Combine(destinationDirectory, fileName), true);
        }
#endif

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

        private static bool TryCreateFingerprint(
            PackageManifest manifest,
            out YooAssetManifestFingerprintResult result)
        {
            result = default;
            if (manifest == null)
            {
                result.Error = "YooAsset manifest is null.";
                return false;
            }

            try
            {
                using (var stream = new MemoryStream())
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    WriteString(writer, "YooAssetManifestFingerprintV1");
                    WriteString(writer, manifest.FileVersion);
                    writer.Write(manifest.LegacyDependency);
                    writer.Write(manifest.EnableAddressable);
                    writer.Write(manifest.LocationToLower);
                    writer.Write(manifest.IncludeAssetGUID);
                    writer.Write(manifest.OutputNameStyle);
                    writer.Write(manifest.BuildBundleType);
                    WriteString(writer, manifest.BuildPipeline);
                    WriteString(writer, manifest.PackageName);
                    WriteString(writer, manifest.PackageVersion);
                    WriteString(writer, manifest.PackageNote);

                    writer.Write(manifest.AssetList == null ? 0 : manifest.AssetList.Count);
                    foreach (var asset in manifest.AssetList ?? new List<PackageAsset>())
                    {
                        WriteString(writer, asset.Address);
                        WriteString(writer, asset.AssetPath);
                        WriteString(writer, asset.AssetGUID);
                        WriteStrings(writer, asset.AssetTags);
                        writer.Write(asset.BundleID);
                        WriteInts(writer, asset.DependBundleIDs);
                    }

                    writer.Write(manifest.BundleList == null ? 0 : manifest.BundleList.Count);
                    foreach (var bundle in manifest.BundleList ?? new List<PackageBundle>())
                    {
                        WriteString(writer, bundle.BundleName);
                        writer.Write(bundle.UnityCRC);
                        WriteString(writer, bundle.FileHash);
                        WriteString(writer, bundle.FileCRC);
                        writer.Write(bundle.FileSize);
                        writer.Write(bundle.Encrypted);
                        WriteStrings(writer, bundle.Tags);
                        WriteInts(writer, bundle.DependIDs);
                        WriteInts(writer, bundle.ReferenceBundleIDs);
                    }

                    writer.Flush();
                    using (var sha256 = SHA256.Create())
                    {
                        result = new YooAssetManifestFingerprintResult
                        {
                            Succeeded = true,
                            PackageName = manifest.PackageName ?? string.Empty,
                            PackageVersion = manifest.PackageVersion ?? string.Empty,
                            Sha256 = ToHex(sha256.ComputeHash(stream.ToArray()))
                        };
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                result.PackageName = manifest.PackageName ?? string.Empty;
                result.PackageVersion = manifest.PackageVersion ?? string.Empty;
                result.Error = $"Calculate YooAsset manifest fingerprint failed. {exception.Message}";
                return false;
            }
        }

        private static void WriteString(BinaryWriter writer, string value)
        {
            if (value == null)
            {
                writer.Write(-1);
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(value);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }

        private static void WriteStrings(BinaryWriter writer, string[] values)
        {
            writer.Write(values == null ? -1 : values.Length);
            if (values == null)
            {
                return;
            }

            foreach (var value in values)
            {
                WriteString(writer, value);
            }
        }

        private static void WriteInts(BinaryWriter writer, int[] values)
        {
            writer.Write(values == null ? -1 : values.Length);
            if (values == null)
            {
                return;
            }

            foreach (var value in values)
            {
                writer.Write(value);
            }
        }

        private static string ToHex(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte value in bytes)
            {
                builder.Append(value.ToString("x2"));
            }

            return builder.ToString();
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
