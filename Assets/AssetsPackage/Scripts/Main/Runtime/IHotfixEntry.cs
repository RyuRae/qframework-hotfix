using System;
using System.Threading;
using System.Threading.Tasks;
using YooAsset;

namespace Framework
{
    /// <summary>
    /// 热更业务启动契约。Task 成功完成表示业务已达到可运行状态。
    /// </summary>
    public interface IHotfixEntry
    {
        /// <summary>
        /// 启动热更业务；返回的 Task 成功结束后，框架才会提交 LastGood 并认为本次启动成功。
        /// </summary>
        Task StartAsync(HotfixContext context);
    }

    /// <summary>
    /// 可选的热更资源预加载契约。Task 成功完成表示业务启动所需的配置和关键资源已准备就绪。
    /// </summary>
    public interface IHotfixResourcePreloader
    {
        /// <summary>
        /// 在业务启动前预加载配置和关键资源，并通过 progress 上报启动进度。
        /// </summary>
        Task PreloadAsync(
            HotfixContext context,
            IProgress<HotfixPreloadProgress> progress);
    }

    /// <summary>
    /// 热更资源预加载进度。Progress 取值范围为 0 到 1。
    /// </summary>
    public readonly struct HotfixPreloadProgress
    {
        public HotfixPreloadProgress(float progress, string description = null)
        {
            if (float.IsNaN(progress) || float.IsNegativeInfinity(progress))
            {
                Progress = 0f;
            }
            else if (float.IsPositiveInfinity(progress))
            {
                Progress = 1f;
            }
            else
            {
                Progress = Math.Max(0f, Math.Min(1f, progress));
            }

            Description = description ?? string.Empty;
        }

        public float Progress { get; }
        public string Description { get; }
    }

    /// <summary>
    /// 主工程传递给热更入口的只读启动上下文，包含资源包、版本身份、降级状态和取消令牌。
    /// </summary>
    public sealed class HotfixContext
    {
        public HotfixContext(
            ResourcePackage mainPackage,
            ResourcePackage rawFilePackage,
            string mainPackageVersion,
            string rawFilePackageVersion,
            string hotfixVersion,
            string aotVersion,
            bool isUsingLocalManifestFallback,
            CancellationToken cancellationToken)
        {
            MainPackage = mainPackage ?? throw new ArgumentNullException(nameof(mainPackage));
            RawFilePackage = rawFilePackage;
            MainPackageVersion = mainPackageVersion ?? string.Empty;
            RawFilePackageVersion = rawFilePackageVersion ?? string.Empty;
            HotfixVersion = hotfixVersion ?? string.Empty;
            AotVersion = aotVersion ?? string.Empty;
            IsUsingLocalManifestFallback = isUsingLocalManifestFallback;
            CancellationToken = cancellationToken;
        }

        public ResourcePackage MainPackage { get; }
        public ResourcePackage RawFilePackage { get; }
        public string MainPackageName => MainPackage == null ? string.Empty : MainPackage.PackageName;
        public string RawFilePackageName => RawFilePackage == null ? string.Empty : RawFilePackage.PackageName;
        public string MainPackageVersion { get; }
        public string RawFilePackageVersion { get; }
        public string HotfixVersion { get; }
        public string AotVersion { get; }
        public bool IsUsingLocalManifestFallback { get; }
        public CancellationToken CancellationToken { get; }
    }
}
