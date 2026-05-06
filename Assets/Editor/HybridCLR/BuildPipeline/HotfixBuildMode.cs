namespace HybridCLR.Editor
{
    public enum HotfixBuildMode
    {
        InitialPackage,
        HotfixPackage,
        AOTMetadataPatch
    }

    public static class HotfixBuildModeUtility
    {
        public static string GetDisplayName(HotfixBuildMode mode)
        {
            switch (mode)
            {
                case HotfixBuildMode.InitialPackage:
                    return "首包构建";
                case HotfixBuildMode.HotfixPackage:
                    return "热更包构建";
                case HotfixBuildMode.AOTMetadataPatch:
                    return "AOT 元数据补丁";
                default:
                    return mode.ToString();
            }
        }
    }

    public static class HotfixBuildMenuPriority
    {
        public const int BuildCenter = 100;
        public const int OneClickInitialPackage = 120;
        public const int OneClickHotfixPackage = 121;
        public const int AdvancedAOTMetadataPatch = 140;
        public const int ReleaseProfile = 160;
        public const int InternalGenerateAllSafe = 200;
        public const int InternalValidateRuntimeSettings = 201;
        public const int InternalSyncPackageNames = 202;
        public const int InternalApplyPlayMode = 203;
        public const int InternalYooAssetInitialOnly = 220;
        public const int InternalYooAssetHotfixOnly = 221;
        public const int InternalCopyAOTMetadata = 240;
        public const int InternalCopyHotfixDlls = 241;
        public const int LegacyCommands = 300;
    }
}
