using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HybridCLR.Editor
{
    public enum HotfixBuildReportSeverity
    {
        Info,
        Warning,
        Error
    }

    public sealed class HotfixBuildReportItem
    {
        public readonly HotfixBuildReportSeverity Severity;
        public readonly string Label;
        public readonly string Value;
        public readonly string Message;

        public HotfixBuildReportItem(
            HotfixBuildReportSeverity severity,
            string label,
            string value,
            string message)
        {
            Severity = severity;
            Label = label;
            Value = value;
            Message = message;
        }
    }

    public sealed class HotfixBuildReport
    {
        private readonly List<HotfixBuildReportItem> mItems = new List<HotfixBuildReportItem>();

        public IReadOnlyList<HotfixBuildReportItem> Items => mItems;
        public bool HasErrors => mItems.Any(item => item.Severity == HotfixBuildReportSeverity.Error);
        public int ErrorCount => mItems.Count(item => item.Severity == HotfixBuildReportSeverity.Error);
        public int WarningCount => mItems.Count(item => item.Severity == HotfixBuildReportSeverity.Warning);
        public int InfoCount => mItems.Count(item => item.Severity == HotfixBuildReportSeverity.Info);

        public int PassedCount => InfoCount;

        public void AddInfo(string label, string value, string message = "")
        {
            Add(HotfixBuildReportSeverity.Info, label, value, message);
        }

        public void AddWarning(string label, string value, string message)
        {
            Add(HotfixBuildReportSeverity.Warning, label, value, message);
        }

        public void AddError(string label, string value, string message)
        {
            Add(HotfixBuildReportSeverity.Error, label, value, message);
        }

        public string BuildErrorSummary()
        {
            var builder = new StringBuilder();
            foreach (var item in mItems.Where(item => item.Severity == HotfixBuildReportSeverity.Error))
            {
                builder.Append("- ");
                builder.Append(item.Label);
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    builder.Append(": ");
                    builder.Append(item.Value);
                }

                if (!string.IsNullOrWhiteSpace(item.Message))
                {
                    builder.Append(" - ");
                    builder.Append(item.Message);
                }

                builder.AppendLine();
            }

            return builder.ToString().TrimEnd();
        }

        private void Add(
            HotfixBuildReportSeverity severity,
            string label,
            string value,
            string message)
        {
            mItems.Add(new HotfixBuildReportItem(
                severity,
                label ?? string.Empty,
                value ?? string.Empty,
                message ?? string.Empty));
        }
    }
}
