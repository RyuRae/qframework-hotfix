using System;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    public static class HotfixInternalBuildMenu
    {
        [MenuItem("Build/热更新/一键构建/构建首包", false, HotfixBuildMenuPriority.OneClickInitialPackage)]
        public static void BuildInitialPackage()
        {
            HotfixBuildRunner.Build(HotfixBuildMode.InitialPackage);
        }

        [MenuItem("Build/热更新/一键构建/构建热更包", false, HotfixBuildMenuPriority.OneClickHotfixPackage)]
        public static void BuildHotfixPackage()
        {
            HotfixBuildRunner.Build(HotfixBuildMode.HotfixPackage);
        }

        [MenuItem("Build/热更新/高级/构建 AOT 元数据补丁", false, HotfixBuildMenuPriority.AdvancedAOTMetadataPatch)]
        public static void BuildAOTMetadataPatch()
        {
            HotfixBuildRunner.Build(HotfixBuildMode.AOTMetadataPatch);
        }

        [MenuItem("Build/热更新/内部工具/校验运行时设置", false, HotfixBuildMenuPriority.InternalValidateRuntimeSettings)]
        public static void ValidateRuntimeSettings()
        {
            var report = HotfixBuildRunner.ValidateOnly(HotfixBuildMode.InitialPackage);
            if (report.HasErrors)
            {
                throw new InvalidOperationException(report.BuildErrorSummary());
            }

            Debug.Log("[HotfixBuild] 运行时设置校验通过。");
        }
    }
}
