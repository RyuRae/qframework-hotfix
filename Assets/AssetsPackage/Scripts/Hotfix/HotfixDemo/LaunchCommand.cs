using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Framework.Events;
using Framework;
using Framework.UI;
using Cysharp.Threading.Tasks;
using System;

namespace HotfixDemo
{
    public class LaunchCommand : AbstractCommand
    {
        // string mainPackageName;
        public LaunchCommand()
        {
            // this.mainPackageName = mainPackageName;
        }

        protected override void OnExecute()
        {
            Debug.Log("[LaunchGameCommand] Execute.");

            // 开发者自己决定加载 Scene 还是 Prefab
            string location = "main";
           YooAssetKit.LoadSceneAsync(location, LoadSceneMode.Single, LocalPhysicsMode.None, false, (progress) =>
            {
                //更新进度
                TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent
                {
                    progress = progress,
                    desc = HotfixText.Get(HotfixTextKey.SceneLoading)
                });
            }, 
            (sceneHandle) =>
            {
                LogKit.I("加载完成");
                //加载完成
                _ = UniTask.Delay(TimeSpan.FromSeconds(0.1f)).ContinueWith(() =>
                {
                    UIPanelRoot.Instance.CloseLoadingPanel();
                    UIPanelRoot.Instance.ClearScreen();
                });
            });
        }
    }
}