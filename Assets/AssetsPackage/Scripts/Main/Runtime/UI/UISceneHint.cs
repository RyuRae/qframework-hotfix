using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace Framework.UI
{
	/// <summary>短消息提示面板数据。</summary>
	public class UISceneHintData : UIPanelData
	{
	}
	/// <summary>显示启动或下载阶段短时提示的 UI 面板。</summary>
	public partial class UISceneHint : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneHintData ?? new UISceneHintData();
			// please add init code here

		}


		public void ShowMessage(string msg, float seconds = -1)
		{
			Text_Hint.text = msg;
			if(seconds == -1) return;
			ActionKit.Delay(seconds, () => 
			{
				this.CloseSelf();
			}).Start(this);

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
