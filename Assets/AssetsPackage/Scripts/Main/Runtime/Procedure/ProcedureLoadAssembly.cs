using MsbFramework.Procedure;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using YooAsset;
using System.Linq;
using Newtonsoft.Json;
using MsbFramework.Assemblies;
using System.IO;
using System;
using Main.Editor;
using System.Reflection;
#if ENABLE_HYBRID_CLR_UNITY
using HybridCLR;
#endif

namespace MsbFramework.Procedure
{
    /// <summary>
    /// 加载热更代码资源
    /// </summary>
    public class ProcedureLoadAssembly : AbstractState<ResPackageStates, ProcedureManager>
    {
       
        //资产配置文件
        private string location = "Assets/AssetsPackage/AssetsHotFix/Configs/YooAssetHybridCLRSetting.asset";

        private Dictionary<string, Assembly> currLoadedAssembliesCache = new Dictionary<string, Assembly>();

        private List<Assembly> currLoadedAssembliesList = new List<Assembly>();

        private List<Assembly> mHotfixAssemblies;

        private bool mIsHotfixAsmLoadComplete = false;
        private bool mIsAotMetaAsmLoadComplete = false;
        public ProcedureLoadAssembly(FSM<ResPackageStates> fsm, ProcedureManager manager) : base(fsm, manager)
        {
        }

        protected override bool OnCondition()
        {

            return mFSM.CurrentStateId == ResPackageStates.CreateDownloader || mFSM.CurrentStateId == ResPackageStates.DownloadPackageOver;
        }

        protected override void OnEnter()
        {
            LogKit.I("加载代码文件");
            currLoadedAssembliesList = AppDomain.CurrentDomain.GetAssemblies().ToList();
            if (currLoadedAssembliesList != null)
            {
                currLoadedAssembliesList.ForEach(p => 
                {
                    string dllName = p.GetName().Name;
                    if (!currLoadedAssembliesCache.ContainsKey(dllName))
                    {
                        currLoadedAssembliesCache.Add(dllName, p);
                    }
                });
            }
            mIsHotfixAsmLoadComplete = false;
            mIsAotMetaAsmLoadComplete = false;
            mHotfixAssemblies = new List<Assembly>();
            CoroutineController.manager.StartCoroutine(LoadAssemblies());
        }

        //hotfix记录文件
        private List<string> mHotfixAssemblyNames = new List<string>();
        //aot元数据记录文件
        private List<string> mAotMetaAssemblies = new List<string>();

        private List<string> mAssetsCache = new List<string>();
        IEnumerator LoadAssemblies()
        {
            var packageName = mTarget._packageName;
            var package = YooAssets.GetPackage(packageName);
            //加载资产配置文件
            AssetHandle assetHandle = package.LoadAssetAsync(location);
            yield return assetHandle;
            YooAssetHybridCLRSetting yooAssetHybridCLRSetting = assetHandle.AssetObject as YooAssetHybridCLRSetting;
            mAotMetaAssemblies = yooAssetHybridCLRSetting.AOTMetaAssemblies;
            mHotfixAssemblyNames = yooAssetHybridCLRSetting.HotfixAssemblies;
            mAssetsCache.AddRange(mHotfixAssemblyNames);
            mAssetsCache.AddRange(mAotMetaAssemblies);

            AssetHandle tempHandle = null;
            var asset = mAssetsCache.GetEnumerator();
            while (asset.MoveNext())
            {
                tempHandle = package.LoadAssetAsync<TextAsset>(asset.Current);
                yield return tempHandle;
                var assetObj = tempHandle.AssetObject as TextAsset;
                mAssetDatas[asset.Current] = assetObj;
            }

            LoadMetadataForAOTAssemblies();
            LoadHotfixAssemblies();

        }

        private static Dictionary<string, TextAsset> mAssetDatas = new Dictionary<string, TextAsset>();

        public static byte[] ReadBytesFromStreamingAssets(string dllName)
        {
            if (mAssetDatas.ContainsKey(dllName))
            {
                return mAssetDatas[dllName].bytes;
            }

            return Array.Empty<byte>();
        }

        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// </summary>
        private void LoadMetadataForAOTAssemblies()
        {
            /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
            /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
#if ENABLE_HYBRID_CLR_UNITY
            HomologousImageMode mode = HomologousImageMode.SuperSet;
            foreach (var aotDllName in mAotMetaAssemblies)
            {
                byte[] dllBytes = ReadBytesFromStreamingAssets(aotDllName);
                // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
                LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
                Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
            }
#endif
            mIsAotMetaAsmLoadComplete = true;
        }

        /// <summary>
        /// 加载热更代码
        /// </summary>
        private void LoadHotfixAssemblies()
        {
            var hotfixAssemblies = mHotfixAssemblyNames.GetEnumerator();
            while (hotfixAssemblies.MoveNext())
            {
                Assembly asm = null;
#if !UNITY_EDITOR
                 if (currLoadedAssembliesCache.ContainsKey(hotfixAssemblies.Current))
                    asm = currLoadedAssembliesCache[hotfixAssemblies.Current];
                else
                    asm = Assembly.Load(ReadBytesFromStreamingAssets($"{hotfixAssemblies.Current}"));
                Debug.Log($"LoadHotfixAssembly:{asm.GetName().Name}. ");
#else
                string tempHotfixAssemblyName = hotfixAssemblies.Current.Replace(".dll", "");
                asm = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == $"{tempHotfixAssemblyName}");
#endif
                mHotfixAssemblies.Add(asm);
                mIsHotfixAsmLoadComplete = true;
            }

        }


        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {
            if (!mIsAotMetaAsmLoadComplete || !mIsHotfixAsmLoadComplete) return;

            AllAsmLoadComplete();
        }

        private void AllAsmLoadComplete()
        {
            LogKit.I("所有代码加载完成！！！");
            mFSM.ChangeState(ResPackageStates.ClearCacheBundle);
        }

        
    }
}
