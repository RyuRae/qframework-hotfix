using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MsbFramework.UI
{
	public class UISceneHintData : UIPanelData
	{
	}
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
