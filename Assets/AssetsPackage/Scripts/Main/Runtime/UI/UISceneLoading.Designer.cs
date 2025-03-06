using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MsbFramework.UI
{
	// Generate Id:bbfa8cc0-1689-4ee4-a92c-ed917c5e75da
	public partial class UISceneLoading
	{
		public const string Name = "UISceneLoading";
		
		[SerializeField]
		public UnityEngine.UI.Slider Slider_Progress;
		[SerializeField]
		public TMPro.TextMeshProUGUI Text_Progress;
		[SerializeField]
		public TMPro.TextMeshProUGUI Text_Hint;
		
		private UISceneLoadingData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Slider_Progress = null;
			Text_Progress = null;
			Text_Hint = null;
			
			mData = null;
		}
		
		public UISceneLoadingData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISceneLoadingData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISceneLoadingData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
