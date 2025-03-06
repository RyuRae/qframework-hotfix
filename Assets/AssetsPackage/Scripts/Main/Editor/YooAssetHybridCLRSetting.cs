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
        //AOT���򼯿���·��
        public string AOTCodesPath = "Assets/AssetsPackage/AssetsHotFix/AOTCodes";

        [SerializeField]
        //�ȸ��³��򼯿���·��
        public string HotfixCodesPath = "Assets/AssetsPackage/AssetsHotFix/HotfixCodes";

        [SerializeField]
        public string mYooAssetHybridCLRSettingSourcePath = "Assets/AssetsPackage/Resources/YooAssetHybridCLRSetting.asset";

        [SerializeField]
        public string mYooAssetHybridCLRSettingDesPath = "Assets/AssetsPackage/AssetsHotFix/Configs/YooAssetHybridCLRSetting.asset";

        
        //�����ļ�Դ·��
        public string YooAssetHybridCLRSettingSourcePath {
            get 
            {
                return Path.Combine(Application.dataPath, "..", mYooAssetHybridCLRSettingSourcePath);
            }
        } 

       
        //���������ļ�Ŀ��·��
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
                    Debug.LogError($"û�ҵ� {typeof(YooAssetHybridCLRSetting)} asset�����ȴ���һ��:{YooAssetHybridCLRSettingsPath}.");
                    return null;
                }

                return _instance;
            }
        }
    }
}
