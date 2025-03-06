using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:b116235a-d625-4521-b6f8-0988b57efc97
	public partial class UISceneTest
	{
		public const string Name = "UISceneTest";
		
		[SerializeField]
		public UnityEngine.UI.Button Button_Test;
		
		private UISceneTestData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Button_Test = null;
			
			mData = null;
		}
		
		public UISceneTestData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISceneTestData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISceneTestData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
