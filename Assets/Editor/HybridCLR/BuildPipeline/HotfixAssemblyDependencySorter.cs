using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Framework;
using Framework.Assemblies;

namespace HybridCLR.Editor
{
    public sealed class HotfixAssemblyDependencySortResult
    {
        public List<string> SortedAssemblies = new List<string>();
        public List<AssemblyDependencyRecord> Dependencies = new List<AssemblyDependencyRecord>();
    }

    public static class HotfixAssemblyDependencySorter
    {
        public static HotfixAssemblyDependencySortResult Sort(string hotfixCodesPath, IEnumerable<string> hotfixAssemblies)
        {
            string folder = (hotfixCodesPath ?? string.Empty).Replace('\\', '/').Trim();
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Hotfix DLL folder not found: {folder}");
            }

            var expectedDlls = HotfixUtility.NormalizeAssemblyNames(hotfixAssemblies)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (expectedDlls.Count == 0)
            {
                throw new InvalidOperationException("No Hotfix DLLs configured for dependency sorting.");
            }

            ValidateNoUnexpectedFiles(folder, expectedDlls);

            var nodes = expectedDlls
                .Select(dllName => ReadNode(folder, dllName))
                .ToList();
            ValidateDuplicateAssemblyNames(nodes);

            var assemblyToNode = nodes.ToDictionary(node => node.AssemblyName, StringComparer.OrdinalIgnoreCase);
            foreach (var node in nodes)
            {
                node.InternalDependencies = node.ReferencedAssemblyNames
                    .Where(assemblyToNode.ContainsKey)
                    .Select(assemblyName => assemblyToNode[assemblyName].DllName)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }

            var nodeByDll = nodes.ToDictionary(node => node.DllName, StringComparer.OrdinalIgnoreCase);
            var sorted = TopologicalSort(nodes, nodeByDll);
            return new HotfixAssemblyDependencySortResult
            {
                SortedAssemblies = sorted.Select(node => node.DllName).ToList(),
                Dependencies = sorted.Select(node => new AssemblyDependencyRecord
                {
                    AssemblyName = node.AssemblyName,
                    DllName = node.DllName,
                    DependsOn = new List<string>(node.InternalDependencies)
                }).ToList()
            };
        }

        public static string FormatLoadingOrder(IEnumerable<string> dllNames)
        {
            var names = HotfixUtility.NormalizeAssemblyNames(dllNames);
            return names.Count == 0 ? "empty" : string.Join(" -> ", names);
        }

        public static string FormatDependencies(IEnumerable<AssemblyDependencyRecord> records)
        {
            var lines = new List<string>();
            foreach (var record in records ?? Enumerable.Empty<AssemblyDependencyRecord>())
            {
                if (record == null)
                {
                    continue;
                }

                string dependsOn = record.DependsOn == null || record.DependsOn.Count == 0
                    ? "none"
                    : string.Join(", ", record.DependsOn);
                lines.Add($"{record.DllName} depends on {dependsOn}");
            }

            return lines.Count == 0 ? "empty" : string.Join("; ", lines);
        }

        private static void ValidateNoUnexpectedFiles(string folder, List<string> expectedDlls)
        {
            var expected = new HashSet<string>(expectedDlls, StringComparer.OrdinalIgnoreCase);
            var actual = Directory.GetFiles(folder, "*.dll.bytes", SearchOption.TopDirectoryOnly)
                .Select(path => Path.GetFileNameWithoutExtension(Path.GetFileName(path)))
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToList();

            foreach (var dllName in expected)
            {
                string filePath = GetDllBytesPath(folder, dllName);
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Hotfix DLL recorded in manifest but file does not exist: {dllName}", filePath);
                }
            }

            var unexpected = actual
                .Where(dllName => !expected.Contains(dllName))
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();
            if (unexpected.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Hotfix DLL file exists but is not recorded in manifest input: {string.Join(", ", unexpected)}");
            }
        }

        private static AssemblyNode ReadNode(string folder, string dllName)
        {
            string filePath = GetDllBytesPath(folder, dllName);
            Assembly assembly = LoadMetadataAssembly(filePath);
            var assemblyName = assembly.GetName().Name;
            var references = assembly.GetReferencedAssemblies()
                .Select(name => name.Name)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return new AssemblyNode
            {
                DllName = dllName,
                AssemblyName = assemblyName,
                ReferencedAssemblyNames = references
            };
        }

        private static Assembly LoadMetadataAssembly(string filePath)
        {
            var reflectionOnlyLoadFrom = typeof(Assembly).GetMethod(
                "ReflectionOnlyLoadFrom",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new[] { typeof(string) },
                null);
            if (reflectionOnlyLoadFrom != null)
            {
                try
                {
                    return (Assembly)reflectionOnlyLoadFrom.Invoke(null, new object[] { filePath });
                }
                catch (TargetInvocationException)
                {
                    // Some editor runtimes reject non-.dll extensions for reflection-only loading.
                    // Loading from bytes still lets us inspect the assembly metadata during the build.
                }
            }

            return Assembly.Load(File.ReadAllBytes(filePath));
        }

        private static void ValidateDuplicateAssemblyNames(List<AssemblyNode> nodes)
        {
            var duplicated = nodes
                .GroupBy(node => node.AssemblyName, StringComparer.OrdinalIgnoreCase)
                .Where(group => group.Count() > 1)
                .Select(group => $"{group.Key}: {string.Join(", ", group.Select(node => node.DllName))}")
                .ToList();
            if (duplicated.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Duplicate Hotfix AssemblyName detected. {string.Join("; ", duplicated)}");
            }
        }

        private static List<AssemblyNode> TopologicalSort(
            List<AssemblyNode> nodes,
            Dictionary<string, AssemblyNode> nodeByDll)
        {
            var sorted = new List<AssemblyNode>();
            var visiting = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var stack = new Stack<string>();

            foreach (var node in nodes.OrderBy(node => node.DllName, StringComparer.OrdinalIgnoreCase))
            {
                Visit(node, nodeByDll, visiting, visited, stack, sorted);
            }

            return sorted;
        }

        private static void Visit(
            AssemblyNode node,
            Dictionary<string, AssemblyNode> nodeByDll,
            HashSet<string> visiting,
            HashSet<string> visited,
            Stack<string> stack,
            List<AssemblyNode> sorted)
        {
            if (visited.Contains(node.DllName))
            {
                return;
            }

            if (visiting.Contains(node.DllName))
            {
                throw new InvalidOperationException($"Circular Hotfix DLL dependency detected: {FormatCycle(stack, node.DllName)}");
            }

            visiting.Add(node.DllName);
            stack.Push(node.DllName);

            foreach (var dependencyDll in node.InternalDependencies)
            {
                if (!nodeByDll.TryGetValue(dependencyDll, out var dependency))
                {
                    throw new InvalidOperationException(
                        $"Missing Hotfix DLL dependency. {node.DllName} depends on {dependencyDll}, but it is not present in the hotfix DLL list.");
                }

                Visit(dependency, nodeByDll, visiting, visited, stack, sorted);
            }

            stack.Pop();
            visiting.Remove(node.DllName);
            visited.Add(node.DllName);
            sorted.Add(node);
        }

        private static string FormatCycle(Stack<string> stack, string repeatedDll)
        {
            var chain = stack.Reverse().ToList();
            int start = chain.FindIndex(name => string.Equals(name, repeatedDll, StringComparison.OrdinalIgnoreCase));
            if (start >= 0)
            {
                chain = chain.Skip(start).ToList();
            }

            chain.Add(repeatedDll);
            return string.Join(" -> ", chain);
        }

        private static string GetDllBytesPath(string folder, string dllName)
        {
            return Path.Combine(folder, $"{dllName}.bytes");
        }

        private sealed class AssemblyNode
        {
            public string DllName;
            public string AssemblyName;
            public List<string> ReferencedAssemblyNames = new List<string>();
            public List<string> InternalDependencies = new List<string>();
        }
    }
}
