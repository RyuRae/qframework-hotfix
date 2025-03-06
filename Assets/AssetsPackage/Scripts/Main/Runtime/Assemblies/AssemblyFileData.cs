using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MsbFramework.Assemblies
{
	public class AssemblyFileData
	{
		public string Name;
		public long StartPosition;
		public long EndPosition;

		public AssemblyFileData(string name, long startPosition, long endPosition)
		{
			Name = name;
			StartPosition = startPosition;
			EndPosition = endPosition;
		}

		public static string GetMergedFileName()
		{
			return "Merged.dll";
		}

		public static string GetSeparator()
		{
			return "\n---------------------\n";
		}
	}
}