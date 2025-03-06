using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace MsbFramework
{
    /// <summary>
    /// 架构中心组件负责各模块注册及分发
    /// 注册Model，获取数据
    /// 注册System，获取系统
    /// 注册Utility，获取工具类
    /// </summary>
    public class GameMainApp : Architecture<GameMainApp>
    {
        protected override void Init()
        {
            //注册数据模型
            //this.RegisterModel<IModel>();
            
            //注册系统类
            //this.RegisterSystem<ISystem>();
            
            //注册工具类
            //this.RegisterUtility<IUtility>();

        }
    }
}
