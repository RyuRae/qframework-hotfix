# 热更新框架优化 TODO

更新时间：2026-04-30

本文档用于记录当前 QFramework + YooAsset + HybridCLR 热更新框架后续需要改善的事项。优先级含义：

- P0：上线前必须解决，影响启动、更新、容灾或打包正确性。
- P1：建议在商业化版本前完善，影响安全、可维护性、发布效率。
- P2：长期优化项，影响体验、工程卫生和扩展性。

## 当前框架概览

当前启动链路：

1. `Assets/AssetsPackage/Scripts/Main/Runtime/Boot.cs`
2. `ProcedureManager`
3. 初始化 YooAsset package
4. 请求远端 package version
5. 更新 package manifest
6. 创建 downloader 并下载资源
7. 加载 AOT metadata
8. 加载热更 DLL
9. 清理缓存
10. 加载入口场景 `main`

当前主要使用：

- YooAsset：资源打包、资源下载、manifest 管理。
- HybridCLR：AOT metadata 补充、热更程序集加载。
- QFramework：启动流程、事件、UI、资源加载封装。
- Luban：配置表生成和读取。

## P0 必须优先处理

### 1. 修复 Player 环境误用 EditorSimulateMode 的风险（已完成）

- [x] 在打包前强制校验 `HotfixRuntimeSettings.PlayerPlayMode`，禁止 Player 使用 `EditorSimulateMode`。
- [x] 在 `BuildPlayerCommand` 和全局构建预处理器中按目标环境自动设置 `HostPlayMode` / `OfflinePlayMode` / `WebPlayMode`。
- [x] 增加启动时保护：非 Editor 环境检测到 `EditorSimulateMode` 时给出明确错误并终止流程。
- [x] 将运行模式从场景序列化字段迁移到可配置的构建 profile，避免人工改场景。

相关位置：

- `Assets/Scenes/Boot.unity`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Boot.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRuntimeSettings.cs`
- `Assets/AssetsPackage/Resources/HotfixRuntimeSettings.asset`
- `Assets/Editor/HybridCLR/BuildPlayerCommand.cs`
- `Assets/Editor/HybridCLR/HotfixBuildProfile.cs`
- `Assets/Editor/HybridCLR/HotfixBuildProfile.asset`

验收标准：

- Windows / Android / iOS / WebGL 打包时不会带着错误 play mode 出包。
- CI 或本地构建脚本能自动失败并输出清晰原因。

### 2. 处理用户取消下载导致流程卡死（已完成）

- [x] 为下载确认弹窗增加取消回调。
- [x] 用户取消下载时明确进入失败 / 退出更新路径，离线启动和本地缓存兜底归入第 3 项继续完善。
- [x] 下载开始后支持通过 `ProcedureManager.CancelDownload` 取消下载，并将 downloader、UI、FSM 状态一起收口。
- [x] 评估并实现下载暂停 / 继续能力，已预留 `TryPauseDownload` / `TryResumeDownload` 状态接口。
- [x] 下载失败后提供用户可点击的“重试 / 退出更新”路径，本地缓存兜底归入第 3 项。
- [x] `ProcedureCreateDownloader` 不再只在确认时推进 FSM，取消时会明确终止流程。
- [x] loading 状态、弹窗状态、FSM 状态保持一致，避免界面隐藏但流程悬挂。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureCreateDownloader.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureDownloadPackageFiles.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/UI/UISceneMessageBox.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/UI/UIPanelRoot.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Events/DownloadEvents.cs`

验收标准：

- 点击取消后不会无限等待。
- 下载中取消不会留下未完成状态或卡住协程。
- 取消路径有明确日志、UI 提示和业务结果。

### 3. 增加网络失败后的本地缓存兜底（已完成）

- [x] `RequestPackageVersionAsync` 失败时允许使用内置 manifest 或上次缓存 manifest 启动。
- [x] `UpdatePackageManifestAsync` 失败时允许使用已缓存版本启动。
- [x] 版本请求、manifest 更新、资源下载失败时增加业务层重试，不只依赖 YooAsset `failedTryAgain`。
- [x] 对服务器不可达、DNS 失败、CDN 404、manifest 损坏等失败类型分别设计恢复路径。
- [x] 区分强更资源和弱更资源：`MustUpdate` 阻断，`AllowCached` / `WifiOnly` / `BackgroundDownload` 允许本地缓存启动。
- [x] 增加启动策略配置：必须更新 / 可跳过更新 / 仅 Wi-Fi 更新 / 后台下载。
- [x] 给所有网络失败路径补充用户可理解的错误提示和重试按钮。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureRequestPackageVersion.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureUpdatePackageManifest.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureDownloadPackageFiles.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/HotfixLocalManifestUtility.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRuntimeSettings.cs`

验收标准：

- 断网情况下，只要本地存在可用资源，就可以进入游戏。
- 远端 manifest 异常不会导致所有用户无法启动。

### 4. 抽离 CDN 地址和环境配置（已完成）

- [x] 将 `http://127.0.0.1:8080/TestProject/PC` 从代码中移除。
- [x] 新增热更新配置文件，支持开发、测试、预发、正式环境。
- [x] 支持主 CDN 和备用 CDN 使用不同地址。
- [x] 启动时校验 main URL 和 fallback URL 不应完全相同，否则给出配置错误提示。
- [x] 支持按平台、渠道、地区生成远端资源地址。
- [x] 支持 HTTPS，并预留证书、域名、CDN 灰度切换能力。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureInitializePackage.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/HotfixRemoteSettings.cs`
- `Assets/AssetsPackage/Resources/HotfixRemoteSettings.asset`

验收标准：

- 切换 CDN 不需要改代码重新出包。
- 一份安装包可以根据环境或渠道使用不同远端资源地址。

### 5. 明确首包和空包启动策略（已完成）

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

- `Assets/AssetsPackage/AssetsHotFix/Configs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureRequestPackageVersion.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureUpdatePackageManifest.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureLoadAOTMetadata.cs`

验收标准：

- 首包、空包、离线启动三种策略有明确选择，不再混用。
- 首包必需资源缺失时构建直接失败。
- 空包启动能先拿到远端版本信息，再决定下载哪个 AOT 和 Hotfix。
- 无网络但本地存在可用缓存时，可以按策略进入游戏或给出明确阻断原因。

### 6. 核心重构：AOT 和 Hotfix Manifest 真正分离（已完成）

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

- `Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/AssemblyManifest.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureLoadAOTMetadata.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureLoadAssembly.cs`
- `Assets/Editor/HybridCLR/BuildAssetsCommand.cs`

验收标准：

- AOT 不变化时，发布 Hotfix 不会重新下载 AOT metadata。
- AOT 变化时，客户端会自动使用 `RequiredAotVersion` 对应的新版 AOT。
- App 版本、平台、AOT 版本不兼容时，客户端拒绝加载并给出明确错误。
- 线上回滚可以准确回滚到一组兼容的 AOT + Hotfix 组合。

## P1 商业化前建议完善

### 7. 建立完整版本协议

- [ ] 设计 `AppVersion`、`ResourceVersion`、`CompatibleAppVersion`、`MinAppVersion`。
- [ ] 支持强更、弱更、灰度、回滚版本。
- [ ] 资源版本不要只依赖当前时间字符串。
- [ ] 生成发布记录：版本号、Git commit、构建平台、资源清单、上传地址。
- [ ] 记录客户端当前使用的资源版本，便于日志和问题定位。

相关位置：

- `Assets/Editor/HybridCLR/BuildAssetsCommand.cs`
- YooAsset build output
- 服务端版本接口或 CDN manifest 管理

验收标准：

- 能回答“某个线上用户当前运行的是哪个 App 版本 + 哪个资源版本 + 哪个 DLL 版本”。
- 能快速将线上资源回滚到上一稳定版本。

### 8. 增加 manifest / DLL 安全校验

- [ ] 对资源发布清单增加签名校验。
- [ ] 对热更 DLL 增加白名单、hash、签名或公钥校验。
- [ ] 在 `Assembly.Load(bytes)` 之前校验 DLL 的 SHA256 / 签名 / 白名单。
- [ ] 防止 CDN 被污染后客户端加载未授权 DLL。
- [ ] 明确测试环境和正式环境的签名密钥隔离策略。
- [ ] 避免把可逆弱加密当作安全方案。
- [ ] 移除源码中硬编码的 Web 解密密钥，至少改为环境隔离的配置和构建期注入。
- [ ] 如果继续使用 Web 解密，补充密钥轮换、版本兼容和泄露后的废弃策略。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureInitializePackage.cs`
- YooAsset manifest 下载和校验流程
- 资源发布脚本

验收标准：

- 任意篡改 DLL 或 manifest 后客户端拒绝加载。
- 正式环境只接受正式签名产物。

### 9. 优化热更程序集加载顺序和依赖校验

- [ ] 不仅依赖 `AssemblyManifest.HotUpdateAssemblies` 的手工顺序。
- [ ] 构建阶段分析程序集依赖，生成稳定加载顺序。
- [ ] 对多个 Hotfix DLL 做依赖拓扑排序，依赖程序集必须先于被依赖程序集加载。
- [ ] 构建阶段发现循环依赖、缺失依赖、跨平台依赖不一致时直接失败。
- [ ] 检查热更程序集是否引用了不允许热更或不存在的程序集。
- [ ] 加载失败时输出具体程序集、依赖名、版本信息。
- [ ] 处理重复加载、已加载旧版本、跨版本兼容等边界。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/HybridCLRAssemblyLoader.cs`
- `Assets/Editor/HybridCLR/BuildAssetsCommand.cs`
- `Assets/AssetsPackage/Scripts/Hotfix/*/*.asmdef`

验收标准：

- 新增热更程序集后无需靠人工猜顺序。
- 依赖不完整时构建失败，而不是运行时崩溃。

### 10. 完善热更入口生命周期

- [ ] 明确热更入口是场景、Prefab、静态方法还是统一 Bootstrap 类。
- [ ] 实现并使用 `EntryPrefabAddress`，或移除无用字段。
- [ ] 入口方法支持上下文参数：资源版本、package、启动参数、环境配置。
- [ ] 反射调用入口前校验方法签名，例如是否 static、参数是否匹配、返回值是否允许。
- [ ] 入口方法异常要输出类型、方法名、内部异常、堆栈和当前版本信息，方便线上诊断。
- [ ] 将 `HotfixDemo/GameMainApp` 从空壳补成完整示例，展示 Model / System / Utility 注册和热更业务启动流程。
- [ ] 热更业务初始化失败时能回到主工程错误页，而不是静默失败。
- [ ] 增加热更模块的退出、重启、清理策略。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Assemblies/AssemblyManifest.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Boot.cs`
- `Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GameMainApp.cs`

验收标准：

- 热更业务有唯一、清晰、可测试的入口。
- 后续接入真实项目时不需要把初始化逻辑散落到场景脚本里。

### 11. 改善 YooAssetKit 资源句柄生命周期

- [ ] `LoadAssetAsync` 不应回调后立即无条件 `Release` 长生命周期资源。
- [ ] 为资源加载返回可释放 handle，或提供统一引用计数封装。
- [ ] 所有加载 API 检查 `handle.Status` 和 `LastError`。
- [ ] 异步加载失败时不要静默返回 `null`。
- [ ] 区分短生命周期数据资源和长期持有资源。
- [ ] `LoadDllBytes` 明确以 byte[] 拷贝作为边界，并在注释或接口上约束不要向外暴露已释放 TextAsset。
- [ ] 场景加载失败时要把错误传回 `Boot` 或流程管理器。

相关位置：

- `Assets/3rd/QFramework/Toolkits/YooAssetKit/YooAssetKit.cs`
- `Assets/3rd/QFramework/Toolkits/YooAssetKit/Base/YooAssetPanelLoaderPool.cs`
- `Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/Test.cs`

验收标准：

- UI、Prefab、配置、音频等资源不会因为过早释放导致偶现丢失。
- 所有资源加载失败都有可观察的错误路径。

### 12. 完善缓存管理策略

- [ ] 不要在每次启动后无条件清理未使用 bundle，需评估启动耗时和复用收益。
- [ ] 支持按 package、版本、标签、空间阈值清理缓存。
- [ ] 增加“清理失败不阻断启动”的策略配置。
- [ ] 至少保留上一组可用 AOT + Hotfix 版本，支持热更失败后回滚。
- [ ] 支持用户设置页手动清理缓存。
- [ ] 增加缓存占用统计和日志。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureClearCacheBundle.cs`

验收标准：

- 热更新缓存不会无限膨胀。
- 缓存清理不会显著拖慢每次启动。

### 13. 规范多 package 使用策略

- [ ] 当前阶段默认保持单 package：`DefaultPackage`。
- [ ] 仅当生命周期明显不同才拆 package，例如 DLC、语音包、多语言包、RawFile。
- [ ] 如果启用 `RawFilePackage`，补齐初始化、版本、manifest、下载、失败兜底、缓存清理全链路。
- [ ] 禁止只按文件夹类型拆 package。
- [ ] 增加跨 package 资源依赖检查。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureInitializePackage.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureCreateDownloader.cs`

验收标准：

- 每个 package 都有明确职责、版本策略和失败策略。
- 新增 package 不会破坏默认启动链路。

## P1 构建和发布体系

### 14. 统一构建入口

- [ ] 把 HybridCLR 生成、DLL 编译、AOT 拷贝、YooAsset 构建、StreamingAssets 拷贝整合到一个命令。
- [ ] 构建前检查目标平台、play mode、package 配置、AOT 列表、热更程序集列表。
- [ ] 构建后检查输出目录、manifest、hash、DLL bytes、入口场景。
- [ ] 支持命令行参数：平台、环境、版本号、是否上传 CDN。
- [ ] 构建失败时返回非 0 exit code，方便 CI 接入。

相关位置：

- `Assets/Editor/HybridCLR/BuildPlayerCommand.cs`
- `Assets/Editor/HybridCLR/BuildAssetsCommand.cs`

验收标准：

- 新成员不需要按 README 手动点很多菜单。
- 本地和 CI 使用同一套构建入口。

### 15. 修正构建管线和文档不一致

- [ ] README 推荐 ScriptableBuildPipeline，但代码目前使用 `BuiltinBuildPipeline`，需统一。
- [ ] 决定继续 Builtin 还是切到 SBP。
- [ ] 如果切到 SBP，补齐对应构建参数、缓存策略和兼容测试。
- [ ] 更新 README 中 Unity 版本，目前文档写 2021.3.41f1，项目实际是 2022.3.62f2。

相关位置：

- `README.md`
- `ProjectSettings/ProjectVersion.txt`
- `Assets/Editor/HybridCLR/BuildAssetsCommand.cs`

验收标准：

- 文档、项目版本、实际构建脚本一致。
- 按文档操作不会走到过期菜单或错误流程。

### 16. 增加发布产物管理

- [ ] 资源包输出目录按平台、环境、版本归档。
- [ ] 生成发布 manifest：资源版本、app version、commit、构建时间、操作者。
- [ ] 上传 CDN 前后做 hash 对比。
- [ ] 支持一键回滚 CDN 指针。
- [ ] 发布完成后自动生成 release note 或变更摘要。

相关位置：

- `Bundles`
- `Assets/StreamingAssets/yoo`
- `yoo/DefaultPackage/ManifestFiles`
- 未来 CI/CD 脚本

验收标准：

- 任意线上版本都能追溯构建来源。
- 发布和回滚不依赖手工拷贝文件。

## P2 长期优化

### 17. 增加自动化测试和 smoke test

- [ ] 增加 Editor 测试：构建配置合法性、manifest 生成、AOT 列表生成。
- [ ] 增加 PlayMode 测试：离线启动、模拟下载、下载失败、取消下载。
- [ ] 增加热更 DLL 加载 smoke test。
- [ ] 增加入口场景加载 smoke test。
- [ ] CI 中至少跑一个最小热更新流程。

验收标准：

- 改动热更新流程后可以快速知道是否破坏基础启动。

### 18. 改善用户更新体验

- [ ] 下载确认界面展示格式化后的文件数量和大小，例如“需要更新 15 个文件，共 23.5 MB”。
- [ ] 下载确认界面展示网络状态、是否建议 Wi-Fi。
- [ ] 下载失败支持重试、退出、使用本地缓存。
- [ ] 展示下载速度、剩余时间、单文件进度、已下载大小 / 总大小。
- [ ] 当前文件和错误信息不应暴露过多技术细节，面向用户展示友好文案，详细错误写日志。
- [ ] 支持后台下载或进入大厅后延迟下载非关键资源。
- [ ] 强更时展示明确提示。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/UI`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Events/DownloadEvents.cs`

验收标准：

- 弱网、断网、取消、重试场景体验完整。

### 19. 清理工程生成物和大文件入库规则

- [ ] 评估是否需要将 `HybridCLRData` 生成目录入库。
- [ ] 评估是否需要将 `Assets/StreamingAssets/yoo` 的构建产物入库。
- [ ] 明确哪些 `.bytes`、bundle、manifest 是示例必需，哪些是构建产物。
- [ ] 更新 `.gitignore`，避免 3G 级别生成物长期污染仓库。
- [ ] 保留必要示例资源时，给出重新生成说明。

相关位置：

- `.gitignore`
- `HybridCLRData`
- `Assets/StreamingAssets/yoo`
- `Assets/AssetsPackage/AssetsHotFix/AOTCodes`
- `Assets/AssetsPackage/AssetsHotFix/HotfixCodes`

验收标准：

- clone、pull、切分支速度可接受。
- 构建产物和源码边界清晰。

### 20. 统一编码和注释质量

- [ ] 修复乱码注释。
- [ ] 将核心流程日志统一中英文风格。
- [ ] 增加日志管理模式：开发包 / 测试包默认打印 Debug、Info、Warning、Error、Fatal，正式上线包默认只打印 Error 和 Fatal。
- [ ] 日志级别由构建环境或运行配置控制，避免手动改代码切换日志输出。
- [ ] 为热更启动、版本请求、manifest 更新、下载、AOT 加载、DLL 加载、入口调用等核心链路定义统一日志分类。
- [ ] 正式包避免输出 CDN 地址、文件路径、用户隐私、密钥、签名明文等敏感信息。
- [ ] 预留线上 Error / Fatal 日志上报能力，附带 AppVersion、HotfixVersion、AotVersion、BuildTarget、设备和网络信息。
- [ ] 为关键错误补充错误码或统一错误类型。
- [ ] 将公开字段命名规范化，例如 `_rawfilwPkgName` 拼写。
- [ ] 避免 public 字段暴露过多内部状态。
- [ ] 将 `ProcedureManager` 内部状态改为属性或上下文对象，避免所有 Procedure 任意修改 public 字段。
- [ ] 修正 `_rawfilwPkgName` 拼写，并统一 raw file 相关命名。
- [ ] 降低 `CoroutineController.manager` 静态耦合，增加 null 检查和生命周期保护。
- [ ] 评估是否由 `ProcedureManager` 自身承载协程调度，避免所有流程依赖全局 MonoBehaviour。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/UI/UISceneMessageBox.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureManager.cs`
- `Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GameMainApp.cs`

验收标准：

- 日志和错误能帮助定位线上问题。
- 代码命名不会增加维护成本。

### 21. 完善文档

- [ ] 新增“首包构建流程”。
- [ ] 新增“热更包发布流程”。
- [ ] 新增“CDN 目录结构和版本命名规则”。
- [ ] 新增“线上回滚流程”。
- [ ] 新增“常见错误排查”。
- [ ] 新增“多 package 拆分原则”。
- [ ] 新增“HybridCLR AOT metadata 更新规则”。
- [ ] 新增“AOTAssemblyManifest / HotfixAssemblyManifest 拆分和兼容规则”。
- [ ] 新增“下载失败、重试、取消、离线降级和回滚流程”。

相关位置：

- `README.md`
- `Docs`

验收标准：

- 没有接触过项目的人能按文档完成一次完整出包和热更发布。

### 22. 下载能力扩展：差分、后台、带宽和并发

- [ ] 评估是否需要差分更新能力，例如 Delta Patch、二进制补丁或按 bundle 粒度进一步拆分。
- [ ] 大版本更新时统计全量下载量，决定是否引入差分方案。
- [ ] 支持后台下载策略，明确 iOS / Android / PC 在切后台后的行为和限制。
- [ ] 下载并发数不要固定为 10，应支持配置或根据网络条件动态调整。
- [ ] 增加下载带宽限制能力，避免更新流程抢占业务网络。
- [ ] 增加按资源重要性分层下载：启动必需资源优先，非关键资源延迟或后台下载。

相关位置：

- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureCreateDownloader.cs`
- `Assets/AssetsPackage/Scripts/Main/Runtime/Procedure/ProcedureDownloadPackageFiles.cs`
- YooAsset downloader 创建和调度策略

验收标准：

- 大包更新时下载量、下载时机和网络占用可控。
- 弱网环境下可以通过并发、暂停、重试、后台下载策略改善体验。

### 23. Luban 配置热更集成

- [ ] 明确 Luban 配置表属于 Hotfix manifest 管理范围，还是独立 Config manifest 管理。
- [ ] 热更流程中增加配置表版本、hash、兼容性校验。
- [ ] 下载 Hotfix 后按版本加载对应配置表，避免 DLL 和配置结构不匹配。
- [ ] 配置加载失败时支持回滚到上一份可用配置。
- [ ] 增加配置表 smoke test，覆盖字段新增、删除、类型变化等场景。

相关位置：

- `LubanConfig`
- `Assets/AssetsPackage/AssetsHotFix/Datas`
- `Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/GenCodes`
- `Assets/AssetsPackage/Scripts/Hotfix/HotfixDemo/Test.cs`

验收标准：

- 热更 DLL、配置表和 App 版本之间有明确兼容关系。
- 配置热更失败不会导致启动期崩溃或业务读取空数据。

## 建议实施顺序

### 第一阶段：保证能安全启动

- [x] 修复 play mode 打包风险。
- [x] 修复取消下载卡死。
- [x] 增加网络失败本地兜底。
- [x] 明确首包资源必需项。

### 第二阶段：保证能商业化发布

- [x] 抽离 CDN 配置。
- [x] 拆分 AOTAssemblyManifest 和 HotfixAssemblyManifest。
- [x] 支持根据 `RequiredAotVersion` 自动选择、下载并加载新版 AOT。
- [ ] 建立版本协议。
- [ ] 增加资源和 DLL 安全校验。
- [ ] 统一构建入口。
- [ ] 生成发布记录和回滚机制。
- [ ] 集成 Luban 配置热更兼容校验。

### 第三阶段：提升长期维护质量

- [ ] 整理 YooAssetKit 资源生命周期。
- [ ] 完善热更入口生命周期。
- [ ] 扩展差分、后台、带宽和并发下载能力。
- [ ] 增加自动化测试。
- [ ] 清理生成物入库规则。
- [ ] 更新 README 和 Docs。

## 当前建议结论

当前项目建议先保持单 package：`DefaultPackage`。

原因：

- 热更 DLL、AOT metadata、配置和入口场景现在强依赖同一个启动链路。
- 单 package 更容易保证版本一致性。
- 当前多 package 相关代码还不完整，尤其是 raw file package 的失败兜底、版本和缓存策略。

后续如果出现 DLC、语音包、多语言资源、大型地图包、活动资源，再按资源生命周期拆 package。
