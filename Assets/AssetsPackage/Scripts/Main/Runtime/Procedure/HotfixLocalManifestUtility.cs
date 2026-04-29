using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        WebServer
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
        private const BindingFlags InstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

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

            object playModeImpl = GetPlayModeImpl(package);
            if (playModeImpl == null)
            {
                result.Error = $"Can not find YooAsset play mode implementation. Package: {package.PackageName}";
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

            yield return TryRequestLocalPackageVersion(playModeImpl, "BuildinFileSystem", candidates);
            yield return TryRequestLocalPackageVersion(playModeImpl, "WebServerFileSystem", candidates);

            string lastError = string.Empty;
            foreach (var packageVersion in candidates)
            {
                if (package.PackageValid && package.GetPackageVersion() == packageVersion)
                {
                    result.Succeeded = true;
                    result.PackageVersion = packageVersion;
                    result.Source = HotfixLocalManifestSource.Active;
                    onCompleted?.Invoke(result);
                    yield break;
                }

                yield return TryLoadLocalManifestFromFileSystem(
                    playModeImpl,
                    "CacheFileSystem",
                    HotfixLocalManifestSource.Cache,
                    packageVersion,
                    result.PackageName,
                    loadResult => result = loadResult);

                if (result.Succeeded)
                {
                    SaveLastUsablePackageVersion(result.PackageName, result.PackageVersion);
                    onCompleted?.Invoke(result);
                    yield break;
                }

                lastError = result.Error;

                yield return TryLoadLocalManifestFromFileSystem(
                    playModeImpl,
                    "BuildinFileSystem",
                    HotfixLocalManifestSource.Buildin,
                    packageVersion,
                    result.PackageName,
                    loadResult => result = loadResult);

                if (result.Succeeded)
                {
                    SaveLastUsablePackageVersion(result.PackageName, result.PackageVersion);
                    onCompleted?.Invoke(result);
                    yield break;
                }

                lastError = result.Error;

                yield return TryLoadLocalManifestFromFileSystem(
                    playModeImpl,
                    "WebServerFileSystem",
                    HotfixLocalManifestSource.WebServer,
                    packageVersion,
                    result.PackageName,
                    loadResult => result = loadResult);

                if (result.Succeeded)
                {
                    SaveLastUsablePackageVersion(result.PackageName, result.PackageVersion);
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

        private static object GetPlayModeImpl(ResourcePackage package)
        {
            return typeof(ResourcePackage)
                .GetField("_playModeImpl", InstanceFlags)
                ?.GetValue(package);
        }

        private static IEnumerator TryRequestLocalPackageVersion(object playModeImpl, string fileSystemPropertyName, List<string> candidates)
        {
            object fileSystem = GetMemberValue(playModeImpl, fileSystemPropertyName);
            if (fileSystem == null)
            {
                yield break;
            }

            MethodInfo method = fileSystem.GetType().GetMethod("RequestPackageVersionAsync", InstanceFlags);
            if (method == null)
            {
                yield break;
            }

            var operation = method.Invoke(fileSystem, new object[] { false, 60 }) as AsyncOperationBase;
            if (operation == null)
            {
                yield break;
            }

            yield return operation;
            if (operation.Status != EOperationStatus.Succeed)
            {
                yield break;
            }

            string packageVersion = GetMemberValue(operation, "PackageVersion") as string;
            AddVersionCandidate(candidates, packageVersion);
        }

        private static IEnumerator TryLoadLocalManifestFromFileSystem(
            object playModeImpl,
            string fileSystemPropertyName,
            HotfixLocalManifestSource source,
            string packageVersion,
            string packageName,
            Action<HotfixLocalManifestResult> onCompleted)
        {
            if (source == HotfixLocalManifestSource.Cache)
            {
                yield return TryLoadCacheManifestFromFileSystem(
                    playModeImpl,
                    fileSystemPropertyName,
                    packageVersion,
                    packageName,
                    onCompleted);
                yield break;
            }

            var result = new HotfixLocalManifestResult
            {
                Succeeded = false,
                PackageName = packageName,
                PackageVersion = packageVersion,
                Source = source
            };

            object fileSystem = GetMemberValue(playModeImpl, fileSystemPropertyName);
            if (fileSystem == null)
            {
                result.Error = $"{fileSystemPropertyName} is null. Package: {packageName}";
                onCompleted?.Invoke(result);
                yield break;
            }

            MethodInfo method = fileSystem.GetType().GetMethod("LoadPackageManifestAsync", InstanceFlags);
            if (method == null)
            {
                result.Error = $"{fileSystemPropertyName}.LoadPackageManifestAsync not found. Package: {packageName}";
                onCompleted?.Invoke(result);
                yield break;
            }

            var operation = method.Invoke(fileSystem, new object[] { packageVersion, 60 }) as AsyncOperationBase;
            if (operation == null)
            {
                result.Error = $"Load local manifest operation is null. Package: {packageName}, Version: {packageVersion}";
                onCompleted?.Invoke(result);
                yield break;
            }

            yield return operation;
            if (operation.Status != EOperationStatus.Succeed)
            {
                result.Error = operation.Error;
                onCompleted?.Invoke(result);
                yield break;
            }

            object manifest = GetMemberValue(operation, "Manifest");
            if (manifest == null)
            {
                result.Error = $"Local manifest is null. Package: {packageName}, Version: {packageVersion}";
                onCompleted?.Invoke(result);
                yield break;
            }

            if (!SetActiveManifest(playModeImpl, manifest))
            {
                result.Error = $"Set active manifest failed. Package: {packageName}, Version: {packageVersion}";
                onCompleted?.Invoke(result);
                yield break;
            }

            LogKit.W($"Use local manifest. Package: {packageName}, Version: {packageVersion}, Source: {source}");
            result.Succeeded = true;
            onCompleted?.Invoke(result);
        }

        private static IEnumerator TryLoadCacheManifestFromFileSystem(
            object playModeImpl,
            string fileSystemPropertyName,
            string packageVersion,
            string packageName,
            Action<HotfixLocalManifestResult> onCompleted)
        {
            var result = new HotfixLocalManifestResult
            {
                Succeeded = false,
                PackageName = packageName,
                PackageVersion = packageVersion,
                Source = HotfixLocalManifestSource.Cache
            };

            object fileSystem = GetMemberValue(playModeImpl, fileSystemPropertyName);
            if (fileSystem == null)
            {
                result.Error = $"{fileSystemPropertyName} is null. Package: {packageName}";
                onCompleted?.Invoke(result);
                yield break;
            }

            MethodInfo hashPathMethod = fileSystem.GetType().GetMethod("GetCachePackageHashFilePath", InstanceFlags);
            if (hashPathMethod == null)
            {
                result.Error = $"{fileSystemPropertyName}.GetCachePackageHashFilePath not found. Package: {packageName}";
                onCompleted?.Invoke(result);
                yield break;
            }

            string hashFilePath = hashPathMethod.Invoke(fileSystem, new object[] { packageVersion }) as string;
            if (string.IsNullOrEmpty(hashFilePath) || !File.Exists(hashFilePath))
            {
                result.Error = $"Can not found cache package hash file : {hashFilePath}";
                onCompleted?.Invoke(result);
                yield break;
            }

            string packageHash = File.ReadAllText(hashFilePath);
            if (string.IsNullOrWhiteSpace(packageHash))
            {
                result.Error = $"Cache package hash file content is empty : {hashFilePath}";
                onCompleted?.Invoke(result);
                yield break;
            }

            Type operationType = fileSystem.GetType().Assembly.GetType("YooAsset.LoadCachePackageManifestOperation");
            if (operationType == null)
            {
                result.Error = "YooAsset.LoadCachePackageManifestOperation not found.";
                onCompleted?.Invoke(result);
                yield break;
            }

            var operation = Activator.CreateInstance(
                operationType,
                InstanceFlags,
                null,
                new object[] { fileSystem, packageVersion, packageHash.Trim() },
                null) as AsyncOperationBase;

            if (operation == null)
            {
                result.Error = $"Load cache manifest operation is null. Package: {packageName}, Version: {packageVersion}";
                onCompleted?.Invoke(result);
                yield break;
            }

            if (!StartYooAssetOperation(packageName, operation))
            {
                result.Error = "Start YooAsset cache manifest operation failed.";
                onCompleted?.Invoke(result);
                yield break;
            }

            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                result.Error = operation.Error;
                onCompleted?.Invoke(result);
                yield break;
            }

            object manifest = GetMemberValue(operation, "Manifest");
            if (manifest == null)
            {
                result.Error = $"Cache manifest is null. Package: {packageName}, Version: {packageVersion}";
                onCompleted?.Invoke(result);
                yield break;
            }

            if (!SetActiveManifest(playModeImpl, manifest))
            {
                result.Error = $"Set active cache manifest failed. Package: {packageName}, Version: {packageVersion}";
                onCompleted?.Invoke(result);
                yield break;
            }

            LogKit.W($"Use local manifest. Package: {packageName}, Version: {packageVersion}, Source: Cache");
            result.Succeeded = true;
            onCompleted?.Invoke(result);
        }

        private static bool SetActiveManifest(object playModeImpl, object manifest)
        {
            PropertyInfo propertyInfo = playModeImpl.GetType().GetProperty("ActiveManifest", InstanceFlags);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(playModeImpl, manifest);
                return true;
            }

            FieldInfo fieldInfo = playModeImpl.GetType().GetField("ActiveManifest", InstanceFlags);
            if (fieldInfo == null)
            {
                return false;
            }

            fieldInfo.SetValue(playModeImpl, manifest);
            return true;
        }

        private static bool StartYooAssetOperation(string packageName, AsyncOperationBase operation)
        {
            Type operationSystemType = typeof(AsyncOperationBase).Assembly.GetType("YooAsset.OperationSystem");
            MethodInfo startOperationMethod = operationSystemType?.GetMethod(
                "StartOperation",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (startOperationMethod == null)
            {
                return false;
            }

            startOperationMethod.Invoke(null, new object[] { packageName, operation });
            return true;
        }

        private static object GetMemberValue(object target, string memberName)
        {
            if (target == null)
            {
                return null;
            }

            Type type = target.GetType();
            PropertyInfo propertyInfo = type.GetProperty(memberName, InstanceFlags);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(target);
            }

            return type.GetField(memberName, InstanceFlags)?.GetValue(target);
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
