using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QFramework;

namespace Framework
{
    public static class HotfixCodeEntryInvoker
    {
        public static bool TryCreateEntry(string entryTypeName, out IHotfixEntry entry, out string error)
        {
            entry = null;
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(entryTypeName))
            {
                error = "Hotfix entry type name is empty.";
                return false;
            }

            var entryType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(entryTypeName, false))
                .FirstOrDefault(type => type != null) ?? Type.GetType(entryTypeName, false);
            if (entryType == null)
            {
                error = $"Hotfix entry type not found: {entryTypeName}";
                return false;
            }

            if (!typeof(IHotfixEntry).IsAssignableFrom(entryType))
            {
                error = $"Hotfix entry type must implement {typeof(IHotfixEntry).FullName}: {entryType.FullName}";
                return false;
            }

            if (entryType.IsAbstract || entryType.IsInterface)
            {
                error = $"Hotfix entry type must be a concrete class: {entryType.FullName}";
                return false;
            }

            if (entryType.GetConstructor(Type.EmptyTypes) == null)
            {
                error = $"Hotfix entry type requires a public parameterless constructor: {entryType.FullName}";
                return false;
            }

            try
            {
                entry = Activator.CreateInstance(entryType) as IHotfixEntry;
                if (entry == null)
                {
                    error = $"Can not create hotfix entry instance: {entryType.FullName}";
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                error = $"Create hotfix entry failed: {entryType.FullName}.\n{GetRootException(exception)}";
                return false;
            }
        }

        public static async Task StartAsync(
            IHotfixEntry entry,
            HotfixContext context,
            CancellationToken cancellationToken)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            cancellationToken.ThrowIfCancellationRequested();
            Task startupTask = entry.StartAsync(context);
            if (startupTask == null)
            {
                throw new InvalidOperationException($"{entry.GetType().FullName}.StartAsync returned null.");
            }

            await startupTask;
            cancellationToken.ThrowIfCancellationRequested();
            LogKit.I($"[HotfixCodeEntryInvoker] {entry.GetType().FullName}.StartAsync completed.");
        }

        public static Exception GetRootException(Exception exception)
        {
            while (exception is AggregateException aggregateException &&
                   aggregateException.InnerExceptions.Count == 1)
            {
                exception = aggregateException.InnerExceptions[0];
            }

            return exception;
        }

        public static void ObserveFailure(Task task)
        {
            if (task == null || task.IsCompletedSuccessfully || task.IsCanceled)
            {
                return;
            }

            if (task.IsFaulted)
            {
                _ = task.Exception;
                return;
            }

            _ = task.ContinueWith(
                completedTask => { _ = completedTask.Exception; },
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
        }
    }
}
