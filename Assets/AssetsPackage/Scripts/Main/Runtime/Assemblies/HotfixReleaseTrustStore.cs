using System;
using UnityEngine;

namespace Framework.Assemblies
{
    /// <summary>
    /// 记录业务启动成功过的最高正式发布序号。Manifest 验签解决伪造，本记录解决合法旧包重放。
    /// 序号只在 IHotfixEntry.StartAsync 成功且 LastGood 可提交后推进。
    /// </summary>
    public static class HotfixReleaseTrustStore
    {
        private const string SequenceKey = "Hotfix.Trust.HighestReleaseSequence";
        private const string VersionKey = "Hotfix.Trust.HighestReleaseVersion";

        public static bool TryValidate(long releaseSequence, string releaseVersion, out string error)
        {
            error = string.Empty;
            if (releaseSequence <= 0)
            {
                error = $"Signed Hotfix manifest ReleaseSequence must be greater than zero. Current={releaseSequence}";
                return false;
            }

            long highestSequence = ReadHighestSequence();
            if (releaseSequence < highestSequence)
            {
                error = $"Hotfix rollback rejected. Manifest sequence={releaseSequence}, " +
                        $"highest accepted sequence={highestSequence}.";
                return false;
            }

            if (releaseSequence == highestSequence && highestSequence > 0)
            {
                string highestVersion = PlayerPrefs.GetString(VersionKey, string.Empty);
                if (!string.IsNullOrWhiteSpace(highestVersion) &&
                    !string.Equals(highestVersion, releaseVersion, StringComparison.Ordinal))
                {
                    error = $"Hotfix release identity conflict. Sequence={releaseSequence}, " +
                            $"manifest version={releaseVersion}, accepted version={highestVersion}.";
                    return false;
                }
            }

            return true;
        }

        public static bool TryCommit(
            long releaseSequence,
            string releaseVersion,
            Func<bool> commitLastGood,
            out string error)
        {
            if (!TryValidate(releaseSequence, releaseVersion, out error))
            {
                return false;
            }

            long highestSequence = ReadHighestSequence();
            if (releaseSequence == highestSequence)
            {
                if (commitLastGood == null || !commitLastGood())
                {
                    error = "Persist LastGood record failed.";
                    return false;
                }

                try
                {
                    PlayerPrefs.Save();
                    return true;
                }
                catch (Exception exception)
                {
                    error = $"Persist LastGood record failed. {exception.Message}";
                    return false;
                }
            }

            string previousSequence = PlayerPrefs.GetString(SequenceKey, string.Empty);
            string previousVersion = PlayerPrefs.GetString(VersionKey, string.Empty);
            try
            {
                PlayerPrefs.SetString(SequenceKey, releaseSequence.ToString(System.Globalization.CultureInfo.InvariantCulture));
                PlayerPrefs.SetString(VersionKey, releaseVersion == null ? string.Empty : releaseVersion.Trim());
                if (commitLastGood == null || !commitLastGood())
                {
                    Restore(SequenceKey, previousSequence);
                    Restore(VersionKey, previousVersion);
                    error = "Persist LastGood record failed.";
                    return false;
                }

                PlayerPrefs.Save();
                return true;
            }
            catch (Exception exception)
            {
                Restore(SequenceKey, previousSequence);
                Restore(VersionKey, previousVersion);
                error = $"Persist highest accepted hotfix release failed. {exception.Message}";
                return false;
            }
        }

        private static void Restore(string key, string previousValue)
        {
            if (string.IsNullOrEmpty(previousValue))
            {
                PlayerPrefs.DeleteKey(key);
            }
            else
            {
                PlayerPrefs.SetString(key, previousValue);
            }
        }

        private static long ReadHighestSequence()
        {
            string value = PlayerPrefs.GetString(SequenceKey, string.Empty);
            return long.TryParse(
                value,
                System.Globalization.NumberStyles.None,
                System.Globalization.CultureInfo.InvariantCulture,
                out var sequence)
                ? Math.Max(0, sequence)
                : 0;
        }
    }
}
