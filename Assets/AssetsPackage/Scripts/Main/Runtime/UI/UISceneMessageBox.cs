using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace Framework.UI
{
	/// <summary>确认对话框数据，包含确认、取消回调及确认后的 Loading 行为。</summary>
	public class UISceneMessageBoxData : UIPanelData
	{
	}
	/// <summary>启动更新阶段的确认/取消对话框。</summary>
	public partial class UISceneMessageBox : UIPanel
	{
		private Action confirmCallback = null;
		private Action cancelCallback = null;
		private bool openLoadingOnConfirm = false;

		private void Start()
		{
			Button_Confirm.onClick.AddListener(() =>
			{
				if (openLoadingOnConfirm)
				{
					UIPanelRoot.Instance.OpenLoadingPanel();
				}

				confirmCallback?.Invoke();
				this.Hide();
			});

			Button_Cancle.onClick.AddListener(() =>
			{
				cancelCallback?.Invoke();
				this.Hide();
			});
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneMessageBoxData ?? new UISceneMessageBoxData();
			// please add init code here

		}

		/// <summary>
		/// Shows the message box.
		/// </summary>
		/// <param name="msg">Message text.</param>
		/// <param name="action">Confirm callback.</param>
		public void ShowMessageBox(string msg, Action action = null, Action cancelAction = null, bool shouldOpenLoadingOnConfirm = false)
		{
			confirmCallback = action;
			cancelCallback = cancelAction;
			openLoadingOnConfirm = shouldOpenLoadingOnConfirm;
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
			confirmCallback = null;
			cancelCallback = null;
			openLoadingOnConfirm = false;
		}

		protected override void OnClose()
		{
		}
	}
}
