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
        [Header("资源系统运行模式")]
        public EPlayMode playMode;

        [Header("游戏运行帧率")]
        [SerializeField]
        private int targetFrame = 45;

        [SerializeField]
        private Camera mCamera;

        void Awake()
        {
            Application.targetFrameRate = targetFrame;//设置目标帧率
            Application.runInBackground = true;//设置后台运行
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator Start()
        {
            LogKit.I(Application.persistentDataPath);
            //初始化资源系统
            YooAssets.Initialize();
            
            //进入资源检查及更新状态
            var operation = new ProcedureManager("DefaultPackage", playMode);
            YooAssets.StartOperation(operation);
            yield return operation;

            LogKit.I("资源检查完毕！！！");
            // 设置默认的资源包
            //var gamePackage = YooAssets.GetPackage("DefaultPackage");
            //YooAssets.SetDefaultPackage(gamePackage);

            //// 切换到主页面场景
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