using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QFramework;

namespace Framework
{
    /// <summary>
    /// 热更入口反射创建与异步调用适配器，统一处理类型校验、取消、异常展开和失败观察。
    /// </summary>
    public static class HotfixCodeEntryInvoker
    {
        /// <summary>
        /// 从已加载程序集查找并实例化指定的 IHotfixEntry 实现。
        /// </summary>
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

        /// <summary>
        /// 调用业务入口并等待其真正启动完成；空 Task、取消和业务异常都会向上抛出。
        /// </summary>
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

        /// <summary>
        /// 调用热更层可选的资源预加载器，并等待配置与关键资源准备完成。
        /// </summary>
        public static async Task PreloadAsync(
            IHotfixResourcePreloader preloader,
            HotfixContext context,
            IProgress<HotfixPreloadProgress> progress,
            CancellationToken cancellationToken)
        {
            if (preloader == null)
            {
                throw new ArgumentNullException(nameof(preloader));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            cancellationToken.ThrowIfCancellationRequested();
            Task preloadTask = preloader.PreloadAsync(context, progress);
            if (preloadTask == null)
            {
                throw new InvalidOperationException($"{preloader.GetType().FullName}.PreloadAsync returned null.");
            }

            await preloadTask;
            cancellationToken.ThrowIfCancellationRequested();
            LogKit.I($"[HotfixCodeEntryInvoker] {preloader.GetType().FullName}.PreloadAsync completed.");
        }

        /// <summary>
        /// 展开只有一个内部异常的 AggregateException，返回便于展示的根异常。
        /// </summary>
        public static Exception GetRootException(Exception exception)
        {
            while (exception is AggregateException aggregateException &&
                   aggregateException.InnerExceptions.Count == 1)
            {
                exception = aggregateException.InnerExceptions[0];
            }

            return exception;
        }

        /// <summary>
        /// 对流程已取消等待的 Task 继续观察异常，防止产生未观察任务异常。
        /// </summary>
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
