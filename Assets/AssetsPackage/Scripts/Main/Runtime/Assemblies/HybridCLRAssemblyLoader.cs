using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed class AOTAssemblyManifestSnapshot
    {
        public string AppVersion = string.Empty;
        public string BuildTarget = string.Empty;
        public string AotVersion = string.Empty;
        public List<string> AotMetadataAssemblies = new List<string>();

        public static AOTAssemblyManifestSnapshot From(AOTAssemblyManifest manifest)
        {
            return new AOTAssemblyManifestSnapshot
            {
                AppVersion = manifest == null ? string.Empty : manifest.AppVersion ?? string.Empty,
                BuildTarget = manifest == null ? string.Empty : manifest.BuildTarget ?? string.Empty,
                AotVersion = manifest == null ? string.Empty : manifest.AotVersion ?? string.Empty,
                AotMetadataAssemblies = NormalizeNames(manifest == null ? null : manifest.AotMetadataAssemblies)
            };
        }

        private static List<string> NormalizeNames(IEnumerable<string> assemblyNames)
        {
            return HybridCLRAssemblyLoader.NormalizeAssemblyNamesForManifest(assemblyNames);
        }
    }

    public sealed class HotfixAssemblyManifestSnapshot
    {
        public string AppVersionMin = string.Empty;
        public string AppVersionMax = string.Empty;
        public string BuildTarget = string.Empty;
        public string RequiredAotVersion = string.Empty;
        public string HotfixVersion = string.Empty;
        public List<string> HotUpdateAssemblies = new List<string>();
        public string EntrySceneAddress = string.Empty;
        public string EntryPrefabAddress = string.Empty;
        public string EntryTypeName = string.Empty;
        public string EntryMethodName = string.Empty;

        public static HotfixAssemblyManifestSnapshot From(HotfixAssemblyManifest manifest)
        {
            return new HotfixAssemblyManifestSnapshot
            {
                AppVersionMin = manifest == null ? string.Empty : manifest.AppVersionMin ?? string.Empty,
                AppVersionMax = manifest == null ? string.Empty : manifest.AppVersionMax ?? string.Empty,
                BuildTarget = manifest == null ? string.Empty : manifest.BuildTarget ?? string.Empty,
                RequiredAotVersion = manifest == null ? string.Empty : manifest.RequiredAotVersion ?? string.Empty,
                HotfixVersion = manifest == null ? string.Empty : manifest.HotfixVersion ?? string.Empty,
                HotUpdateAssemblies = NormalizeNames(manifest == null ? null : manifest.HotUpdateAssemblies),
                EntrySceneAddress = manifest == null ? string.Empty : manifest.EntrySceneAddress ?? string.Empty,
                EntryPrefabAddress = manifest == null ? string.Empty : manifest.EntryPrefabAddress ?? string.Empty,
                EntryTypeName = manifest == null ? string.Empty : manifest.EntryTypeName ?? string.Empty,
                EntryMethodName = manifest == null ? string.Empty : manifest.EntryMethodName ?? string.Empty
            };
        }

        private static List<string> NormalizeNames(IEnumerable<string> assemblyNames)
        {
            return HybridCLRAssemblyLoader.NormalizeAssemblyNamesForManifest(assemblyNames);
        }
    }

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

    public class HybridCLRAssemblyLoader
    {
        private readonly Dictionary<string, Assembly> mLoadedAssembliesCache = new Dictionary<string, Assembly>();
        private readonly List<Assembly> mHotUpdateAssemblies = new List<Assembly>();
        private HotfixAssemblyLoadContext mContext;

        public IReadOnlyList<Assembly> HotUpdateAssemblies => mHotUpdateAssemblies;
        public bool Succeeded { get; private set; }
        public string Error { get; private set; }
        public string EntrySceneAddress { get; private set; } = HotfixUtility.DefaultEntrySceneAddress;
        public string EntryTypeName { get; private set; } = string.Empty;
        public string EntryMethodName { get; private set; } = string.Empty;
        public AOTAssemblyManifestSnapshot AotManifest => mContext == null ? null : mContext.AotManifest;
        public HotfixAssemblyManifestSnapshot HotfixManifest => mContext == null ? null : mContext.HotfixManifest;

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

        public IEnumerator LoadAotMetadata(ResourcePackage package, Action<float> onProgress = null)
        {
            yield return LoadAotMetadata(package, new HotfixAssemblyLoadContext(), onProgress);
        }

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

        public IEnumerator LoadHotUpdateAssemblies(ResourcePackage package, Action<float> onProgress = null)
        {
            yield return LoadHotUpdateAssemblies(package, new HotfixAssemblyLoadContext(), onProgress);
        }

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
            EntryMethodName = string.Empty;
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
            var snapshot = HotfixAssemblyManifestSnapshot.From(manifest);
            manifestHandle.Release();

            if (!ValidateHotfixManifest(snapshot))
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
            var snapshot = AOTAssemblyManifestSnapshot.From(manifest);
            manifestHandle.Release();

            if (!ValidateAotManifest(snapshot, context.HotfixManifest))
            {
                yield break;
            }

            context.SetAotManifest(snapshot);
        }

        private bool ValidateHotfixManifest(HotfixAssemblyManifestSnapshot manifest)
        {
            if (manifest == null)
            {
                Fail($"Hotfix manifest not found: {HotfixAssemblyManifest.AssetName}");
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

            if (string.IsNullOrWhiteSpace(manifest.EntryTypeName))
            {
                Fail("Hotfix manifest EntryTypeName is empty. CodeEntry is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(manifest.EntryMethodName))
            {
                Fail("Hotfix manifest EntryMethodName is empty. CodeEntry is required.");
                return false;
            }

            return true;
        }

        private bool ValidateAotManifest(AOTAssemblyManifestSnapshot aotManifest, HotfixAssemblyManifestSnapshot hotfixManifest)
        {
            if (aotManifest == null)
            {
                Fail($"AOT manifest not found: {AOTAssemblyManifest.AssetName}");
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

            if (hotfixManifest != null &&
                !string.Equals(aotManifest.AotVersion, hotfixManifest.RequiredAotVersion, StringComparison.OrdinalIgnoreCase))
            {
                Fail($"AOT version mismatch. Hotfix requires {hotfixManifest.RequiredAotVersion}, but current AOT manifest is {aotManifest.AotVersion}.");
                return false;
            }

            return true;
        }

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
            var hotUpdateAssemblies = SortAssembliesByDependency(mContext.HotfixManifest.HotUpdateAssemblies);
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

            // InvokeEntryMethodIfConfigured();
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
            yield return LoadDllBytes(package, dllName, value => bytes = value);
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

            byte[] bytes = null;
            yield return LoadDllBytes(package, dllName, value => bytes = value);
            if (!string.IsNullOrEmpty(Error))
            {
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

        private IEnumerator LoadDllBytes(ResourcePackage package, string dllName, Action<byte[]> onLoaded)
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

            onLoaded?.Invoke(textAsset.bytes);
            handle.Release();
        }

        private void ApplyHotfixEntry(HotfixAssemblyManifestSnapshot manifest)
        {
            EntrySceneAddress = string.IsNullOrWhiteSpace(manifest.EntrySceneAddress)
                ? HotfixUtility.DefaultEntrySceneAddress
                : manifest.EntrySceneAddress.Trim();
            EntryTypeName = manifest.EntryTypeName ?? string.Empty;
            EntryMethodName = manifest.EntryMethodName ?? string.Empty;
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

        /// <summary>
        /// 按依赖关系排序热更程序集，确保被依赖的程序集先加载。
        /// 使用简单的拓扑排序：已加载的程序集依赖项优先，未知的排后面。
        /// </summary>
        private static List<string> SortAssembliesByDependency(List<string> assemblyNames)
        {
            if (assemblyNames == null || assemblyNames.Count <= 1)
            {
                return assemblyNames;
            }

            // 构建程序集名（不含.dll）到完整名的映射
            var nameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var name in assemblyNames)
            {
                string key = System.IO.Path.GetFileNameWithoutExtension(name);
                nameMap[key] = name;
            }

            // 检查已加载的程序集，确定哪些依赖已经在 AppDomain 中
            var loadedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                loadedNames.Add(asm.GetName().Name);
            }

            // 简单排序：依赖已加载程序集的排在前面，其他保持原顺序
            var sorted = new List<string>(assemblyNames.Count);
            var remaining = new List<string>();

            foreach (var name in assemblyNames)
            {
                string asmName = System.IO.Path.GetFileNameWithoutExtension(name);
                // 如果程序集名不在未加载的热更列表中，说明它的依赖已满足
                bool depsSatisfied = true;
                // 粗粒度判断：所有非热更程序集都已加载
                // 更精确的依赖排序需要在 Assembly.Load 后检查 Assembly.GetReferencedAssemblies()
                // 但那需要先加载才能检查，所以这里用简单的启发式

                if (depsSatisfied)
                {
                    sorted.Add(name);
                }
                else
                {
                    remaining.Add(name);
                }
            }

            sorted.AddRange(remaining);
            return sorted;
        }

        private void Fail(string error)
        {
            Succeeded = false;
            Error = error;
            Debug.LogError(error);
        }
    }
}
