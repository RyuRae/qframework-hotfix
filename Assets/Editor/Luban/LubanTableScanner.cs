using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Framework.Luban.Editor
{
    public static class LubanTableScanner
    {
        private static readonly string[] Extensions = { ".xlsx", ".xlsm", ".csv", ".json", ".xml" };

        public static List<LubanTableSelection> Scan(string projectRoot, LubanBuildTask task)
        {
            string root = Resolve(projectRoot, task.DataSourceRoot);
            if (!Directory.Exists(root)) return new List<LubanTableSelection>();
            var previous = task.Tables.ToDictionary(item => Normalize(item.InputPath), StringComparer.OrdinalIgnoreCase);
            return Directory.GetFiles(root, "*", SearchOption.AllDirectories)
                .Where(path => Extensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase))
                .Where(path => !Path.GetFileName(path).StartsWith("__", StringComparison.Ordinal))
                .Select(path =>
                {
                    string relative = MakeRelative(projectRoot, path);
                    if (previous.TryGetValue(Normalize(relative), out var existing)) return existing;
                    return new LubanTableSelection
                    {
                        Enabled = true,
                        Name = Path.GetFileNameWithoutExtension(path).TrimStart('#'),
                        InputPath = relative,
                        OutputTable = GuessTableName(Path.GetFileNameWithoutExtension(path).TrimStart('#'))
                    };
                }).OrderBy(item => item.InputPath, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static string Resolve(string projectRoot, string path) => Path.IsPathRooted(path ?? string.Empty) ? path : Path.GetFullPath(Path.Combine(projectRoot, path ?? string.Empty));
        public static string MakeRelative(string projectRoot, string path) => new Uri(AppendSlash(projectRoot)).MakeRelativeUri(new Uri(path)).ToString().Replace('/', Path.DirectorySeparatorChar);
        private static string AppendSlash(string path) => path.EndsWith(Path.DirectorySeparatorChar.ToString()) ? path : path + Path.DirectorySeparatorChar;
        private static string Normalize(string path) => (path ?? string.Empty).Replace('\\', '/');
        private static string GuessTableName(string sourceName)
        {
            var parts = (sourceName ?? string.Empty).Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return "Tb" + string.Concat(parts.Select(part => char.ToUpperInvariant(part[0]) + part.Substring(1)));
        }
    }
}
