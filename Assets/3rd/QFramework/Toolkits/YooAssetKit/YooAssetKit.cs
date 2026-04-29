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
        /// 通过资源包名同步加载资源
        /// </summary>
        public static T LoadAssetSync<T>(string assetName, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            return package.LoadAssetSync<T>(assetName).AssetObject as T;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public static T LoadAssetSync<T>(string assetName) where T : UnityEngine.Object
        {
            return YooAssets.LoadAssetSync<T>(assetName).AssetObject as T;
        }

        /// <summary>
        /// 同步加载子资源
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="assetName"></param>
        /// <param name="subAssetName"></param>
        /// <returns></returns>
        public static T LoadSubAssetSync<T>(string assetName, string subAssetName, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            return package.LoadSubAssetsSync<T>(assetName).GetSubAssetObject<T>(subAssetName);
        }

        /// <summary>
        /// 同步加载子资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="subAssetName"></param>
        /// <returns></returns>
        public static T LoadSubAssetSync<T>(string assetName, string subAssetName) where T : UnityEngine.Object
        {
            return YooAssets.LoadSubAssetsSync<T>(assetName).GetSubAssetObject<T>(subAssetName);
        }

        /// <summary>
        /// 通过包名异步加载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">资源包名</param>
        public static void LoadAssetAsync<T>(string assetName, Action<T> onLoad, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            AssetHandle handle = package.LoadAssetAsync<T>(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.AssetObject as T);
                handle.Release();
            };
        }

        /// <summary>
        /// 异步加载资源（加载默认包资源）
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        public static void LoadAssetAsync<T>(string assetName, Action<T> onLoad) where T : UnityEngine.Object
        {
            AssetHandle handle = YooAssets.LoadAssetAsync<T>(assetName);
            handle.Completed += (assetHandle) => 
            {
                onLoad?.Invoke(assetHandle.AssetObject as T);
                handle.Release();
            };
        }

        /// <summary>
        /// 加载资源（异步转同步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static async UniTask<T> LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
        {
            AssetHandle handle = YooAssets.LoadAssetAsync<T>(assetName);
            await handle.Task;
            T asset = handle.AssetObject as T;
            handle.Release();
            return asset;
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
        public static void LoadAssetsByTagsAsync<T>(
            string[] tags,
            Action<List<T>> onLoad,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            LoadAssetsByTags<T>(package, tags, onLoad).ToAction().StartGlobal();
        }

        /// <summary>
        /// 根据Tag异步加载资源集合。
        /// </summary>
        public static async UniTask<List<T>> LoadAssetsByTagAsync<T>(
            string tag,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            return await LoadAssetsByTagsAsync<T>(new[] { tag }, packageName);
        }

        /// <summary>
        /// 根据多个Tag异步加载资源集合。
        /// </summary>
        public static async UniTask<List<T>> LoadAssetsByTagsAsync<T>(
            string[] tags,
            string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            var normalizedTags = NormalizeTags(tags);
            if (normalizedTags.Length == 0)
            {
                return new List<T>();
            }

            var assetInfos = package.GetAssetInfos(normalizedTags);
            var handles = new List<AssetHandle>(assetInfos.Length);
            var assets = new List<T>(assetInfos.Length);

            foreach (var assetInfo in assetInfos)
            {
                var handle = package.LoadAssetAsync(assetInfo);
                handles.Add(handle);
                await handle.Task;

                if (handle.Status != EOperationStatus.Succeed)
                {
                    Debug.LogError(handle.LastError);
                    continue;
                }

                if (handle.AssetObject is T asset)
                {
                    assets.Add(asset);
                }
                else
                {
                    Debug.LogError($"Asset type cast failed: {assetInfo.Address}");
                }
            }

            foreach (var handle in handles)
            {
                handle.Release();
            }

            return assets;
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
        public static void LoadGameObjectAsync(string assetName, Action<GameObject> onLoad, string packageName = "DefaultPackage")
        {
            var package = GetPackageOrDefault(packageName);
            AssetHandle handle = package.LoadAssetAsync<GameObject>(assetName);
            handle.Completed += (assetHanlde) =>
            {
                onLoad?.Invoke(assetHanlde.AssetObject as GameObject);
                handle.Release();
            };
        }

        /// <summary>
        /// 异步加载预制体（默认包）
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        public static void LoadGameObjectAsync(string assetName, Action<GameObject> onLoad)
        {
            AssetHandle handle = YooAssets.LoadAssetAsync<GameObject>(assetName);
            handle.Completed += (assetHanlde) =>
            {
                onLoad?.Invoke(assetHanlde.AssetObject as GameObject);
                handle.Release();
            };
        }

        /// <summary>
        /// 通过包名异步加载子对象（可用于加载纹理图集等）
        /// </summary>
        /// <typeparam name="T">子对象资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="subAssetName">子对象名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">包名</param>
        public static void LoadSubAssetAsync<T>(string assetName, string subAssetName, Action<T> onLoad, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = GetPackageOrDefault(packageName);
            SubAssetsHandle handle = package.LoadSubAssetsAsync<T>(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.GetSubAssetObject<T>(subAssetName));
                handle.Release();
            };
        }

        /// <summary>
        /// 异步加载子对象（可用于加载纹理图集等）
        /// </summary>
        /// <typeparam name="T">子对象资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <param name="subAssetName">子对象名称</param>
        /// <param name="onLoad">加载完成回调</param>
        public static void LoadSubAssetAsync<T>(string assetName, string subAssetName, Action<T> onLoad) where T : UnityEngine.Object
        {
            SubAssetsHandle handle = YooAssets.LoadSubAssetsAsync<T>(assetName);
            handle.Completed += (assetHandle) =>
            {
                onLoad?.Invoke(assetHandle.GetSubAssetObject<T>(subAssetName));
                handle.Release();
            };
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

        private static IEnumerator LoadAssetsByTags<T>(
            ResourcePackage package,
            string[] tags,
            Action<List<T>> onLoad) where T : UnityEngine.Object
        {
            var normalizedTags = NormalizeTags(tags);
            if (normalizedTags.Length == 0)
            {
                onLoad?.Invoke(new List<T>());
                yield break;
            }

            var assetInfos = package.GetAssetInfos(normalizedTags);
            var handles = new List<AssetHandle>(assetInfos.Length);
            var assets = new List<T>(assetInfos.Length);

            foreach (var assetInfo in assetInfos)
            {
                var handle = package.LoadAssetAsync(assetInfo);
                handles.Add(handle);
            }

            foreach (var handle in handles)
            {
                yield return handle;
                if (handle.Status != EOperationStatus.Succeed)
                {
                    Debug.LogError(handle.LastError);
                    continue;
                }

                if (handle.AssetObject is T asset)
                {
                    assets.Add(asset);
                }
                else
                {
                    Debug.LogError($"Asset type cast failed: {handle.GetAssetInfo().Address}");
                }
            }

            foreach (var handle in handles)
            {
                handle.Release();
            }

            onLoad?.Invoke(assets);
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

            onCompleted?.Invoke(handle);
            handle.Release();
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

            onCompleted?.Invoke(handle);
            handle.Release();
        }

    }
}
