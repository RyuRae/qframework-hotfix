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
            AddReleaseProfile(context, report);
            AddLogPolicy(context, report);
            AddRuntimeSettings(context, report);
            AddRemoteSettings(context, report);
            AddEntryResource(context, report);
            AddManifestStatus(context, report);
            return report;
        }

        private static void AddReleaseProfile(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.ReleaseProfile == null)
            {
                report.AddError(
                    "ReleaseProfile",
                    "缺失",
                    $"缺少发布配置：{HotfixReleaseProfile.DefaultAssetPath}。请在构建中心创建并绑定默认发布配置。");
                return;
            }

            var profile = context.ReleaseProfile;
            report.AddInfo(
                "ReleaseProfile",
                profile.DisplayName,
                $"Flavor={profile.BuildFlavor}, Target={profile.BuildTarget}, App={profile.AppVersion}, Compat={profile.AppVersionMin}-{profile.AppVersionMax}, Resource={FormatOptional(profile.ResourceVersion)}, Hotfix={FormatOptional(profile.HotfixVersion)}, Env={profile.RemoteEnvironment}, Channel={profile.Channel}, Region={profile.Region}");

            if (!profile.ValidateForBuild(context, out var error))
            {
                report.AddError("ReleaseProfile", profile.DisplayName, error);
                return;
            }

            if (profile.IsFormalRelease)
            {
                report.AddInfo("正式发布保护", "开启", "Development CDN 将被阻断。");
            }
            else
            {
                report.AddWarning("正式发布保护", "未开启", "当前 ReleaseProfile 允许 Development CDN，仅建议开发或测试使用。");
            }
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

        private static void AddLogPolicy(HotfixBuildContext context, HotfixBuildReport report)
        {
            var profile = context.ReleaseProfile;
            if (profile == null)
            {
                report.AddWarning(
                    "Player 日志策略",
                    EditorUserBuildSettings.development ? "完整日志" : "仅 Error/Exception",
                    "缺少 ReleaseProfile，当前显示 Unity Development Build 状态。");
                return;
            }

            string desired = profile.UsesDevelopmentBuild ? "完整日志" : "仅 Error/Exception";
            string synced = EditorUserBuildSettings.development ? "完整日志" : "仅 Error/Exception";
            if (profile.UsesDevelopmentBuild == EditorUserBuildSettings.development)
            {
                report.AddInfo("Player 日志策略", desired, "由构建环境预设自动控制，无需 ENABLE_LOG。");
            }
            else
            {
                report.AddWarning(
                    "Player 日志策略",
                    $"Profile={desired} / 当前 Unity={synced}",
                    "执行“应用并自动修复”或开始构建后会同步 Development Build。无需配置 ENABLE_LOG。");
            }
        }

        private static void AddRuntimeSettings(HotfixBuildContext context, HotfixBuildReport report)
        {
            if (context.RuntimeSettings == null)
            {
                report.AddError(
                    "运行时设置",
                    "缺失",
                    $"缺少资源：{HotfixBuildProfileUtility.RuntimeSettingsAssetPath}。可使用“应用并自动修复”创建并同步。");
                return;
            }

            try
            {
                HotfixBuildProfileUtility.ValidatePlayerPlayMode(context.PlayerPlayMode, context.BuildTarget);
                ValidateStartupPackageMode(context);
                BuildAssetsCommand.ValidateStartupDownloadTags(context.StartupDownloadMode, context.StartupDownloadTags);
                if (context.IncludeRawFilePackage)
                {
                    BuildAssetsCommand.ValidateRawFilePackageForBuild(
                        context.MainPackageName,
                        context.RawFilePackageName,
                        context.StartupDownloadMode,
                        context.RawFileStartupDownloadTags);
                }
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
                if (context.IncludeRawFilePackage)
                {
                    report.AddInfo("RawFile 启动下载 Tags", FormatTags(context.RawFileStartupDownloadTags));
                }
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
                FormatRemoteSelector(context));

            var releaseProfile = context.ReleaseProfile;
            if (releaseProfile != null)
            {
                if (!releaseProfile.ValidateRemoteConfigurationForBuild(context, out var profileError))
                {
                    report.AddError("远端设置", releaseProfile.RemoteEnvironment.ToString(), profileError);
                }
                else
                {
                    report.AddInfo(
                        "远端设置",
                        "Profile 配置有效",
                        "只读校验直接检查 ReleaseProfile；底层 HotfixRemoteSettings 会在修复或构建时同步。");
                }

                return;
            }

            bool allowDevelopmentCdn = releaseProfile == null || releaseProfile.AllowsDevelopmentCdnForBuild;
            var environment = releaseProfile == null
                ? context.RemoteSettings.DefaultEnvironment
                : releaseProfile.RemoteEnvironment;
            string channel = releaseProfile == null
                ? context.RemoteSettings.DefaultChannel
                : NormalizeSelector(releaseProfile.Channel);
            string region = releaseProfile == null
                ? context.RemoteSettings.DefaultRegion
                : NormalizeSelector(releaseProfile.Region);

            if (!context.RemoteSettings.TryValidateForPlayerBuild(
                    allowDevelopmentCdn,
                    context.BuildTargetName,
                    environment,
                    channel,
                    region,
                    out var error))
            {
                report.AddError("远端设置", environment.ToString(), error);
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
            if (context.Mode == HotfixBuildMode.AOTMetadataPatch)
            {
                var playerBaseline = HotfixPlayerAOTBaselineUtility.Load();
                if (HotfixPlayerAOTBaselineUtility.TryValidateIdentity(
                        playerBaseline,
                        context.BuildTarget,
                        context.AppVersion,
                        out string baselineMessage))
                {
                    report.AddInfo(
                        "Player AOT 基线",
                        playerBaseline.BaselineFingerprint,
                        $"身份校验通过；构建后还会严格比对 {playerBaseline.StrippedAOTAssemblies.Count} 个裁剪 AOT DLL。");
                }
                else
                {
                    report.AddError("Player AOT 基线", "无效", baselineMessage);
                }
            }

            if (context.AOTManifest == null)
            {
                if (context.Mode == HotfixBuildMode.HotfixPackage ||
                    context.Mode == HotfixBuildMode.AOTMetadataPatch)
                {
                    report.AddError(
                        "AOT 清单",
                        "缺失",
                        $"当前任务需要已有 AOT 清单：{BuildAssetsCommand.AOTAssemblyManifestAssetPath}。");
                }
                else
                {
                    report.AddWarning(
                        "AOT 清单",
                        "缺失",
                        "首包构建会重新生成它，并在完整成功后建立独立 Player AOT 基线。");
                }

                return;
            }

            string status = $"{context.AOTManifest.AotVersion} / {context.AOTManifest.BuildTarget}";
            if (!string.Equals(context.AOTManifest.BuildTarget, context.BuildTargetName, StringComparison.OrdinalIgnoreCase))
            {
                if (context.Mode == HotfixBuildMode.HotfixPackage ||
                    context.Mode == HotfixBuildMode.AOTMetadataPatch)
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

            if (context.Mode == HotfixBuildMode.AOTMetadataPatch &&
                !string.Equals(context.AOTManifest.AppVersion, context.AppVersion, StringComparison.OrdinalIgnoreCase))
            {
                report.AddError(
                    "AOT 清单 AppVersion",
                    context.AOTManifest.AppVersion,
                    $"AOT 元数据补丁必须与当前 AppVersion={context.AppVersion} 一致。");
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
                if (context.IncludeRawFilePackage)
                {
                    if (string.IsNullOrWhiteSpace(context.HotfixManifest.RawFilePackageName) ||
                        string.IsNullOrWhiteSpace(context.HotfixManifest.RawFilePackageVersion) ||
                        string.IsNullOrWhiteSpace(context.HotfixManifest.RawFileManifestSha256))
                    {
                        report.AddWarning(
                            "RawFile 清单信任",
                            "构建时将生成",
                            "当前 Hotfix Manifest 尚未绑定 RawFile 包身份与 YooAsset Manifest SHA-256。");
                    }
                    else
                    {
                        report.AddInfo(
                            "RawFile 清单信任",
                            $"{context.HotfixManifest.RawFilePackageName}:{context.HotfixManifest.RawFilePackageVersion}",
                            context.HotfixManifest.RawFileManifestSha256);
                    }
                }
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

        private static string FormatRemoteSelector(HotfixBuildContext context)
        {
            if (context.ReleaseProfile == null)
            {
                return $"{context.RemoteEnvironmentName} / {context.RemoteChannel} / {context.RemoteRegion}";
            }

            return $"{context.ReleaseProfile.RemoteEnvironment} / {NormalizeSelector(context.ReleaseProfile.Channel)} / {NormalizeSelector(context.ReleaseProfile.Region)}";
        }

        private static string FormatOptional(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "auto" : value.Trim();
        }

        private static string NormalizeSelector(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "default" : value.Trim();
        }
    }
}
