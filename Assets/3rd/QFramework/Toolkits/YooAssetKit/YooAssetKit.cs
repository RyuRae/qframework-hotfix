using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

namespace QFramework
{
    /// <summary>
    /// YooAsset资源加载系统
    /// </summary>
    public class YooAssetKit
    {
        /// <summary>
        /// 设置默认包
        /// </summary>
        /// <param name="packageName"></param>
        public static void SetDefaultPackage(string packageName = "DefaultPackage")
        {
            var package = YooAssets.GetPackage(packageName);
            YooAssets.SetDefaultPackage(package);
        }

        /// <summary>
        /// 通过资源包名同步加载资源
        /// </summary>
        public static T LoadAssetSync<T>(string assetName, string packageName = "DefaultPackage") where T : UnityEngine.Object
        {
            var package = YooAssets.GetPackage(packageName);
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
            var package = YooAssets.GetPackage(packageName);
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
            var package = YooAssets.GetPackage(packageName);
            AssetHandle handle = package.LoadAssetAsync<T>(assetName);
            handle.Completed += (assetHandle) => 
            {
                onLoad?.Invoke(assetHandle.AssetObject as T);
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
            };
        }

        /// <summary>
        /// 通过包名异步加载预制体
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="onLoad">加载完成回调</param>
        /// <param name="packageName">包名</param>
        public static void LoadGameObjectAsync(string assetName, Action<GameObject> onLoad, string packageName = "DefaultPackage")
        {
            var package = YooAssets.GetPackage(packageName);
            AssetHandle handle = package.LoadAssetAsync<GameObject>(assetName);
            handle.Completed += (assetHanlde) => 
            {
                onLoad?.Invoke(assetHanlde.InstantiateSync());
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
                onLoad?.Invoke(assetHanlde.InstantiateSync());
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
            var package = YooAssets.GetPackage(packageName);
            SubAssetsHandle handle = package.LoadSubAssetsAsync<T>(assetName);
            handle.Completed += (assetHandle) => 
            {
                onLoad?.Invoke(assetHandle.GetSubAssetObject<T>(subAssetName));
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
            var package = YooAssets.GetPackage(packageName);
            RawFileHandle handle = package.LoadRawFileAsync(assetName);
            handle.Completed += (assetHandle) => 
            {
                onLoad?.Invoke(assetHandle.GetRawFileData());
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
            var package = YooAssets.GetPackage(packageName);
            RawFileHandle handle = package.LoadRawFileAsync(assetName);
            handle.Completed += (assetHandle) => 
            {
                onLoad?.Invoke(assetHandle.GetRawFileText());
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
            var package = YooAssets.GetPackage(packageName);
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
            var package = YooAssets.GetPackage(packageName);
            UnloadUnusedAssets(package).ToAction().StartGlobal();
        }

        /// <summary>
        /// 尝试卸载指定的资源对象（资源在使用中，该方法无效）
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="packageName">包名</param>
        public static void TryUnloadUnusedAsset(string assetName, string packageName = "DefaultPackage")
        {
            var package = YooAssets.GetPackage(packageName);
            package.TryUnloadUnusedAsset(assetName);
        }

        /// <summary>
        /// 强制卸载所有资源包（请在合适时机调用）
        /// </summary>
        public static void ForceUnloadAllAssets(string packageName = "DefaultPackage")
        {
            var package = YooAssets.GetPackage(packageName);
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
                    targetBar = (int)handle.Progress * 100;
                    while (displayBar < targetBar)
                    {
                        ++displayBar;
                        currProgress = displayBar / 100.0f;
                        onUpdateProgress.Invoke(currProgress);
                        yield return new WaitForEndOfFrame();
                    }
                    yield return null;
                }
                targetBar = 100;
                while (displayBar < targetBar)
                {
                    ++displayBar;
                    currProgress = displayBar / 100.0f;
                    onUpdateProgress.Invoke(currProgress);
                    yield return new WaitForEndOfFrame();
                }
            }
            yield return handle;
            onCompleted?.Invoke(handle);
        }


        private static IEnumerator LoadScene(string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single, LocalPhysicsMode physicsMode = LocalPhysicsMode.None, bool suspendLoad = true, Action<float> onUpdateProgress = null, Action<SceneHandle> onCompleted = null)
        {
            int displayBar = 0;
            int targetBar;
            float currProgress;
            SceneHandle handle = YooAssets.LoadSceneAsync(scene, loadSceneMode, physicsMode, suspendLoad);
            //场景流畅加载
            if (!handle.IsDone)
            {
                while (handle.Progress < 0.9f)
                {
                    targetBar = (int)handle.Progress * 100;
                    while (displayBar < targetBar)
                    {
                        ++displayBar;
                        currProgress = displayBar / 100.0f;
                        onUpdateProgress.Invoke(currProgress);
                        yield return new WaitForEndOfFrame();
                    }
                    yield return null;
                }
                targetBar = 100;
                while (displayBar < targetBar)
                {
                    ++displayBar;
                    currProgress = displayBar / 100.0f;
                    onUpdateProgress.Invoke(currProgress);
                    yield return new WaitForEndOfFrame();
                }
            }
            yield return handle;
            onCompleted?.Invoke(handle);
        }
    }
}