using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace MsbFramework.Utils
{
    public class PathUtils
    {
        /// <summary>
        /// AOT程序集文件资源地址
        /// </summary>
        public static string AOTAssemblyTextAssetPath
        {
            get { return Path.Combine(Application.dataPath, "..", AOTCodesPath); }
        }


        public static string AOTCodesPath = "Assets/AssetsPackage/AssetsHotFix/AOTCodes";

        /// <summary>
        /// AOT元数据文件
        /// </summary>
        /// <returns></returns>
        public static string GetMergedFileName()
        {
            return "AotAssemblies.dll";
        }

        public static string AssemblyAssetExtension = ".bytes";

        public static string AssembliesVersionTextFileName = "AssembliesRecord.json";
    }
}