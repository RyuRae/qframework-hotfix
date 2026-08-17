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

    internal struct HotfixLastGoodRecord
    {
        public string MainPackageVersion;
        public string RawFilePackageVersion;
        public string HotfixVersion;
        public string AotVersion;

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(MainPackageVersion) &&
            !string.IsNullOrWhiteSpace(HotfixVersion) &&
            !string.IsNullOrWhiteSpace(AotVersion);
    }

    internal static class HotfixLocalManifestUtility
    {
        private const string LastGoodRecordKeyPrefix = "Hotfix.LastGoodRecord.";
        private const string LegacyLastUsablePackageVersionKeyPrefix = "Hotfix.LastUsablePackageVersion.";
        private const string LegacyLastUsableAotVersionKeyPrefix = "Hotfix.LastUsableAotVersion.";
        private const string LegacyLastUsableHotfixVersionKeyPrefix = "Hotfix.LastUsableHotfixVersion.";
        private const string LegacyLastUsableAssemblyCombinationKeyPrefix = "Hotfix.LastUsableAssemblyCombination.";
        private const char RecordSeparator = '\u001f';

        public static string GetLastUsablePackageVersion(string packageName)
        {
            return TryGetLastGoodRecord(packageName, out var record)
                ? record.MainPackageVersion
                : GetLegacyValue(LegacyLastUsablePackageVersionKeyPrefix, packageName);
        }

        public static bool TryGetLastGoodRecord(string packageName, out HotfixLastGoodRecord record)
        {
            record = default;
            if (string.IsNullOrWhiteSpace(packageName))
            {
                return false;
            }

            string serialized;
            try
            {
                serialized = PlayerPrefs.GetString(GetVersionKey(LastGoodRecordKeyPrefix, packageName), string.Empty);
            }
            catch (Exception exception)
            {
                LogKit.W($"Read LastGood record failed. Package={packageName}. {exception.Message}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(serialized))
            {
                return false;
            }

            string[] fields = serialized.Split(RecordSeparator);
            if (fields.Length != 4)
            {
                return false;
            }

            record = new HotfixLastGoodRecord
            {
                MainPackageVersion = fields[0].Trim(),
                RawFilePackageVersion = fields[1].Trim(),
                HotfixVersion = fields[2].Trim(),
                AotVersion = fields[3].Trim()
            };
            return record.IsValid;
        }

        public static string GetLastUsableAotVersion(string packageName)
        {
            return TryGetLastGoodRecord(packageName, out var record)
                ? record.AotVersion
                : GetLegacyValue(LegacyLastUsableAotVersionKeyPrefix, packageName);
        }

        public static string GetLastUsableHotfixVersion(string packageName)
        {
            return TryGetLastGoodRecord(packageName, out var record)
                ? record.HotfixVersion
                : GetLegacyValue(LegacyLastUsableHotfixVersionKeyPrefix, packageName);
        }

        public static string GetLastUsableAssemblyCombination(string packageName)
        {
            if (TryGetLastGoodRecord(packageName, out var record))
            {
                return $"{record.HotfixVersion}|{record.AotVersion}";
            }

            return GetLegacyValue(LegacyLastUsableAssemblyCombinationKeyPrefix, packageName);
        }

        public static bool SaveLastGoodRecord(
            string packageName,
            string mainPackageVersion,
            string rawFilePackageVersion,
            string hotfixVersion,
            string aotVersion,
            out string error)
        {
            return SaveLastGoodRecord(
                packageName,
                mainPackageVersion,
                rawFilePackageVersion,
                hotfixVersion,
                aotVersion,
                true,
                out error);
        }

        public static bool SaveLastGoodRecord(
            string packageName,
            string mainPackageVersion,
            string rawFilePackageVersion,
            string hotfixVersion,
            string aotVersion,
            bool flushImmediately,
            out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(packageName) ||
                string.IsNullOrWhiteSpace(mainPackageVersion) ||
                string.IsNullOrWhiteSpace(hotfixVersion) ||
                string.IsNullOrWhiteSpace(aotVersion))
            {
                error = "LastGood record requires package name, package version, HotfixVersion, and AotVersion.";
                return false;
            }

            string normalizedPackageName = packageName.Trim();
            string normalizedPackageVersion = mainPackageVersion.Trim();
            string normalizedRawFileVersion = string.IsNullOrWhiteSpace(rawFilePackageVersion)
                ? string.Empty
                : rawFilePackageVersion.Trim();
            string normalizedHotfixVersion = hotfixVersion.Trim();
            string normalizedAotVersion = aotVersion.Trim();
            if (ContainsRecordSeparator(normalizedPackageVersion) ||
                ContainsRecordSeparator(normalizedRawFileVersion) ||
                ContainsRecordSeparator(normalizedHotfixVersion) ||
                ContainsRecordSeparator(normalizedAotVersion))
            {
                error = "LastGood record contains an unsupported separator character.";
                return false;
            }

            string key = GetVersionKey(LastGoodRecordKeyPrefix, normalizedPackageName);
            try
            {
                PlayerPrefs.SetString(key, string.Join(
                    RecordSeparator.ToString(),
                    normalizedPackageVersion,
                    normalizedRawFileVersion,
                    normalizedHotfixVersion,
                    normalizedAotVersion));
                if (flushImmediately)
                {
                    PlayerPrefs.Save();
                }
                return true;
            }
            catch (Exception exception)
            {
                error = $"Save LastGood record failed. Package={normalizedPackageName}. {exception.Message}";
                return false;
            }
        }

        public static IEnumerator TryLoadLastGoodManifest(
            ResourcePackage package,
            string lastGoodVersion,
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

            if (!YooAssetLocalManifestBridge.TryCreate(package, out var bridge, out var bridgeError))
            {
                result.Error = bridgeError;
                onCompleted?.Invoke(result);
                yield break;
            }

            if (string.IsNullOrWhiteSpace(lastGoodVersion))
            {
                result.Error = $"LastGood package version is empty. Package: {package.PackageName}";
                onCompleted?.Invoke(result);
                yield break;
            }

            string normalizedLastGoodVersion = lastGoodVersion.Trim();
            if (TryUseActiveManifest(package, normalizedLastGoodVersion, ref result))
            {
                onCompleted?.Invoke(result);
                yield break;
            }

            YooAssetLocalManifestResult cacheResult = default;
            yield return bridge.TryLoadCacheManifest(
                normalizedLastGoodVersion,
                value => cacheResult = value);
            result = ConvertResult(cacheResult);
            if (result.Succeeded)
            {
                LogKit.W($"Use LastGood cache manifest. Package: {result.PackageName}, Version: {result.PackageVersion}");
            }

            onCompleted?.Invoke(result);
        }

        public static IEnumerator TryLoadBuildinManifest(
            ResourcePackage package,
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

            if (!YooAssetLocalManifestBridge.TryCreate(package, out var bridge, out var bridgeError))
            {
                result.Error = bridgeError;
                onCompleted?.Invoke(result);
                yield break;
            }

            YooAssetLocalManifestResult buildinResult = default;
            yield return bridge.TryLoadBuildinManifest(value => buildinResult = value);
            result = ConvertResult(buildinResult);
            if (result.Succeeded)
            {
                LogKit.W($"Use build-in manifest. Package: {result.PackageName}, Version: {result.PackageVersion}, Source: {result.Source}");
            }

            onCompleted?.Invoke(result);
        }

        private static string GetLegacyValue(string prefix, string packageName)
        {
            return string.IsNullOrWhiteSpace(packageName)
                ? string.Empty
                : PlayerPrefs.GetString(GetVersionKey(prefix, packageName), string.Empty);
        }

        private static string GetVersionKey(string prefix, string packageName)
        {
            return $"{prefix}{packageName.Trim()}";
        }

        private static bool ContainsRecordSeparator(string value)
        {
            return !string.IsNullOrEmpty(value) && value.IndexOf(RecordSeparator) >= 0;
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

    }
}
