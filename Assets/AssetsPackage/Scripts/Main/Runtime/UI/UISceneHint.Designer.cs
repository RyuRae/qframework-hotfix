using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MsbFramework.UI
{
	// Generate Id:b49607cd-f093-4791-8e41-61a38d294198
	public partial class UISceneHint
	{
		public const string Name = "UISceneHint";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI Text_Hint;
		
		private UISceneHintData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Text_Hint = null;
			
			mData = null;
		}
		
		public UISceneHintData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISceneHintData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISceneHintData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
