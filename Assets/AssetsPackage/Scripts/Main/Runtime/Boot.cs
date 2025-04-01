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
        [Header("资源系统运行模式")]
        public EPlayMode playMode;

        [Header("游戏运行帧率")]
        [SerializeField]
        private int targetFrame = 45;

        [SerializeField]
        private Camera mCamera;

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
            var operation = new ProcedureManager(mainPackageName, playMode);
            YooAssets.StartOperation(operation);
            yield return operation;

            string location = "main";
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