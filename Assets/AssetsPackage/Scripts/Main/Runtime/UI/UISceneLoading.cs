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
        /// ���½������ݣ����ظ��£�
        /// </summary>
        /// <param name="data">���ظ�������</param>
        public void OnUpdateProgressExcute(DownloadUpdateData data)
		{
			//float progress = data.CurrentDownloadBytes / (float)data.TotalDownloadBytes;
			//LogKit.I("���ؽ��ȶԱȣ�" + "\n" +progress + "\n" + data.Progress);
			LogKit.I(data.Progress);
			Slider_Progress.value = data.Progress;
			Text_Hint.text = "�ļ�������";
			Text_Progress.text = data.Progress.ToString("0.0%");

        }

		/// <summary>
		/// ���½������ݣ����س������£�
		/// </summary>
		/// <param name="progress"></param>
		public void OnUpdateProgressExcute(float progress)
		{
			Slider_Progress.value = progress;
			Text_Hint.text = "�ļ�������";
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
