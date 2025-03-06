using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MsbFramework.UI
{
	// Generate Id:3e700dc9-0647-499d-8c42-ad82d3c8d049
	public partial class UISceneMessageBox
	{
		public const string Name = "UISceneMessageBox";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI Text_Hint;
		[SerializeField]
		public UnityEngine.UI.Button Button_Cancle;
		[SerializeField]
		public UnityEngine.UI.Button Button_Confirm;
		
		private UISceneMessageBoxData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Text_Hint = null;
			Button_Cancle = null;
			Button_Confirm = null;
			
			mData = null;
		}
		
		public UISceneMessageBoxData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISceneMessageBoxData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISceneMessageBoxData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
