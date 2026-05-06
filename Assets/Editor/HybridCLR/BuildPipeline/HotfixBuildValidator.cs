using System;
using Framework;
using UnityEditor;
using YooAsset;

namespace HybridCLR.Editor
{
    public static class HotfixBuildValidator
    {
        public static HotfixBuildReport Validate(HotfixBuildContext context)
        {
            var report = new HotfixBuildReport();
            AddBuildIdentity(context, report);
            AddRuntimeSettings(context, report);
            AddRemoteSettings(context, report);
            AddEntryResource(context, report);
            AddManifestStatus(context, report);
            return report;
        }

        private static void AddBuildIdentity(HotfixBuildContext context, HotfixBuildReport report)
        {
            report.AddInfo("构建模式", HotfixBuildModeUtility.GetDisplayName(context.Mode));

            if ((int)context.BuildTarget < 0)
            {
                report.AddError("构建目标", context.BuildTarget.ToString(), "当前未选择有效构建目标。");
            }
            else
            {
                report.AddInfo("构建目标", $"{context.BuildTarget} / {context.BuildTargetName}");
            }

            if (string.IsNullOrWhiteSpace(context.AppVersion))
            {
                report.AddError("App 版本", "缺失", "构建热更新包前必须设置 PlayerSettings.bundleVersion。");
            }
            else
            {
                report.AddInfo("App 版本", context.AppVersion);
            }
        }

        private static void AddRuntimeSettings(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.RuntimeSettings == null)
            {
                report.AddError(
                    "运行时设置",
                    "缺失",
                    $"缺少资源：{HotfixBuildProfileUtility.RuntimeSettingsAssetPath}。可使用“一键修复”创建并同步。");
                return;
            }

            try
            {
                HotfixBuildProfileUtility.ValidatePlayerPlayMode(context.PlayerPlayMode, context.BuildTarget);
                ValidateStartupPackageMode(context);
                BuildAssetsCommand.ValidateStartupDownloadTags(context.RuntimeSettings);
                report.AddInfo("Player 运行模式", context.PlayerPlayMode.ToString());
            }
            catch (Exception exception)
            {
                report.AddError("启动设置", context.PlayerPlayMode.ToString(), exception.Message);
            }

            report.AddInfo("启动包策略", context.StartupPackageMode.ToString());
            report.AddInfo("启动下载模式", context.StartupDownloadMode.ToString());
            if (context.StartupDownloadMode == StartupDownloadMode.DownloadByTags)
            {
                report.AddInfo("启动下载 Tags", FormatTags(context.StartupDownloadTags));
            }

            report.AddInfo("主包", context.MainPackageName);
            if (context.IncludeRawFilePackage)
            {
                report.AddInfo("RawFile 包", context.RawFilePackageName);
            }

            report.AddInfo(
                "StreamingAssets 复制",
                context.ShouldCopyInitialPackageToStreamingAssets ? "开启" : "关闭");
        }

        private static void AddRemoteSettings(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.RemoteSettings == null)
            {
                report.AddError(
                    "远端设置",
                    "缺失",
                    $"缺少资源：{HotfixBuildProfileUtility.RemoteSettingsAssetPath}。");
                return;
            }

            report.AddInfo(
                "远端环境",
                $"{context.RemoteEnvironmentName} / {context.RemoteChannel} / {context.RemoteRegion}");

            if (!context.RemoteSettings.TryValidateForPlayerBuild(true, context.BuildTargetName, out var error))
            {
                report.AddError("远端设置", context.RemoteEnvironmentName, error);
            }
            else
            {
                report.AddInfo("远端设置", "有效");
            }
        }

        private static void AddEntryResource(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.HotfixManifest == null)
            {
                report.AddWarning(
                    "入口资源",
                    BuildAssetsCommand.DefaultEntrySceneAddress,
                    "Hotfix 清单缺失。首包构建会使用默认入口场景创建清单。");
                return;
            }

            string entry = !string.IsNullOrWhiteSpace(context.EntrySceneAddress)
                ? $"场景：{context.EntrySceneAddress}"
                : $"Prefab：{context.EntryPrefabAddress}";
            if (!string.IsNullOrWhiteSpace(context.EntryMethod))
            {
                entry += $" / 入口方法：{context.EntryMethod}";
            }

            report.AddInfo("入口资源", entry);
        }

        private static void AddManifestStatus(HotfixBuildContext context, HotfixBuildReport report)
        {
            AddAOTManifestStatus(context, report);
            AddHotfixManifestStatus(context, report);

            if (context.AOTManifest == null || context.HotfixManifest == null)
            {
                return;
            }

            try
            {
                BuildAssetsCommand.ValidateSplitAssemblyManifestsForBuild(context.BuildTarget);
                report.AddInfo("清单兼容性", "有效");
            }
            catch (Exception exception)
            {
                report.AddWarning(
                    "清单兼容性",
                    "构建时将重新生成",
                    exception.Message);
                return;
            }

            try
            {
                BuildAssetsCommand.ValidateStartupPackageForBuild(context.BuildTarget);
                report.AddInfo("启动资源校验", "有效");
            }
            catch (Exception exception)
            {
                report.AddError("启动资源校验", "失败", exception.Message);
            }
        }

        private static void AddAOTManifestStatus(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.AOTManifest == null)
            {
                if (context.Mode == HotfixBuildMode.HotfixPackage)
                {
                    report.AddError(
                        "AOT 清单",
                        "缺失",
                        $"热更包构建需要已有 AOT 清单：{BuildAssetsCommand.AOTAssemblyManifestAssetPath}。");
                }
                else
                {
                    report.AddWarning(
                        "AOT 清单",
                        "缺失",
                        "首包构建或 AOT 元数据补丁会重新生成它。");
                }

                return;
            }

            string status = $"{context.AOTManifest.AotVersion} / {context.AOTManifest.BuildTarget}";
            if (!string.Equals(context.AOTManifest.BuildTarget, context.BuildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                if (context.Mode == HotfixBuildMode.HotfixPackage)
                {
                    report.AddError("AOT 清单", status, "构建目标不匹配。");
                }
                else
                {
                    report.AddWarning("AOT 清单", status, "构建目标不匹配。当前构建模式会重新生成它。");
                }
            }
            else
            {
                report.AddInfo("AOT 清单", status);
            }
        }

        private static void AddHotfixManifestStatus(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.HotfixManifest == null)
            {
                report.AddWarning(
                    "Hotfix 清单",
                    "缺失",
                    "构建会基于当前热更程序集设置创建它。");
                return;
            }

            string status = $"{context.HotfixManifest.HotfixVersion} / RequiredAot={context.HotfixManifest.RequiredAotVersion}";
            if (!string.Equals(context.HotfixManifest.BuildTarget, context.BuildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                report.AddWarning("Hotfix 清单", status, "构建目标不匹配。当前构建模式会重新生成它。");
            }
            else
            {
                report.AddInfo("Hotfix 清单", status);
                report.AddInfo(
                    "Hotfix DLL 加载顺序",
                    HotfixAssemblyDependencySorter.FormatLoadingOrder(context.HotfixManifest.HotUpdateAssemblies));
                report.AddInfo(
                    "Hotfix DLL 依赖关系",
                    HotfixAssemblyDependencySorter.FormatDependencies(context.HotfixManifest.HotUpdateDependencies));
            }
        }

        private static void ValidateStartupPackageMode(HotfixBuildContext context)
        {
            if (context.StartupPackageMode == StartupPackageMode.EmptyPackage &&
                context.PlayerPlayMode == EPlayMode.OfflinePlayMode)
            {
                throw new InvalidOperationException(
                    "StartupPackageMode.EmptyPackage 需要搭配 HostPlayMode 或 WebPlayMode。");
            }

            if (context.StartupPackageMode == StartupPackageMode.OfflinePackage &&
                context.PlayerPlayMode != EPlayMode.OfflinePlayMode)
            {
                throw new InvalidOperationException(
                    $"StartupPackageMode.OfflinePackage 需要搭配 OfflinePlayMode，当前模式为 {context.PlayerPlayMode}。");
            }
        }

        private static string FormatTags(string[] tags)
        {
            return tags == null || tags.Length == 0
                ? "空"
                : string.Join(", ", tags);
        }
    }
}
