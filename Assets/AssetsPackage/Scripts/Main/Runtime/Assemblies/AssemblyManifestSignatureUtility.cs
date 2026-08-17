using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Assemblies
{
    /// <summary>
    /// AOT/Hotfix Manifest 的确定性签名协议。签名覆盖版本、平台、入口、依赖和所有 DLL 摘要；
    /// 私钥只在 Editor 构建进程中使用，运行时仅持有公钥。
    /// </summary>
    public static class AssemblyManifestSignatureUtility
    {
        public const int CurrentSignatureVersion = 1;
        public const string RsaSha256Algorithm = "RSA-SHA256-PKCS1";

        public static bool HasSignature(AOTAssemblyManifest manifest)
        {
            return manifest != null && HasSignature(
                manifest.SignatureVersion,
                manifest.SignatureAlgorithm,
                manifest.SigningKeyId,
                manifest.Signature);
        }

        public static bool HasSignature(HotfixAssemblyManifest manifest)
        {
            return manifest != null && HasSignature(
                manifest.SignatureVersion,
                manifest.SignatureAlgorithm,
                manifest.SigningKeyId,
                manifest.Signature);
        }

        public static bool HasAnySignatureMetadata(AOTAssemblyManifest manifest)
        {
            return manifest != null && HasAnySignatureMetadata(
                manifest.SignatureVersion,
                manifest.SignatureAlgorithm,
                manifest.SigningKeyId,
                manifest.Signature);
        }

        public static bool HasAnySignatureMetadata(HotfixAssemblyManifest manifest)
        {
            return manifest != null && HasAnySignatureMetadata(
                manifest.SignatureVersion,
                manifest.SignatureAlgorithm,
                manifest.SigningKeyId,
                manifest.Signature);
        }

        public static byte[] CreateCanonicalPayload(AOTAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            var builder = new StringBuilder(2048);
            Append(builder, "ManifestType", "AOT");
            AppendSignatureIdentity(builder, manifest.SignatureVersion, manifest.SignatureAlgorithm, manifest.SigningKeyId);
            Append(builder, "ReleaseVersion", manifest.ReleaseVersion);
            Append(builder, "ReleaseSequence", manifest.ReleaseSequence.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Append(builder, "AppVersion", manifest.AppVersion);
            Append(builder, "BuildTarget", manifest.BuildTarget);
            Append(builder, "AotVersion", manifest.AotVersion);
            Append(builder, "BaselineFingerprint", manifest.BaselineFingerprint);
            Append(builder, "BaselineGeneratedAtUtc", manifest.BaselineGeneratedAtUtc);
            Append(builder, "BaselineGitCommit", manifest.BaselineGitCommit);
            AppendOrderedStrings(builder, "AotMetadataAssemblies", manifest.AotMetadataAssemblies);
            AppendFileRecords(builder, "AotMetadataFiles", manifest.AotMetadataFiles);
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public static byte[] CreateCanonicalPayload(HotfixAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            var builder = new StringBuilder(2048);
            Append(builder, "ManifestType", "Hotfix");
            AppendSignatureIdentity(builder, manifest.SignatureVersion, manifest.SignatureAlgorithm, manifest.SigningKeyId);
            Append(builder, "ReleaseVersion", manifest.ReleaseVersion);
            Append(builder, "ReleaseSequence", manifest.ReleaseSequence.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Append(builder, "AppVersionMin", manifest.AppVersionMin);
            Append(builder, "AppVersionMax", manifest.AppVersionMax);
            Append(builder, "BuildTarget", manifest.BuildTarget);
            Append(builder, "RequiredAotVersion", manifest.RequiredAotVersion);
            Append(builder, "HotfixVersion", manifest.HotfixVersion);
            Append(builder, "EntrySceneAddress", manifest.EntrySceneAddress);
            Append(builder, "EntryPrefabAddress", manifest.EntryPrefabAddress);
            Append(builder, "EntryTypeName", manifest.EntryTypeName);
            Append(builder, "EntryMethodName", manifest.EntryMethodName);
            AppendOrderedStrings(builder, "HotUpdateAssemblies", manifest.HotUpdateAssemblies);
            AppendFileRecords(builder, "HotUpdateFiles", manifest.HotUpdateFiles);
            AppendDependencyRecords(builder, manifest.HotUpdateDependencies);
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public static bool Verify(
            AOTAssemblyManifest manifest,
            HotfixManifestPublicKey publicKey,
            out string error)
        {
            return VerifyCore(
                manifest == null ? 0 : manifest.SignatureVersion,
                manifest == null ? string.Empty : manifest.SignatureAlgorithm,
                manifest == null ? string.Empty : manifest.SigningKeyId,
                manifest == null ? string.Empty : manifest.Signature,
                publicKey,
                manifest == null ? null : CreateCanonicalPayload(manifest),
                "AOT",
                out error);
        }

        public static bool Verify(
            HotfixAssemblyManifest manifest,
            HotfixManifestPublicKey publicKey,
            out string error)
        {
            return VerifyCore(
                manifest == null ? 0 : manifest.SignatureVersion,
                manifest == null ? string.Empty : manifest.SignatureAlgorithm,
                manifest == null ? string.Empty : manifest.SigningKeyId,
                manifest == null ? string.Empty : manifest.Signature,
                publicKey,
                manifest == null ? null : CreateCanonicalPayload(manifest),
                "Hotfix",
                out error);
        }

#if UNITY_EDITOR
        public static string Sign(byte[] payload, RSA privateKey)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (privateKey == null)
            {
                throw new ArgumentNullException(nameof(privateKey));
            }

            byte[] signature = privateKey.SignData(
                payload,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(signature);
        }
#endif

        public static bool TryDecodePublicKey(
            HotfixManifestPublicKey publicKey,
            out RSAParameters parameters,
            out string error)
        {
            parameters = default;
            error = string.Empty;
            if (publicKey == null)
            {
                error = "Trusted manifest public key is null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(publicKey.KeyId) ||
                string.IsNullOrWhiteSpace(publicKey.Modulus) ||
                string.IsNullOrWhiteSpace(publicKey.Exponent))
            {
                error = $"Trusted manifest public key is incomplete. KeyId={publicKey.KeyId}";
                return false;
            }

            try
            {
                parameters = new RSAParameters
                {
                    Modulus = Convert.FromBase64String(publicKey.Modulus.Trim()),
                    Exponent = Convert.FromBase64String(publicKey.Exponent.Trim())
                };
            }
            catch (FormatException exception)
            {
                error = $"Trusted manifest public key is not valid Base64. KeyId={publicKey.KeyId}. {exception.Message}";
                return false;
            }

            if (parameters.Modulus == null || parameters.Modulus.Length < 256)
            {
                error = $"Trusted manifest RSA key must be at least 2048 bits. KeyId={publicKey.KeyId}";
                return false;
            }

            if (parameters.Exponent == null || parameters.Exponent.Length == 0)
            {
                error = $"Trusted manifest RSA exponent is empty. KeyId={publicKey.KeyId}";
                return false;
            }

            return true;
        }

        private static bool VerifyCore(
            int signatureVersion,
            string signatureAlgorithm,
            string signingKeyId,
            string signature,
            HotfixManifestPublicKey publicKey,
            byte[] payload,
            string label,
            out string error)
        {
            error = string.Empty;
            if (payload == null)
            {
                error = $"{label} manifest is null.";
                return false;
            }

            if (!HasSignature(signatureVersion, signatureAlgorithm, signingKeyId, signature))
            {
                error = $"{label} manifest signature is missing or incomplete.";
                return false;
            }

            if (signatureVersion != CurrentSignatureVersion ||
                !string.Equals(signatureAlgorithm, RsaSha256Algorithm, StringComparison.Ordinal))
            {
                error = $"{label} manifest signature protocol is unsupported. " +
                        $"Version={signatureVersion}, Algorithm={signatureAlgorithm}";
                return false;
            }

            if (publicKey == null ||
                !string.Equals(publicKey.KeyId, signingKeyId, StringComparison.Ordinal))
            {
                error = $"{label} manifest signing key is not trusted. KeyId={signingKeyId}";
                return false;
            }

            if (!TryDecodePublicKey(publicKey, out var parameters, out error))
            {
                return false;
            }

            byte[] signatureBytes;
            try
            {
                signatureBytes = Convert.FromBase64String(signature.Trim());
            }
            catch (FormatException exception)
            {
                error = $"{label} manifest signature is not valid Base64. {exception.Message}";
                return false;
            }

            try
            {
                using (var rsa = RSA.Create())
                {
                    rsa.ImportParameters(parameters);
                    if (!rsa.VerifyData(
                            payload,
                            signatureBytes,
                            HashAlgorithmName.SHA256,
                            RSASignaturePadding.Pkcs1))
                    {
                        error = $"{label} manifest signature verification failed. KeyId={signingKeyId}";
                        return false;
                    }
                }
            }
            catch (Exception exception)
            {
                error = $"{label} manifest signature verification error. KeyId={signingKeyId}. {exception.Message}";
                return false;
            }

            return true;
        }

        private static bool HasSignature(int version, string algorithm, string keyId, string signature)
        {
            return version > 0 &&
                   !string.IsNullOrWhiteSpace(algorithm) &&
                   !string.IsNullOrWhiteSpace(keyId) &&
                   !string.IsNullOrWhiteSpace(signature);
        }

        private static bool HasAnySignatureMetadata(int version, string algorithm, string keyId, string signature)
        {
            return version != 0 ||
                   !string.IsNullOrWhiteSpace(algorithm) ||
                   !string.IsNullOrWhiteSpace(keyId) ||
                   !string.IsNullOrWhiteSpace(signature);
        }

        private static void AppendSignatureIdentity(StringBuilder builder, int version, string algorithm, string keyId)
        {
            Append(builder, "SignatureVersion", version.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Append(builder, "SignatureAlgorithm", algorithm);
            Append(builder, "SigningKeyId", keyId);
        }

        private static void AppendOrderedStrings(StringBuilder builder, string label, IEnumerable<string> values)
        {
            var items = (values ?? Enumerable.Empty<string>()).Select(Normalize).ToList();
            Append(builder, label + ".Count", items.Count.ToString(System.Globalization.CultureInfo.InvariantCulture));
            for (int index = 0; index < items.Count; index++)
            {
                Append(builder, label + "[" + index + "]", items[index]);
            }
        }

        private static void AppendFileRecords(StringBuilder builder, string label, IEnumerable<AssemblyFileRecord> records)
        {
            var items = (records ?? Enumerable.Empty<AssemblyFileRecord>())
                .Where(record => record != null)
                .OrderBy(GetFileName, StringComparer.Ordinal)
                .ToList();
            Append(builder, label + ".Count", items.Count.ToString(System.Globalization.CultureInfo.InvariantCulture));
            for (int index = 0; index < items.Count; index++)
            {
                var record = items[index];
                string prefix = label + "[" + index + "].";
                Append(builder, prefix + "FileName", record.FileName);
                Append(builder, prefix + "AssemblyName", record.AssemblyName);
                Append(builder, prefix + "Size", record.Size.ToString(System.Globalization.CultureInfo.InvariantCulture));
                Append(builder, prefix + "Sha256", record.Sha256);
            }
        }

        private static void AppendDependencyRecords(StringBuilder builder, IEnumerable<AssemblyDependencyRecord> records)
        {
            var items = (records ?? Enumerable.Empty<AssemblyDependencyRecord>())
                .Where(record => record != null)
                .OrderBy(record => Normalize(record.DllName), StringComparer.Ordinal)
                .ToList();
            Append(builder, "HotUpdateDependencies.Count", items.Count.ToString(System.Globalization.CultureInfo.InvariantCulture));
            for (int index = 0; index < items.Count; index++)
            {
                var record = items[index];
                string prefix = "HotUpdateDependencies[" + index + "].";
                Append(builder, prefix + "AssemblyName", record.AssemblyName);
                Append(builder, prefix + "DllName", record.DllName);
                AppendOrderedStrings(builder, prefix + "DependsOn", record.DependsOn);
            }
        }

        private static string GetFileName(AssemblyFileRecord record)
        {
            return Normalize(string.IsNullOrWhiteSpace(record.FileName) ? record.AssemblyName : record.FileName);
        }

        private static void Append(StringBuilder builder, string name, string value)
        {
            string normalized = Normalize(value);
            builder.Append(name)
                .Append(':')
                .Append(normalized.Length.ToString(System.Globalization.CultureInfo.InvariantCulture))
                .Append(':')
                .Append(normalized)
                .Append('\n');
        }

        private static string Normalize(string value)
        {
            return value == null ? string.Empty : value.Trim();
        }
    }
}
