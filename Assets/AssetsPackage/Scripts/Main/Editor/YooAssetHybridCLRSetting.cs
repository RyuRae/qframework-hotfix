using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Main.Editor
{
    [CreateAssetMenu(fileName = "YooAssetHybridCLRSetting", menuName = "YooAsset/Create YooAsset HybridCLR Setting", order = 3)]
    public class YooAssetHybridCLRSetting : ScriptableObject
    {
        [SerializeField]
        public List<string> AOTMetaAssemblies;

        [SerializeField]
        public List<string> HotfixAssemblies;

        [SerializeField]
        private static readonly string YooAssetHybridCLRSettingsPath = $"YooAssetHybridCLRSetting";

        private static YooAssetHybridCLRSetting _instance;
        [SerializeField]
        //AOT程序集拷贝路径
        public string AOTCodesPath = "Assets/AssetsPackage/AssetsHotFix/AOTCodes";

        [SerializeField]
        //热更新程序集拷贝路径
        public string HotfixCodesPath = "Assets/AssetsPackage/AssetsHotFix/HotfixCodes";

        [SerializeField]
        public string mYooAssetHybridCLRSettingSourcePath = "Assets/AssetsPackage/Resources/YooAssetHybridCLRSetting.asset";

        [SerializeField]
        public string mYooAssetHybridCLRSettingDesPath = "Assets/AssetsPackage/AssetsHotFix/Configs/YooAssetHybridCLRSetting.asset";

        
        //设置文件源路径
        public string YooAssetHybridCLRSettingSourcePath {
            get 
            {
                return Path.Combine(Application.dataPath, "..", mYooAssetHybridCLRSettingSourcePath);
            }
        } 

       
        //拷贝设置文件目标路径
        public string YooAssetHybridCLRSettingDesPath 
        {
            get
            {
                return Path.Combine(Application.dataPath, "..", mYooAssetHybridCLRSettingDesPath);
            }
        }

        private YooAssetHybridCLRSetting()
        {
            AOTMetaAssemblies = new List<string>();
            HotfixAssemblies = new List<string>();
        }

        public static YooAssetHybridCLRSetting Instance
        {
            set { _instance = value;  }
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<YooAssetHybridCLRSetting>(YooAssetHybridCLRSettingsPath);
                if (_instance == null)
                {
                    Debug.LogError($"没找到 {typeof(YooAssetHybridCLRSetting)} asset，请先创建一个:{YooAssetHybridCLRSettingsPath}.");
                    return null;
                }

                return _instance;
            }
        }
    }
}
