# 热更新框架优化 TODO

更新时间：2026-04-30  
适用分支：`develop`  
适用范围：QFramework + YooAsset + HybridCLR 热更新框架

本文档用于记录当前热更新框架后续需要改善的事项。

优先级含义：

- **P0**：上线前必须解决，影响启动、更新、容灾、热更安全或打包正确性。
- **P1**：建议在商业化版本前完善，影响安全、可维护性、发布效率和诊断能力。
- **P2**：长期优化项，影响体验、工程卫生和扩展能力。

本版整理原则：

1. **保留原来已完成的 TODO**，作为框架演进记录和验收基线。
2. 删除或合并已经过期的未完成 TODO。
3. 将当前框架已经暴露出的风险项提升为 P0。
4. 将 Build 菜单收口、Build Center、一键首包、一键热更包作为下一阶段主线。
5. 普通开发者只面对“首包构建”和“热更包构建”；底层工具迁移到 Internal / Advanced。

---

## 当前框架概览

当前启动链路：

```text
Boot
 └─ ProcedureManager
    └─ 初始化 YooAsset package
       └─ 请求远端 package version
          └─ 更新 package manifest
             └─ 创建 downloader 并下载资源
                └─ 加载 AOT metadata
                   └─ 加载热更 DLL
                      └─ 清理缓存
                         └─ 加载入口场景 main
```

当前主要使用：

- **YooAsset**：资源打包、资源下载、manifest 管理。
- **HybridCLR**：AOT metadata 补充、热更程序集加载。
- **QFramework**：启动流程、事件、UI、资源加载封装。
- **Luban**：配置表生成和读取。

---

# P0 必须优先处理

## 1. 修复 Player 环境误用 EditorSimulateMode 的风险（已完成）

- [x] 在打包前强制校验 `HotfixRuntimeSettings.PlayerPlayMode`，禁止 Player 使用 `EditorSimulateMode`。
- [x] 在 `BuildPlayerCommand` 和全局构建预处理器中按目标环境自动设置 `HostPlayMode` / `OfflinePlayMode` / `WebPlayMode`。
- [x] 增加启动时保护：非 Editor 环境检测到 `EditorSimulateMode` 时给出明确错误并终止流程。
- [x] 将运行模式从场景序列化字段迁移到可配置的构建 profile，避免人工改场景。

相关位置：

```text
Assets/Scenes/Boot.unity
Assets/AssetsPackage/Scripts/Main/Runtime/Boot.cs
Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRuntimeSettings.cs
Assets/AssetsPackage/Resources/HotfixRuntimeSettings.asset
Assets/Editor/HybridCLR/BuildPlayerCommand.cs
Assets/Editor/HybridCLR/HotfixBuildProfile.cs
Assets/Editor/HybridCLR/HotfixBuildProfile.asset
```

验收标准：

- Windows / Android / iOS / WebGL 打包时不会带着错误 play mode 出包。
- CI 或本地构建脚本能自动失败并输出清晰原因。

---

## 2. 处理用户取消下载导致流程卡死（已完成）

- [x] 为下载确认弹窗增加取消回调。
- [x] 用户取消下载时明确进入失败 / 退出更新路径，离线启动和本地缓存兜底归入第 3 项继续完善。
- [x] 下载开始后支持通过 `ProcedureManager.CancelDownload` 取消下载，并将 downloader、UI、FSM 状态一起收口。
- [x] 评估并实现下载暂停 / 继续能力，已预留 `TryPauseDownload` / `TryResumeDownload` 状态接口。
- [x] 下载失败后提供用户可点击的“重试 / 退出更新”路径，本地缓存兜底归入第 3 项。
- [x] `ProcedureCreateDownloader` 不再只在确认时推进 FSM，取消时会明确终止流程。
- [x] loading 状态、弹窗状态、FSM 状态保持一致，避免界面隐藏但流程悬挂。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureCreateDownloader.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureDownloadPackageFiles.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs
Assets/AssetsPackage/Scripts/Main/Runtime/UI/UISceneMessageBox.cs
Assets/AssetsPackage/Scripts/Main/Runtime/UI/UIPanelRoot.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Events/DownloadEvents.cs
```

验收标准：

- 点击取消后不会无限等待。
- 下载中取消不会留下未完成状态或卡住协程。
- 取消路径有明确日志、UI 提示和业务结果。

---

## 3. 增加网络失败后的本地缓存兜底（已完成）

- [x] `RequestPackageVersionAsync` 失败时允许使用内置 manifest 或上次缓存 manifest 启动。
- [x] `UpdatePackageManifestAsync` 失败时允许使用已缓存版本启动。
- [x] 版本请求、manifest 更新、资源下载失败时增加业务层重试，不只依赖 YooAsset `failedTryAgain`。
- [x] 对服务器不可达、DNS 失败、CDN 404、manifest 损坏等失败类型分别设计恢复路径。
- [x] 区分强更资源和弱更资源：`MustUpdate` 阻断，`AllowCached` / `WifiOnly` / `BackgroundDownload` 允许本地缓存启动。
- [x] 增加启动策略配置：必须更新 / 可跳过更新 / 仅 Wi-Fi 更新 / 后台下载。
- [x] 给所有网络失败路径补充用户可理解的错误提示和重试按钮。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureRequestPackageVersion.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureUpdatePackageManifest.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureDownloadPackageFiles.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/HotfixLocalManifestUtility.cs
Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRuntimeSettings.cs
```

验收标准：

- 断网情况下，只要本地存在可用资源，就可以进入游戏。
- 远端 manifest 异常不会导致所有用户无法启动。

---

## 4. 抽离 CDN 地址和环境配置（已完成）

- [x] 将 `http://127.0.0.1:8080/TestProject/PC` 从代码中移除。
- [x] 新增热更新配置文件，支持开发、测试、预发、正式环境。
- [x] 支持主 CDN 和备用 CDN 使用不同地址。
- [x] 启动时校验 main URL 和 fallback URL 不应完全相同，否则给出配置错误提示。
- [x] 支持按平台、渠道、地区生成远端资源地址。
- [x] 支持 HTTPS，并预留证书、域名、CDN 灰度切换能力。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureInitializePackage.cs
Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRemoteSettings.cs
Assets/AssetsPackage/Resources/HotfixRemoteSettings.asset
```

验收标准：

- 切换 CDN 不需要改代码重新出包。
- 一份安装包可以根据环境或渠道使用不同远端资源地址。

---

## 5. 明确首包和空包启动策略（已完成）

- [x] 明确首包模式：首包是否必须包含启动所需的 manifest、AOT metadata、热更 DLL、入口场景。
- [x] 明确空包模式：如果支持真正空包，启动后应先拉取远端热更索引或 Hotfix manifest，再按需下载 AOT metadata、热更 DLL 和入口资源。
- [x] 明确离线模式：无网络时是否允许使用内置资源或上次缓存资源进入游戏。
- [x] 明确首包资源最小集合：至少包括能展示更新 UI、请求远端配置、处理错误和执行降级的资源。
- [x] 首包 / 离线包在构建阶段强校验必需资源完整性。
- [x] 为 manifest 缺失、AOT 缺失、DLL 缺失、入口资源缺失提供明确错误和恢复建议。
- [x] 第 6 条的 AOT / Hotfix manifest 拆分是本策略的具体实现基础，避免继续依赖单一 manifest。

实现说明：

- `FirstPackage` / `OfflinePackage` 会在构建阶段校验 split manifest、AOT metadata、热更 DLL、入口资源以及 YooAsset 收集器路径。
- `EmptyPackage` 不拷贝 YooAsset 资源到 StreamingAssets，启动后通过 Host/Web 远端版本和 package manifest 决定下载哪些 AOT metadata、热更 DLL 和入口资源；空包不能搭配 `OfflinePlayMode`。
- `OfflinePackage` 必须搭配 `OfflinePlayMode`，无网络时只使用内置资源；Host/Web 模式仍按更新策略允许上次可用缓存兜底。

相关位置：

```text
Assets/AssetsPackage/AssetsHotFix/Configs
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureRequestPackageVersion.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureUpdatePackageManifest.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureLoadAOTMetadata.cs
```

验收标准：

- 首包、空包、离线启动三种策略有明确选择，不再混用。
- 首包必需资源缺失时构建直接失败。
- 空包启动能先拿到远端版本信息，再决定下载哪个 AOT 和 Hotfix。
- 无网络但本地存在可用缓存时，可以按策略进入游戏或给出明确阻断原因。

---

## 6. 核心重构：AOT 和 Hotfix Manifest 真正分离（已完成）

- [x] 将当前单一 `AssemblyManifest` 拆分为 `AOTAssemblyManifest` 和 `HotfixAssemblyManifest`。
- [x] `AOTAssemblyManifest` 字段设计：

```csharp
public string AppVersion;
public string BuildTarget;
public string AotVersion;
public List<string> AotMetadataAssemblies;
```

- [x] `HotfixAssemblyManifest` 字段设计：

```csharp
public string AppVersionMin;
public string AppVersionMax;
public string BuildTarget;
public string RequiredAotVersion;
public string HotfixVersion;
public List<string> HotUpdateAssemblies;
public string EntrySceneAddress;
public string EntryPrefabAddress;
public string EntryTypeName;
public string EntryMethodName;
```

- [x] 构建阶段分别生成 AOT manifest 和 Hotfix manifest。
- [x] `AotVersion` 建议由 AppVersion、BuildTarget、AOT metadata 列表和文件 hash 共同决定，避免只靠时间字符串。
- [x] `HotfixAssemblyManifest.RequiredAotVersion` 必须指向一个明确存在的 AOT 版本。
- [x] Runtime 启动时先解析 Hotfix manifest，再根据 `RequiredAotVersion` 查找本地或远端 AOT manifest。
- [x] 当 AOT 版本发生变化时，先下载并加载新版 AOT metadata，再加载热更 DLL。
- [x] 调整当前 AOT 加载时序，避免在请求远端 Hotfix manifest 前提前加载旧 AOT metadata。
- [x] 当 AOT 版本未变化时，热更包只更新 Hotfix manifest 和热更 DLL，不重复下载 AOT metadata。
- [x] 将 AOT manifest 和 Hotfix manifest 在启动流程中缓存到上下文，避免 `LoadAotMetadata` 和 `LoadHotUpdateAssemblies` 重复加载同一份 manifest。
- [x] 校验 `AppVersionMin <= 当前 AppVersion <= AppVersionMax`，不兼容时阻断热更并提示更新 App。
- [x] 校验 `BuildTarget` 必须和当前运行平台一致，避免跨平台加载错误 DLL。
- [x] AOT 缓存和 Hotfix 缓存需要分开记录版本，支持独立清理和回滚。
- [x] 回滚时必须以 `HotfixVersion + RequiredAotVersion` 作为一组兼容组合回滚，避免只回滚热更 DLL。

实现说明：

- 旧 `AssemblyManifest` 保留为兼容迁移资产；运行时加载改为使用 `AOTAssemblyManifest` 和 `HotfixAssemblyManifest`。
- `AotVersion` / `HotfixVersion` 基于 AppVersion、BuildTarget、RequiredAotVersion、程序集文件 SHA256 和大小生成。
- 热更构建不再移除 AOT 收集器；YooAsset 会通过文件 hash 判断未变化的 AOT metadata，无需重复下载。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/AssemblyManifest.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureLoadAOTMetadata.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureLoadAssembly.cs
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
```

验收标准：

- AOT 不变化时，发布 Hotfix 不会重新下载 AOT metadata。
- AOT 变化时，客户端会自动使用 `RequiredAotVersion` 对应的新版 AOT。
- App 版本、平台、AOT 版本不兼容时，客户端拒绝加载并给出明确错误。
- 线上回滚可以准确回滚到一组兼容的 AOT + Hotfix 组合。

---

## 7. Build 菜单收口与构建中心 （已完成）

### 背景

当前 `Build` 菜单暴露了较多底层入口，例如：

```text
Build/Build Initial YooAsset Package
Build/Build Hotfix YooAsset Package
Build/BuildAssetsAndCopyToAssetsPackage
Build/BuildAssetsAndCopyToStreamingAssets
Build/CopyAotDllsToAssetsPackage
Build/CopyHotUpdateDllsToAssetsPackage
Build/Win64
```

这些菜单对框架维护者有用，但对刚接触框架的开发人员来说，上手成本高，也容易误点错误流程。

### 目标菜单结构

```text
Build
└── 热更新
    ├── 构建中心...
    ├── 一键构建
    │   ├── 构建首包
    │   └── 构建热更包
    ├── 高级
    │   └── 构建 AOT 元数据补丁
    └── 内部工具
        ├── 安全生成 HybridCLR 数据
        ├── 仅构建首包 YooAsset
        ├── 仅构建热更 YooAsset
        ├── 复制 AOT 元数据 DLL
        ├── 复制热更 DLL
        ├── 校验运行时设置
        └── 旧命令
```

### 任务

- [x] 新增 `Build/热更新/构建中心...`。
- [x] 新增 `Build/热更新/一键构建/构建首包`。
- [x] 新增 `Build/热更新/一键构建/构建热更包`。
- [x] 新增 `Build/热更新/高级/构建 AOT 元数据补丁`。
- [x] 将原有底层菜单迁移到 `Build/热更新/内部工具/...`。
- [x] 原菜单可以保留一段时间，但需要标记为 Obsolete 或隐藏。
- [x] README 只推荐构建中心和一键构建菜单，不再要求新人手动点多个底层菜单。
- [x] 构建中心中显示当前 BuildTarget、AppVersion、远端环境、启动模式、下载模式、入口资源、AOT / Hotfix Manifest 状态。
- [x] 构建中心支持 `仅校验`、`一键修复`、`开始构建`。
- [x] 构建中心中红色错误项必须阻断构建。

实现文件：

```text
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildCenterWindow.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildMode.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildContext.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildRunner.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildValidator.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildReport.cs
Assets/Editor/HybridCLR/BuildPipeline/HybridCLRGenerateAllSafe.cs
Assets/Editor/HybridCLR/BuildPipeline/Internal/HotfixInternalBuildMenu.cs
```

实现记录：

- 第 7 项已完成菜单收口和构建中心骨架验收。
- `HotfixBuildRunner` 已实现 `BuildInitialPackage`、`BuildHotfixPackage`、`BuildAOTMetadataPatch` 三条构建流水线。
- `HybridCLRGenerateAllSafe.Run()` 和 `HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild` 已作为第 8 项的稳定接入点预留。

验收标准：

- [x] 新开发人员只需要打开 `Build/热更新/构建中心...` 即可完成首包构建或热更包构建。
- [x] 原底层菜单不会出现在 `Build` 根目录下干扰使用。
- [x] 构建前所有关键配置都能在一个窗口中看到。
- [x] 配置错误时不能继续构建。

---

## 8. HybridCLR Generate All Safe （已完成）

### 背景

`HybridCLR/Generate/All` 内部会触发 `BuildPipeline.BuildPlayer`。当前框架有构建前置校验，会阻止 Release Player Build 使用 Development 远端环境。

这条规则对正式出包是正确的，但在 `Generate All` 期间会误伤 HybridCLR 的临时构建流程。

### 任务

- [x] 新增 `GenerateAllSafe()` 包装方法。
- [x] `GenerateAllSafe()` 执行期间跳过远端环境校验。
- [x] 只跳过 HybridCLR 临时生成流程，不跳过真实 Player Build 校验。
- [x] 首包构建自动调用 `GenerateAllSafe()`。
- [x] AOT Metadata Patch 自动调用 `GenerateAllSafe()`。
- [x] 普通 Hotfix Package 默认不调用 `GenerateAllSafe()`。
- [x] README 中不再推荐直接点击 `HybridCLR/Generate/All`，统一推荐 Build Center。

建议实现：

```csharp
public static class HybridCLRGenerateAllSafe
{
    public static void Run()
    {
        try
        {
            HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild = true;
            PrebuildCommand.GenerateAll();
        }
        finally
        {
            HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild = false;
        }
    }
}
```

构建预处理器中增加：

```csharp
if (HotfixBuildProfileUtility.SkipRemoteSettingsValidationForCurrentBuild)
{
    Debug.Log("[HotfixBuild] Skip remote validation for HybridCLR Generate All.");
    return;
}
```

相关位置：

```text
Assets/Editor/HybridCLR/HotfixBuildProfile.cs
Assets/Editor/HybridCLR/BuildPlayerCommand.cs
新增 Assets/Editor/HybridCLR/BuildPipeline/HybridCLRGenerateAllSafe.cs
```

验收标准：

- 当前远端环境为 Development 时，执行 `Generate All Safe` 不报错。
- Release Player Build + Development 远端环境仍然会被阻止。
- 首包一键构建不需要开发者手动执行 HybridCLR Generate All。

---

## 9. 一键首包构建 Initial Package （已完成）

### 背景

首包构建时开发者不应手动关心：

```text
Generate All
Compile DLL
Copy AOT Metadata DLLs
Copy Hotfix DLLs
Generate AOTAssemblyManifest
Generate HotfixAssemblyManifest
Build YooAsset Package
Copy StreamingAssets
```

这些都应该由一键首包流程自动完成。

### 任务

- [x] 新增 `HotfixBuildMode.InitialPackage`。
- [x] 新增 `Build Initial Package` Runner。
- [x] 自动执行 `GenerateAllSafe()`。
- [x] 自动执行 Hotfix DLL 编译。
- [x] 自动拷贝 AOT Metadata DLLs。
- [x] 自动拷贝 Hotfix DLLs。
- [x] 自动生成 `AOTAssemblyManifest`。
- [x] 自动生成 `HotfixAssemblyManifest`。
- [x] 自动校验 `RequiredAotVersion == AotVersion`。
- [x] 自动校验首包必需资源。
- [x] 自动构建 YooAsset Package。
- [x] 按 `StartupPackageMode` 决定是否复制到 `StreamingAssets`。
- [x] 生成构建报告。

首包构建步骤：

```text
Build Initial Package
├── Validate BuildTarget
├── Validate AppVersion
├── Validate RemoteSettings
├── Validate RuntimeSettings
├── Validate YooAsset Collector
├── Validate Entry Resource
├── Generate All Safe
├── Compile Hotfix DLL
├── Copy AOT Metadata DLLs
├── Copy Hotfix DLLs
├── Generate AOTAssemblyManifest
├── Generate HotfixAssemblyManifest
├── Validate Split Manifests
├── Build YooAsset Package
├── Copy FirstPackage / OfflinePackage to StreamingAssets
└── Write Build Report
```

实现记录：

- `HotfixBuildRunner.BuildInitialPackage()` 已按首包流程编排：GenerateAllSafe、编译 DLL、复制 AOT/Hotfix DLL、生成双清单、校验首包资源、构建 YooAsset、按启动包策略复制 StreamingAssets，并写入构建报告。

验收标准：

- [x] 新人不需要手动执行 `HybridCLR/Generate/All`。
- [x] 首包构建产物包含启动必需的 Manifest、AOT Metadata、Hotfix DLL 和入口资源。
- [x] 构建失败时能明确指出缺失项。
- [x] 构建报告能追溯 AppVersion、AotVersion、HotfixVersion、PackageVersion。

---

## 10. 一键热更包构建 Hotfix Package （已完成）

### 背景

普通热更包构建时，不应该默认执行 `Generate All`。普通热更应只更新 Hotfix DLL、热更资源、配置和 Manifest。

### 任务

- [x] 新增 `HotfixBuildMode.HotfixPackage`。
- [x] 新增 `Build Hotfix Package` Runner。
- [x] 默认不执行 `GenerateAllSafe()`。
- [x] 构建前必须检查 `AOTAssemblyManifest` 存在。
- [x] 构建前必须执行 AOTManifest 过期校验。
- [x] 自动编译 Hotfix DLL。
- [x] 自动拷贝 Hotfix DLLs。
- [x] 复用当前 `AOTAssemblyManifest.AotVersion`。
- [x] 生成新的 `HotfixAssemblyManifest`。
- [x] 校验 `HotfixAssemblyManifest.RequiredAotVersion`。
- [x] 构建 YooAsset 远端包。
- [x] 不复制到 `StreamingAssets`。
- [x] 生成构建报告和 CDN 上传目录提示。

热更包构建步骤：

```text
Build Hotfix Package
├── Validate BuildTarget
├── Validate AppVersionMin / AppVersionMax
├── Validate RemoteSettings
├── Validate RuntimeSettings
├── Validate AOTAssemblyManifest Exists
├── Validate AOTManifest Not Expired
├── Validate YooAsset Collector
├── Validate Entry Resource
├── Compile Hotfix DLL
├── Copy Hotfix DLLs
├── Generate HotfixAssemblyManifest
├── Validate Split Manifests
├── Build YooAsset Package
└── Write Build Report
```

实现记录：

- `HotfixBuildRunner.BuildHotfixPackage()` 已按热更包流程编排：校验 AOT 清单存在且未过期、校验 AppVersion 范围、编译并复制 Hotfix DLL、复用当前 AotVersion 生成 Hotfix 清单、构建远端 YooAsset 包、不复制 StreamingAssets，并写入构建报告和 CDN 上传目录。

验收标准：

- [x] 普通热更包不会误更新 AOT 基线。
- [x] 修改 Hotfix 代码 / 热更资源后可以一键构建热更包。
- [x] 修改 AOT 代码后，普通热更构建会被阻断。
- [x] 输出路径明确区分本地构建产物和待上传 CDN 产物。

---

## 11. AOT Metadata Patch 高级构建模式 （已完成）

### 背景

有些情况不是普通热更，也不是新 App 基线：

```text
AOT 代码逻辑没有变化
但 Hotfix 需要补充新的泛型元数据
```

此时可以发布 AOT Metadata Patch，但必须明确选择，不能和普通热更混在一起。

### 任务

- [x] 新增 `HotfixBuildMode.AOTMetadataPatch`。
- [x] 放在 `Build/热更新/高级/构建 AOT 元数据补丁`。
- [x] 执行前弹出风险说明。
- [x] 自动执行 `GenerateAllSafe()`。
- [x] 自动拷贝 AOT Metadata DLLs。
- [x] 自动拷贝 Hotfix DLLs。
- [x] 重新生成 `AOTAssemblyManifest`。
- [x] 重新生成 `HotfixAssemblyManifest`。
- [x] 构建 YooAsset 远端包。
- [x] 不复制到 `StreamingAssets`。
- [x] 构建报告中明确标记这是 AOT Metadata Patch。

必须提示的边界：

```text
AOT Metadata Patch 只能补充同一 App 基线下的元数据。
如果修改了主工程 AOT 代码逻辑、公共接口、原生 SDK、PlayerSettings，
应发布新 App，而不是发 AOT Metadata Patch。
```

实现记录：

- `HotfixBuildRunner.BuildAOTMetadataPatch()` 已实现风险确认、同 AppVersion/BuildTarget 基线校验、GenerateAllSafe、复制 AOT/Hotfix DLL、重新生成双清单、构建远端 YooAsset 包，并在构建报告中标记高级模式。

验收标准：

- [x] 普通开发者默认不会误触该模式。
- [x] Patch 构建产物能明确绑定 AppVersion 和 RequiredAotVersion。
- [x] 修改 AOT 逻辑代码时不会被误认为只需要 Metadata Patch。

---

## 12. AOTManifest 过期校验 （已完成）

### 背景

当前热更包构建会复用已有 `AOTAssemblyManifest`。如果开发者修改了 AOT 代码、非 Hotfix asmdef、公共接口、HybridCLR AOT 列表，但仍然执行普通热更构建，就可能产生线上兼容风险。

### 任务

- [x] 在 `AOTAssemblyManifest` 中记录 Baseline Fingerprint。
- [x] Fingerprint 至少包含：
  - AppVersion
  - BuildTarget
  - HybridCLR AOT Metadata 列表
  - AOT DLL 文件名
  - AOT DLL size
  - AOT DLL sha256
  - 生成时间
  - 可选 Git commit
- [x] Build Hotfix 前重新扫描当前 AOTCodes。
- [x] 对比当前扫描结果与 `AOTAssemblyManifest`。
- [x] 对比当前 BuildTarget 与 Manifest BuildTarget。
- [x] 对比当前 AppVersion 与 Manifest AppVersion。
- [x] 如果检测到 AOT 基线变化，禁止普通热更构建。
- [x] 提示开发者选择：
  - Build Initial Package / New App Baseline
  - Build AOT Metadata Patch
  - 取消构建

实现记录：

- `AOTAssemblyManifest` 新增 `BaselineFingerprint`、`BaselineGeneratedAtUtc`、`BaselineGitCommit`。
- `BuildAssetsCommand.ValidateAOTManifestNotExpired()` 会在热更包构建前检查 AppVersion、BuildTarget、HybridCLR AOT metadata 列表、AOTCodes 文件 size/sha256 和基线指纹。
- 一键热更包、内部“仅构建热更 YooAsset”和“复制热更 DLL”入口都会先校验现有 AOT 基线，不再通过热更-only 路径隐式创建或绕过 AOTManifest。
- 普通热更构建检测到 AOT 基线变化时会弹窗引导选择构建首包、构建 AOT 元数据补丁或取消。

建议方法：

```csharp
ValidateAOTManifestNotExpired(BuildTarget target, AOTAssemblyManifest manifest)
```

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/AOTAssemblyManifest.cs
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildRunner.cs
```

验收标准：

- [x] 只改 Hotfix 代码时，热更包构建成功。
- [x] 修改 AOT 相关程序集后，普通热更包构建失败并提示原因。
- [x] 不会因为误复用旧 AOTManifest 导致线上旧包加载不兼容 DLL。

---

## 13. DownloadByTags 启动资源强校验（已完成）

### 背景

`DownloadByTags` 可以减少启动下载量，但如果启动必需资源没有打上对应 Tag，下载流程可能成功，后续 AOT / DLL / 入口资源加载失败。

统一约定默认启动标签：

```text
startup
```

必须带 `startup` 的资源：

```text
AOTAssemblyManifest
HotfixAssemblyManifest
AOTCodes/*.dll.bytes
HotfixCodes/*.dll.bytes
入口场景 EntryScene
入口 Prefab EntryPrefab，如果启用
启动 UI
必要配置表
必要多语言资源
```

### 任务

- [x] `StartupDownloadMode = DownloadByTags` 时，`StartupDownloadTags` 必须包含 `startup`。
- [x] 构建前扫描 YooAsset Collector，确认启动资源具备 `startup` Tag。
- [x] FirstPackage / EmptyPackage / HotfixPackage 都执行启动资源校验。
- [x] EntryScene 或 EntryPrefab 没有被 YooAsset 收集时构建失败。
- [x] AOTCodes / HotfixCodes / Configs 没有被收集时构建失败。
- [x] 错误信息中明确指出缺失资源、缺失 Tag、所在 Collector。

实现记录：

- `HotfixRuntimeSettings.DefaultStartupTag` 统一定义默认启动标签 `startup`。
- `BuildAssetsCommand.ValidateStartupDownloadTags()` 会阻止 `DownloadByTags` 漏配 `startup`。
- `BuildAssetsCommand.ValidateStartupResourceCollection()` 会检查 `Configs`、`AOTCodes`、`HotfixCodes`、`Datas`，并在 Manifest 显式配置入口场景 / Prefab 时检查对应 Collector 收集状态。
- `DownloadByTags` 模式下，启动资源缺少 `startup` Tag 会在构建前失败，并输出资源路径、包名、分组、Collector 和当前 Tags。
- 一键首包、空包首包、普通热更包、AOT Metadata Patch 和内部“仅构建热更 YooAsset”都会执行启动资源校验。

相关位置：

```text
Assets/AssetsPackage/Resources/HotfixRuntimeSettings.asset
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildRunner.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildValidator.cs
YooAsset Collector 配置
```

验收标准：

- [x] `DownloadByTags` 漏配 startup 时构建失败。
- [x] 缺少 AOT / Hotfix / Config / Entry 资源时构建失败。
- [x] 正确配置 startup 后，首包和空包都能完成启动链路。

---

## 14. Hotfix DLL / AOT Metadata bytes 加载前校验（已完成）

### 背景

YooAssets 可以校验 Bundle 层 Hash，但 Hotfix DLL 属于可执行代码，建议在 `Assembly.Load(bytes)` 之前再做内容层校验。

### 任务

- [x] `AOTAssemblyManifest` 记录每个 AOT Metadata 文件的：
  - fileName
  - size
  - sha256
- [x] `HotfixAssemblyManifest` 记录每个 Hotfix DLL 文件的：
  - fileName
  - size
  - sha256
- [x] `LoadDllBytes` 后计算 bytes size。
- [x] `LoadDllBytes` 后计算 sha256。
- [x] AOT Metadata 加载前校验 size + sha256。
- [x] Hotfix DLL `Assembly.Load(bytes)` 前校验 size + sha256。
- [x] 校验失败时拒绝加载并输出明确错误。
- [x] 构建报告中写入 DLL hash 信息。

实现记录：

- `AssemblyFileRecord` 新增 `FileName`，并继续兼容旧 `AssemblyName` 字段。
- 构建阶段生成 `AotMetadataFiles` / `HotUpdateFiles`，记录每个 DLL bytes 的 size 和 sha256。
- 运行时 `HybridCLRAssemblyLoader.LoadDllBytes()` 读取 bytes 后会计算 size 和 sha256。
- AOT Metadata 调用 `RuntimeApi.LoadMetadataForAOTAssembly()` 前会先校验 bytes。
- Hotfix DLL 调用 `Assembly.Load(bytes)` 前会先校验 bytes。
- 校验失败时会拒绝加载，并输出文件名、期望 size/hash 和实际 size/hash。
- `BuildReports/Hotfix/*.txt` 会输出 AOT Metadata 和 Hotfix DLL hash 信息。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/AOTAssemblyManifest.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HotfixAssemblyManifest.cs
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
```

验收标准：

- [x] DLL 正常时可以加载。
- [x] 手动篡改 DLL bytes 后，运行时拒绝加载。
- [x] 手动篡改 AOT Metadata bytes 后，运行时拒绝加载。
- [x] 错误信息能指出具体文件和期望 hash / 实际 hash。

---

## 15. Hotfix DLL 依赖自动排序与依赖校验（已完成）

### 背景

当前 Hotfix DLL 的加载顺序主要依赖 `HotfixAssemblyManifest.HotUpdateAssemblies` 中的列表顺序。

如果项目只有一个 Hotfix DLL，问题不明显；但如果后续拆分为多个热更程序集，例如：

```text
Hotfix.Core.dll
Hotfix.Config.dll
Hotfix.UI.dll
Hotfix.Game.dll
Hotfix.Entry.dll
```

并且它们之间存在依赖关系，则不能依赖人工维护或 YooAsset Collector 的手动排序。

例如：

```text
Hotfix.Entry.dll 依赖 Hotfix.Game.dll
Hotfix.Game.dll  依赖 Hotfix.UI.dll
Hotfix.UI.dll    依赖 Hotfix.Core.dll
```

最终加载顺序必须是：

```text
Hotfix.Core.dll
Hotfix.UI.dll
Hotfix.Game.dll
Hotfix.Entry.dll
```

否则可能出现运行时程序集依赖缺失、类型解析失败、入口初始化失败等问题。

### 目标

在构建期自动分析 Hotfix DLL 之间的依赖关系，生成稳定、正确、可追踪的加载顺序。

运行时仍然保持简单逻辑：

```text
foreach HotfixAssemblyManifest.HotUpdateAssemblies
    Assembly.Load(bytes)
```

但 `HotUpdateAssemblies` 的顺序必须由构建期自动拓扑排序生成，而不是依赖人工填写或 Collector 排序。

### 任务

- [x] 新增 `HotfixAssemblyDependencySorter`。
- [x] 构建期读取每个 Hotfix DLL 的 `AssemblyName`。
- [x] 构建期读取每个 Hotfix DLL 的 `ReferencedAssemblies`。
- [x] 建立 `dllName -> assemblyName` 映射。
- [x] 建立 `assemblyName -> dllName` 映射。
- [x] 只保留 Hotfix DLL 内部依赖关系。
- [x] 对 Hotfix DLL 内部依赖执行拓扑排序。
- [x] 依赖 DLL 排在前面，被依赖 DLL 排在后面。
- [x] 检测循环依赖。
- [x] 检测缺失依赖。
- [x] 检测重复 `AssemblyName`。
- [x] 检测 Manifest 中记录但文件不存在的 DLL。
- [x] 检测文件存在但未写入 Manifest 的 DLL。
- [x] 将排序后的结果写入 `HotfixAssemblyManifest.HotUpdateAssemblies`。
- [x] Build Report 输出最终 DLL 加载顺序。
- [x] Build Report 输出 Hotfix DLL 依赖关系。
- [x] Build Center 中显示最终加载顺序和依赖检查结果。
- [x] 运行时不做复杂依赖排序，只按 Manifest 顺序加载。
- [x] README 中说明 Hotfix DLL 顺序由构建期自动生成，开发者不需要手动排序。

实现记录：

- `HotfixAssemblyDependencySorter.Sort()` 会读取 Hotfix DLL metadata，生成稳定拓扑顺序和依赖记录。
- `HotfixAssemblyManifest.HotUpdateAssemblies` 写入构建期排序后的最终加载顺序。
- `HotfixAssemblyManifest.HotUpdateDependencies` 记录每个 Hotfix DLL 的内部依赖。
- 构建校验会检测重复 `AssemblyName`、循环依赖、Manifest 记录但文件缺失、文件存在但未记录、依赖记录过期。
- 构建报告输出 Hotfix DLL 最终加载顺序、依赖关系、AOT / Hotfix hash。
- Build Center 显示 Hotfix DLL 加载顺序和依赖关系。
- 运行时删除启发式排序，只按 Manifest 顺序加载。

### 建议新增字段

短期可以只调整 `HotUpdateAssemblies` 的顺序，不修改 Manifest 结构。

中期建议增加依赖记录，方便 Build Center 和构建报告展示：

```csharp
[Serializable]
public sealed class AssemblyDependencyRecord
{
    public string AssemblyName;
    public string DllName;
    public List<string> DependsOn = new();
}
```

并在 `HotfixAssemblyManifest` 中增加：

```csharp
public List<AssemblyDependencyRecord> HotUpdateDependencies = new();
```

### 建议新增工具

```text
Assets/Editor/HybridCLR/BuildPipeline/HotfixAssemblyDependencySorter.cs
Assets/Editor/HybridCLR/BuildPipeline/Steps/SortHotfixAssembliesStep.cs
Assets/Editor/HybridCLR/BuildPipeline/Steps/ValidateHotfixAssemblyDependenciesStep.cs
```

### 推荐排序流程

```text
Build Initial Package / Build Hotfix Package
├── Compile Hotfix DLL
├── Copy Hotfix DLLs
├── Scan HotfixCodes/*.dll.bytes
├── Read AssemblyName
├── Read ReferencedAssemblies
├── Filter internal hotfix dependencies
├── Topological Sort
├── Validate missing dependencies
├── Validate circular dependencies
├── Write sorted HotUpdateAssemblies
└── Generate HotfixAssemblyManifest
```

### 建议实现方式

推荐在生成 `HotfixAssemblyManifest` 前执行：

```csharp
var sortedHotfixAssemblies = HotfixAssemblyDependencySorter.Sort(
    hotfixCodesPath,
    hotfixAssemblies);

manifest.HotUpdateAssemblies = NormalizeDllNames(sortedHotfixAssemblies);
```

排序原则：

```text
如果 A.dll 依赖 B.dll：
    B.dll 必须排在 A.dll 前面
```

### 相关位置

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HotfixAssemblyManifest.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
Assets/Editor/HybridCLR/BuildPipeline
Assets/AssetsPackage/AssetsHotFix/HotfixCodes
```

### 验收标准

- [x] 单 DLL 热更构建成功。
- [x] 多 DLL 无依赖时构建成功，并且输出顺序稳定。
- [x] `A.dll` 依赖 `B.dll` 时，Manifest 中 `B.dll` 排在 `A.dll` 前面。
- [x] 多层依赖时顺序正确，例如 `Core -> UI -> Game -> Entry`。
- [x] 存在循环依赖时构建失败。
- [x] 存在缺失依赖时构建失败。
- [x] 存在重复 `AssemblyName` 时构建失败。
- [x] Manifest 中记录但文件不存在时构建失败。
- [x] 文件存在但未记录到 Manifest 时构建失败或给出明确警告。
- [x] 构建报告能查看最终 DLL 加载顺序。
- [x] 构建报告能查看 DLL 依赖关系。
- [x] Build Center 能显示依赖排序结果。
- [x] 运行时不再依赖人工维护 DLL 顺序。
- [x] 运行时按 Manifest 顺序加载所有 Hotfix DLL 成功。

# P1 商业化前建议完善

## 16. ReleaseProfile 发布配置（已完成）

### 背景

当前配置分散在 RuntimeSettings、RemoteSettings、BuildTarget、PlayerSettings、Manifest 字段中。商业化发布需要一个稳定的发布 Profile。

### 任务

- [x] 新增 `HotfixReleaseProfile.asset`。
- [x] 管理：
  - BuildTarget
  - AppVersion
  - AppVersionMin
  - AppVersionMax
  - ResourceVersion
  - HotfixVersion
  - RemoteEnvironment
  - Channel
  - Region
  - StartupPackageMode
  - StartupDownloadMode
  - StartupDownloadTags
  - EntryTypeName
  - EntryMethodName
  - 是否允许 Development CDN
- [x] Build Center 绑定 ReleaseProfile。
- [x] 没有选择 ReleaseProfile 时禁止正式构建。
- [x] ReleaseProfile 支持保存、复制、导出。
- [x] ReleaseProfile Inspector 统一编辑发布配置，必填项用 `*` 标记，自动生成 / 派生状态灰色只读。
- [x] `PlayerPlayMode`、CDN 模板、HTTPS、域名白名单和灰度 CDN 配置收口到 ReleaseProfile，并自动同步到底层 asset。

实现记录：

- 新增 `HotfixReleaseProfile`，统一保存发布目标、App 兼容区间、资源版本、远端环境、CDN 模板、PlayerPlayMode、启动策略和 CodeEntry。
- 新增 `HotfixReleaseProfileInspector`，发布人员选中 `HotfixReleaseProfile.asset` 即可看到带 `*` 的必填项、可选覆盖 / 高级配置、灰色只读同步状态和校验 / 构建操作。
- 构建中心可绑定 ReleaseProfile，并提供创建 / 保存当前配置 / 复制 / 导出 JSON。
- 一键构建会先应用 ReleaseProfile 到 `PlayerSettings.bundleVersion`、`HotfixRuntimeSettings`、`HotfixRemoteSettings`、`HotfixBuildProfile` 和 `HotfixAssemblyManifest`。
- `ResourceVersion` 非空时会作为 YooAsset `PackageVersion`，为空时继续使用时间戳自动版本。
- Player Build 预处理器会读取选中的 ReleaseProfile；`Production` 或关闭 `AllowDevelopmentCdn` 的正式 Profile 会阻断 Development CDN。
- Build Center 校验页显示 ReleaseProfile、正式发布保护和配置错误。

相关位置：

```text
Assets/Editor/HybridCLR/HotfixReleaseProfile.asset
Assets/Editor/HybridCLR/BuildPipeline/HotfixReleaseProfile.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixReleaseProfileInspector.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildCenterWindow.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildValidator.cs
Assets/Editor/HybridCLR/BuildPipeline/HotfixBuildRunner.cs
Assets/Editor/HybridCLR/HotfixBuildProfile.cs
Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRemoteSettings.cs
```

验收标准：

- 不再依赖散落配置完成发布。
- 发布人员只需要编辑 ReleaseProfile；底层 RuntimeSettings、RemoteSettings、BuildProfile 作为同步产物和运行时读取资产保留。
- 开发、测试、预发、正式环境可以各自维护 Profile。
- 资源包版本可由 ReleaseProfile 固定，方便发布记录和回滚。
- Release 包不会误连 Development CDN。

---

## 17. 建立完整版本协议

- [ ] 设计 `AppVersion`、`ResourceVersion`、`CompatibleAppVersion`、`MinAppVersion`。
- [ ] 明确并记录：
  - AppVersion
  - PackageVersion
  - ResourceVersion
  - AotVersion
  - HotfixVersion
  - RequiredAotVersion
  - MinAppVersion
  - MaxAppVersion
  - ReleaseChannel
- [ ] 支持强更、弱更、灰度、回滚版本。
- [ ] 资源版本不要只依赖当前时间字符串。
- [ ] 生成发布记录：版本号、Git commit、构建平台、资源清单、上传地址。
- [ ] 记录客户端当前使用的资源版本，便于日志和问题定位。

相关位置：

```text
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
YooAsset build output
服务端版本接口或 CDN manifest 管理
```

验收标准：

- 能回答“某个线上用户当前运行的是哪个 App 版本 + 哪个资源版本 + 哪个 DLL 版本”。
- 能快速将线上资源回滚到上一稳定版本。

---

## 18. 增加 manifest / DLL 安全校验

### 说明

P0 第 14 项先做 bytes size + sha256。
本项继续完善 Manifest 签名、公钥验签、密钥隔离等生产级安全能力。

### 任务

- [ ] 对资源发布清单增加签名校验。
- [ ] 对热更 DLL 增加白名单、hash、签名或公钥校验。
- [ ] 在 `Assembly.Load(bytes)` 之前校验 DLL 的 SHA256 / 签名 / 白名单。
- [ ] 防止 CDN 被污染后客户端加载未授权 DLL。
- [ ] 明确测试环境和正式环境的签名密钥隔离策略。
- [ ] 避免把可逆弱加密当作安全方案。
- [ ] 移除源码中硬编码的 Web 解密密钥，至少改为环境隔离的配置和构建期注入。
- [ ] 如果继续使用 Web 解密，补充密钥轮换、版本兼容和泄露后的废弃策略。
- [ ] 正式环境只接受正式签名产物。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureInitializePackage.cs
YooAsset manifest 下载和校验流程
资源发布脚本
```

验收标准：

- 任意篡改 DLL 或 manifest 后客户端拒绝加载。
- 正式环境只接受正式签名产物。

---

## 19. 强化热更程序集依赖治理和运行时诊断

说明：

- 第 15 项已完成 Hotfix DLL 构建期依赖排序、循环依赖检测、重复 `AssemblyName` 检测、Manifest 记录与文件一致性检查。
- 本项不再重复“自动排序”本身，后续聚焦第 15 项之上的生产级依赖治理、白名单和运行时诊断。

- [ ] 检查热更程序集是否引用了不允许热更、不可用或平台不兼容的程序集。
- [ ] 建立 Hotfix DLL 外部引用白名单，例如 Unity、QFramework、主工程稳定 API、允许的第三方库。
- [ ] 构建阶段发现外部引用缺失、禁止引用、跨平台依赖不一致时直接失败。
- [ ] 依赖分析尽量使用只读 metadata 方式，避免在 Unity Editor 构建期把 Hotfix DLL 加载进当前 AppDomain。
- [ ] 运行时加载失败时输出具体程序集、依赖名、版本信息和当前 `HotfixVersion + RequiredAotVersion`。
- [ ] 处理重复加载、已加载旧版本、跨版本兼容等边界，并给出可诊断日志。
- [ ] Build Center 展示外部引用检查结果和风险项。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
Assets/AssetsPackage/Scripts/Hotfix/*/*.asmdef
```

验收标准：

- 禁止或缺失外部依赖会在构建期失败，而不是运行时崩溃。
- 运行时加载失败时可以定位到具体 DLL、依赖名和版本组合。
- 构建期依赖分析不会污染当前 Editor AppDomain。

---

## 20. 完善热更入口生命周期

- [ ] 明确热更入口是场景、Prefab、静态方法还是统一 Bootstrap 类。
- [ ] 定义统一热更入口接口，例如 `IHotfixEntry`。
- [ ] 入口方法支持上下文参数：资源版本、package、启动参数、环境配置。
- [ ] 反射调用入口前校验方法签名，例如是否 static、参数是否匹配、返回值是否允许。
- [ ] 入口方法异常要输出类型、方法名、内部异常、堆栈和当前版本信息，方便线上诊断。
- [ ] 将 `HotfixDemo/GameMainApp` 从空壳补成完整示例，展示 Model / System / Utility 注册和热更业务启动流程。
- [ ] 热更业务初始化失败时能回到主工程错误页，而不是静默失败。
- [ ] 增加热更模块的退出、重启、清理策略。

说明：

- `CodeEntry` 已作为当前统一入口。
- 本项更关注热更业务入口的生命周期、上下文和异常治理。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HotfixAssemblyManifest.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Boot.cs
Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GameMainApp.cs
```

验收标准：

- 热更业务有唯一、清晰、可测试的入口。
- 后续接入真实项目时不需要把初始化逻辑散落到场景脚本里。

---

## 21. 改善 YooAssetKit 资源句柄生命周期

- [ ] `LoadAssetAsync` 不应回调后立即无条件 `Release` 长生命周期资源。
- [ ] 为资源加载返回可释放 handle，或提供统一引用计数封装。
- [ ] 所有加载 API 检查 `handle.Status` 和 `LastError`。
- [ ] 异步加载失败时不要静默返回 `null`。
- [ ] 区分短生命周期数据资源和长期持有资源。
- [ ] `LoadDllBytes` 明确以 byte[] 拷贝作为边界，并在注释或接口上约束不要向外暴露已释放 TextAsset。
- [ ] 场景加载失败时要把错误传回 `Boot` 或流程管理器。

相关位置：

```text
Assets/3rd/QFramework/Toolkits/YooAssetKit/YooAssetKit.cs
Assets/3rd/QFramework/Toolkits/YooAssetKit/Base/YooAssetPanelLoaderPool.cs
Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/Test.cs
```

验收标准：

- UI、Prefab、配置、音频等资源不会因为过早释放导致偶现丢失。
- 所有资源加载失败都有可观察的错误路径。

---

## 22. 完善缓存管理策略和 LastGood 回滚

- [ ] 不要在每次启动后无条件清理未使用 bundle，需评估启动耗时和复用收益。
- [ ] 支持按 package、版本、标签、空间阈值清理缓存。
- [ ] 增加“清理失败不阻断启动”的策略配置。
- [ ] 至少保留上一组可用 AOT + Hotfix 版本，支持热更失败后回滚。
- [ ] 支持用户设置页手动清理缓存。
- [ ] 增加缓存占用统计和日志。
- [ ] 记录 `LastGoodAotVersion`、`LastGoodHotfixVersion`、`LastGoodPackageVersion`。
- [ ] 记录 `LastFailedAotVersion`、`LastFailedHotfixVersion`、`LastFailedPackageVersion`。
- [ ] 连续失败 N 次自动回退 LastGood。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureClearCacheBundle.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs
```

验收标准：

- 热更新缓存不会无限膨胀。
- 热更失败后可以回滚上一稳定版本。
- 缓存清理不会显著拖慢每次启动。

---

## 23. 规范多 package 使用策略

- [ ] 当前阶段默认保持单 package：`DefaultPackage`。
- [ ] 仅当生命周期明显不同才拆 package，例如 DLC、语音包、多语言包、RawFile、大型地图包、活动资源。
- [ ] 如果启用 `RawFilePackage`，补齐初始化、版本、manifest、下载、失败兜底、缓存清理全链路。
- [ ] 禁止只按文件夹类型拆 package。
- [ ] 增加跨 package 资源依赖检查。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureInitializePackage.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureCreateDownloader.cs
```

验收标准：

- 每个 package 都有明确职责、版本策略和失败策略。
- 新增 package 不会破坏默认启动链路。

---

## 24. 构建报告与发布产物管理

### 背景

原 TODO 的“统一构建入口”和“发布产物管理”保留，但现在应与 Build Center / One Click 构建合并推进。

### 任务

- [ ] 每次构建生成 `HotfixBuildReport.json`。
- [ ] 报告内容包含：
  - buildMode
  - appVersion
  - target
  - buildOptions
  - remoteEnvironment
  - startupPackageMode
  - startupDownloadMode
  - packageName
  - packageVersion
  - aotVersion
  - hotfixVersion
  - requiredAotVersion
  - outputPath
  - file list
  - hash list
  - success / failed reason
- [ ] 资源包输出目录按平台、环境、版本归档。
- [ ] 生成发布 manifest：资源版本、app version、commit、构建时间、操作者。
- [ ] 上传 CDN 前后做 hash 对比。
- [ ] 支持一键回滚 CDN 指针。
- [ ] 发布完成后自动生成 release note 或变更摘要。

建议路径：

```text
Library/HotfixBuildReports/
Bundles/{Platform}/{Environment}/{PackageVersion}/
Releases/{Platform}/{Environment}/{PackageVersion}/publish_manifest.json
```

验收标准：

- 任意线上版本都能追溯构建来源。
- 发布和回滚不依赖手工拷贝文件。
- 出现线上问题时能快速还原构建输入和输出。

---

## 25. 修正构建管线和文档不一致

### 任务

- [ ] README 只推荐 Build Center 和 One Click 构建入口。
- [ ] 删除或移动过期的手动构建步骤。
- [ ] 明确当前使用的 YooAsset 构建管线。
- [ ] 不再把 SBP 切换作为默认目标，除非项目明确需要。
- [ ] 更新 Unity 版本说明。
- [ ] 更新 HybridCLR Generate All 说明，推荐 `Generate All Safe`。
- [ ] 新增：
  - 首包构建流程
  - 热更包发布流程
  - AOT Metadata Patch 流程
  - CDN 目录结构
  - 回滚流程
  - 常见错误排查
  - DownloadByTags startup 规则
  - AOT / Hotfix Manifest 兼容规则

相关位置：

```text
README.md
ProjectSettings/ProjectVersion.txt
Assets/Editor/HybridCLR/BuildAssetsCommand.cs
Docs
```

验收标准：

- 文档、项目版本、实际构建脚本一致。
- 按文档操作不会走到过期菜单或错误流程。
- 新成员按文档能完成一次首包构建和一次热更包发布。

---

## 26. BackgroundDownload 生效规则

### 背景

Hotfix DLL 已加载后，一般不能在当前进程卸载并替换。后台下载的新 DLL 应该下次冷启动生效。

### 任务

- [ ] 明确文档：后台下载的新 Hotfix DLL 下次冷启动生效。
- [ ] 增加 `CurrentHotfixVersion`。
- [ ] 增加 `PendingHotfixVersion`。
- [ ] 下载完成后记录 Pending 版本。
- [ ] 本次进程继续使用 Current 版本。
- [ ] 下次冷启动切换到 Pending 版本。
- [ ] 禁止业务层下载完成后直接替换已加载程序集。
- [ ] 可提示用户“重启后生效”。

验收标准：

- 当前运行 A，后台下载 B，本次进程仍使用 A。
- 下次冷启动加载 B。
- 不会出现代码版本和资源版本半切换状态。

---

## 27. 日志、错误码和线上诊断

- [ ] 为核心流程定义日志分类：
  - Startup
  - RemoteVersion
  - Manifest
  - Download
  - AOT
  - HotfixAssembly
  - Entry
  - Cache
  - Rollback
- [ ] 定义错误码。
- [ ] 正式包默认只输出 Warning / Error / Fatal。
- [ ] 开发包输出 Debug / Info / Warning / Error / Fatal。
- [ ] 日志级别由构建环境或运行配置控制，避免手动改代码切换日志输出。
- [ ] 正式包避免输出 CDN 地址、文件路径、用户隐私、密钥、签名明文等敏感信息。
- [ ] 线上 Error / Fatal 日志上报时附带：
  - AppVersion
  - PackageVersion
  - AotVersion
  - HotfixVersion
  - BuildTarget
  - RemoteEnvironment
  - Device
  - NetworkType

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure
Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies
Assets/AssetsPackage/Scripts/Main/Runtime/UI
```

验收标准：

- 线上热更失败可以定位到具体阶段。
- 日志不会泄露敏感信息。
- 错误码能用于用户提示和后台统计。

---

## 28. CI / 命令行构建入口

### 任务

- [ ] Build Center 对应能力提供命令行入口。
- [ ] 支持命令行参数：
  - buildMode
  - target
  - profile
  - environment
  - version
  - outputPath
  - upload
- [ ] 构建失败返回非 0 exit code。
- [ ] CI 中至少支持：
  - Validate Only
  - Build Initial Package
  - Build Hotfix Package
- [ ] CI 输出构建报告。
- [ ] CI 可以校验 Git 工作区是否干净。

验收标准：

- 本地和 CI 使用同一套构建逻辑。
- 不需要维护两套构建脚本。
- CI 构建失败能明确定位原因。

---

# P2 长期优化

## 29. 增加自动化测试和 smoke test

- [ ] 增加 Editor 测试：构建配置合法性、manifest 生成、AOT 列表生成。
- [ ] 增加 PlayMode 测试：离线启动、模拟下载、下载失败、取消下载。
- [ ] 增加热更 DLL 加载 smoke test。
- [ ] 增加入口场景加载 smoke test。
- [ ] 增加 AOT Metadata 加载 smoke test。
- [ ] 增加 LastGood 回滚 smoke test。
- [ ] CI 中至少跑一个最小热更新流程。

验收标准：

- 改动热更新流程后可以快速知道是否破坏基础启动。

---

## 30. 改善用户更新体验

- [ ] 下载确认界面展示格式化后的文件数量和大小，例如“需要更新 15 个文件，共 23.5 MB”。
- [ ] 下载确认界面展示网络状态、是否建议 Wi-Fi。
- [ ] 下载失败支持重试、退出、使用本地缓存。
- [ ] 展示下载速度、剩余时间、单文件进度、已下载大小 / 总大小。
- [ ] 当前文件和错误信息不应暴露过多技术细节，面向用户展示友好文案，详细错误写日志。
- [ ] 支持后台下载或进入大厅后延迟下载非关键资源。
- [ ] 强更时展示明确提示。
- [ ] 弱更时允许跳过。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/UI
Assets/AssetsPackage/Scripts/Main/Runtime/Events/DownloadEvents.cs
```

验收标准：

- 弱网、断网、取消、重试场景体验完整。

---

## 31. 清理工程生成物和大文件入库规则

- [ ] 评估是否需要将 `HybridCLRData` 生成目录入库。
- [ ] 评估是否需要将 `Assets/StreamingAssets/yoo` 的构建产物入库。
- [ ] 明确哪些 `.bytes`、bundle、manifest 是示例必需，哪些是构建产物。
- [ ] 更新 `.gitignore`，避免 3G 级别生成物长期污染仓库。
- [ ] 保留必要示例资源时，给出重新生成说明。

相关位置：

```text
.gitignore
HybridCLRData
Assets/StreamingAssets/yoo
Assets/AssetsPackage/AssetsHotFix/AOTCodes
Assets/AssetsPackage/AssetsHotFix/HotfixCodes
```

验收标准：

- clone、pull、切分支速度可接受。
- 构建产物和源码边界清晰。

---

## 32. 统一编码和注释质量

- [ ] 修复乱码注释。
- [ ] 将核心流程日志统一中英文风格。
- [ ] 为关键错误补充错误码或统一错误类型。
- [ ] 将公开字段命名规范化，例如 `_rawfilwPkgName` 拼写。
- [ ] 避免 public 字段暴露过多内部状态。
- [ ] 将 `ProcedureManager` 内部状态改为属性或上下文对象，避免所有 Procedure 任意修改 public 字段。
- [ ] 降低 `CoroutineController.manager` 静态耦合，增加 null 检查和生命周期保护。
- [ ] 评估是否由 `ProcedureManager` 自身承载协程调度，避免所有流程依赖全局 MonoBehaviour。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/UI/UISceneMessageBox.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs
Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GameMainApp.cs
```

验收标准：

- 日志和错误能帮助定位线上问题。
- 代码命名不会增加维护成本。

---

## 33. 完善文档

- [ ] 新增“首包构建流程”。
- [ ] 新增“热更包发布流程”。
- [ ] 新增“AOT Metadata Patch 流程”。
- [ ] 新增“CDN 目录结构和版本命名规则”。
- [ ] 新增“线上回滚流程”。
- [ ] 新增“常见错误排查”。
- [ ] 新增“多 package 拆分原则”。
- [ ] 新增“HybridCLR AOT metadata 更新规则”。
- [ ] 新增“AOTAssemblyManifest / HotfixAssemblyManifest 拆分和兼容规则”。
- [ ] 新增“下载失败、重试、取消、离线降级和回滚流程”。

相关位置：

```text
README.md
Docs
```

验收标准：

- 没有接触过项目的人能按文档完成一次完整出包和热更发布。

---

## 34. 下载能力扩展：差分、后台、带宽和并发

- [ ] 评估是否需要差分更新能力，例如 Delta Patch、二进制补丁或按 bundle 粒度进一步拆分。
- [ ] 大版本更新时统计全量下载量，决定是否引入差分方案。
- [ ] 支持后台下载策略，明确 iOS / Android / PC 在切后台后的行为和限制。
- [ ] 下载并发数不要固定为 10，应支持配置或根据网络条件动态调整。
- [ ] 增加下载带宽限制能力，避免更新流程抢占业务网络。
- [ ] 增加按资源重要性分层下载：启动必需资源优先，非关键资源延迟或后台下载。

相关位置：

```text
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureCreateDownloader.cs
Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureDownloadPackageFiles.cs
YooAsset downloader 创建和调度策略
```

验收标准：

- 大包更新时下载量、下载时机和网络占用可控。
- 弱网环境下可以通过并发、暂停、重试、后台下载策略改善体验。

---

## 35. 灰度发布

- [ ] 根据 userId / deviceId 做稳定 hash。
- [ ] 灰度命中用户使用 Gray CDN 或 Gray Version。
- [ ] 未命中用户使用 Production CDN。
- [ ] 灰度失败可回退正式 CDN。
- [ ] 支持按渠道、地区、版本灰度。
- [ ] 上报灰度命中状态。
- [ ] Build Report 记录灰度版本信息。

验收标准：

- 可以按百分比灰度资源版本。
- 灰度异常时可以关闭灰度或回滚。

---

## 36. Luban 配置热更集成

- [ ] 明确 Luban 配置表属于 Hotfix manifest 管理范围，还是独立 Config manifest 管理。
- [ ] 热更流程中增加配置表版本、hash、兼容性校验。
- [ ] 下载 Hotfix 后按版本加载对应配置表，避免 DLL 和配置结构不匹配。
- [ ] 配置加载失败时支持回滚到上一份可用配置。
- [ ] 增加配置表 smoke test，覆盖字段新增、删除、类型变化等场景。

相关位置：

```text
LubanConfig
Assets/AssetsPackage/AssetsHotFix/Datas
Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GenCodes
Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/Test.cs
```

验收标准：

- 热更 DLL、配置表和 App 版本之间有明确兼容关系。
- 配置热更失败不会导致启动期崩溃或业务读取空数据。

---

# 已删除 / 合并 / 降级的过期 TODO

## A. 删除：继续围绕单一 AssemblyManifest 优化的任务

原因：

当前框架已经完成 `AOTAssemblyManifest` 和 `HotfixAssemblyManifest` 拆分。后续不再围绕旧 `AssemblyManifest` 设计新功能，只保留迁移兼容。

处理方式：

- 删除“优化单一 AssemblyManifest”的相关描述。
- 新任务全部基于 split manifest 设计。

## B. 合并：泛泛的“统一构建入口”

原因：

原 TODO 中的“统一构建入口”描述过宽，无法直接指导落地。

处理方式：

- 合并为：
  - Build 菜单收口与构建中心
  - 一键首包构建
  - 一键热更包构建
  - AOT Metadata Patch 高级构建模式
  - CI / 命令行构建入口

## C. 删除：普通热更包自动 Generate All 的方向

原因：

普通热更包默认自动执行 `Generate All` 会混淆 AOT 基线与 Hotfix 更新，容易导致线上旧 App 与新 AOT Metadata / Hotfix DLL 兼容关系不清晰。

处理方式：

- 首包构建：自动执行 `Generate All Safe`。
- 普通热更包构建：默认不执行 `Generate All`。
- AOT Metadata Patch：明确选择后才执行 `Generate All Safe`。

## D. 降级：RawFilePackage 完整链路作为 P1 必做项

原因：

当前建议仍是默认保持单 Package：`DefaultPackage`。RawFilePackage 只有在确实存在独立生命周期资源时才需要完善。

处理方式：

- RawFilePackage 全链路治理降级到 P2 / 多 Package 策略。
- P1 只保留 Package 拆分原则和风险说明。

## E. 调整：SBP / Builtin 构建管线争议

原因：

当前更重要的是“文档与实际代码一致”，不是立刻切换 SBP。切换构建管线本身不是热更框架上线阻断项。

处理方式：

- 删除“必须切到 SBP”的倾向。
- 保留“文档与实际构建管线一致”的任务。
- 只有当项目明确需要 SBP 特性时，再单独立项。

## F. 删除：EntryPrefabAddress 独立入口闭环任务

原因：

当前入口已经由 `CodeEntry` 收口，`EntrySceneAddress` / `EntryPrefabAddress` 不再作为主工程启动入口决策项。

处理方式：

- 删除 P0 中旧的 Prefab 独立入口闭环 TODO。
- 热更入口的生命周期、上下文、异常治理继续归入 `CodeEntry` 入口生命周期任务。

---

# 建议实施顺序

## 第一阶段：保留现有能力，收口构建入口

- [x] 修复 play mode 打包风险。
- [x] 修复取消下载卡死。
- [x] 增加网络失败本地兜底。
- [x] 抽离 CDN 配置。
- [x] 明确首包资源必需项。
- [x] 拆分 AOTAssemblyManifest 和 HotfixAssemblyManifest。
- [ ] Build 菜单收口与构建中心。
- [ ] HybridCLR Generate All Safe。
- [ ] 一键首包构建。
- [ ] 一键热更包构建。

目标：

```text
新人只通过 Build Center 完成首包和热更包构建。
```

## 第二阶段：防止热更事故

- [ ] AOT Metadata Patch 高级模式。
- [x] AOTManifest 过期校验。
- [x] DownloadByTags 启动资源强校验。
- [x] Hotfix DLL / AOT Metadata bytes 加载前校验。
- [x] Hotfix DLL 依赖自动排序与依赖校验。

目标：

```text
防止 AOT 基线误复用、启动资源漏配、DLL 被篡改。
```

## 第三阶段：商业化发布治理

- [x] ReleaseProfile 发布配置。
- [ ] 完整版本协议与版本记录。
- [ ] Manifest 签名校验。
- [ ] 构建报告与发布产物管理。
- [ ] CDN 原子发布与回滚。
- [ ] CI / 命令行构建入口。

目标：

```text
构建可追溯、发布可回滚、CI 可复用、正式环境安全。
```

## 第四阶段：运行期稳定性

- [ ] 热更程序集依赖排序和依赖校验。
- [ ] 热更入口生命周期完善。
- [ ] BackgroundDownload 生效规则。
- [ ] YooAssetKit 资源句柄生命周期。
- [ ] 缓存管理与 LastGood 回滚。
- [ ] 日志、错误码和线上诊断。

目标：

```text
热更运行期可诊断、可恢复、可长期维护。
```

## 第五阶段：长期扩展

- [ ] 自动化测试和 Smoke Test。
- [ ] 用户更新体验优化。
- [ ] 工程生成物和 Git 入库规则。
- [ ] 多 Package 策略。
- [ ] 下载能力扩展。
- [ ] 灰度发布。
- [ ] Luban 配置热更集成。

目标：

```text
形成长期可迭代的商业化热更框架。
```

---

# 当前建议结论

当前项目建议先保持单 package：`DefaultPackage`。

原因：

- Hotfix DLL、AOT metadata、配置和 CodeEntry 入口现在强依赖同一个启动链路。
- 单 package 更容易保证版本一致性。
- 当前多 package 相关代码还不完整，尤其是 raw file package 的失败兜底、版本和缓存策略。

下一阶段最应该做的不是继续增加底层菜单，而是收口为：

```text
Build Center
一键首包构建
一键热更包构建
AOT Metadata Patch 高级入口
Internal 底层工具菜单
```

同时必须补上：

```text
Generate All Safe
```

这些完成后，框架才更接近“团队可用、可交付、可商业化”的状态。
