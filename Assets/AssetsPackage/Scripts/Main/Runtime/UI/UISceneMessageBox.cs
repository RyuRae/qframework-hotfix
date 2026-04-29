using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace Framework.UI
{
	public class UISceneMessageBoxData : UIPanelData
	{
	}
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
