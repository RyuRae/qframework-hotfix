using System.Collections;
using UnityEngine;
using QFramework;
using YooAsset;
using Framework.Procedure;
using UnityEngine.SceneManagement;
using Framework.Events;
using Framework.UI;

namespace Framework
{
    public class Boot : MonoBehaviour
    {
        [Header("资源系统运行模式")]
        public EPlayMode playMode;

        [Header("游戏运行帧率")]
        [SerializeField]
        private int targetFrame = 45;

        [SerializeField]
        private Camera mCamera;

        [Header("按Tag下载主资源包，为空时下载全部差异资源")]
        [SerializeField]
        private string[] downloadTags;

        /// <summary>
		/// 主包名称，根据打包设置变化
		/// </summary>
		public static string mainPackageName = "DefaultPackage";
        /// <summary>
        /// 原生文件包名称，根据打包设置变化
        /// </summary>
        public static string rawfilePackageName = "RawFilePackage";

        void Awake()
        {
            Application.targetFrameRate = targetFrame;//设置目标帧率
            Application.runInBackground = true;//设置后台运行
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            //初始化资源系统
            YooAssets.Initialize();

            //进入资源检查及更新状态
            var operation = new ProcedureManager(mainPackageName, playMode, false, downloadTags);
            YooAssets.StartOperation(operation);
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                var error = string.IsNullOrEmpty(operation.Error) ? "热更流程初始化失败！" : operation.Error;
                Debug.LogError(error);
                UIPanelRoot.Instance.ShowMessage(error);
                yield break;
            }

            string location = operation.EntrySceneAddress;
            //加载场景
            YooAssetKit.LoadSceneAsync(location, LoadSceneMode.Single, LocalPhysicsMode.None, false, (progress) =>
            {
                //更新进度
                TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent() { progress = progress, desc = "场景加载中" });
            }, (sceneHandle) =>
            {
                //加载完成
                ActionKit.Delay(0.2f, () =>
                {
                    UIPanelRoot.Instance.CloseLoadingPanel();
                    UIPanelRoot.Instance.ClearScreen();
                }).Start(this);
            });
        }
    }
}
