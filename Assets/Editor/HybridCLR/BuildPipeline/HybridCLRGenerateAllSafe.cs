using System;
using HybridCLR.Editor.Commands;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    /// <summary>
    /// HybridCLR Generate All 安全包装器，只在临时 Player 生成阶段跳过远端发布校验，并确保 finally 恢复。
    /// </summary>
    public static class HybridCLRGenerateAllSafe
    {
        [MenuItem("Build/热更新/内部工具/安全生成 HybridCLR 数据", false, HotfixBuildMenuPriority.InternalGenerateAllSafe)]
        public static void RunMenu()
        {
            Run();
        }

        /// <summary>执行 HybridCLR 全量生成，保留异常并恢复构建前置校验开关。</summary>
        public static void Run()
        {
            try
            {
                HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild = true;
                PrebuildCommand.GenerateAll();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                throw;
            }
            finally
            {
                HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild = false;
            }
        }
    }
}
