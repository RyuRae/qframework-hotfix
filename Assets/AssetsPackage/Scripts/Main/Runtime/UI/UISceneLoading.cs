using UnityEngine;
using UnityEngine.UI;
using QFramework;
using YooAsset;

namespace MsbFramework.UI
{
	public class UISceneLoadingData : UIPanelData
	{
	}
	public partial class UISceneLoading : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneLoadingData ?? new UISceneLoadingData();
			// please add init code here

		}

        /// <summary>
        /// 更新进度数据（下载更新）
        /// </summary>
        /// <param name="data">下载更新数据</param>
        public void OnUpdateProgressExcute(DownloadUpdateData data)
		{
			//float progress = data.CurrentDownloadBytes / (float)data.TotalDownloadBytes;
			//LogKit.I("下载进度对比：" + "\n" +progress + "\n" + data.Progress);
			LogKit.I(data.Progress);
			Slider_Progress.value = data.Progress;
			Text_Hint.text = "文件下载中";
			Text_Progress.text = data.Progress.ToString("0.0%");

        }

		/// <summary>
		/// 更新进度数据（加载场景更新）
		/// </summary>
		/// <param name="progress"></param>
		public void OnUpdateProgressExcute(float progress)
		{
			Slider_Progress.value = progress;
			Text_Hint.text = "文件下载中";
			Text_Progress.text = progress.ToString("0.0%");
		}



        protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
