using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace QFramework
{

    public class UIKitWithYooAssetInit
    {
        /// <summary>
        /// 方法在场景加载之前执行，设置PanelLoaderPool指向当前加载器
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        { 
            UIKit.Config.PanelLoaderPool = new YooAssetPanelLoaderPool();
        }
    }

    /// <summary>
    /// 自定义YooAsset加载方案（默认继续使用UIKit工具）
    /// </summary>
    public class YooAssetPanelLoaderPool : AbstractPanelLoaderPool
    {
        public class YooAssetPanelLoader : IPanelLoader
        {
            private YooAssetLease<GameObject> mPrefabLease;
            private int mLoadVersion;

            public GameObject LoadPanelPrefab(PanelSearchKeys panelSearchKeys)
            {
                Unload();
                string assetName = ResolveAssetName(panelSearchKeys);
                mPrefabLease = YooAssetKit.LoadAssetLeaseSync<GameObject>(assetName);
                return mPrefabLease.Asset;
            }

            public void LoadPanelPrefabAsync(PanelSearchKeys panelSearchKeys, Action<GameObject> onLoad)
            {
                Unload();
                string assetName = ResolveAssetName(panelSearchKeys);
                int loadVersion = mLoadVersion;
                YooAssetKit.LoadAssetLeaseAsync<GameObject>(assetName, lease =>
                {
                    if (loadVersion != mLoadVersion)
                    {
                        lease?.Dispose();
                        return;
                    }

                    mPrefabLease = lease;
                    onLoad?.Invoke(lease == null ? null : lease.Asset);
                });
            }

            private static string ResolveAssetName(PanelSearchKeys panelSearchKeys)
            {
                return panelSearchKeys.PanelType.IsNotNull() && panelSearchKeys.GameObjName.IsNullOrEmpty()
                    ? panelSearchKeys.PanelType.Name
                    : panelSearchKeys.GameObjName;
            }

            public void Unload()
            {
                mLoadVersion++;
                mPrefabLease?.Dispose();
                mPrefabLease = null;
            }
        }

        protected override IPanelLoader CreatePanelLoader()
        {
            return new YooAssetPanelLoader();
        }

       
    }
}
