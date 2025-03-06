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
				//��loading����
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
		/// ��ʾ��ʾ��
		/// </summary>
		/// <param name="msg">��ʾ��Ϣ</param>
		/// <param name="action">�ص�</param>
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
