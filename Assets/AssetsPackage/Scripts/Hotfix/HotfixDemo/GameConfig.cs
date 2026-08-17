using System;
using cfg;

namespace HotfixDemo
{
    /// <summary>
    /// 示例业务配置入口。业务代码只能在预加载完成后读取 Tables。
    /// </summary>
    public static class GameConfig
    {
        private static Tables _tables;

        public static bool IsReady => _tables != null;

        public static Tables Tables => _tables ??
            throw new InvalidOperationException("Game configuration has not been preloaded.");

        public static void SetTables(Tables tables)
        {
            _tables = tables ?? throw new ArgumentNullException(nameof(tables));
        }
    }
}
