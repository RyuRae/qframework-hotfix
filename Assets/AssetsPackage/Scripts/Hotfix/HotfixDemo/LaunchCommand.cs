using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Framework.Events;
using Framework;
using Framework.UI;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using YooAsset;

namespace HotfixDemo
{
    public class LaunchCommand : AbstractCommand<Task>
    {
        private readonly HotfixContext context;

        public LaunchCommand(HotfixContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override Task OnExecute()
        {
            return StartGameAsync();
        }

        private async Task StartGameAsync()
        {
            Debug.Log("[LaunchCommand] Start business scene.");
            context.CancellationToken.ThrowIfCancellationRequested();

            // 开发者自己决定加载 Scene 还是 Prefab
            string location = "main";
            SceneHandle sceneHandle = context.MainPackage.LoadSceneAsync(
                location,
                LoadSceneMode.Single,
                LocalPhysicsMode.None,
                false);
            try
            {
                while (!sceneHandle.IsDone)
                {
                    context.CancellationToken.ThrowIfCancellationRequested();
                    TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent
                    {
                        progress = sceneHandle.Progress,
                        desc = HotfixText.Get(HotfixTextKey.SceneLoading)
                    });
                    await UniTask.NextFrame(context.CancellationToken);
                }

                if (sceneHandle.Status != EOperationStatus.Succeed)
                {
                    throw new InvalidOperationException(
                        $"Business scene load failed: {location}. {sceneHandle.LastError}");
                }

                TypeEventSystem.Global.Send(new OnSceneloadUpdateEvent
                {
                    progress = 1f,
                    desc = HotfixText.Get(HotfixTextKey.SceneLoading)
                });

                // 等待新场景对象完成一帧初始化后，才把业务视为真正启动成功。
                await UniTask.NextFrame(context.CancellationToken);
                UIPanelRoot.Instance.CloseLoadingPanel();
                UIPanelRoot.Instance.ClearScreen();
                LogKit.I("Business scene startup completed.");
            }
            finally
            {
                sceneHandle.Release();
            }
        }
    }
}
