using System;
using System.Collections;
using System.Collections.Generic;
using Framework.YooAssetBridge;
using QFramework;
using UnityEngine;
using YooAsset;

namespace Framework.Procedure
{
    internal enum HotfixLocalManifestSource
    {
        None,
        Active,
        Cache,
        Buildin,
        WebServer,
        Editor
    }

    internal struct HotfixLocalManifestResult
    {
        public bool Succeeded;
        public string PackageName;
        public string PackageVersion;
        public string Error;
        public HotfixLocalManifestSource Source;
    }

    internal static class HotfixLocalManifestUtility
    {
        private const string LastUsablePackageVersionKeyPrefix = "Hotfix.LastUsablePackageVersion.";

        public static string GetLastUsablePackageVersion(string packageName)
        {
            return string.IsNullOrWhiteSpace(packageName)
                ? string.Empty
                : PlayerPrefs.GetString(GetLastUsablePackageVersionKey(packageName), string.Empty);
        }

        public static void SaveLastUsablePackageVersion(string packageName, string packageVersion)
        {
            if (string.IsNullOrWhiteSpace(packageName) || string.IsNullOrWhiteSpace(packageVersion))
            {
                return;
            }

            PlayerPrefs.SetString(GetLastUsablePackageVersionKey(packageName), packageVersion.Trim());
            PlayerPrefs.Save();
        }

        public static IEnumerator TryLoadLocalManifest(
            ResourcePackage package,
            string preferredVersion,
            Action<HotfixLocalManifestResult> onCompleted)
        {
            var result = new HotfixLocalManifestResult
            {
                Succeeded = false,
                PackageName = package == null ? string.Empty : package.PackageName,
                Source = HotfixLocalManifestSource.None
            };

            if (package == null)
            {
                result.Error = "Resource package is null.";
                onCompleted?.Invoke(result);
                yield break;
            }

            var candidates = new List<string>();
            AddVersionCandidate(candidates, preferredVersion);
            AddVersionCandidate(candidates, GetLastUsablePackageVersion(package.PackageName));
            if (package.PackageValid)
            {
                AddVersionCandidate(candidates, package.GetPackageVersion());
            }

            if (!YooAssetLocalManifestBridge.TryCreate(package, out var bridge, out var bridgeError))
            {
                result.Error = bridgeError;
                onCompleted?.Invoke(result);
                yield break;
            }

            yield return bridge.TryCollectLocalPackageVersions(candidates);

            string lastError = string.Empty;
            foreach (var packageVersion in candidates)
            {
                if (TryUseActiveManifest(package, packageVersion, ref result))
                {
                    onCompleted?.Invoke(result);
                    yield break;
                }

                YooAssetLocalManifestResult bridgeResult = default;
                yield return bridge.TryLoadLocalManifest(packageVersion, value => bridgeResult = value);
                result = ConvertResult(bridgeResult);

                if (result.Succeeded)
                {
                    SaveLastUsablePackageVersion(result.PackageName, result.PackageVersion);
                    LogKit.W($"Use local manifest. Package: {result.PackageName}, Version: {result.PackageVersion}, Source: {result.Source}");
                    onCompleted?.Invoke(result);
                    yield break;
                }

                lastError = result.Error;
            }

            result.Succeeded = false;
            result.Source = HotfixLocalManifestSource.None;
            result.PackageVersion = string.Empty;
            result.Error = string.IsNullOrEmpty(lastError)
                ? $"No usable local manifest found. Package: {package.PackageName}"
                : lastError;
            onCompleted?.Invoke(result);
        }

        private static string GetLastUsablePackageVersionKey(string packageName)
        {
            return $"{LastUsablePackageVersionKeyPrefix}{packageName.Trim()}";
        }

        private static bool TryUseActiveManifest(
            ResourcePackage package,
            string packageVersion,
            ref HotfixLocalManifestResult result)
        {
            if (!package.PackageValid || package.GetPackageVersion() != packageVersion)
            {
                return false;
            }

            result = new HotfixLocalManifestResult
            {
                Succeeded = true,
                PackageName = package.PackageName,
                PackageVersion = packageVersion,
                Source = HotfixLocalManifestSource.Active
            };
            return true;
        }

        private static HotfixLocalManifestResult ConvertResult(YooAssetLocalManifestResult result)
        {
            return new HotfixLocalManifestResult
            {
                Succeeded = result.Succeeded,
                PackageName = result.PackageName,
                PackageVersion = result.PackageVersion,
                Error = result.Error,
                Source = ConvertSource(result.Source)
            };
        }

        private static HotfixLocalManifestSource ConvertSource(YooAssetLocalManifestSource source)
        {
            switch (source)
            {
                case YooAssetLocalManifestSource.Cache:
                    return HotfixLocalManifestSource.Cache;
                case YooAssetLocalManifestSource.Buildin:
                    return HotfixLocalManifestSource.Buildin;
                case YooAssetLocalManifestSource.WebServer:
                    return HotfixLocalManifestSource.WebServer;
                case YooAssetLocalManifestSource.Editor:
                    return HotfixLocalManifestSource.Editor;
                default:
                    return HotfixLocalManifestSource.None;
            }
        }

        private static void AddVersionCandidate(List<string> candidates, string packageVersion)
        {
            if (string.IsNullOrWhiteSpace(packageVersion))
            {
                return;
            }

            string normalizedVersion = packageVersion.Trim();
            if (!candidates.Contains(normalizedVersion))
            {
                candidates.Add(normalizedVersion);
            }
        }
    }
}
