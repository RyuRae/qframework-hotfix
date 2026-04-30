using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework
{
    /// <summary>
    /// 热更新框架通用工具方法，消除各模块间的重复逻辑。
    /// </summary>
    public static class HotfixUtility
    {
        /// <summary>
        /// 默认入口场景地址，统一管理避免多文件重复定义。
        /// </summary>
        public const string DefaultEntrySceneAddress = "main";

        /// <summary>
        /// 规范化包名：空或空白时返回 fallback，否则去除首尾空白。
        /// </summary>
        public static string NormalizePackageName(string packageName, string fallback)
        {
            return string.IsNullOrWhiteSpace(packageName) ? fallback : packageName.Trim();
        }

        /// <summary>
        /// 规范化标签数组：去除空白项、trim、去重（不区分大小写）。
        /// </summary>
        public static string[] NormalizeTags(string[] tags)
        {
            if (tags == null || tags.Length == 0)
            {
                return Array.Empty<string>();
            }

            var results = new List<string>();
            var exists = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                {
                    continue;
                }

                var normalizedTag = tag.Trim();
                if (exists.Add(normalizedTag))
                {
                    results.Add(normalizedTag);
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// 规范化程序集名称列表：去除空白项、trim、确保 .dll 后缀、去重（不区分大小写）。
        /// </summary>
        public static List<string> NormalizeAssemblyNames(IEnumerable<string> assemblyNames)
        {
            if (assemblyNames == null)
            {
                return new List<string>();
            }

            return assemblyNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.Trim())
                .Select(name => name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) ? name : $"{name}.dll")
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// 获取当前运行时平台名称，统一各模块的平台判断逻辑。
        /// </summary>
        public static string GetRuntimePlatformName()
        {
#if UNITY_EDITOR
            return ConvertEditorBuildTarget(EditorUserBuildSettings.activeBuildTarget);
#elif UNITY_ANDROID
            return "Android";
#elif UNITY_IOS
            return "iOS";
#elif UNITY_WEBGL
            return "WebGL";
#elif UNITY_STANDALONE_WIN
            return "Windows";
#elif UNITY_STANDALONE_OSX
            return "macOS";
#elif UNITY_STANDALONE_LINUX
            return "Linux";
#else
            return Application.platform.ToString();
#endif
        }

        /// <summary>
        /// 判断当前运行时平台是否与 manifest 中的 BuildTarget 兼容。
        /// </summary>
        public static bool IsBuildTargetCompatible(string manifestBuildTarget)
        {
            return string.IsNullOrWhiteSpace(manifestBuildTarget) ||
                   string.Equals(manifestBuildTarget.Trim(), GetRuntimePlatformName(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 从命令行参数中获取 --key=value 格式的值。
        /// </summary>
        public static string GetCommandLineValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            string normalizedKey = $"--{key.Trim()}=";
            var args = Environment.GetCommandLineArgs();
            foreach (var arg in args)
            {
                if (arg.StartsWith(normalizedKey, StringComparison.OrdinalIgnoreCase))
                {
                    return arg.Substring(normalizedKey.Length).Trim();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 添加版本候选项到列表，自动去重和 trim。
        /// </summary>
        public static void AddVersionCandidate(List<string> candidates, string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                return;
            }

            var trimmed = version.Trim();
            if (!candidates.Contains(trimmed))
            {
                candidates.Add(trimmed);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// 将 Unity BuildTarget 转换为平台名称字符串。
        /// </summary>
        public static string ConvertEditorBuildTarget(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.WebGL:
                    return "WebGL";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.StandaloneOSX:
                    return "macOS";
                case BuildTarget.StandaloneLinux64:
                    return "Linux";
                default:
                    return target.ToString();
            }
        }

        /// <summary>
        /// 获取指定 BuildTarget 对应的平台名称。
        /// </summary>
        public static string GetPlatformNameForBuildTarget(BuildTarget target)
        {
            return ConvertEditorBuildTarget(target);
        }
#endif
    }
}
