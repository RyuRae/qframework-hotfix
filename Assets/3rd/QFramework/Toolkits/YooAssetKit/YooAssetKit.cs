using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;
using Luban;


namespace QFramework
{
    /// <summary>
    /// YooAsset资源加载系统
    /// </summary>
    public class YooAssetKit
    {
        private const string DefaultPackageName = "DefaultPackage";
        private static string sDefaultPackageName = DefaultPackageName;
        private static readonly Dictionary<int, Stack<IDisposable>> sLegacyAssetLeases =
            new Dictionary<int, Stack<IDisposable>>();

        /// <summary>
        /// 设置默认包
        /// </summary>
        /// <param name="packageName"></param>
        public static void SetDefaultPackage(string packageName = DefaultPackageName)
        {
            sDefaultPackageName = NormalizePackageName(packageName);
            var package = YooAssets.GetPackage(sDefaultPackageName);
            YooAssets.SetDefaultPackage(package);
        }

        public static ResourcePackage GetPackageOrDefault(string packageName = null)
        {
            var resolvedPackageName = NormalizePackageName(packageName);
            var package = YooAssets.TryGetPackage(resolvedPackageName);
            if (package != null)
            {
                return package;
            }

            if (string.Equals(resolvedPackageName, DefaultPackageName, StringComparison.Ordinal) &&
                !string.Equals(sDefaultPackageName, DefaultPackageName, StringComparison.Ordinal))
            {
                return YooAssets.GetPackage(sDefaultPackageName);
            }

            return YooAssets.GetPackage(resolvedPackageName);
        }

        /// <summary>
        /// 同步加载资源并把底层 Handle 的所有权交给租约。调用方必须 Dispose 返回值。
        /// </summary>
        public static YooAssetLease<T> LoadAssetLeaseSync<T>(
            string assetName,
            string packageName = null) where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            AssetHandle handle = package.LoadAssetSync<T>(assetName);
            try
            {
                return CreateAssetLease<T>(handle, assetName, package.PackageName);
            }
            catch
            {
                handle?.Release();
                throw;
            }
        }

        /// <summary>
        /// 异步加载资源并把底层 Handle 的所有权交给租约。调用方必须 Dispose 返回值。
        /// </summary>
        public static async UniTask<YooAssetLease<T>> LoadAssetLeaseAsync<T>(
            string assetName,
            string packageName = null) where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            AssetHandle handle = package.LoadAssetAsync<T>(assetName);
            try
            {
                await handle.Task;
                return CreateAssetLease<T>(handle, assetName, package.PackageName);
            }
            catch
            {
                handle?.Release();
                throw;
            }
        }

        /// <summary>
        /// 回调式异步加载。成功时 lease 的所有权转移给回调；失败时回调收到 null。
        /// </summary>
        public static void LoadAssetLeaseAsync<T>(
            string assetName,
            Action<YooAssetLease<T>> onLoad,
            string packageName = null) where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            AssetHandle handle = package.LoadAssetAsync<T>(assetName);
            handle.Completed += completedHandle =>
            {
                YooAssetLease<T> lease;
                try
                {
                    lease = CreateAssetLease<T>(completedHandle, assetName, package.PackageName);
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception);
                    completedHandle.Release();
                    try
                    {
                        onLoad?.Invoke(null);
                    }
                    catch (Exception callbackException)
                    {
                        Debug.LogException(callbackException);
                    }
                    return;
                }

                if (onLoad == null)
                {
                    lease.Dispose();
                    return;
                }

                try
                {
                    onLoad(lease);
                }
                catch (Exception exception)
                {
                    lease.Dispose();
                    Debug.LogException(exception);
                }
            };
        }

        /// <summary>
        /// 同步加载指定子资源。返回的租约持有整个 SubAssetsHandle，调用方必须 Dispose。
        /// </summary>
        public static YooAssetLease<T> LoadSubAssetLeaseSync<T>(
            string assetName,
            string subAssetName,
            string packageName = null) where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            SubAssetsHandle handle = package.LoadSubAssetsSync<T>(assetName);
            try
            {
                return CreateSubAssetLease<T>(handle, assetName, subAssetName, package.PackageName);
            }
            catch
            {
                handle?.Release();
                throw;
            }
        }

        /// <summary>
        /// 异步加载指定子资源。返回的租约持有整个 SubAssetsHandle，调用方必须 Dispose。
        /// </summary>
        public static async UniTask<YooAssetLease<T>> LoadSubAssetLeaseAsync<T>(
            string assetName,
            string subAssetName,
            string packageName = null) where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            SubAssetsHandle handle = package.LoadSubAssetsAsync<T>(assetName);
            try
            {
                await handle.Task;
                return CreateSubAssetLease<T>(handle, assetName, subAssetName, package.PackageName);
            }
            catch
            {
                handle?.Release();
                throw;
            }
        }

        /// <summary>
        /// 通过资源包名同步加载资源
        /// </summary>
        [Obsolete("Use LoadAssetLeaseSync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static T LoadAssetSync<T>(string assetName, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var lease = LoadAssetLeaseSync<T>(assetName, packageName);
            return RetainLegacyAsset(lease);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        [Obsolete("Use LoadAssetLeaseSync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static T LoadAssetSync<T>(string assetName) where T : UnityEngine.Object
        {
            var lease = LoadAssetLeaseSync<T>(assetName);
            return RetainLegacyAsset(lease);
        }

        /// <summary>
        /// 同步加载子资源
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="assetName"></param>
        /// <param name="subAssetName"></param>
        /// <returns></returns>
        [Obsolete("Use LoadSubAssetLeaseSync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static T LoadSubAssetSync<T>(string assetName, string subAssetName, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            return RetainLegacyAsset(LoadSubAssetLeaseSync<T>(assetName, subAssetName, packageName));
        }

        /// <summary>
        /// 同步加载子资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="subAssetName"></param>
        /// <returns></returns>
        [Obsolete("Use LoadSubAssetLeaseSync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static T LoadSubAssetSync<T>(string assetName, string subAssetName) where T : UnityEngine.Object
        {
            return RetainLegacyAsset(LoadSubAssetLeaseSync<T>(assetName, subAssetName));
        }

        /// <summary>
        /// 通过包名异步加载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">资源包名</param>
        [Obsolete("Use LoadAssetLeaseAsync<T>() and dispose the lease after use. If compatibility is required, call ReleaseAsset(asset).")]
        public static void LoadAssetAsync<T>(string assetName, Action<T> onLoad, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            LoadAssetLeaseAsync<T>(assetName, lease => InvokeLegacyCallback(lease, onLoad), packageName);
        }

        /// <summary>
        /// 异步加载资源（加载默认包资源）
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        [Obsolete("Use LoadAssetLeaseAsync<T>() and dispose the lease after use. If compatibility is required, call ReleaseAsset(asset).")]
        public static void LoadAssetAsync<T>(string assetName, Action<T> onLoad) where T : UnityEngine.Object
        {
            LoadAssetLeaseAsync<T>(assetName, lease => InvokeLegacyCallback(lease, onLoad));
        }

        /// <summary>
        /// 加载资源（异步转同步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        [Obsolete("Use LoadAssetLeaseAsync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static async UniTask<T> LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
        {
            var lease = await LoadAssetLeaseAsync<T>(assetName);
            return RetainLegacyAsset(lease);
        }

        /// <summary>
        /// 仅用于旧裸资源 API 的配对释放。新代码应直接 Dispose YooAssetLease。
        /// 同一资源被旧 API 加载多次时，每次调用只释放一次引用。
        /// </summary>
        public static bool ReleaseAsset(UnityEngine.Object asset)
        {
            if (ReferenceEquals(asset, null))
            {
                return false;
            }

            int instanceId = asset.GetInstanceID();
            if (!sLegacyAssetLeases.TryGetValue(instanceId, out var leases) || leases.Count == 0)
            {
                Debug.LogWarning($"YooAssetKit.ReleaseAsset ignored: no legacy lease owns '{asset.name}'.");
                return false;
            }

            leases.Pop().Dispose();
            if (leases.Count == 0)
            {
                sLegacyAssetLeases.Remove(instanceId);
            }

            return true;
        }

        /// <summary>
        /// 释放所有仍由旧裸资源 API 托管的句柄。只建议在退出游戏或测试清理阶段使用。
        /// </summary>
        public static void ReleaseAllLegacyAssets()
        {
            foreach (var leases in sLegacyAssetLeases.Values)
            {
                while (leases.Count > 0)
                {
                    leases.Pop().Dispose();
                }
            }

            sLegacyAssetLeases.Clear();
        }

        /// <summary>
        /// 获取指定Tag下的资源信息。
        /// </summary>
        public static AssetInfo[] GetAssetInfosByTag(string tag, string packageName = "DefaultPackage")
        {
            return GetAssetInfosByTags(new[] { tag }, packageName);
        }

        /// <summary>
        /// 获取多个Tag下的资源信息。
        /// </summary>
        public static AssetInfo[] GetAssetInfosByTags(string[] tags, string packageName = "DefaultPackage")
        {
            var normalizedTags = NormalizeTags(tags);
            if (normalizedTags.Length == 0)
            {
                return Array.Empty<AssetInfo>();
            }

            var package = GetPackageOrDefault(packageName);
            return package.GetAssetInfos(normalizedTags);
        }

        /// <summary>
        /// 根据Tag创建下载器。Tag为空时创建全部差异资源下载器。
        /// </summary>
        public static ResourceDownloaderOperation CreateDownloaderByTag(
            string tag,
            int downloadingMaxNum = 10,
            int failedTryAgain = 3,
            string packageName = "DefaultPackage")
        {
            return CreateDownloaderByTags(new[] { tag }, downloadingMaxNum, failedTryAgain, packageName);
        }

        /// <summary>
        /// 根据多个Tag创建下载器。Tags为空时创建全部差异资源下载器。
        /// </summary>
        public static ResourceDownloaderOperation CreateDownloaderByTags(
            string[] tags,
            int downloadingMaxNum = 10,
            int failedTryAgain = 3,
            string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            var normalizedTags = NormalizeTags(tags);
            return normalizedTags.Length > 0
                ? package.CreateResourceDownloader(normalizedTags, downloadingMaxNum, failedTryAgain)
                : package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
        }

        /// <summary>
        /// 根据Tag创建并启动下载器。
        /// </summary>
        public static ResourceDownloaderOperation DownloadByTagAsync(
            string tag,
            Action<ResourceDownloaderOperation> onCompleted = null,
            Action<DownloadUpdateData> onUpdate = null,
            Action<DownloadErrorData> onError = null,
            Action<DownloadFileData> onBeginDownloadFile = null,
            Action<DownloaderFinishData> onFinish = null,
            int downloadingMaxNum = 10,
            int failedTryAgain = 3,
            string packageName = "DefaultPackage")
        {
            return DownloadByTagsAsync(
                new[] { tag },
                onCompleted,
                onUpdate,
                onError,
                onBeginDownloadFile,
                onFinish,
                downloadingMaxNum,
                failedTryAgain,
                packageName);
        }

        /// <summary>
        /// 根据多个Tag创建并启动下载器。
        /// </summary>
        public static ResourceDownloaderOperation DownloadByTagsAsync(
            string[] tags,
            Action<ResourceDownloaderOperation> onCompleted = null,
            Action<DownloadUpdateData> onUpdate = null,
            Action<DownloadErrorData> onError = null,
            Action<DownloadFileData> onBeginDownloadFile = null,
            Action<DownloaderFinishData> onFinish = null,
            int downloadingMaxNum = 10,
            int failedTryAgain = 3,
            string packageName = "DefaultPackage")
        {
            var downloader = CreateDownloaderByTags(tags, downloadingMaxNum, failedTryAgain, packageName);
            Download(downloader, onCompleted, onUpdate, onError, onBeginDownloadFile, onFinish).ToAction().StartGlobal();
            return downloader;
        }

        /// <summary>
        /// 根据Tag异步加载资源集合。
        /// </summary>
        [Obsolete("Use LoadAssetsByTagsLeaseAsync<T>() and dispose the returned collection. If compatibility is required, call ReleaseAsset for every asset.")]
        public static void LoadAssetsByTagAsync<T>(
            string tag,
            Action<List<T>> onLoad,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            LoadAssetsByTagsAsync(new[] { tag }, onLoad, packageName);
        }

        /// <summary>
        /// 根据多个Tag异步加载资源集合。
        /// </summary>
        [Obsolete("Use LoadAssetsByTagsLeaseAsync<T>() and dispose the returned collection. If compatibility is required, call ReleaseAsset for every asset.")]
        public static void LoadAssetsByTagsAsync<T>(
            string[] tags,
            Action<List<T>> onLoad,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            LoadAssetsByTagsCompatAsync<T>(tags, onLoad, packageName).Forget();
        }

        /// <summary>
        /// 根据Tag异步加载资源集合。
        /// </summary>
        [Obsolete("Use LoadAssetsByTagsLeaseAsync<T>() and dispose the returned collection. If compatibility is required, call ReleaseAsset for every asset.")]
        public static async UniTask<List<T>> LoadAssetsByTagAsync<T>(
            string tag,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            return await LoadAssetsByTagsAsync<T>(new[] { tag }, packageName);
        }

        /// <summary>
        /// 根据多个Tag异步加载资源集合。
        /// </summary>
        [Obsolete("Use LoadAssetsByTagsLeaseAsync<T>() and dispose the returned collection. If compatibility is required, call ReleaseAsset for every asset.")]
        public static async UniTask<List<T>> LoadAssetsByTagsAsync<T>(
            string[] tags,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var leases = await LoadAssetsByTagsLeaseAsync<T>(tags, packageName);
            return RetainLegacyAssets(leases);
        }

        public static UniTask<YooAssetLeaseCollection<T>> LoadAssetsByTagLeaseAsync<T>(
            string tag,
            string packageName = null) where T : UnityEngine.Object
        {
            return LoadAssetsByTagsLeaseAsync<T>(new[] { tag }, packageName);
        }

        public static async UniTask<YooAssetLeaseCollection<T>> LoadAssetsByTagsLeaseAsync<T>(
            string[] tags,
            string packageName = null) where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            var normalizedTags = NormalizeTags(tags);
            if (normalizedTags.Length == 0)
            {
                return new YooAssetLeaseCollection<T>(new List<YooAssetLease<T>>());
            }

            var assetInfos = package.GetAssetInfos(normalizedTags);
            var leases = new List<YooAssetLease<T>>(assetInfos.Length);

            try
            {
                foreach (var assetInfo in assetInfos)
                {
                    AssetHandle handle = package.LoadAssetAsync(assetInfo);
                    try
                    {
                        await handle.Task;
                        leases.Add(CreateAssetLease<T>(handle, assetInfo.Address, package.PackageName));
                    }
                    catch
                    {
                        handle?.Release();
                        throw;
                    }
                }

                return new YooAssetLeaseCollection<T>(leases);
            }
            catch
            {
                foreach (var lease in leases)
                {
                    lease.Dispose();
                }

                throw;
            }
        }

        private static async UniTask LoadAssetsByTagsCompatAsync<T>(
            string[] tags,
            Action<List<T>> onLoad,
            string packageName) where T : UnityEngine.Object
        {
            YooAssetLeaseCollection<T> leases;
            try
            {
                leases = await LoadAssetsByTagsLeaseAsync<T>(tags, packageName);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                onLoad?.Invoke(new List<T>());
                return;
            }

            InvokeLegacyCollectionCallback(leases, onLoad);
        }


        //public static async UniTask<T> LoadTableAsync<T>(string assetName) where T : class, new()
        //{
        //    AssetHandle handle = YooAssets.LoadAssetAsync<TextAsset>(assetName);
        //    await handle.Task;
        //    handle.Release();
        //    var textAsset = handle.AssetObject as TextAsset;
        //    //var config = new T();
        //    //config._LoadData(textAsset.bytes);
        //    return config;
        //}

        /// <summary>
        /// 通过包名异步加载预制体
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">包名</param>
        [Obsolete("Use LoadAssetLeaseAsync<GameObject>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static void LoadGameObjectAsync(string assetName, Action<GameObject> onLoad, string packageName = "DefaultPackage")
        {
            LoadAssetLeaseAsync<GameObject>(assetName, lease => InvokeLegacyCallback(lease, onLoad), packageName);
        }

        /// <summary>
        /// 异步加载预制体（默认包）
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        [Obsolete("Use LoadAssetLeaseAsync<GameObject>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static void LoadGameObjectAsync(string assetName, Action<GameObject> onLoad)
        {
            LoadAssetLeaseAsync<GameObject>(assetName, lease => InvokeLegacyCallback(lease, onLoad));
        }

        /// <summary>
        /// 通过包名异步加载子对象（可用于加载纹理图集等）
        /// </summary>
        /// <typeparam name="T">子对象资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="subAssetName">子对象名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">包名</param>
        [Obsolete("Use LoadSubAssetLeaseAsync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static void LoadSubAssetAsync<T>(string assetName, string subAssetName, Action<T> onLoad, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            LoadSubAssetLeaseCompatAsync(assetName, subAssetName, onLoad, packageName).Forget();
        }

        private static async UniTask LoadSubAssetLeaseCompatAsync<T>(
            string assetName,
            string subAssetName,
            Action<T> onLoad,
            string packageName) where T : UnityEngine.Object
        {
            try
            {
                var lease = await LoadSubAssetLeaseAsync<T>(assetName, subAssetName, packageName);
                InvokeLegacyCallback(lease, onLoad);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                onLoad?.Invoke(null);
            }
        }

        /// <summary>
        /// 异步加载子对象（可用于加载纹理图集等）
        /// </summary>
        /// <typeparam name="T">子对象资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="subAssetName">子对象名称</param>
        /// <param name="onLoad">加载完成回调</param>
        [Obsolete("Use LoadSubAssetLeaseAsync<T>() and dispose the returned lease. If compatibility is required, call ReleaseAsset(asset).")]
        public static void LoadSubAssetAsync<T>(string assetName, string subAssetName, Action<T> onLoad) where T : UnityEngine.Object
        {
            LoadSubAssetLeaseCompatAsync(assetName, subAssetName, onLoad, null).Forget();
        }


        /// <summary>
        /// 通过包名异步加载原生文件返回bytes
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">包名</param>
        public static void LoadRawToByteAsync(string assetName, Action<byte[]> onLoad, string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            RawFileHandle handle = package.LoadRawFileAsync(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.GetRawFileData());
                handle.Release();
            };
        }

        /// <summary>
        /// 异步加载原生文件返回bytes
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        public static void LoadRawToByteAsync(string assetName, Action<byte[]> onLoad)
        {
            RawFileHandle handle = YooAssets.LoadRawFileAsync(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.GetRawFileData());
                handle.Release();
            };
        }


        /// <summary>
        /// 通过包名异步加载原生文件返回String
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">包名</param>
        public static void LoadRawToStringAsync(string assetName, Action<string> onLoad, string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            RawFileHandle handle = package.LoadRawFileAsync(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.GetRawFileText());
                handle.Release();
            };
        }

        /// <summary>
        /// 异步加载原生文件返回String
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        public static void LoadRawToStringAsync(string assetName, Action<string> onLoad)
        {
            RawFileHandle handle = YooAssets.LoadRawFileAsync(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.GetRawFileText());
                handle.Release();
            };
        }

        /// <summary>
        /// 通过包名异步加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="loadSceneMode">加载模式</param>
        /// <param name="suspendLoad">是否追加</param>
        /// <param name="onUpdateProgress">加载进度</param>
        /// <param name="packageName">包名</param>
        /// <remarks>onCompleted 非空时，成功 SceneHandle 的所有权转移给调用方；场景卸载后 YooAsset 会自动释放。</remarks>
        public static void LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, LocalPhysicsMode physicsMode = LocalPhysicsMode.None, bool suspendLoad = true, Action<float> onUpdateProgress = null, Action<SceneHandle> onCompleted = null, string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            LoadScene(package, sceneName, loadSceneMode, physicsMode, suspendLoad, onUpdateProgress, onCompleted).ToAction().StartGlobal();
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="loadSceneMode">加载模式</param>
        /// <param name="suspendLoad">是否追加</param>
        /// <param name="onUpdateProgress">加载进度</param>
        /// <remarks>onCompleted 非空时，成功 SceneHandle 的所有权转移给调用方；场景卸载后 YooAsset 会自动释放。</remarks>
        public static void LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, LocalPhysicsMode physicsMode = LocalPhysicsMode.None, bool suspendLoad = true, Action<float> onUpdateProgress = null, Action<SceneHandle> onCompleted = null)
        {
            LoadScene(sceneName, loadSceneMode, physicsMode, suspendLoad, onUpdateProgress, onCompleted).ToAction().StartGlobal();
        }

        /// <summary>
        /// 卸载所有未使用的资源（引用计数为0的资源）
        /// </summary>
        /// <param name="packageName"></param>
        public static void UnloadUnusedAssets(string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            UnloadUnusedAssets(package).ToAction().StartGlobal();
        }

        /// <summary>
        /// 尝试卸载指定的资源对象（资源在使用中，该方法无效）
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="packageName">包名</param>
        public static void TryUnloadUnusedAsset(string assetName, string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            package.TryUnloadUnusedAsset(assetName);
        }

        /// <summary>
        /// 强制卸载所有资源包（请在合适时机调用）
        /// </summary>
        public static void ForceUnloadAllAssets(string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            UnloadAllAssets(package).ToAction().StartGlobal();
        }


        private static IEnumerator UnloadUnusedAssets(ResourcePackage package)
        {
            var operation = package.UnloadUnusedAssetsAsync();
            yield return operation;
        }
        

        private static IEnumerator UnloadAllAssets(ResourcePackage package)
        {
            var operation = package.UnloadAllAssetsAsync();
            yield return operation;
        }

        private static YooAssetLease<T> CreateAssetLease<T>(
            AssetHandle handle,
            string assetName,
            string packageName) where T : UnityEngine.Object
        {
            if (handle == null)
            {
                throw new InvalidOperationException($"YooAsset load returned a null handle: {packageName}:{assetName}");
            }

            if (handle.Status != EOperationStatus.Succeed)
            {
                throw new InvalidOperationException(
                    $"YooAsset load failed: {packageName}:{assetName}. {handle.LastError}");
            }

            var asset = handle.GetAssetObject<T>();
            if (asset == null)
            {
                throw new InvalidCastException(
                    $"YooAsset type mismatch or empty asset: {packageName}:{assetName}, Expected={typeof(T).FullName}");
            }

            return new YooAssetLease<T>(asset, handle, assetName, packageName);
        }

        private static YooAssetLease<T> CreateSubAssetLease<T>(
            SubAssetsHandle handle,
            string assetName,
            string subAssetName,
            string packageName) where T : UnityEngine.Object
        {
            if (handle == null)
            {
                throw new InvalidOperationException($"YooAsset sub-asset load returned a null handle: {packageName}:{assetName}");
            }

            if (handle.Status != EOperationStatus.Succeed)
            {
                throw new InvalidOperationException(
                    $"YooAsset sub-asset load failed: {packageName}:{assetName}. {handle.LastError}");
            }

            var asset = handle.GetSubAssetObject<T>(subAssetName);
            if (asset == null)
            {
                throw new InvalidOperationException(
                    $"YooAsset sub-asset is missing or has the wrong type: " +
                    $"{packageName}:{assetName}/{subAssetName}, Expected={typeof(T).FullName}");
            }

            return new YooAssetLease<T>(asset, handle, $"{assetName}/{subAssetName}", packageName);
        }

        private static T RetainLegacyAsset<T>(YooAssetLease<T> lease) where T : UnityEngine.Object
        {
            if (lease == null || !lease.IsValid)
            {
                lease?.Dispose();
                return null;
            }

            T asset = lease.Asset;
            int instanceId = asset.GetInstanceID();
            if (!sLegacyAssetLeases.TryGetValue(instanceId, out var leases))
            {
                leases = new Stack<IDisposable>();
                sLegacyAssetLeases.Add(instanceId, leases);
            }

            leases.Push(lease);
            return asset;
        }

        private static List<T> RetainLegacyAssets<T>(
            YooAssetLeaseCollection<T> collection) where T : UnityEngine.Object
        {
            var assets = new List<T>();
            if (collection == null)
            {
                return assets;
            }

            foreach (var lease in collection.TransferOwnership())
            {
                T asset = RetainLegacyAsset(lease);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            return assets;
        }

        private static void InvokeLegacyCallback<T>(
            YooAssetLease<T> lease,
            Action<T> onLoad) where T : UnityEngine.Object
        {
            if (onLoad == null)
            {
                lease?.Dispose();
                return;
            }

            T asset = lease == null ? null : RetainLegacyAsset(lease);
            try
            {
                onLoad(asset);
            }
            catch
            {
                if (!ReferenceEquals(asset, null))
                {
                    ReleaseAsset(asset);
                }

                throw;
            }
        }

        private static void InvokeLegacyCollectionCallback<T>(
            YooAssetLeaseCollection<T> collection,
            Action<List<T>> onLoad) where T : UnityEngine.Object
        {
            if (onLoad == null)
            {
                collection?.Dispose();
                return;
            }

            var assets = RetainLegacyAssets(collection);
            try
            {
                onLoad(assets);
            }
            catch (Exception exception)
            {
                foreach (var asset in assets)
                {
                    ReleaseAsset(asset);
                }

                Debug.LogException(exception);
            }
        }

        private static string NormalizePackageName(string packageName)
        {
            return string.IsNullOrWhiteSpace(packageName) ? sDefaultPackageName : packageName.Trim();
        }

        private static IEnumerator Download(
            ResourceDownloaderOperation downloader,
            Action<ResourceDownloaderOperation> onCompleted,
            Action<DownloadUpdateData> onUpdate,
            Action<DownloadErrorData> onError,
            Action<DownloadFileData> onBeginDownloadFile,
            Action<DownloaderFinishData> onFinish)
        {
            if (downloader == null)
            {
                onCompleted?.Invoke(null);
                yield break;
            }

            downloader.DownloadUpdateCallback = data => onUpdate?.Invoke(data);
            downloader.DownloadErrorCallback = data => onError?.Invoke(data);
            downloader.DownloadFileBeginCallback = data => onBeginDownloadFile?.Invoke(data);
            downloader.DownloadFinishCallback = data => onFinish?.Invoke(data);
            downloader.BeginDownload();
            yield return downloader;
            onCompleted?.Invoke(downloader);
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


        private static IEnumerator LoadScene(ResourcePackage resourcePackage, string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single, LocalPhysicsMode physicsMode = LocalPhysicsMode.None, bool suspendLoad = true, Action<float> onUpdateProgress = null, Action<SceneHandle> onCompleted = null)
        {
            int displayBar = 0;
            int targetBar;
            float currProgress;
            SceneHandle handle = resourcePackage.LoadSceneAsync(scene, loadSceneMode, physicsMode, suspendLoad);
            //场景流畅加载
            if (!handle.IsDone)
            {
                while (handle.Progress < 0.9f)
                {
                    targetBar = (int)(handle.Progress * 100);
                    while (displayBar < targetBar)
                    {
                        ++displayBar;
                        currProgress = displayBar / 100.0f;
                        onUpdateProgress?.Invoke(currProgress);
                        yield return new WaitForEndOfFrame();
                    }
                    yield return null;
                }
                targetBar = 100;
                while (displayBar < targetBar)
                {
                    ++displayBar;
                    currProgress = displayBar / 100.0f;
                    onUpdateProgress?.Invoke(currProgress);
                    yield return new WaitForEndOfFrame();
                }
            }
            yield return handle;
            if (handle.Status != EOperationStatus.Succeed)
            {
                Debug.LogError(handle.LastError);
                handle.Release();
                yield break;
            }

            TransferSceneHandle(handle, onCompleted);
        }


        private static IEnumerator LoadScene(string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single, LocalPhysicsMode physicsMode = LocalPhysicsMode.None, bool suspendLoad = true, Action<float> onUpdateProgress = null, Action<SceneHandle> onCompleted = null)
        {
            int displayBar = 0;
            int targetBar;
            float currProgress;
            SceneHandle handle = YooAssets.LoadSceneAsync(scene, loadSceneMode, physicsMode, suspendLoad);
            //场景流畅加载
            //if (!handle.IsDone)
            {
                while (handle.Progress < 0.9f)
                {
                    targetBar = (int)(handle.Progress * 100);
                    while (displayBar < targetBar)
                    {
                        ++displayBar;
                        currProgress = displayBar / 100.0f;
                        onUpdateProgress?.Invoke(currProgress);
                        yield return new WaitForEndOfFrame();
                    }
                    yield return null;
                }
                targetBar = 100;
                while (displayBar < targetBar)
                {
                    ++displayBar;
                    currProgress = displayBar / 100.0f;
                    onUpdateProgress?.Invoke(currProgress);
                    yield return new WaitForEndOfFrame();
                }
            }
            //yield return new WaitUntil(() => displayBar == 100);
            yield return handle;
            if (handle.Status != EOperationStatus.Succeed)
            {
                Debug.LogError(handle.LastError);
                handle.Release();
                yield break;
            }

            TransferSceneHandle(handle, onCompleted);
        }

        private static void TransferSceneHandle(SceneHandle handle, Action<SceneHandle> onCompleted)
        {
            if (onCompleted == null)
            {
                handle.Release();
                return;
            }

            try
            {
                onCompleted(handle);
            }
            catch (Exception exception)
            {
                handle.Release();
                Debug.LogException(exception);
            }
        }

    }
}
