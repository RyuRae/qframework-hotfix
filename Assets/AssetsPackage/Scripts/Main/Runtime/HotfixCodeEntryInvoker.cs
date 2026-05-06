

using System;
using System.Linq;
using System.Reflection;
using QFramework;

namespace Framework
{
    public class HotfixCodeEntryInvoker
    {
        public static bool InvokeEntryMethod(string EntryTypeName, string EntryMethodName, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(EntryTypeName) ||
                string.IsNullOrWhiteSpace(EntryMethodName))
            {
                error = "Hotfix entry type or method name is null.";
                return false;
            }

            var entryType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(assembly => assembly.GetType(EntryTypeName))
                .FirstOrDefault(type => type != null) ?? Type.GetType(EntryTypeName);
            if (entryType == null)
            {
               
                error = $"Hotfix entry type not found: {EntryTypeName}";
                return false;
            }

            var entryMethod = entryType.GetMethod(
                EntryMethodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
                null,
                Type.EmptyTypes,
                null);
            if (entryMethod == null)
            {
                error = $"Hotfix entry method not found: {EntryTypeName}.{EntryMethodName}()";
                return false;
            }

            if (entryMethod.ReturnType != typeof(void))
            {
                error = $"Hotfix entry method must return void: {EntryTypeName}.{EntryMethodName}()";
                return false;
            }

            try
            {
                entryMethod.Invoke(null, null);
                LogKit.I($"[HotfixCodeEntryInvoker] Invoked {EntryTypeName}.{EntryMethodName}().");
                return true;
            }
            catch (TargetInvocationException exception)
            {
                error = $"Invoke Hotfix CodeEntry failed: {EntryTypeName}.{EntryMethodName}().\n{exception.InnerException ?? exception}";
                return false;
            }
            catch (Exception exception)
            {
                error = $"Invoke Hotfix CodeEntry failed: {EntryTypeName}.{EntryMethodName}().\n{exception}";
                return false;
            }
        }
    }
}
