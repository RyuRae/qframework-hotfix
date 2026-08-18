using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using YooAsset;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if ENABLE_HYBRID_CLR_UNITY
using HybridCLR;
#endif

namespace Framework.Assemblies
{
    /// <summary>
    /// AOT 清单的运行时不可变快照，避免卸载 ScriptableObject 后继续持有资源对象。
    /// </summary>
    public sealed class AOTAssemblyManifestSnapshot
    {
        public string ReleaseVersion = string.Empty;
        public long ReleaseSequence;
        public string AppVersion = string.Empty;
        public string BuildTarget = string.Empty;
        public string AotVersion = string.Empty;
        public List<string> AotMetadataAssemblies = new List<string>();
        public List<AssemblyFileRecord> AotMetadataFiles = new List<AssemblyFileRecord>();

        /// <summary>从资源清单复制运行时需要的发布身份与文件记录。</summary>
        public static AOTAssemblyManifestSnapshot From(AOTAssemblyManifest manifest)
        {
            return new AOTAssemblyManifestSnapshot
            {
                ReleaseVersion = manifest == null ? string.Empty : manifest.ReleaseVersion ?? string.Empty,
                ReleaseSequence = manifest == null ? 0 : manifest.ReleaseSequence,
                AppVersion = manifest == null ? string.Empty : manifest.AppVersion ?? string.Empty,
                BuildTarget = manifest == null ? string.Empty : manifest.BuildTarget ?? string.Empty,
                AotVersion = manifest == null ? string.Empty : manifest.AotVersion ?? string.Empty,
                AotMetadataAssemblies = NormalizeNames(manifest == null ? null : manifest.AotMetadataAssemblies),
                AotMetadataFiles = AssemblyManifestSnapshotUtility.CloneFileRecords(manifest == null ? null : manifest.AotMetadataFiles)
            };
        }

        private static List<string> NormalizeNames(IEnumerable<string> assemblyNames)
        {
            return HybridCLRAssemblyLoader.NormalizeAssemblyNamesForManifest(assemblyNames);
        }
    }

    /// <summary>
    /// Hotfix 清单的运行时不可变快照，供程序集加载、入口创建和 LastGood 校验共享。
    /// </summary>
    public sealed class HotfixAssemblyManifestSnapshot
    {
        public int SignatureVersion;
        public string ReleaseVersion = string.Empty;
        public long ReleaseSequence;
        public string AppVersionMin = string.Empty;
        public string AppVersionMax = string.Empty;
        public string BuildTarget = string.Empty;
        public string RequiredAotVersion = string.Empty;
        public string HotfixVersion = string.Empty;
        public string RawFilePackageName = string.Empty;
        public string RawFilePackageVersion = string.Empty;
        public string RawFileManifestSha256 = string.Empty;
        public List<string> HotUpdateAssemblies = new List<string>();
        public List<AssemblyFileRecord> HotUpdateFiles = new List<AssemblyFileRecord>();
        public List<AssemblyDependencyRecord> HotUpdateDependencies = new List<AssemblyDependencyRecord>();
        public string EntrySceneAddress = string.Empty;
        public string EntryPrefabAddress = string.Empty;
        public string EntryTypeName = string.Empty;

        /// <summary>从资源清单复制运行时需要的兼容性、依赖、入口和 RawFile 信息。</summary>
        public static HotfixAssemblyManifestSnapshot From(HotfixAssemblyManifest manifest)
        {
            return new HotfixAssemblyManifestSnapshot
            {
                SignatureVersion = manifest == null ? 0 : manifest.SignatureVersion,
                ReleaseVersion = manifest == null ? string.Empty : manifest.ReleaseVersion ?? string.Empty,
                ReleaseSequence = manifest == null ? 0 : manifest.ReleaseSequence,
                AppVersionMin = manifest == null ? string.Empty : manifest.AppVersionMin ?? string.Empty,
                AppVersionMax = manifest == null ? string.Empty : manifest.AppVersionMax ?? string.Empty,
                BuildTarget = manifest == null ? string.Empty : manifest.BuildTarget ?? string.Empty,
                RequiredAotVersion = manifest == null ? string.Empty : manifest.RequiredAotVersion ?? string.Empty,
                HotfixVersion = manifest == null ? string.Empty : manifest.HotfixVersion ?? string.Empty,
                RawFilePackageName = manifest == null ? string.Empty : manifest.RawFilePackageName ?? string.Empty,
                RawFilePackageVersion = manifest == null ? string.Empty : manifest.RawFilePackageVersion ?? string.Empty,
                RawFileManifestSha256 = manifest == null ? string.Empty : manifest.RawFileManifestSha256 ?? string.Empty,
                HotUpdateAssemblies = NormalizeNames(manifest == null ? null : manifest.HotUpdateAssemblies),
                HotUpdateFiles = AssemblyManifestSnapshotUtility.CloneFileRecords(manifest == null ? null : manifest.HotUpdateFiles),
                HotUpdateDependencies = AssemblyManifestSnapshotUtility.CloneDependencyRecords(manifest == null ? null : manifest.HotUpdateDependencies),
                EntrySceneAddress = manifest == null ? string.Empty : manifest.EntrySceneAddress ?? string.Empty,
                EntryPrefabAddress = manifest == null ? string.Empty : manifest.EntryPrefabAddress ?? string.Empty,
                EntryTypeName = manifest == null ? string.Empty : manifest.EntryTypeName ?? string.Empty
            };
        }

        private static List<string> NormalizeNames(IEnumerable<string> assemblyNames)
        {
            return HybridCLRAssemblyLoader.NormalizeAssemblyNamesForManifest(assemblyNames);
        }
    }

    /// <summary>清单快照深拷贝辅助工具。</summary>
    internal static class AssemblyManifestSnapshotUtility
    {
        public static List<AssemblyFileRecord> CloneFileRecords(IEnumerable<AssemblyFileRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyFileRecord>())
                .Where(record => record != null)
                .Select(record => new AssemblyFileRecord
                {
                    FileName = record.FileName ?? string.Empty,
                    AssemblyName = record.AssemblyName ?? string.Empty,
                    Sha256 = record.Sha256 ?? string.Empty,
                    Size = record.Size
                })
                .ToList();
        }

        public static List<AssemblyDependencyRecord> CloneDependencyRecords(IEnumerable<AssemblyDependencyRecord> records)
        {
            return (records ?? Enumerable.Empty<AssemblyDependencyRecord>())
                .Where(record => record != null)
                .Select(record => new AssemblyDependencyRecord
                {
                    AssemblyName = record.AssemblyName ?? string.Empty,
                    DllName = record.DllName ?? string.Empty,
                    DependsOn = record.DependsOn == null ? new List<string>() : new List<string>(record.DependsOn)
                })
                .ToList();
        }
    }

    /// <summary>
    /// 单次启动内共享的 AOT/Hotfix 清单上下文，避免两个加载阶段重复读取和产生版本漂移。
    /// </summary>
    public sealed class HotfixAssemblyLoadContext
    {
        public AOTAssemblyManifestSnapshot AotManifest { get; private set; }
        public HotfixAssemblyManifestSnapshot HotfixManifest { get; private set; }

        public bool HasAotManifest => AotManifest != null;
        public bool HasHotfixManifest => HotfixManifest != null;

        public void SetAotManifest(AOTAssemblyManifestSnapshot manifest)
        {
            AotManifest = manifest;
        }

        public void SetHotfixManifest(HotfixAssemblyManifestSnapshot manifest)
        {
            HotfixManifest = manifest;
        }
    }

    /// <summary>
    /// HybridCLR 程序集加载器：先验证并补充 AOT 元数据，再按 Manifest 顺序校验和加载热更 DLL。
    /// </summary>
    public class HybridCLRAssemblyLoader
    {
        private readonly Dictionary<string, Assembly> mLoadedAssembliesCache = new Dictionary<string, Assembly>();
        private readonly List<Assembly> mHotUpdateAssemblies = new List<Assembly>();
        private HotfixAssemblyLoadContext mContext;
        private HotfixRuntimeSettings mRuntimeSettings;
        private HotfixRemoteSettings mRemoteSettings;

        public IReadOnlyList<Assembly> HotUpdateAssemblies => mHotUpdateAssemblies;
        public bool Succeeded { get; private set; }
        public string Error { get; private set; }
        public string EntrySceneAddress { get; private set; } = HotfixUtility.DefaultEntrySceneAddress;
        public string EntryTypeName { get; private set; } = string.Empty;
        public AOTAssemblyManifestSnapshot AotManifest => mContext == null ? null : mContext.AotManifest;
        public HotfixAssemblyManifestSnapshot HotfixManifest => mContext == null ? null : mContext.HotfixManifest;

        /// <summary>顺序执行 AOT 元数据和热更程序集的完整加载流程。</summary>
        public IEnumerator Load(ResourcePackage package, Action<float> onProgress = null)
        {
            var context = new HotfixAssemblyLoadContext();
            yield return LoadAotMetadata(package, context, progress => onProgress?.Invoke(progress * 0.5f));
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            yield return LoadHotUpdateAssemblies(package, context, progress => onProgress?.Invoke(0.5f + progress * 0.5f));
        }

        /// <summary>创建独立上下文并加载当前发布要求的 AOT 元数据。</summary>
        public IEnumerator LoadAotMetadata(ResourcePackage package, Action<float> onProgress = null)
        {
            yield return LoadAotMetadata(package, new HotfixAssemblyLoadContext(), onProgress);
        }

        /// <summary>复用指定上下文加载并验证 AOT Manifest 与 metadata DLL。</summary>
        public IEnumerator LoadAotMetadata(ResourcePackage package, HotfixAssemblyLoadContext context, Action<float> onProgress = null)
        {
            ResetOperation(context);
            yield return EnsureHotfixManifest(package, context);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            yield return EnsureAotManifest(package, context);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            yield return LoadAotMetadataAssemblies(package, onProgress);
        }

        /// <summary>创建独立上下文并加载热更程序集。</summary>
        public IEnumerator LoadHotUpdateAssemblies(ResourcePackage package, Action<float> onProgress = null)
        {
            yield return LoadHotUpdateAssemblies(package, new HotfixAssemblyLoadContext(), onProgress);
        }

        /// <summary>复用已验证的清单上下文，按依赖排序加载全部热更程序集。</summary>
        public IEnumerator LoadHotUpdateAssemblies(ResourcePackage package, HotfixAssemblyLoadContext context, Action<float> onProgress = null)
        {
            ResetOperation(context);
            yield return EnsureHotfixManifest(package, context);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            if (!context.HasAotManifest)
            {
                yield return EnsureAotManifest(package, context);
                if (!string.IsNullOrEmpty(Error))
                {
                    yield break;
                }
            }

            yield return LoadHotUpdateAssembliesFromManifest(package, onProgress);
        }

        private void ResetOperation(HotfixAssemblyLoadContext context)
        {
            Succeeded = false;
            Error = string.Empty;
            EntrySceneAddress = HotfixUtility.DefaultEntrySceneAddress;
            EntryTypeName = string.Empty;
            mContext = context ?? new HotfixAssemblyLoadContext();
        }

        private IEnumerator EnsureHotfixManifest(ResourcePackage package, HotfixAssemblyLoadContext context)
        {
            if (package == null)
            {
                Fail("YooAsset package is null, cannot load hotfix assembly manifest.");
                yield break;
            }

            if (context.HasHotfixManifest)
            {
                ApplyHotfixEntry(context.HotfixManifest);
                yield break;
            }

            var manifestHandle = package.LoadAssetAsync<HotfixAssemblyManifest>(HotfixAssemblyManifest.AssetName);
            yield return manifestHandle;
            if (manifestHandle.Status != EOperationStatus.Succeed)
            {
                Fail($"Hotfix manifest load failed: {HotfixAssemblyManifest.AssetName}. {manifestHandle.LastError}. " +
                     "Please ensure the first package or remote package contains the hotfix manifest.");
                manifestHandle.Release();
                yield break;
            }

            var manifest = manifestHandle.AssetObject as HotfixAssemblyManifest;
            if (!ValidateManifestSignature(manifest))
            {
                manifestHandle.Release();
                yield break;
            }

            var snapshot = HotfixAssemblyManifestSnapshot.From(manifest);
            manifestHandle.Release();

            if (!ValidateHotfixManifest(snapshot, package.GetPackageVersion()))
            {
                yield break;
            }

            context.SetHotfixManifest(snapshot);
            ApplyHotfixEntry(snapshot);
        }

        private IEnumerator EnsureAotManifest(ResourcePackage package, HotfixAssemblyLoadContext context)
        {
            if (package == null)
            {
                Fail("YooAsset package is null, cannot load AOT assembly manifest.");
                yield break;
            }

            if (context.HasAotManifest)
            {
                ValidateAotManifest(context.AotManifest, context.HotfixManifest);
                yield break;
            }

            var manifestHandle = package.LoadAssetAsync<AOTAssemblyManifest>(AOTAssemblyManifest.AssetName);
            yield return manifestHandle;
            if (manifestHandle.Status != EOperationStatus.Succeed)
            {
                Fail($"AOT manifest load failed: {AOTAssemblyManifest.AssetName}. {manifestHandle.LastError}. " +
                     "Please ensure the first package contains AOT metadata or the remote package provides the required AOT manifest.");
                manifestHandle.Release();
                yield break;
            }

            var manifest = manifestHandle.AssetObject as AOTAssemblyManifest;
            if (!ValidateManifestSignature(manifest))
            {
                manifestHandle.Release();
                yield break;
            }

            var snapshot = AOTAssemblyManifestSnapshot.From(manifest);
            manifestHandle.Release();

            if (!ValidateAotManifest(snapshot, context.HotfixManifest))
            {
                yield break;
            }

            context.SetAotManifest(snapshot);
        }

        private bool ValidateHotfixManifest(HotfixAssemblyManifestSnapshot manifest, string activePackageVersion)
        {
            if (manifest == null)
            {
                Fail($"Hotfix manifest not found: {HotfixAssemblyManifest.AssetName}");
                return false;
            }

            if (!ValidateReleaseVersion(manifest.ReleaseVersion, activePackageVersion, "Hotfix"))
            {
                return false;
            }

            if (AreSignedManifestsRequired() &&
                !HotfixReleaseTrustStore.TryValidate(
                    manifest.ReleaseSequence,
                    manifest.ReleaseVersion,
                    out var rollbackError))
            {
                Fail(rollbackError);
                return false;
            }

            if (!IsBuildTargetCompatible(manifest.BuildTarget))
            {
                Fail($"Hotfix manifest BuildTarget mismatch. Manifest: {manifest.BuildTarget}, Runtime: {HotfixUtility.GetRuntimePlatformName()}");
                return false;
            }

            if (!IsAppVersionCompatible(
                    manifest.AppVersionMin,
                    manifest.AppVersionMax,
                    Application.version,
                    out var versionError))
            {
                Fail(versionError);
                return false;
            }

            if (string.IsNullOrWhiteSpace(manifest.RequiredAotVersion))
            {
                Fail("Hotfix manifest RequiredAotVersion is empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(manifest.HotfixVersion))
            {
                Fail("Hotfix manifest HotfixVersion is empty.");
                return false;
            }

            if (manifest.HotUpdateAssemblies == null || manifest.HotUpdateAssemblies.Count == 0)
            {
                Fail("Hotfix manifest does not contain any hot update assembly.");
                return false;
            }

            if (!ValidateAssemblyFileRecords(manifest.HotUpdateAssemblies, manifest.HotUpdateFiles, "Hotfix manifest"))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(manifest.EntryTypeName))
            {
                Fail("Hotfix manifest EntryTypeName is empty. IHotfixEntry is required.");
                return false;
            }

            return true;
        }

        private bool ValidateAotManifest(
            AOTAssemblyManifestSnapshot aotManifest,
            HotfixAssemblyManifestSnapshot hotfixManifest,
            string activePackageVersion = "")
        {
            if (aotManifest == null)
            {
                Fail($"AOT manifest not found: {AOTAssemblyManifest.AssetName}");
                return false;
            }

            if (!ValidateReleaseVersion(aotManifest.ReleaseVersion, activePackageVersion, "AOT"))
            {
                return false;
            }

            if (!IsBuildTargetCompatible(aotManifest.BuildTarget))
            {
                Fail($"AOT manifest BuildTarget mismatch. Manifest: {aotManifest.BuildTarget}, Runtime: {HotfixUtility.GetRuntimePlatformName()}");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(aotManifest.AppVersion) &&
                !string.Equals(aotManifest.AppVersion, Application.version, StringComparison.OrdinalIgnoreCase))
            {
                Fail($"AOT manifest AppVersion mismatch. Manifest: {aotManifest.AppVersion}, Runtime: {Application.version}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(aotManifest.AotVersion))
            {
                Fail("AOT manifest AotVersion is empty.");
                return false;
            }

            if (aotManifest.AotMetadataAssemblies.Count == 0)
            {
                Fail("AOT manifest does not contain any AOT metadata assembly.");
                return false;
            }

            if (!ValidateAssemblyFileRecords(aotManifest.AotMetadataAssemblies, aotManifest.AotMetadataFiles, "AOT manifest"))
            {
                return false;
            }

            if (hotfixManifest != null &&
                !string.Equals(aotManifest.AotVersion, hotfixManifest.RequiredAotVersion, StringComparison.OrdinalIgnoreCase))
            {
                Fail($"AOT version mismatch. Hotfix requires {hotfixManifest.RequiredAotVersion}, but current AOT manifest is {aotManifest.AotVersion}.");
                return false;
            }

            return true;
        }

        private bool ValidateManifestSignature(HotfixAssemblyManifest manifest)
        {
            bool hasMetadata = AssemblyManifestSignatureUtility.HasAnySignatureMetadata(manifest);
            bool required = AreSignedManifestsRequired();
            if (!required && !hasMetadata)
            {
                return true;
            }

            if (RuntimeSettings == null ||
                !RuntimeSettings.TryGetTrustedManifestPublicKey(
                    manifest == null ? string.Empty : manifest.SigningKeyId,
                    out var publicKey))
            {
                Fail($"Hotfix manifest signing key is not trusted. KeyId={manifest?.SigningKeyId}");
                return false;
            }

            if (!AssemblyManifestSignatureUtility.Verify(manifest, publicKey, out var error))
            {
                Fail(error);
                return false;
            }

            return true;
        }

        private bool ValidateManifestSignature(AOTAssemblyManifest manifest)
        {
            bool hasMetadata = AssemblyManifestSignatureUtility.HasAnySignatureMetadata(manifest);
            bool required = AreSignedManifestsRequired();
            if (!required && !hasMetadata)
            {
                return true;
            }

            if (RuntimeSettings == null ||
                !RuntimeSettings.TryGetTrustedManifestPublicKey(
                    manifest == null ? string.Empty : manifest.SigningKeyId,
                    out var publicKey))
            {
                Fail($"AOT manifest signing key is not trusted. KeyId={manifest?.SigningKeyId}");
                return false;
            }

            if (!AssemblyManifestSignatureUtility.Verify(manifest, publicKey, out var error))
            {
                Fail(error);
                return false;
            }

            return true;
        }

        private bool ValidateReleaseVersion(string manifestReleaseVersion, string activePackageVersion, string label)
        {
            bool required = AreSignedManifestsRequired();
            if (!required)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(manifestReleaseVersion))
            {
                Fail($"{label} manifest ReleaseVersion is empty.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(activePackageVersion) &&
                !string.Equals(manifestReleaseVersion.Trim(), activePackageVersion.Trim(), StringComparison.Ordinal))
            {
                Fail($"{label} manifest ReleaseVersion mismatch. Manifest={manifestReleaseVersion}, Package={activePackageVersion}");
                return false;
            }

            return true;
        }

        private bool AreSignedManifestsRequired()
        {
            return RuntimeSettings != null && RuntimeSettings.RequireSignedAssemblyManifests ||
                   RemoteSettings != null && RemoteSettings.IsProductionRuntimeEnvironment;
        }

        private HotfixRuntimeSettings RuntimeSettings =>
            mRuntimeSettings == null ? mRuntimeSettings = HotfixRuntimeSettings.Load() : mRuntimeSettings;

        private HotfixRemoteSettings RemoteSettings =>
            mRemoteSettings == null ? mRemoteSettings = HotfixRemoteSettings.Load() : mRemoteSettings;

        private IEnumerator LoadAotMetadataAssemblies(ResourcePackage package, Action<float> onProgress)
        {
            var aotAssemblies = mContext.AotManifest.AotMetadataAssemblies;
            int totalCount = aotAssemblies.Count;
            int loadedCount = 0;
            onProgress?.Invoke(0f);

#if ENABLE_HYBRID_CLR_UNITY
            foreach (var dllName in aotAssemblies)
            {
                yield return LoadAotMetadataAssembly(package, dllName);
                if (!string.IsNullOrEmpty(Error))
                {
                    yield break;
                }

                loadedCount++;
                onProgress?.Invoke(GetProgress(loadedCount, totalCount));
            }
#else
            loadedCount = totalCount;
            onProgress?.Invoke(GetProgress(loadedCount, totalCount));
            yield return null;
#endif

            Succeeded = true;
            onProgress?.Invoke(1f);
        }

        private IEnumerator LoadHotUpdateAssembliesFromManifest(ResourcePackage package, Action<float> onProgress)
        {
            var hotUpdateAssemblies = mContext.HotfixManifest.HotUpdateAssemblies;
            int totalCount = hotUpdateAssemblies.Count;
            int loadedCount = 0;
            onProgress?.Invoke(0f);

            foreach (var dllName in hotUpdateAssemblies)
            {
                yield return LoadHotUpdateAssembly(package, dllName);
                if (!string.IsNullOrEmpty(Error))
                {
                    yield break;
                }

                loadedCount++;
                onProgress?.Invoke(GetProgress(loadedCount, totalCount));
            }

            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            Succeeded = true;
            onProgress?.Invoke(1f);
        }

        private IEnumerator LoadAotMetadataAssembly(ResourcePackage package, string dllName)
        {
            byte[] bytes = null;
            var expectedRecord = FindAssemblyFileRecord(mContext.AotManifest.AotMetadataFiles, dllName);
            yield return LoadDllBytes(package, dllName, expectedRecord, "AOT metadata", value => bytes = value);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

#if ENABLE_HYBRID_CLR_UNITY
            var errorCode = RuntimeApi.LoadMetadataForAOTAssembly(bytes, HomologousImageMode.SuperSet);
            Debug.Log($"Load AOT metadata: {dllName}, mode: {HomologousImageMode.SuperSet}, result: {errorCode}");
            if (errorCode != LoadImageErrorCode.OK &&
                errorCode != LoadImageErrorCode.HOMOLOGOUS_ASSEMBLY_HAS_LOADED)
            {
                Fail($"Load metadata for AOT assembly failed: {dllName}, error: {errorCode}");
            }
#endif
        }

        private IEnumerator LoadHotUpdateAssembly(ResourcePackage package, string dllName)
        {
            byte[] bytes = null;
            var expectedRecord = FindAssemblyFileRecord(mContext.HotfixManifest.HotUpdateFiles, dllName);
            yield return LoadDllBytes(package, dllName, expectedRecord, "Hotfix DLL", value => bytes = value);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            if (mLoadedAssembliesCache.TryGetValue(dllName, out var cachedAssembly))
            {
                CacheHotUpdateAssembly(dllName, cachedAssembly);
                yield break;
            }

            Assembly assembly = FindLoadedAssembly(dllName);
            if (assembly != null)
            {
                CacheHotUpdateAssembly(dllName, assembly);
                Debug.Log($"Use loaded hotfix assembly: {assembly.GetName().Name}");
                yield break;
            }

            try
            {
                assembly = Assembly.Load(bytes);
            }
            catch (Exception exception)
            {
                Fail($"Load hot update assembly failed: {dllName}. {exception}");
                yield break;
            }

            CacheHotUpdateAssembly(dllName, assembly);
            Debug.Log($"Load hotfix assembly: {assembly.GetName().Name}");
        }

        private IEnumerator LoadDllBytes(
            ResourcePackage package,
            string dllName,
            AssemblyFileRecord expectedRecord,
            string label,
            Action<byte[]> onLoaded)
        {
            var handle = package.LoadAssetAsync<TextAsset>(dllName);
            yield return handle;
            if (handle.Status != EOperationStatus.Succeed)
            {
                Fail($"Assembly asset load failed: {dllName}. {handle.LastError}. " +
                     "Check the AOT/Hotfix manifest version and ensure the DLL is included in the first package or remote package.");
                handle.Release();
                yield break;
            }

            var textAsset = handle.AssetObject as TextAsset;
            if (textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0)
            {
                Fail($"Assembly bytes missing or empty: {dllName}");
                handle.Release();
                yield break;
            }

            var bytes = textAsset.bytes;
            if (!ValidateAssemblyBytes(dllName, bytes, expectedRecord, label))
            {
                handle.Release();
                yield break;
            }

            onLoaded?.Invoke(bytes);
            handle.Release();
        }

        private bool ValidateAssemblyFileRecords(
            List<string> assemblyNames,
            List<AssemblyFileRecord> records,
            string manifestLabel)
        {
            var expectedDllNames = NormalizeAssemblyNamesForManifest(assemblyNames);
            var expectedSet = new HashSet<string>(expectedDllNames, StringComparer.OrdinalIgnoreCase);
            var recordMap = new Dictionary<string, AssemblyFileRecord>(StringComparer.OrdinalIgnoreCase);

            foreach (var record in records ?? Enumerable.Empty<AssemblyFileRecord>())
            {
                string fileName = GetAssemblyRecordFileName(record);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Fail($"{manifestLabel} contains an empty file hash record.");
                    return false;
                }

                string normalizedFileName = NormalizeAssemblyNamesForManifest(new[] { fileName }).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(normalizedFileName))
                {
                    Fail($"{manifestLabel} contains an invalid file hash record: {fileName}");
                    return false;
                }

                if (!expectedSet.Contains(normalizedFileName))
                {
                    Fail($"{manifestLabel} contains an unexpected file hash record: {normalizedFileName}");
                    return false;
                }

                if (recordMap.ContainsKey(normalizedFileName))
                {
                    Fail($"{manifestLabel} contains duplicate file hash record: {normalizedFileName}");
                    return false;
                }

                if (record.Size <= 0 || !IsSha256Hex(record.Sha256))
                {
                    Fail($"{manifestLabel} has invalid file hash record for {normalizedFileName}. Size={record.Size}, Sha256={record.Sha256}");
                    return false;
                }

                recordMap.Add(normalizedFileName, record);
            }

            foreach (var dllName in expectedDllNames)
            {
                if (!recordMap.ContainsKey(dllName))
                {
                    Fail($"{manifestLabel} is missing file hash record for {dllName}.");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateAssemblyBytes(
            string dllName,
            byte[] bytes,
            AssemblyFileRecord expectedRecord,
            string label)
        {
            if (expectedRecord == null)
            {
                Fail($"{label} hash record missing before loading bytes: {dllName}");
                return false;
            }

            long actualSize = bytes == null ? 0 : bytes.LongLength;
            string actualSha256 = ComputeSha256(bytes);
            if (expectedRecord.Size != actualSize ||
                !string.Equals(expectedRecord.Sha256, actualSha256, StringComparison.OrdinalIgnoreCase))
            {
                Fail($"{label} bytes validation failed: {dllName}. " +
                     $"Expected size={expectedRecord.Size}, sha256={expectedRecord.Sha256}; " +
                     $"Actual size={actualSize}, sha256={actualSha256}");
                return false;
            }

            return true;
        }

        private static AssemblyFileRecord FindAssemblyFileRecord(IEnumerable<AssemblyFileRecord> records, string dllName)
        {
            string normalizedDllName = NormalizeAssemblyNamesForManifest(new[] { dllName }).FirstOrDefault() ?? string.Empty;
            var map = ToAssemblyFileRecordMap(records);
            return map.TryGetValue(normalizedDllName, out var record) ? record : null;
        }

        private static Dictionary<string, AssemblyFileRecord> ToAssemblyFileRecordMap(IEnumerable<AssemblyFileRecord> records)
        {
            var map = new Dictionary<string, AssemblyFileRecord>(StringComparer.OrdinalIgnoreCase);
            foreach (var record in records ?? Enumerable.Empty<AssemblyFileRecord>())
            {
                string fileName = GetAssemblyRecordFileName(record);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    continue;
                }

                string normalizedFileName = NormalizeAssemblyNamesForManifest(new[] { fileName }).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(normalizedFileName))
                {
                    map[normalizedFileName] = record;
                }
            }

            return map;
        }

        private static string GetAssemblyRecordFileName(AssemblyFileRecord record)
        {
            if (record == null)
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(record.FileName) ? record.AssemblyName : record.FileName;
        }

        private static string ComputeSha256(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(bytes ?? Array.Empty<byte>());
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        private static bool IsSha256Hex(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string text = value.Trim();
            if (text.Length != 64)
            {
                return false;
            }

            return text.All(IsHexChar);
        }

        private static bool IsHexChar(char character)
        {
            return character >= '0' && character <= '9' ||
                   character >= 'a' && character <= 'f' ||
                   character >= 'A' && character <= 'F';
        }

        private void ApplyHotfixEntry(HotfixAssemblyManifestSnapshot manifest)
        {
            EntrySceneAddress = string.IsNullOrWhiteSpace(manifest.EntrySceneAddress)
                ? HotfixUtility.DefaultEntrySceneAddress
                : manifest.EntrySceneAddress.Trim();
            EntryTypeName = manifest.EntryTypeName ?? string.Empty;
        }

        private void CacheHotUpdateAssembly(string dllName, Assembly assembly)
        {
            mLoadedAssembliesCache[dllName] = assembly;
            if (!mHotUpdateAssemblies.Contains(assembly))
            {
                mHotUpdateAssemblies.Add(assembly);
            }
        }

        private static Assembly FindLoadedAssembly(string dllName)
        {
            string assemblyName = Path.GetFileNameWithoutExtension(dllName);
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => assembly.GetName().Name == assemblyName);
        }

       
        internal static List<string> NormalizeAssemblyNamesForManifest(IEnumerable<string> assemblyNames)
        {
            return HotfixUtility.NormalizeAssemblyNames(assemblyNames);
        }

        private static bool IsBuildTargetCompatible(string manifestBuildTarget)
        {
            return HotfixUtility.IsBuildTargetCompatible(manifestBuildTarget);
        }

#if UNITY_EDITOR
        private static string ConvertBuildTarget(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.WebGL:
                    return "WebGL";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.StandaloneOSX:
                    return "macOS";
                case BuildTarget.StandaloneLinux64:
                    return "Linux";
                default:
                    return target.ToString();
            }
        }
#endif

        private static bool IsAppVersionCompatible(string minVersion, string maxVersion, string currentVersion, out string error)
        {
            error = string.Empty;
            if (!string.IsNullOrWhiteSpace(minVersion) && CompareVersion(currentVersion, minVersion) < 0)
            {
                error = $"Hotfix manifest requires app version >= {minVersion}, current app version is {currentVersion}. Please update the app.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(maxVersion) && CompareVersion(currentVersion, maxVersion) > 0)
            {
                error = $"Hotfix manifest requires app version <= {maxVersion}, current app version is {currentVersion}. Please update hotfix resources.";
                return false;
            }

            return true;
        }

        private static int CompareVersion(string left, string right)
        {
            if (Version.TryParse(left, out var leftVersion) &&
                Version.TryParse(right, out var rightVersion))
            {
                return leftVersion.CompareTo(rightVersion);
            }

            return string.Compare(left ?? string.Empty, right ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }

        private static float GetProgress(int loadedCount, int totalCount)
        {
            return totalCount <= 0 ? 1f : loadedCount / (float)totalCount;
        }

        private void Fail(string error)
        {
            Succeeded = false;
            Error = error;
            Debug.LogError(error);
        }
    }
}
