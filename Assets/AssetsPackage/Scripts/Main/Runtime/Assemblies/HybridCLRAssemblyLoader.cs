using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YooAsset;
#if ENABLE_HYBRID_CLR_UNITY
using HybridCLR;
#endif

namespace MsbFramework.Assemblies
{
    public class HybridCLRAssemblyLoader
    {
        private readonly Dictionary<string, Assembly> mLoadedAssembliesCache = new Dictionary<string, Assembly>();
        private readonly List<Assembly> mHotUpdateAssemblies = new List<Assembly>();

        public IReadOnlyList<Assembly> HotUpdateAssemblies => mHotUpdateAssemblies;
        public bool Succeeded { get; private set; }
        public string Error { get; private set; }

        public IEnumerator Load(ResourcePackage package, Action<float> onProgress = null)
        {
            Succeeded = false;
            Error = string.Empty;
            mHotUpdateAssemblies.Clear();

            if (package == null)
            {
                Fail("YooAsset package is null, cannot load hot update assemblies.");
                yield break;
            }

            var manifestHandle = package.LoadAssetAsync<AssemblyManifest>(AssemblyManifest.AssetName);
            yield return manifestHandle;

            var manifest = manifestHandle.AssetObject as AssemblyManifest;
            if (manifest == null)
            {
                Fail($"Assembly manifest not found: {AssemblyManifest.AssetName}");
                manifestHandle.Release();
                yield break;
            }

            var aotAssemblies = NormalizeAssemblyNames(manifest.AotMetadataAssemblies);
            var hotUpdateAssemblies = NormalizeAssemblyNames(manifest.HotUpdateAssemblies);
            manifestHandle.Release();

            int totalCount = aotAssemblies.Count + hotUpdateAssemblies.Count;
            int loadedCount = 0;

            onProgress?.Invoke(0f);

#if ENABLE_HYBRID_CLR_UNITY
            foreach (var dllName in aotAssemblies)
            {
                yield return LoadAotMetadata(package, dllName);
                if (!string.IsNullOrEmpty(Error))
                {
                    yield break;
                }

                loadedCount++;
                onProgress?.Invoke(GetProgress(loadedCount, totalCount));
            }
#else
            loadedCount += aotAssemblies.Count;
            onProgress?.Invoke(GetProgress(loadedCount, totalCount));
#endif

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

            Succeeded = true;
            onProgress?.Invoke(1f);
        }

        private IEnumerator LoadAotMetadata(ResourcePackage package, string dllName)
        {
            byte[] bytes = null;
            yield return LoadDllBytes(package, dllName, value => bytes = value);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

#if ENABLE_HYBRID_CLR_UNITY
            var errorCode = RuntimeApi.LoadMetadataForAOTAssembly(bytes, HomologousImageMode.SuperSet);
            Debug.Log($"LoadMetadataForAOTAssembly:{dllName}. mode:{HomologousImageMode.SuperSet} ret:{errorCode}");
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
                Debug.Log($"Use loaded hotfix assembly:{assembly.GetName().Name}");
                yield break;
            }

            byte[] bytes = null;
            yield return LoadDllBytes(package, dllName, value => bytes = value);
            if (!string.IsNullOrEmpty(Error))
            {
                yield break;
            }

            assembly = Assembly.Load(bytes);
            CacheHotUpdateAssembly(dllName, assembly);
            Debug.Log($"LoadHotfixAssembly:{assembly.GetName().Name}");
        }

        private IEnumerator LoadDllBytes(ResourcePackage package, string dllName, Action<byte[]> onLoaded)
        {
            var handle = package.LoadAssetAsync<TextAsset>(dllName);
            yield return handle;

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

        private static List<string> NormalizeAssemblyNames(IEnumerable<string> assemblyNames)
        {
            if (assemblyNames == null)
            {
                return new List<string>();
            }

            return assemblyNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.EndsWith(".dll") ? name : $"{name}.dll")
                .Distinct()
                .ToList();
        }

        private static float GetProgress(int loadedCount, int totalCount)
        {
            return totalCount <= 0 ? 1f : loadedCount / (float)totalCount;
        }

        private void Fail(string error)
        {
            Error = error;
            Debug.LogError(error);
        }
    }
}
