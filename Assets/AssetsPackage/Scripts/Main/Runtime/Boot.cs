using System.Collections;
using UnityEngine;
using QFramework;
using YooAsset;
using MsbFramework.Procedure;
using UnityEngine.SceneManagement;
using MsbFramework.Events;
using MsbFramework.UI;
//using MsbFramework.Events;
//using MsbFramework.UI;

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

        /// <summary>
		/// �������ƣ����ݴ�����ñ仯
		/// </summary>
		public static string mainPackageName = "DefaultPackage";
        /// <summary>
        /// ԭ���ļ������ƣ����ݴ�����ñ仯
        /// </summary>
        public static string rawfilePackageName = "RawFilePackage";

        void Awake()
        {
            Application.targetFrameRate = targetFrame;//����Ŀ��֡��
            Application.runInBackground = true;//���ú�̨����
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            //��ʼ����Դϵͳ
            YooAssets.Initialize();

            //������Դ��鼰����״̬
            var operation = new ProcedureManager(mainPackageName, playMode);
            YooAssets.StartOperation(operation);
            yield return operation;

            string location = "main";
            //���س���
            YooAssetKit.LoadSceneAsync(location, LoadSceneMode.Single, LocalPhysicsMode.None, false, (progress) =>
            {
                //���½���
                TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent() { progress = progress, desc = "����������" });
            }, (sceneHandle) =>
            {
                //�������
                ActionKit.Delay(0.2f, () =>
                {
                    UIPanelRoot.Instance.CloseLoadingPanel();
                    UIPanelRoot.Instance.ClearScreen();
                }).Start(this);
            });
        }
    }
}