# qframework-hotfix
这是热更版qframework开发框架，使用 YooAsset 实现资源打包及热更新管理，使用 HybridCLR 实现逻辑热更新管理。

**热更目录结构说明**

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

根据需要可自定义文件夹及资源设置；

PS：Shader变体收集，可使用YooAsset官方提供的工具，在Package Manager中找到YooAsset包导入Sample；

友情提示:资源包和执行程序包可以分为两个工程，避免混乱；

4）着色器变体收集，工具栏Tools-->着色器变种收集器，将收集好的shader变体配置在上述资源包；

5）热更新代码配置，HybridCLR-->CompileDll-->发布的目标平台，执行代码生成；

6）热更代码收集，工具栏Build-->BuildAssetsAndCopyToAssetsPackage;代码收集完成，确认步骤3中有收集代码文件夹；

7）打资源包，打包完成后将资源包放在cdn服务器，完成！

详情请查看Docs文件夹-->快速开始




**报错处理**

此版本Unity编辑器版本为2021.3.41f1，利用编辑器打开项目时报错，如下：

1.D3DComplier_47.dll缺失

<img width="381" height="173" alt="2A16A2B7-BFEE-4673-8703-4A505C903696" src="https://github.com/user-attachments/assets/27569e89-f5c4-454c-8412-2b709e0ab1fc" />

解决方案：
卸载编辑器后，重新安装，安装时一定要关闭杀毒软件！！！

2.进入项目后报错，No ‘git‘ executable was found. Please install Git on your system then restart

解决方案：

https://blog.csdn.net/Ling_SevoL_Y/article/details/124403207?fromshare=blogdetail&sharetype=blogdetail&sharerId=124403207&sharerefer=PC&sharesource=Little_Crayon&sharefrom=from_link

安装git，并给git配置环境变量，具体操作步骤见上述链接。

















特别鸣谢
---
[QFramework](https://github.com/liangxiegame/QFramework)：提供一套简单、强大、易上手、符合 SOLID 原则、支持领域驱动设计（DDD）、事件驱动、数据驱动、分层、MVC 、CQRS、模块化、易扩展的架构。  
  
[YooAsset](https://github.com/tuyoogame/YooAsset)：一套用于Unity3D的资源管理系统，用于帮助研发团队快速部署和交付游戏。它可以满足商业化游戏的各类需求，并且经历多款百万DAU游戏产品的验证。  
  
[HybridCLR](https://github.com/focus-creative-games/hybridclr)：HybridCLR是一个特性完整、零成本、高性能、低内存的近乎完美的Unity全平台原生c#热更新解决方案。  
