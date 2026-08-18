using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Framework.Luban.Editor
{
    public sealed class LubanCommand
    {
        public string FileName;
        public string Arguments;
        public string WorkingDirectory;
        public string DisplayText => FileName + " " + Arguments;
    }

    public static class LubanCommandBuilder
    {
        public static List<LubanCommand> Build(string projectRoot, LubanBuildProfile profile, LubanBuildTask task)
        {
            string dll = Quote(LubanTableScanner.Resolve(projectRoot, profile.LubanDllPath));
            string conf = Quote(LubanTableScanner.Resolve(projectRoot, task.ConfigPath));
            var selectedTables = task.Tables.Where(item => item.Enabled && !string.IsNullOrWhiteSpace(item.OutputTable)).ToArray();
            if (task.Tables.Count > 0 && selectedTables.Length == 0)
                throw new InvalidOperationException($"Task '{task.Name}' has no selected table with a valid OutputTable.");
            var args = new List<string> { dll, "-t", Quote(task.Target), "--conf", conf };
            if (task.GenerateCode && !string.IsNullOrWhiteSpace(task.CodeTarget)) { args.Add("-c"); args.Add(Quote(task.CodeTarget)); }
            if (task.GenerateData && !string.IsNullOrWhiteSpace(task.DataTarget)) { args.Add("-d"); args.Add(Quote(task.DataTarget)); }
            if (task.ValidationFailAsError) args.Add("--validationFailAsError");
            foreach (var table in selectedTables) { args.Add("-o"); args.Add(Quote(table.OutputTable.Trim())); }
            if (task.GenerateCode) { args.Add("-x"); args.Add(Quote(task.CodeTarget + ".outputCodeDir=" + LubanTableScanner.Resolve(projectRoot, task.CodeOutputDirectory))); }
            if (task.GenerateData) { args.Add("-x"); args.Add(Quote(task.DataTarget + ".outputDataDir=" + LubanTableScanner.Resolve(projectRoot, task.DataOutputDirectory))); }
            return new List<LubanCommand> { new LubanCommand { FileName = DotNetHostResolver.ResolveOrThrow(), Arguments = string.Join(" ", args), WorkingDirectory = Path.GetDirectoryName(LubanTableScanner.Resolve(projectRoot, task.ConfigPath)) } };
        }

        private static string Quote(string value) => "\"" + (value ?? string.Empty).Replace("\"", "\\\"") + "\"";
    }
}
