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
        Task StartAsync(HotfixContext context);
    }

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
