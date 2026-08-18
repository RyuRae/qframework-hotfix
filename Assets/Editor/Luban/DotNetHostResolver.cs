using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEngine;

namespace Framework.Luban.Editor
{
    /// <summary>解析 Unity Editor 可用的 dotnet 主机，避免 Hub 启动时缺少终端 PATH。</summary>
    public static class DotNetHostResolver
    {
        public static string ResolveOrThrow()
        {
            foreach (string candidate in GetCandidates())
            {
                if (!string.IsNullOrWhiteSpace(candidate) && File.Exists(candidate))
                    return Path.GetFullPath(candidate);
            }
            throw new BuildFailedException(
                "dotnet executable was not found. Install .NET Runtime/SDK, or set DOTNET_HOST_PATH/DOTNET_ROOT before launching Unity. " +
                "Unity launched from Hub may not inherit the terminal PATH.");
        }

        private static IEnumerable<string> GetCandidates()
        {
            string host = Environment.GetEnvironmentVariable("DOTNET_HOST_PATH");
            if (!string.IsNullOrWhiteSpace(host)) yield return host;
            string root = Environment.GetEnvironmentVariable("DOTNET_ROOT");
            if (!string.IsNullOrWhiteSpace(root)) yield return Path.Combine(root, ExecutableName);
#if UNITY_EDITOR_OSX
            yield return "/usr/local/share/dotnet/dotnet";
            yield return "/opt/homebrew/bin/dotnet";
            yield return "/usr/local/bin/dotnet";
#elif UNITY_EDITOR_WIN
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            yield return Path.Combine(programFiles, "dotnet", "dotnet.exe");
#else
            yield return "/usr/bin/dotnet";
            yield return "/usr/local/bin/dotnet";
            yield return "/usr/share/dotnet/dotnet";
#endif
        }

        private static string ExecutableName => Application.platform == RuntimePlatform.WindowsEditor ? "dotnet.exe" : "dotnet";
    }
}
