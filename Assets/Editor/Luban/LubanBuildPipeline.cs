using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Framework.Luban.Editor
{
    public static class LubanBuildPipeline
    {
        private static readonly Queue<Tuple<LubanBuildTask, LubanCommand>> Pending = new Queue<Tuple<LubanBuildTask, LubanCommand>>();
        private static Process process;
        private static Stopwatch stopwatch;
        private static LubanBuildReport report;
        private static LubanBuildProfile profile;
        private static LubanBuildTask currentTask;
        private static LubanCommand currentCommand;
        private static string stdout = string.Empty;
        private static string stderr = string.Empty;

        public static bool IsRunning => process != null;
        public static LubanBuildReport LastReport { get; private set; }
        public static event Action Changed;

        public static void Generate(LubanBuildProfile selectedProfile, IEnumerable<LubanBuildTask> tasks)
        {
            if (IsRunning) throw new InvalidOperationException("A Luban build is already running.");
            profile = selectedProfile ?? throw new ArgumentNullException(nameof(selectedProfile));
            string root = Directory.GetParent(Application.dataPath).FullName;
            if (profile.ValidateBeforeGenerate) LubanProfileUtility.ValidateOrThrow(root, profile);
            Pending.Clear();
            foreach (var task in tasks)
                foreach (var command in LubanCommandBuilder.Build(root, profile, task))
                    Pending.Enqueue(Tuple.Create(task, command));
            if (Pending.Count == 0) throw new BuildFailedException("No enabled Luban task was selected.");
            report = new LubanBuildReport { StartedAt = DateTime.Now };
            EditorApplication.update += Update;
            StartNext();
        }

        public static void Cancel()
        {
            if (!IsRunning) return;
            report.Canceled = true;
            try { process.Kill(); } catch { }
            Finish(false);
        }

        private static void StartNext()
        {
            if (Pending.Count == 0) { Finish(true); return; }
            var item = Pending.Dequeue();
            currentTask = item.Item1;
            currentCommand = item.Item2;
            stdout = string.Empty;
            stderr = string.Empty;
            stopwatch = Stopwatch.StartNew();
            var info = new ProcessStartInfo(currentCommand.FileName, currentCommand.Arguments)
            {
                WorkingDirectory = currentCommand.WorkingDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            process = new Process { StartInfo = info, EnableRaisingEvents = true };
            process.OutputDataReceived += (_, args) => { if (args.Data != null) stdout += args.Data + "\n"; };
            process.ErrorDataReceived += (_, args) => { if (args.Data != null) stderr += args.Data + "\n"; };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            Changed?.Invoke();
        }

        private static void Update()
        {
            if (process == null || !process.HasExited) return;
            process.WaitForExit();
            stopwatch.Stop();
            int exitCode = process.ExitCode;
            report.Tasks.Add(new LubanTaskBuildResult
            {
                TaskName = currentTask.Name,
                Succeeded = exitCode == 0,
                ExitCode = exitCode,
                DurationSeconds = stopwatch.Elapsed.TotalSeconds,
                Command = currentCommand.DisplayText,
                Output = stdout + stderr
            });
            process.Dispose();
            process = null;
            if (exitCode != 0) { Finish(false); return; }
            if (profile.CleanupStaleOutputs) CleanupDisabledTableOutputs(currentTask);
            StartNext();
        }

        private static void Finish(bool succeeded)
        {
            EditorApplication.update -= Update;
            process?.Dispose();
            process = null;
            Pending.Clear();
            report.Succeeded = succeeded && !report.Canceled;
            report.FinishedAt = DateTime.Now;
            LastReport = report;
            if (report.Succeeded)
            {
                try
                {
                    if (profile.SyncLocalizationCollectors)
                        HybridCLR.Editor.LocalizationContentSynchronizer.SyncOrThrow();
                    if (profile.RefreshAssetDatabase) AssetDatabase.Refresh();
                }
                catch (Exception exception)
                {
                    report.Succeeded = false;
                    report.Tasks.Add(new LubanTaskBuildResult { TaskName = "PostProcess", Output = exception.ToString(), ExitCode = -1 });
                }
            }
            Debug.Log(report.Succeeded ? $"Luban build completed in {report.DurationSeconds:F2}s." : "Luban build failed or canceled.");
            Changed?.Invoke();
        }

        private static void CleanupDisabledTableOutputs(LubanBuildTask task)
        {
            if (!task.GenerateData || string.IsNullOrWhiteSpace(task.DataOutputDirectory)) return;
            string root = Directory.GetParent(Application.dataPath).FullName;
            string output = LubanTableScanner.Resolve(root, task.DataOutputDirectory);
            string extension = string.Equals(task.DataTarget, "bin", StringComparison.OrdinalIgnoreCase) ? ".bytes" : "." + task.DataTarget;
            foreach (var table in task.Tables)
            {
                if (table.Enabled || string.IsNullOrWhiteSpace(table.OutputTable)) continue;
                string path = Path.Combine(output, table.OutputTable.Trim().ToLowerInvariant() + extension);
                if (File.Exists(path)) FileUtil.DeleteFileOrDirectory(path.Replace('\\', '/'));
                if (File.Exists(path + ".meta")) FileUtil.DeleteFileOrDirectory((path + ".meta").Replace('\\', '/'));
            }
        }
    }
}
