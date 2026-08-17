using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using Framework;
using Framework.Assemblies;
using UnityEditor;

namespace HybridCLR.Editor
{
    /// <summary>
    /// 构建期 Manifest 签名。私钥只能来自环境变量，环境变量的值可以是 RSA XML，
    /// 也可以是仓库外 RSA XML 文件的绝对路径。
    /// </summary>
    public static class HotfixManifestSigningUtility
    {
        public static void SignOrClear(AOTAssemblyManifest manifest)
        {
            var profile = HotfixReleaseProfile.LoadSelectedOrDefault();
            if (profile == null || !profile.IsFormalRelease)
            {
                ClearSignature(manifest);
                return;
            }

            using (var rsa = LoadAndValidatePrivateKey(profile))
            {
                ApplySignatureIdentity(manifest, profile.ManifestSigningKeyId);
                manifest.Signature = AssemblyManifestSignatureUtility.Sign(
                    AssemblyManifestSignatureUtility.CreateCanonicalPayload(manifest),
                    rsa);
                VerifyGeneratedSignature(manifest, profile);
            }

            EditorUtility.SetDirty(manifest);
        }

        public static void SignOrClear(HotfixAssemblyManifest manifest)
        {
            var profile = HotfixReleaseProfile.LoadSelectedOrDefault();
            if (profile == null || !profile.IsFormalRelease)
            {
                ClearSignature(manifest);
                return;
            }

            using (var rsa = LoadAndValidatePrivateKey(profile))
            {
                ApplySignatureIdentity(manifest, profile.ManifestSigningKeyId);
                manifest.Signature = AssemblyManifestSignatureUtility.Sign(
                    AssemblyManifestSignatureUtility.CreateCanonicalPayload(manifest),
                    rsa);
                VerifyGeneratedSignature(manifest, profile);
            }

            EditorUtility.SetDirty(manifest);
        }

        public static bool TryValidateSigningConfiguration(HotfixReleaseProfile profile, out string error)
        {
            error = string.Empty;
            if (profile == null)
            {
                error = "Manifest signing requires a ReleaseProfile.";
                return false;
            }

            var publicKey = CreatePublicKey(profile);
            if (!AssemblyManifestSignatureUtility.TryDecodePublicKey(publicKey, out _, out error))
            {
                return false;
            }

            try
            {
                using (LoadAndValidatePrivateKey(profile))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                error = exception.Message;
                return false;
            }
        }

        private static RSA LoadAndValidatePrivateKey(HotfixReleaseProfile profile)
        {
            if (string.IsNullOrWhiteSpace(profile.ManifestPrivateKeyEnvironmentVariable))
            {
                throw new InvalidOperationException("Manifest private key environment variable name is empty.");
            }

            string variableName = profile.ManifestPrivateKeyEnvironmentVariable.Trim();
            string secret = Environment.GetEnvironmentVariable(variableName);
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException(
                    $"Manifest signing private key is missing. Set environment variable '{variableName}' " +
                    "to RSA XML content or an absolute path outside the repository.");
            }

            string privateKeyXml = secret.Trim();
            if (!privateKeyXml.StartsWith("<", StringComparison.Ordinal))
            {
                string path = Path.GetFullPath(privateKeyXml);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(
                        $"Manifest private key file from '{variableName}' does not exist: {path}",
                        path);
                }

                if (IsPathInsideProject(path))
                {
                    throw new InvalidOperationException(
                        $"Manifest private key must not be stored inside the project repository: {path}");
                }

                privateKeyXml = File.ReadAllText(path).Trim();
            }

            RSAParameters privateParameters = ParseRsaXml(privateKeyXml, true);
            if (privateParameters.Modulus == null || privateParameters.Modulus.Length < 256)
            {
                throw new InvalidOperationException("Manifest signing RSA private key must be at least 2048 bits.");
            }

            var configuredPublicKey = CreatePublicKey(profile);
            if (!AssemblyManifestSignatureUtility.TryDecodePublicKey(
                    configuredPublicKey,
                    out var configuredParameters,
                    out var publicKeyError))
            {
                throw new InvalidOperationException(publicKeyError);
            }

            if (!AreEqual(privateParameters.Modulus, configuredParameters.Modulus) ||
                !AreEqual(privateParameters.Exponent, configuredParameters.Exponent))
            {
                throw new InvalidOperationException(
                    $"Manifest private key does not match the packaged public key. KeyId={profile.ManifestSigningKeyId}");
            }

            var rsa = RSA.Create();
            rsa.ImportParameters(privateParameters);
            return rsa;
        }

        private static RSAParameters ParseRsaXml(string xml, bool requirePrivateKey)
        {
            try
            {
                var document = new XmlDocument { XmlResolver = null };
                document.LoadXml(xml);
                var root = document.DocumentElement;
                if (root == null || root.Name != "RSAKeyValue")
                {
                    throw new FormatException("RSA XML root must be RSAKeyValue.");
                }

                var parameters = new RSAParameters
                {
                    Modulus = ReadRequiredNode(root, "Modulus"),
                    Exponent = ReadRequiredNode(root, "Exponent"),
                    P = ReadOptionalNode(root, "P"),
                    Q = ReadOptionalNode(root, "Q"),
                    DP = ReadOptionalNode(root, "DP"),
                    DQ = ReadOptionalNode(root, "DQ"),
                    InverseQ = ReadOptionalNode(root, "InverseQ"),
                    D = ReadOptionalNode(root, "D")
                };

                if (requirePrivateKey &&
                    (parameters.D == null || parameters.P == null || parameters.Q == null))
                {
                    throw new FormatException("RSA XML does not contain private key parameters.");
                }

                return parameters;
            }
            catch (Exception exception) when (!(exception is InvalidOperationException))
            {
                throw new InvalidOperationException("Manifest RSA XML is invalid. " + exception.Message, exception);
            }
        }

        private static byte[] ReadRequiredNode(XmlElement root, string name)
        {
            byte[] value = ReadOptionalNode(root, name);
            if (value == null || value.Length == 0)
            {
                throw new FormatException($"RSA XML node is missing: {name}");
            }

            return value;
        }

        private static byte[] ReadOptionalNode(XmlElement root, string name)
        {
            var node = root.SelectSingleNode(name);
            return node == null || string.IsNullOrWhiteSpace(node.InnerText)
                ? null
                : Convert.FromBase64String(node.InnerText.Trim());
        }

        private static bool IsPathInsideProject(string path)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, ".."))
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string fullPath = Path.GetFullPath(path);
            return fullPath.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase);
        }

        private static HotfixManifestPublicKey CreatePublicKey(HotfixReleaseProfile profile)
        {
            return new HotfixManifestPublicKey
            {
                KeyId = profile.ManifestSigningKeyId == null ? string.Empty : profile.ManifestSigningKeyId.Trim(),
                Modulus = profile.ManifestPublicKeyModulus == null ? string.Empty : profile.ManifestPublicKeyModulus.Trim(),
                Exponent = profile.ManifestPublicKeyExponent == null ? string.Empty : profile.ManifestPublicKeyExponent.Trim()
            };
        }

        private static void ApplySignatureIdentity(AOTAssemblyManifest manifest, string keyId)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            manifest.SignatureVersion = AssemblyManifestSignatureUtility.CurrentSignatureVersion;
            manifest.SignatureAlgorithm = AssemblyManifestSignatureUtility.RsaSha256Algorithm;
            manifest.SigningKeyId = keyId.Trim();
            manifest.Signature = string.Empty;
        }

        private static void ApplySignatureIdentity(HotfixAssemblyManifest manifest, string keyId)
        {
            if (manifest == null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            manifest.SignatureVersion = AssemblyManifestSignatureUtility.CurrentSignatureVersion;
            manifest.SignatureAlgorithm = AssemblyManifestSignatureUtility.RsaSha256Algorithm;
            manifest.SigningKeyId = keyId.Trim();
            manifest.Signature = string.Empty;
        }

        private static void VerifyGeneratedSignature(AOTAssemblyManifest manifest, HotfixReleaseProfile profile)
        {
            if (!AssemblyManifestSignatureUtility.Verify(manifest, CreatePublicKey(profile), out var error))
            {
                throw new InvalidOperationException("Generated AOT manifest signature failed self-verification. " + error);
            }
        }

        private static void VerifyGeneratedSignature(HotfixAssemblyManifest manifest, HotfixReleaseProfile profile)
        {
            if (!AssemblyManifestSignatureUtility.Verify(manifest, CreatePublicKey(profile), out var error))
            {
                throw new InvalidOperationException("Generated Hotfix manifest signature failed self-verification. " + error);
            }
        }

        private static void ClearSignature(AOTAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                return;
            }

            manifest.SignatureVersion = 0;
            manifest.SignatureAlgorithm = string.Empty;
            manifest.SigningKeyId = string.Empty;
            manifest.Signature = string.Empty;
            EditorUtility.SetDirty(manifest);
        }

        private static void ClearSignature(HotfixAssemblyManifest manifest)
        {
            if (manifest == null)
            {
                return;
            }

            manifest.SignatureVersion = 0;
            manifest.SignatureAlgorithm = string.Empty;
            manifest.SigningKeyId = string.Empty;
            manifest.Signature = string.Empty;
            EditorUtility.SetDirty(manifest);
        }

        private static bool AreEqual(byte[] left, byte[] right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }

            int difference = 0;
            for (int index = 0; index < left.Length; index++)
            {
                difference |= left[index] ^ right[index];
            }

            return difference == 0;
        }
    }
}
