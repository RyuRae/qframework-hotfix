using System;
using UnityEngine;
using YooAsset;

namespace QFramework
{
    /// <summary>
    /// YooAsset 资源租约。租约存活期间底层 Handle 保持引用计数；资源使用方负责 Dispose。
    /// 不要只保存 Asset 而丢弃租约，否则资源生命周期将再次变得不可追踪。
    /// </summary>
    public sealed class YooAssetLease<T> : IDisposable where T : UnityEngine.Object
    {
        private HandleBase mHandle;
        private T mAsset;

        internal YooAssetLease(T asset, HandleBase handle, string assetName, string packageName)
        {
            mAsset = asset;
            mHandle = handle ?? throw new ArgumentNullException(nameof(handle));
            AssetName = assetName ?? string.Empty;
            PackageName = packageName ?? string.Empty;
        }

        public string AssetName { get; }
        public string PackageName { get; }
        public bool IsDisposed => mHandle == null;
        public bool IsValid => !IsDisposed && mHandle.IsValid && mAsset != null;

        /// <summary>
        /// 租约释放后返回 null，尽早暴露错误的生命周期使用。
        /// </summary>
        public T Asset => IsDisposed ? null : mAsset;

        public void Dispose()
        {
            var handle = mHandle;
            if (handle == null)
            {
                return;
            }

            mHandle = null;
            mAsset = null;
            handle.Release();
        }

        public override string ToString()
        {
            return $"{nameof(YooAssetLease<T>)}({PackageName}:{AssetName}, Valid={IsValid})";
        }
    }

    /// <summary>
    /// 一组资源租约。Dispose 会释放集合内所有 YooAsset Handle。
    /// </summary>
    public sealed class YooAssetLeaseCollection<T> : IDisposable where T : UnityEngine.Object
    {
        private System.Collections.Generic.List<YooAssetLease<T>> mLeases;

        internal YooAssetLeaseCollection(System.Collections.Generic.List<YooAssetLease<T>> leases)
        {
            mLeases = leases ?? new System.Collections.Generic.List<YooAssetLease<T>>();
        }

        public bool IsDisposed => mLeases == null;
        public int Count => IsDisposed ? 0 : mLeases.Count;

        public System.Collections.Generic.IReadOnlyList<YooAssetLease<T>> Leases =>
            IsDisposed ? Array.Empty<YooAssetLease<T>>() : mLeases;

        public System.Collections.Generic.List<T> GetAssets()
        {
            var assets = new System.Collections.Generic.List<T>(Count);
            if (mLeases == null)
            {
                return assets;
            }

            foreach (var lease in mLeases)
            {
                if (lease != null && lease.Asset != null)
                {
                    assets.Add(lease.Asset);
                }
            }

            return assets;
        }

        public void Dispose()
        {
            var leases = mLeases;
            if (leases == null)
            {
                return;
            }

            mLeases = null;
            foreach (var lease in leases)
            {
                lease?.Dispose();
            }
        }

        internal System.Collections.Generic.List<YooAssetLease<T>> TransferOwnership()
        {
            var leases = mLeases;
            mLeases = null;
            return leases ?? new System.Collections.Generic.List<YooAssetLease<T>>();
        }
    }
}
