using System;
using System.Threading.Tasks;
using cfg;
using Framework;
using Luban;
using QFramework;
using UnityEngine;

namespace HotfixDemo
{
    /// <summary>
    /// 示例：在进入业务场景前读取并解析 Luban 配置。
    /// </summary>
    public sealed class PreloadConfigCommand : AbstractCommand<Task>
    {
        private const string PersonTableName = "tbperson";
        private readonly HotfixContext _context;
        private readonly IProgress<HotfixPreloadProgress> _progress;

        public PreloadConfigCommand(
            HotfixContext context,
            IProgress<HotfixPreloadProgress> progress)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _progress = progress;
        }

        protected override Task OnExecute()
        {
            return PreloadAsync();
        }

        private async Task PreloadAsync()
        {
            _context.CancellationToken.ThrowIfCancellationRequested();
            _progress?.Report(new HotfixPreloadProgress(0f, "加载游戏配置"));

            using (var lease = await YooAssetKit.LoadAssetLeaseAsync<TextAsset>(
                       PersonTableName,
                       _context.MainPackageName))
            {
                _context.CancellationToken.ThrowIfCancellationRequested();

                // 在租约释放前完成解析。Tables 只保留解析后的托管对象，不依赖 TextAsset Handle。
                var tables = new Tables(tableName =>
                {
                    if (!string.Equals(tableName, PersonTableName, StringComparison.Ordinal))
                    {
                        throw new InvalidOperationException($"Configuration table was not preloaded: {tableName}");
                    }

                    return new ByteBuf(lease.Asset.bytes);
                });
                GameConfig.SetTables(tables);
            }

            _context.CancellationToken.ThrowIfCancellationRequested();
            _progress?.Report(new HotfixPreloadProgress(1f, "游戏配置加载完成"));
            LogKit.I("Luban tables preloaded: tbperson");
        }
    }
}
