using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	/// <summary>启动 UI 测试面板数据。</summary>
	public class UISceneTestData : UIPanelData
	{
	}
	/// <summary>用于验证启动 UI 框架接入的测试面板。</summary>
	public partial class UISceneTest : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneTestData ?? new UISceneTestData();
			// please add init code here
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
