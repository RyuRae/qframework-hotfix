using UnityEngine;
using YooAsset;

namespace Framework
{
    [CreateAssetMenu(fileName = "HotfixRuntimeSettings", menuName = "Hotfix/Runtime Settings", order = 0)]
    public sealed class HotfixRuntimeSettings : ScriptableObject
    {
        public const string AssetName = "HotfixRuntimeSettings";
        public const string ResourcesPath = AssetName;

        [SerializeField]
        private EPlayMode editorPlayMode = EPlayMode.EditorSimulateMode;

        [SerializeField]
        private EPlayMode playerPlayMode = EPlayMode.HostPlayMode;

        public EPlayMode PlayMode
        {
            get
            {
#if UNITY_EDITOR
                return editorPlayMode;
#else
                return playerPlayMode;
#endif
            }
        }

        public EPlayMode PlayerPlayMode => playerPlayMode;

        public static HotfixRuntimeSettings Load()
        {
            return Resources.Load<HotfixRuntimeSettings>(ResourcesPath);
        }

#if UNITY_EDITOR
        public void SetPlayerPlayModeForEditor(EPlayMode value)
        {
            playerPlayMode = value;
        }
#endif
    }
}
