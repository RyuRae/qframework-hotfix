using System.Collections;
using UnityEngine;
using QFramework;
using YooAsset;
using MsbFramework.Procedure;
using UnityEngine.SceneManagement;
using MsbFramework.Events;
using MsbFramework.UI;

namespace MsbFramework
{
    public class Boot : MonoBehaviour
    {
        [Header("��Դϵͳ����ģʽ")]
        public EPlayMode playMode;

        [Header("��Ϸ����֡��")]
        [SerializeField]
        private int targetFrame = 45;

        [SerializeField]
        private Camera mCamera;

        void Awake()
        {
            Application.targetFrameRate = targetFrame;//����Ŀ��֡��
            Application.runInBackground = true;//���ú�̨����
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            LogKit.I(Application.persistentDataPath);
            //��ʼ����Դϵͳ
            YooAssets.Initialize();
            
            //������Դ��鼰����״̬
            var operation = new ProcedureManager("DefaultPackage", playMode);
            YooAssets.StartOperation(operation);
            yield return operation;

            LogKit.I("��Դ�����ϣ�����");
            // ����Ĭ�ϵ���Դ��
            //var gamePackage = YooAssets.GetPackage("DefaultPackage");
            //YooAssets.SetDefaultPackage(gamePackage);

            //// �л�����ҳ�泡��
            //string location = "Assets/AssetsPackage/AssetsHotFix/HotfixDemo/Scenes/main";
            //var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
            //var physicsMode = LocalPhysicsMode.None;
            //bool suspendLoad = false;
            //SceneHandle handle = gamePackage.LoadSceneAsync(location, sceneMode, physicsMode, suspendLoad);
            //TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent() { progress = handle.Progress });
            //yield return handle;
            //ActionKit.Delay(0.5f, () =>
            //{
            //    //mCamera.Hide();
            //    UIPanelRoot.Instance.CloseLoadingPanel();
            //}).Start(this);


            YooAssetKit.SetDefaultPackage();
            string location = "main";
            YooAssetKit.LoadSceneAsync(location, LoadSceneMode.Single, LocalPhysicsMode.None, false, (progress) => 
            {
                TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent() { progress = progress });
            }, (sceneHandle) => 
            {
                ActionKit.Delay(0.5f, () =>
                {
                    UIPanelRoot.Instance.CloseLoadingPanel();
                }).Start(this);
            });
        }
    }
}