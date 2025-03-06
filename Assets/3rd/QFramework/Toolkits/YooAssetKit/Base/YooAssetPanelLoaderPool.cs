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
            private ResourcePackage mPackage;
            private string mCurrOpenPanelName;

            public GameObject LoadPanelPrefab(PanelSearchKeys panelSearchKeys)
            {
                mPackage = SetCurrResPackage();
                AssetHandle assetHandle = null;
                if (panelSearchKeys.PanelType.IsNotNull() && panelSearchKeys.GameObjName.IsNullOrEmpty())
                {
                    mCurrOpenPanelName = panelSearchKeys.PanelType.Name;
                    assetHandle = mPackage.LoadAssetSync<GameObject>(mCurrOpenPanelName);
                    return assetHandle.InstantiateSync();
                }

                mCurrOpenPanelName = panelSearchKeys.GameObjName;
                assetHandle = mPackage.LoadAssetSync<GameObject>(mCurrOpenPanelName);
                return assetHandle.InstantiateSync();
            }

            public void LoadPanelPrefabAsync(PanelSearchKeys panelSearchKeys, Action<GameObject> onLoad)
            {
                mPackage = SetCurrResPackage();
                AssetHandle handle = null;
                if (panelSearchKeys.PanelType.IsNotNull() && panelSearchKeys.GameObjName.IsNullOrEmpty())
                {
                    mCurrOpenPanelName = panelSearchKeys.PanelType.Name;
                    handle = mPackage.LoadAssetSync<GameObject>(mCurrOpenPanelName);
                    handle.Completed += (assetHandle) =>
                    {
                        onLoad(assetHandle.InstantiateSync());
                    };
                    return;
                }

                mCurrOpenPanelName = panelSearchKeys.GameObjName;
                //可以是fullName（路径全拼）或者物体名称
                handle = mPackage.LoadAssetSync<GameObject>(mCurrOpenPanelName);
                handle.Completed += (assetHandle) =>
                {
                    onLoad(assetHandle.InstantiateSync());
                };
            }

            //设置当前资源包
            private ResourcePackage SetCurrResPackage()
            {
                var package = YooAssets.GetPackage("DefaultPackage");//需要获取当前包名，这里暂时用默认包名
                return package;
            }

            public void Unload()
            {
                mPackage = SetCurrResPackage();
                mPackage.TryUnloadUnusedAsset(mCurrOpenPanelName);
                mPackage = null;
            }
        }

        protected override IPanelLoader CreatePanelLoader()
        {
            return new YooAssetPanelLoader();
        }

       
    }
}