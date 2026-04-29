using System;
using UnityEngine;
using YooAsset;

namespace QFramework
{
    public class AudioKitWithYooAssetInit
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            AudioKit.Config.AudioLoaderPool = new YooAssetAudioLoaderPool();
        }
    }

    public class YooAssetAudioLoaderPool : AbstractAudioLoaderPool
    {

        public class YooAssetAudioLoader : IAudioLoader
        {
            public AudioClip Clip => mClip;
            private AudioClip mClip;
            private ResourcePackage mPackage;
            private string mCurrAudioName;

            public AudioClip LoadClip(AudioSearchKeys audioSearchKeys)
            {
                mPackage = SetCurrResPackage();
                AssetHandle assetHandle = null;
                mCurrAudioName = audioSearchKeys.AssetName;
                assetHandle = mPackage.LoadAssetSync<AudioClip>(mCurrAudioName);
                mClip = assetHandle.AssetObject as AudioClip;

                return mClip;
            }

            public void LoadClipAsync(AudioSearchKeys audioSearchKeys, Action<bool, AudioClip> onLoad)
            {
                mPackage = SetCurrResPackage();
                AssetHandle handle = null;
                mCurrAudioName = audioSearchKeys.AssetName;
                handle = mPackage.LoadAssetSync<AudioClip>(mCurrAudioName);
                handle.Completed += (assetHandle) =>
                {
                    mClip = assetHandle.AssetObject as AudioClip;
                    onLoad(assetHandle.IsDone, mClip);
                };
            }

            //设置当前资源包
            private ResourcePackage SetCurrResPackage()
            {
                var package = YooAssetKit.GetPackageOrDefault();
                return package;
            }

            public void Unload()
            {
                mPackage = SetCurrResPackage();
                mPackage.TryUnloadUnusedAsset(mCurrAudioName);
                mPackage = null;
            }

        }

        protected override IAudioLoader CreateLoader()
        {
            return new YooAssetAudioLoader();
        }
    }
}