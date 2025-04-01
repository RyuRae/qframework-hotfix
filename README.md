# yooasset_qframework_hybridclr_master
这是一套基于 Unity3D 实现的热更新开发框架，使用 YooAsset 实现资源打包及热更新管理，使用 HybridCLR 实现逻辑热更新管理，逻辑开发基于 QFramework 实现的开发工具。

![image](https://github.com/user-attachments/assets/25ce0e8d-74fc-4463-867d-d590285133a1)

**快速开始**

1.发布执行程序（热更基础包）

1）找到工具栏-->HybridCLR-->Installer-->Install，安装HybridCLR，确认已安装；

2）初始化生成框架数据 HybridCLR -->Generate-->All;

3）生成link.xml链接文件，避免代码过度裁剪，HybridCLR-->Generate-->LinkXml;

4）配置初始热更资源包（必须），可根据需要设置空包，初始包等；工具栏YooAsset-->AssetBundle Collector，根据需要配置初始资源包；

5）资源打包，工具栏YooAsset-->AssetBundle Builder，打包管线推荐使用ScriptableBuildPipeline；初始需要拷贝资源到StreamingAssets，CopyBuildinFileOption设置为ClearAndCopyAll，然后打包；

6）发布框架包，选择发布平台（和打包资源匹配），Build；

2.发布热更资源包

1）需要热更的代码生成Assembly Definition，在代码文件夹右键Create-->Assembly Definition;

2）配置需要热更的代码，HybridCLR-->Setting-->Hot Update Assembly Definitions;

3）配置热更新资源（代码和需要热更的prefab/UI/Shder/Sound等），YooAsset-->AssetBundle Collector；

上述为资源文件夹分类，分别为AOT元数据，通用素材文件，代码数据记录配置文件，热更代码文件，HotfixMain为更新的主资源包括场景/prefab/UI/shader和shader变体等；

根据需要可自定义文件夹及资源设置；

PS：Shader变体收集，可使用YooAsset官方提供的工具，在Package Manager中找到YooAsset包导入Sample；

友情提示:资源包和执行程序包可以分为两个工程，避免混乱；

4）着色器变体收集，工具栏Tools-->着色器变种收集器，将收集好的shader变体配置在上述资源包；

5）热更新代码配置，HybridCLR-->CompileDll-->发布的目标平台，执行代码生成；

6）热更代码收集，工具栏Build-->BuildAssetsAndCopyToAssetsPackage;代码收集完成，确认步骤3中有收集代码文件夹；

7）打资源包，打包完成后将资源包放在cdn服务器，完成！

详情请查看Docs文件夹-->快速开始




















特别鸣谢
---
[QFramework](https://github.com/liangxiegame/QFramework)：提供一套简单、强大、易上手、符合 SOLID 原则、支持领域驱动设计（DDD）、事件驱动、数据驱动、分层、MVC 、CQRS、模块化、易扩展的架构。  
  
[YooAsset](https://github.com/tuyoogame/YooAsset)：一套用于Unity3D的资源管理系统，用于帮助研发团队快速部署和交付游戏。它可以满足商业化游戏的各类需求，并且经历多款百万DAU游戏产品的验证。  
  
[HyBridCLR](https://github.com/focus-creative-games/hybridclr)：HybridCLR是一个特性完整、零成本、高性能、低内存的近乎完美的Unity全平台原生c#热更新解决方案。  
