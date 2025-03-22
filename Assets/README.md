[QFramwork + YooAsset + HybridCLR]
1.HybridCLR

2.YooAsset

3.QFramwork

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
