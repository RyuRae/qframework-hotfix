# yooasset_qframework_hybridclr_master
这是一套基于 Unity3D 实现的热更新开发框架，使用 YooAsset 实现资源打包及热更新管理，使用 HybridCLR 实现逻辑热更新管理，逻辑开发基于 QFramework 实现的开发工具。

![image](https://github.com/user-attachments/assets/25ce0e8d-74fc-4463-867d-d590285133a1)

快速开始

1.发布执行程序（热更基础包）

1）找到工具栏-->HybridCLR-->Installer-->Install，安装HybridCLR，确认已安装；

<img width="88" alt="image" src="https://github.com/user-attachments/assets/625aeabc-011f-4f43-9d2e-d5c5920196c2" />

2）初始化生成框架数据 HybridCLR -->Generate-->All;

3）生成link.xml链接文件，避免代码过度裁剪，HybridCLR-->Generate-->LinkXml;

4）配置初始热更资源包（必须），可根据需要设置空包，初始包等；工具栏YooAsset-->AssetBundle Collector，根据需要配置初始资源包；

<img width="277" alt="image" src="https://github.com/user-attachments/assets/0078e9bd-435f-4767-8b66-a044fc849504" />

5）资源打包，工具栏YooAsset-->AssetBundle Builder，打包管线推荐使用ScriptableBuildPipeline；初始需要拷贝资源到StreamingAssets，CopyBuildinFileOption设置为ClearAndCopyAll，然后打包；

<img width="288" alt="image" src="https://github.com/user-attachments/assets/65077c0c-8c5d-463a-b160-abb62b869bd5" />

6）发布框架包，选择发布平台（和打包资源匹配），Build；

<img width="233" alt="image" src="https://github.com/user-attachments/assets/c138fdf0-f95e-44d4-b6f0-9ebf343ee6ce" />





















特别鸣谢
---
[QFramework](https://github.com/liangxiegame/QFramework)：提供一套简单、强大、易上手、符合 SOLID 原则、支持领域驱动设计（DDD）、事件驱动、数据驱动、分层、MVC 、CQRS、模块化、易扩展的架构。  
  
[YooAsset](https://github.com/tuyoogame/YooAsset)：一套用于Unity3D的资源管理系统，用于帮助研发团队快速部署和交付游戏。它可以满足商业化游戏的各类需求，并且经历多款百万DAU游戏产品的验证。  
  
[HyBridCLR](https://github.com/focus-creative-games/hybridclr)：HybridCLR是一个特性完整、零成本、高性能、低内存的近乎完美的Unity全平台原生c#热更新解决方案。  
