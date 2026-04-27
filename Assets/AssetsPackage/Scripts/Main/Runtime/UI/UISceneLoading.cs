using UnityEngine;
using UnityEngine.UI;
using QFramework;
using YooAsset;

namespace Framework.UI
{
	public class UISceneLoadingData : UIPanelData
	{
	}
	public partial class UISceneLoading : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneLoadingData ?? new UISceneLoadingData();
		}

		/// <summary>
		/// 更新下载进度。
		/// </summary>
		/// <param name="data">下载进度数据。</param>
		public void OnUpdateProgressExcute(DownloadUpdateData data, string desc = "文件下载中")
		{
			Slider_Progress.value = data.Progress;
			Text_Hint.text = desc;
			Text_Progress.text = data.Progress.ToString("0.0%");
		}

		/// <summary>
		/// 更新资源或场景加载进度。
		/// </summary>
		/// <param name="progress"></param>
		public void OnUpdateProgressExcute(float progress, string desc = "资源加载中")
		{
			Slider_Progress.value = progress;
			Text_Hint.text = desc;
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
