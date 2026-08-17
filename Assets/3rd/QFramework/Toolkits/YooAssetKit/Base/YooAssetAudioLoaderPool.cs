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
            private YooAssetLease<AudioClip> mClipLease;
            private int mLoadVersion;

            public AudioClip LoadClip(AudioSearchKeys audioSearchKeys)
            {
                Unload();
                mClipLease = YooAssetKit.LoadAssetLeaseSync<AudioClip>(audioSearchKeys.AssetName);
                mClip = mClipLease.Asset;

                return mClip;
            }

            public void LoadClipAsync(AudioSearchKeys audioSearchKeys, Action<bool, AudioClip> onLoad)
            {
                Unload();
                int loadVersion = mLoadVersion;
                YooAssetKit.LoadAssetLeaseAsync<AudioClip>(audioSearchKeys.AssetName, lease =>
                {
                    if (loadVersion != mLoadVersion)
                    {
                        lease?.Dispose();
                        return;
                    }

                    mClipLease = lease;
                    mClip = lease == null ? null : lease.Asset;
                    onLoad?.Invoke(mClip != null, mClip);
                });
            }

            public void Unload()
            {
                mLoadVersion++;
                mClipLease?.Dispose();
                mClipLease = null;
                mClip = null;
            }

        }

        protected override IAudioLoader CreateLoader()
        {
            return new YooAssetAudioLoader();
        }
    }
}
