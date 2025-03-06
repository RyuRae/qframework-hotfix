using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace MsbFramework.UI
{
	public class UISceneMessageBoxData : UIPanelData
	{
	}
	public partial class UISceneMessageBox : UIPanel
	{
		private Action callback = null;

        private void Start()
        {
			Button_Confirm.onClick.AddListener(() =>
			{
				//打开loading界面
				UIPanelRoot.Instance.OpenLoadingPanel();
				callback?.Invoke();
				this.Hide();
			});

			Button_Cancle.onClick.AddListener(() =>
			{
				this.Hide();
			});
		}

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneMessageBoxData ?? new UISceneMessageBoxData();
			// please add init code here
			
		}

		/// <summary>
		/// 显示提示框
		/// </summary>
		/// <param name="msg">提示信息</param>
		/// <param name="action">回调</param>
		public void ShowMessageBox(string msg, Action action = null)
		{
            
			callback = action ?? null;
			Text_Hint.text = msg;
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
