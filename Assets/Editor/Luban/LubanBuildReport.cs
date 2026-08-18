using System;
using System.Collections.Generic;

namespace Framework.Luban.Editor
{
    [Serializable]
    public sealed class LubanTaskBuildResult
    {
        public string TaskName;
        public bool Succeeded;
        public int ExitCode;
        public double DurationSeconds;
        public string Command;
        public string Output;
    }

    [Serializable]
    public sealed class LubanBuildReport
    {
        public DateTime StartedAt;
        public DateTime FinishedAt;
        public bool Succeeded;
        public bool Canceled;
        public readonly List<LubanTaskBuildResult> Tasks = new List<LubanTaskBuildResult>();
        public double DurationSeconds => (FinishedAt - StartedAt).TotalSeconds;
    }
}
