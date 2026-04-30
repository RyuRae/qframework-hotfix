using System;
using HybridCLR.Editor.Commands;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    public static class HybridCLRGenerateAllSafe
    {
        [MenuItem("Build/热更新/内部工具/安全生成 HybridCLR 数据", false, HotfixBuildMenuPriority.InternalGenerateAllSafe)]
        public static void RunMenu()
        {
            Run();
        }

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
