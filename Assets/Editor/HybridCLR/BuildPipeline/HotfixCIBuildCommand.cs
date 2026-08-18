using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    /// <summary>
    /// Unity batchmode 专用热更新资源构建入口。
    /// 使用 -executeMethod HybridCLR.Editor.HotfixCIBuildCommand.Run 调用。
    /// </summary>
    public static class HotfixCIBuildCommand
    {
        private const int SuccessExitCode = 0;
        private const int FailureExitCode = 1;
        private const string DefaultResultPath = "BuildReports/Hotfix/ci-result.json";

        /// <summary>
        /// 解析 -hotfix* 参数、执行非交互构建、写入 JSON，并以 0/1 退出 Unity 进程。
        /// </summary>
        public static void Run()
        {
            if (!Application.isBatchMode)
            {
                throw new InvalidOperationException(
                    "HotfixCIBuildCommand 只能在 Unity -batchmode 下执行，避免意外关闭开发者的 Unity Editor。");
            }

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var result = HotfixCIBuildResult.CreateStarted();
            string resultPath = GetPreliminaryResultPath();
            int exitCode = FailureExitCode;
            HotfixReleaseProfile profileClone = null;

            try
            {
                var arguments = HotfixCIArguments.Parse(Environment.GetCommandLineArgs());
                resultPath = arguments.ResultPath;
                result.Task = arguments.Mode.ToString();
                result.ProfileAssetPath = arguments.ProfileAssetPath;

                var sourceProfile = AssetDatabase.LoadAssetAtPath<HotfixReleaseProfile>(arguments.ProfileAssetPath);
                if (sourceProfile == null)
                {
                    throw new FileNotFoundException(
                        $"CI ReleaseProfile 不存在或类型错误：{arguments.ProfileAssetPath}",
                        arguments.ProfileAssetPath);
                }

                profileClone = UnityEngine.Object.Instantiate(sourceProfile);
                profileClone.name = sourceProfile.name + "_CI";
                profileClone.hideFlags = HideFlags.HideAndDontSave;
                arguments.ApplyOverrides(profileClone);
                ValidateRequest(arguments, profileClone);
                PopulateProfileIdentity(result, profileClone);

                using (HotfixReleaseProfile.PushBuildProfileOverride(profileClone))
                {
                    var report = HotfixBuildRunner.Build(
                        arguments.Mode,
                        new HotfixBuildExecutionOptions
                        {
                            NonInteractive = true,
                            ConfirmAOTMetadataPatch = arguments.ConfirmAOTMetadataPatch
                        });
                    PopulateSuccess(result, report, HotfixBuildRunner.LastExecutionResult);
                }

                result.Success = true;
                result.ExitCode = SuccessExitCode;
                exitCode = SuccessExitCode;
                Debug.Log($"[HotfixCI] 构建成功。Result={resultPath}");
            }
            catch (Exception exception)
            {
                result.Success = false;
                result.ExitCode = FailureExitCode;
                result.ErrorType = exception.GetType().FullName;
                result.ErrorMessage = exception.Message;
                result.ErrorStackTrace = exception.ToString();
                PopulatePartialExecutionResult(result, HotfixBuildRunner.LastExecutionResult);
                Debug.LogException(exception);
            }
            finally
            {
                stopwatch.Stop();
                result.DurationSeconds = stopwatch.Elapsed.TotalSeconds;
                result.CompletedAtUtc = DateTime.UtcNow.ToString("O");
                bool resultWritten = TryWriteResult(resultPath, result);
                if (!resultWritten && exitCode == SuccessExitCode)
                {
                    exitCode = FailureExitCode;
                    result.Success = false;
                    result.ExitCode = FailureExitCode;
                    result.ErrorType = typeof(IOException).FullName;
                    result.ErrorMessage = $"构建成功，但 CI 结果 JSON 无法写入：{resultPath}";
                }
                if (profileClone != null)
                {
                    UnityEngine.Object.DestroyImmediate(profileClone);
                }

                AssetDatabase.SaveAssets();
                EditorApplication.Exit(exitCode);
            }
        }

        private static void ValidateRequest(HotfixCIArguments arguments, HotfixReleaseProfile profile)
        {
            if (!Application.isBatchMode)
            {
                throw new InvalidOperationException("专用 CI 构建入口必须使用 Unity -batchmode 执行。");
            }

            BuildTarget activeTarget = EditorUserBuildSettings.activeBuildTarget;
            if (profile.BuildTarget != activeTarget)
            {
                throw new InvalidOperationException(
                    $"CI BuildTarget 不匹配。Profile={profile.BuildTarget}, Unity={activeTarget}。" +
                    "请通过 Unity -buildTarget 切换平台，并确保 Profile 使用相同平台。");
            }

            if (profile.IsFormalRelease && !arguments.ConfirmProduction)
            {
                throw new InvalidOperationException(
                    "正式发布必须显式传入 -hotfixConfirmProduction true，" +
                    "确认环境、版本、CDN、签名和 ReleaseSequence 已由流水线审核。 ");
            }

            if (arguments.Mode == HotfixBuildMode.AOTMetadataPatch &&
                !arguments.ConfirmAOTMetadataPatch)
            {
                throw new InvalidOperationException(
                    "AOT 元数据补丁必须显式传入 -hotfixConfirmAotPatch true。 ");
            }
        }

        private static void PopulateProfileIdentity(HotfixCIBuildResult result, HotfixReleaseProfile profile)
        {
            result.BuildTarget = profile.BuildTarget.ToString();
            result.AppVersion = profile.AppVersion ?? string.Empty;
            result.AppVersionMin = profile.AppVersionMin ?? string.Empty;
            result.AppVersionMax = profile.AppVersionMax ?? string.Empty;
            result.ResourceVersion = profile.ResourceVersion ?? string.Empty;
            result.ReleaseSequence = profile.ReleaseSequence;
            result.RemoteEnvironment = profile.RemoteEnvironment.ToString();
            result.IsProduction = profile.IsFormalRelease;
            result.SigningKeyId = profile.ManifestSigningKeyId ?? string.Empty;
        }

        private static void PopulateSuccess(
            HotfixCIBuildResult result,
            HotfixBuildReport report,
            HotfixBuildExecutionResult executionResult)
        {
            result.ErrorCount = report == null ? 0 : report.ErrorCount;
            result.WarningCount = report == null ? 0 : report.WarningCount;
            result.InfoCount = report == null ? 0 : report.InfoCount;
            PopulatePartialExecutionResult(result, executionResult);
        }

        private static void PopulatePartialExecutionResult(
            HotfixCIBuildResult result,
            HotfixBuildExecutionResult executionResult)
        {
            if (executionResult == null)
            {
                return;
            }

            result.PackageName = executionResult.PackageName ?? string.Empty;
            result.PackageVersion = executionResult.PackageVersion ?? string.Empty;
            result.OutputPackageDirectory = executionResult.OutputPackageDirectory ?? string.Empty;
            result.CdnUploadDirectory = executionResult.CdnUploadDirectory ?? string.Empty;
            result.RawFilePackageName = executionResult.RawFilePackageName ?? string.Empty;
            result.RawFilePackageVersion = executionResult.RawFilePackageVersion ?? string.Empty;
            result.RawFileOutputPackageDirectory = executionResult.RawFileOutputPackageDirectory ?? string.Empty;
            result.RawFileCdnUploadDirectory = executionResult.RawFileCdnUploadDirectory ?? string.Empty;
            result.AotVersion = executionResult.AotVersion ?? string.Empty;
            result.HotfixVersion = executionResult.HotfixVersion ?? string.Empty;
            result.RequiredAotVersion = executionResult.RequiredAotVersion ?? string.Empty;
            result.PlayerBaselinePath = executionResult.PlayerBaselinePath ?? string.Empty;
            result.PlayerBaselineFingerprint = executionResult.PlayerBaselineFingerprint ?? string.Empty;
            result.ReportPath = executionResult.ReportPath ?? string.Empty;
            result.AotAddedFiles = ToArray(executionResult.AotAddedFiles);
            result.AotChangedFiles = ToArray(executionResult.AotChangedFiles);
            result.AotRemovedFiles = ToArray(executionResult.AotRemovedFiles);
        }

        private static string[] ToArray(List<string> values)
        {
            return values == null ? Array.Empty<string>() : values.ToArray();
        }

        private static string GetPreliminaryResultPath()
        {
            try
            {
                var raw = HotfixCIArguments.ReadRaw(Environment.GetCommandLineArgs());
                return HotfixCIArguments.ResolveResultPath(
                    raw.TryGetValue("hotfixResult", out string value) ? value : DefaultResultPath);
            }
            catch
            {
                return HotfixCIArguments.ResolveResultPath(DefaultResultPath);
            }
        }

        private static bool TryWriteResult(string path, HotfixCIBuildResult result)
        {
            try
            {
                string directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(path, JsonUtility.ToJson(result, true));
                Debug.Log($"[HotfixCI] Result JSON: {path}");
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[HotfixCI] 无法写入结果 JSON：{path}\n{exception}");
                return false;
            }
        }
    }

    /// <summary>专用 CI 命令行参数模型，负责白名单解析、类型转换和内存 Profile 覆盖。</summary>
    internal sealed class HotfixCIArguments
    {
        private const string DefaultResultPath = "BuildReports/Hotfix/ci-result.json";
        private static readonly HashSet<string> SupportedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "hotfixTask",
            "hotfixProfile",
            "hotfixResult",
            "hotfixBuildTarget",
            "hotfixAppVersion",
            "hotfixAppVersionMin",
            "hotfixAppVersionMax",
            "hotfixResourceVersion",
            "hotfixReleaseSequence",
            "hotfixConfirmProduction",
            "hotfixConfirmAotPatch"
        };

        public HotfixBuildMode Mode;
        public string ProfileAssetPath;
        public string ResultPath;
        public BuildTarget? BuildTargetOverride;
        public string AppVersion;
        public string AppVersionMin;
        public string AppVersionMax;
        public string ResourceVersion;
        public long? ReleaseSequence;
        public bool ConfirmProduction;
        public bool ConfirmAOTMetadataPatch;

        /// <summary>解析并严格校验 Unity 命令行中的所有 -hotfix* 参数。</summary>
        public static HotfixCIArguments Parse(string[] commandLine)
        {
            var raw = ReadRaw(commandLine);
            foreach (string key in raw.Keys)
            {
                if (!SupportedKeys.Contains(key))
                {
                    throw new ArgumentException($"未知 CI 参数：-{key}");
                }
            }

            var arguments = new HotfixCIArguments
            {
                Mode = ParseMode(Require(raw, "hotfixTask")),
                ProfileAssetPath = NormalizeAssetPath(Require(raw, "hotfixProfile")),
                ResultPath = ResolveResultPath(Get(raw, "hotfixResult", DefaultResultPath)),
                AppVersion = Get(raw, "hotfixAppVersion"),
                AppVersionMin = Get(raw, "hotfixAppVersionMin"),
                AppVersionMax = Get(raw, "hotfixAppVersionMax"),
                ResourceVersion = Get(raw, "hotfixResourceVersion"),
                ConfirmProduction = ParseBoolean(raw, "hotfixConfirmProduction"),
                ConfirmAOTMetadataPatch = ParseBoolean(raw, "hotfixConfirmAotPatch")
            };

            if (raw.TryGetValue("hotfixReleaseSequence", out string sequenceValue))
            {
                if (!long.TryParse(sequenceValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out long sequence))
                {
                    throw new ArgumentException(
                        $"-hotfixReleaseSequence 必须是整数，当前值：{sequenceValue}");
                }

                arguments.ReleaseSequence = sequence;
            }

            if (raw.TryGetValue("hotfixBuildTarget", out string targetValue))
            {
                if (!Enum.TryParse(targetValue, true, out BuildTarget target) ||
                    !Enum.IsDefined(typeof(BuildTarget), target))
                {
                    throw new ArgumentException(
                        $"-hotfixBuildTarget 不是有效 Unity BuildTarget：{targetValue}");
                }

                arguments.BuildTargetOverride = target;
            }

            return arguments;
        }

        /// <summary>把 CI 版本和平台参数应用到临时 Profile，不修改磁盘资产。</summary>
        public void ApplyOverrides(HotfixReleaseProfile profile)
        {
            if (BuildTargetOverride.HasValue)
            {
                profile.BuildTarget = BuildTargetOverride.Value;
            }

            ApplyIfPresent(AppVersion, value => profile.AppVersion = value);
            ApplyIfPresent(AppVersionMin, value => profile.AppVersionMin = value);
            ApplyIfPresent(AppVersionMax, value => profile.AppVersionMax = value);
            ApplyIfPresent(ResourceVersion, value => profile.ResourceVersion = value);
            if (ReleaseSequence.HasValue)
            {
                profile.ReleaseSequence = ReleaseSequence.Value;
            }
        }

        public static Dictionary<string, string> ReadRaw(string[] commandLine)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < (commandLine == null ? 0 : commandLine.Length); i++)
            {
                string token = commandLine[i];
                if (string.IsNullOrWhiteSpace(token) || !token.StartsWith("-hotfix", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string key;
                string value;
                int equalsIndex = token.IndexOf('=');
                if (equalsIndex > 1)
                {
                    key = token.Substring(1, equalsIndex - 1);
                    value = token.Substring(equalsIndex + 1);
                }
                else
                {
                    key = token.Substring(1);
                    if (i + 1 >= commandLine.Length || commandLine[i + 1].StartsWith("-", StringComparison.Ordinal))
                    {
                        throw new ArgumentException($"CI 参数缺少值：-{key}");
                    }

                    value = commandLine[++i];
                }

                if (result.ContainsKey(key))
                {
                    throw new ArgumentException($"CI 参数重复：-{key}");
                }

                result.Add(key, value == null ? string.Empty : value.Trim());
            }

            return result;
        }

        public static string ResolveResultPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = DefaultResultPath;
            }

            string projectRoot = Directory.GetParent(Application.dataPath)?.FullName ?? Directory.GetCurrentDirectory();
            return Path.GetFullPath(Path.IsPathRooted(path) ? path : Path.Combine(projectRoot, path));
        }

        private static HotfixBuildMode ParseMode(string value)
        {
            switch ((value ?? string.Empty).Trim().ToLowerInvariant())
            {
                case "initial":
                case "initialpackage":
                    return HotfixBuildMode.InitialPackage;
                case "hotfix":
                case "hotfixpackage":
                    return HotfixBuildMode.HotfixPackage;
                case "aot":
                case "aotpatch":
                case "aotmetadatapatch":
                    return HotfixBuildMode.AOTMetadataPatch;
                default:
                    throw new ArgumentException(
                        $"未知 -hotfixTask：{value}。可选值：InitialPackage、HotfixPackage、AOTMetadataPatch。");
            }
        }

        private static string NormalizeAssetPath(string path)
        {
            string normalized = (path ?? string.Empty).Trim().Replace('\\', '/');
            if (!normalized.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase) ||
                !normalized.EndsWith(".asset", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"-hotfixProfile 必须是 Assets 下的 .asset 路径，当前值：{path}");
            }

            return normalized;
        }

        private static string Require(Dictionary<string, string> raw, string key)
        {
            string value = Get(raw, key);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"缺少必填 CI 参数：-{key}");
            }

            return value;
        }

        private static string Get(
            Dictionary<string, string> raw,
            string key,
            string defaultValue = null)
        {
            return raw.TryGetValue(key, out string value) ? value : defaultValue;
        }

        private static bool ParseBoolean(Dictionary<string, string> raw, string key)
        {
            if (!raw.TryGetValue(key, out string value))
            {
                return false;
            }

            if (!bool.TryParse(value, out bool parsed))
            {
                throw new ArgumentException($"-{key} 必须为 true 或 false，当前值：{value}");
            }

            return parsed;
        }

        private static void ApplyIfPresent(string value, Action<string> apply)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                apply(value.Trim());
            }
        }
    }

    /// <summary>CI 成功或失败时写入 JSON 的机器可读构建结果。</summary>
    [Serializable]
    public sealed class HotfixCIBuildResult
    {
        public int SchemaVersion = 1;
        public bool Success;
        public int ExitCode = 1;
        public string StartedAtUtc = string.Empty;
        public string CompletedAtUtc = string.Empty;
        public double DurationSeconds;
        public string UnityVersion = string.Empty;
        public string Task = string.Empty;
        public string ProfileAssetPath = string.Empty;
        public string BuildTarget = string.Empty;
        public string AppVersion = string.Empty;
        public string AppVersionMin = string.Empty;
        public string AppVersionMax = string.Empty;
        public string ResourceVersion = string.Empty;
        public long ReleaseSequence;
        public string RemoteEnvironment = string.Empty;
        public bool IsProduction;
        public string SigningKeyId = string.Empty;
        public string PackageName = string.Empty;
        public string PackageVersion = string.Empty;
        public string OutputPackageDirectory = string.Empty;
        public string CdnUploadDirectory = string.Empty;
        public string RawFilePackageName = string.Empty;
        public string RawFilePackageVersion = string.Empty;
        public string RawFileOutputPackageDirectory = string.Empty;
        public string RawFileCdnUploadDirectory = string.Empty;
        public string AotVersion = string.Empty;
        public string HotfixVersion = string.Empty;
        public string RequiredAotVersion = string.Empty;
        public string PlayerBaselinePath = string.Empty;
        public string PlayerBaselineFingerprint = string.Empty;
        public string ReportPath = string.Empty;
        public int ErrorCount;
        public int WarningCount;
        public int InfoCount;
        public string[] AotAddedFiles = Array.Empty<string>();
        public string[] AotChangedFiles = Array.Empty<string>();
        public string[] AotRemovedFiles = Array.Empty<string>();
        public string ErrorType = string.Empty;
        public string ErrorMessage = string.Empty;
        public string ErrorStackTrace = string.Empty;

        /// <summary>创建带 Unity 版本和 UTC 开始时间的初始结果。</summary>
        public static HotfixCIBuildResult CreateStarted()
        {
            return new HotfixCIBuildResult
            {
                StartedAtUtc = DateTime.UtcNow.ToString("O"),
                UnityVersion = Application.unityVersion
            };
        }
    }
}
