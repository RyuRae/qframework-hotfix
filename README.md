# yooasset_qframework_hybridclr_master
这是一套基于 Unity3D 实现的热更新开发框架，使用 YooAsset 实现资源打包及热更新管理，使用 HybridCLR 实现逻辑热更新管理，逻辑开发基于 QFramework 实现的开发工具。

//文件目录设计
Assets/AssetsPackage
├── AssetsHotfix				// 热更新资源目录
|   ├── AOTCodes				// AOT元数据代码目录
|   ├── BaseAssets				// 基础通用资源目录
|   ├── Configs					// 热更新代码记录文件
|   ├── HotfixCodes				// 热更新程序集目录
|   ├── HotfixDemo				// 热更新项目资源目录
|           ├── Entities		// 预制件目录
|           ├── Scenes		    // 场景目录，其他资源可新建文件夹       
|   ├── Resources				// 资源配置记录文件
|   ├── Scripts                 // 程序编写目录
|           ├── Hotfix          // 需要热更的代码
|           ├── Main            // 框架公共代码
└── 






















特别鸣谢
---
[QFramework](https://github.com/liangxiegame/QFramework)：提供一套简单、强大、易上手、符合 SOLID 原则、支持领域驱动设计（DDD）、事件驱动、数据驱动、分层、MVC 、CQRS、模块化、易扩展的架构。  
  
[YooAsset](https://github.com/tuyoogame/YooAsset)：一套用于Unity3D的资源管理系统，用于帮助研发团队快速部署和交付游戏。它可以满足商业化游戏的各类需求，并且经历多款百万DAU游戏产品的验证。  
  
[HyBridCLR](https://github.com/focus-creative-games/hybridclr)：HybridCLR是一个特性完整、零成本、高性能、低内存的近乎完美的Unity全平台原生c#热更新解决方案。  
