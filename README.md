# qframework-hotfix

基于 **QFramework + YooAsset + HybridCLR** 的 Unity 热更新示例工程。

当前工程的发布策略是：

- **AOT 元数据随首包发布**：首包内置 `AOTCodes`、首版热更 DLL、配置清单和基础资源。
- **后续热更包增量发布**：远端只发布变更后的热更 DLL、资源、配置清单等内容，不再重复发布 AOT 元数据。
- **客户端按 YooAsset Manifest 差异下载**：远端构建产物是一个完整版本目录，但运行时 downloader 只会下载本地缺失或 hash 变化的 bundle。

## 技术栈

- [QFramework](https://github.com/liangxiegame/QFramework)：项目框架、事件、状态机、UI 等基础能力。
- [YooAsset](https://github.com/tuyoogame/YooAsset)：资源打包、内置资源、远端资源、版本清单、差异下载。
- [HybridCLR](https://github.com/focus-creative-games/hybridclr)：IL2CPP 平台 C# 代码热更新和 AOT 泛型元数据补充。

## 目录结构

```text
Assets/AssetsPackage
├── AssetsHotFix
│   ├── AOTCodes          # AOT 元数据 DLL.bytes，首包内置，后续热更默认不发布
│   ├── HotfixCodes       # 热更新程序集 DLL.bytes
│   ├── Configs           # AssemblyManifest.asset 等热更配置
│   ├── HotfixDemo        # 示例热更资源
│   │   ├── Entities      # 示例预制体
│   │   └── Scenes        # 示例热更场景，当前入口地址为 main
│   └── Datas             # 配表等热更数据
└── Scripts
    ├── Main              # 首包主工程代码，负责启动、下载、加载热更
    └── Hotfix            # 热更新业务代码，编译为热更程序集
```

关键配置文件：

- `ProjectSettings/HybridCLRSettings.asset`：HybridCLR 热更程序集配置。当前热更程序集来自 asmdef，包括 `HotfixCommon`、`HotfixDemo`。
- `Assets/Resources/AssetBundleCollectorSetting.asset`：YooAsset 收集器配置。当前 `DefaultPackage` 收集 `AOTCodes`、`HotfixCodes`、`Configs`、示例资源和数据。
- `Assets/AssetsPackage/Resources/HotfixRuntimeSettings.asset`：热更新运行配置，记录 YooAsset play mode、主资源包名、RawFile 包名和启动阶段下载策略。
- `Assets/AssetsPackage/Resources/HotfixRemoteSettings.asset`：热更新远端配置，记录开发、测试、预发、正式环境的主/备 CDN 地址和运行时选择规则。
- `Assets/AssetsPackage/Resources/HotfixLocalizationSettings.asset`：热更新启动阶段本地化文本配置，记录中文、英文提示文案和运行时语言选择规则。
- `Assets/AssetsPackage/AssetsHotFix/Configs/AssemblyManifest.asset`：运行时程序集加载清单，记录 AOT 元数据 DLL、热更 DLL、入口场景和可选入口方法。

`HotfixRuntimeSettings.asset` 核心字段：

- `MainPackageName`：主资源包名，默认 `DefaultPackage`。
- `IncludeRawFilePackage`：是否初始化并更新 RawFile 包。
- `RawFilePackageName`：RawFile 包名，默认 `RawFilePackage`。
- `StartupDownloadMode`：启动阶段下载策略，可选 `DownloadAll`、`DownloadByTags`、`Skip`。
- `StartupUpdatePolicy`：启动阶段更新策略，可选 `MustUpdate`、`AllowCached`、`WifiOnly`、`BackgroundDownload`。
- `StartupDownloadTags`：主资源包启动阶段按 Tag 下载的 Tag 列表。
- `RawFileStartupDownloadTags`：RawFile 包启动阶段按 Tag 下载的 Tag 列表。

包名可通过菜单 `Build/Hotfix/Sync Package Names From YooAsset Collector` 从 YooAsset Collector 配置同步。常规构建菜单也会在构建前自动同步包名到运行时配置。

## 运行时热更流程

入口脚本是 `Assets/AssetsPackage/Scripts/Main/Runtime/Boot.cs`。

启动后流程如下：

1. `Boot` 从 `HotfixRuntimeSettings.asset` 读取 play mode、package 名称和启动下载策略。
2. `YooAssets.Initialize()` 初始化 YooAsset。
3. 创建 `ProcedureManager`，进入资源初始化和热更状态机。
4. `ProcedureInitializePackage` 初始化主资源包；如启用 RawFile 包，会同时初始化 RawFile 包。Host/Web 模式会配置远端文件系统。
5. `ProcedureRequestPackageVersion` 请求远端最新包版本；失败时按 `StartupUpdatePolicy` 选择重试、退出或使用本地缓存。
6. `ProcedureUpdatePackageManifest` 更新远端 manifest；manifest 请求或校验失败时可回退到上次可用缓存或首包内置 manifest。
7. `ProcedureCreateDownloader` 根据 `StartupDownloadMode` 创建启动阶段差异下载器。
8. 如有差异文件，`ProcedureDownloadPackageFiles` 下载变更 bundle。
9. `ProcedureLoadAOTMetadata` 从当前可用包中加载 `AssemblyManifest`，再加载 AOT 元数据并调用 `RuntimeApi.LoadMetadataForAOTAssembly`。
10. `ProcedureLoadAssembly` 根据最新 `AssemblyManifest` 加载热更 DLL。
11. `ProcedureClearCacheBundle` 清理无用缓存。
12. 状态机完成后，`Boot` 设置当前默认 YooAsset 包，并加载入口场景，默认地址为 `main`。

`StartupDownloadMode` 语义：

- `DownloadAll`：启动阶段下载全部差异资源。
- `DownloadByTags`：启动阶段只下载 `StartupDownloadTags` / `RawFileStartupDownloadTags` 指定的资源；Tag 为空则不创建下载器。
- `Skip`：跳过启动阶段下载，直接进入 AOT 和热更 DLL 加载流程。

`StartupUpdatePolicy` 语义：

- `MustUpdate`：强更策略。远端版本、manifest 或资源下载失败时只能重试或退出，不使用本地缓存。
- `AllowCached`：默认弱更策略。远端失败时允许使用上次成功启动的缓存版本；没有缓存时尝试首包内置 manifest。
- `WifiOnly`：仅 Wi-Fi 更新。非 Wi-Fi 环境优先使用本地可用版本，Wi-Fi 环境按正常热更流程请求远端。
- `BackgroundDownload`：优先本地启动策略。如已有可用缓存，启动阶段直接使用本地版本；后续后台下载入口由业务层接入。

Player 的 Host 模式初始化时会把首包内置 manifest 复制到 YooAsset 缓存目录，因此首次安装后即使远端版本接口不可达，也可以尝试用首包内置版本启动。

### 启动下载取消与失败处理

启动阶段如发现差异文件，`ProcedureCreateDownloader` 会发送 `OnDownloadInfoHandlerEvent`，由 `UIPanelRoot` 弹出下载确认框：

- 点击“确定”：进入 `ProcedureDownloadPackageFiles` 开始下载。
- 点击“取消”：发送 `OnDownloadCancelRequestEvent`，由 `ProcedureManager` 统一调用 `CancelDownload` 收口。

下载取消请求可以由任意 UI 或业务代码触发：

```csharp
TypeEventSystem.Global.Send(new OnDownloadCancelRequestEvent
{
    reason = "用户取消资源下载。"
});
```

如果是在 UI 层，也可以使用封装方法：

```csharp
UIPanelRoot.Instance.RequestCancelDownload();
UIPanelRoot.Instance.RequestCancelDownloadWithReason("用户在下载中取消。");
```

`CancelDownload` 会执行以下收口动作：

- 调用主包和 RawFile 包 downloader 的 `CancelDownload()`。
- 发送 `OnDownloadCanceledEvent`，关闭 loading 并显示取消原因。
- 将热更流程标记为失败，避免 FSM 或协程继续悬挂。

下载过程中如果失败，会弹出“确定重试 / 取消使用本地缓存或退出更新”提示。点击确定会重新创建 downloader 并重试当前包；点击取消时，弱更策略会尝试使用本地缓存，强更策略会退出更新。`ProcedureManager` 也预留了 `TryPauseDownload()` 和 `TryResumeDownload()`，后续可接入弱网或大包下载 UI。

在 `AllowCached`、`WifiOnly`、`BackgroundDownload` 策略下，网络失败弹窗的取消路径会优先变为“使用本地缓存启动”。本地缓存来源包括：

- 上次完整完成启动下载后记录的可用 package version。
- 首包内置 manifest 复制到缓存目录后的内置版本。
- 当前已经激活的 YooAsset manifest。

## 首包发布流程

首包用于首次安装，必须包含：

- HybridCLR 运行环境。
- 首版 YooAsset 内置资源。
- AOT 元数据补充 DLL。
- 首版热更 DLL。
- `AssemblyManifest.asset`。
- 启动场景 `Assets/Scenes/Boot.unity`。

推荐步骤：

1. 切换到目标构建平台，例如 Windows、Android、iOS。
2. 执行 `HybridCLR/Installer/Install`，确保 HybridCLR 已安装。
3. 执行 `HybridCLR/Generate/All`，生成 AOT 泛型引用、桥接函数、裁剪后的 AOT DLL 等数据。
4. 检查 `HybridCLR/Settings` 中的 `Hot Update Assembly Definitions`，确保热更 asmdef 已加入。
5. 检查 `YooAsset/AssetBundle Collector`，确保首包资源和热更资源都被主资源包收集；默认主包名为 `DefaultPackage`。
6. 执行菜单 `Build/Build Initial YooAsset Package`。
7. 构建 Player。Windows 示例可执行菜单 `Build/Win64`。

`Build/Build Initial YooAsset Package` 会自动完成：

- 调用 `CompileDllCommand.CompileDll(target)` 编译热更 DLL。
- 从 `HybridCLRData/AssembliesPostIl2CppStrip` 复制 AOT 元数据 DLL 到 `Assets/AssetsPackage/AssetsHotFix/AOTCodes/*.dll.bytes`。
- 从 `HybridCLRData/HotUpdateDlls` 复制热更 DLL 到 `Assets/AssetsPackage/AssetsHotFix/HotfixCodes/*.dll.bytes`。
- 生成或更新 `AssemblyManifest.asset`，写入 AOT DLL 列表和热更 DLL 列表。
- 从 YooAsset Collector 同步运行时包名配置，并构建当前主资源包。
- 使用 `ClearAndCopyAll` 将构建产物复制到 `StreamingAssets`，作为首包内置资源。

Windows 的 `Build/Win64` 菜单额外封装了 Player 构建流程：先构建 `Assets/Scenes/Boot.unity`，再构建首包 YooAsset 资源，并把 `StreamingAssets` 复制到 `Release-Win64/HybridCLRTrial_Data/StreamingAssets`。

## 热更包发布流程

当只更新热更代码、资源、场景或配置时，走热更包流程。

推荐步骤：

1. 修改 `Assets/AssetsPackage/Scripts/Hotfix` 下的热更代码，或修改 `AssetsHotFix` 下被 YooAsset 收集的资源。
2. 如新增热更代码模块，先创建 asmdef，并加入 `HybridCLR/Settings -> Hot Update Assembly Definitions`。
3. 如新增热更资源目录，在 `YooAsset/AssetBundle Collector` 中加入当前主资源包的收集器。
4. 执行菜单 `Build/Build Hotfix YooAsset Package`。
5. 将本次 YooAsset 构建输出目录中的版本文件、manifest 文件和 bundle 文件上传到 CDN/资源服务器。
6. 客户端重启或进入热更流程后，会请求最新版本并按 manifest 差异下载。

`Build/Build Hotfix YooAsset Package` 的行为：

- 重新编译热更 DLL。
- 只复制 `HotfixCodes`。
- 临时把 `AssemblyManifest.asset` 的 AOT 列表置空，让本次远端 manifest 不要求下载 AOT 元数据。
- 构建时临时禁用 `AOTCodes` 收集器。
- YooAsset 构建参数使用 `BuildinFileCopyOption.None`，不会覆盖 `StreamingAssets`。
- 构建完成后恢复本地 `AssemblyManifest.asset` 中的 AOT 列表，保证工程配置仍可用于下一次首包构建。

因此后续热更包默认只包含：

- 变化后的热更 DLL。
- 变化后的热更资源、场景、配表。
- 最新 `AssemblyManifest.asset`。
- YooAsset 版本和 manifest 文件。

## 远端资源地址

Host/Web 模式远端地址由 `Assets/AssetsPackage/Resources/HotfixRemoteSettings.asset` 控制，不再写死在代码里。

每个环境都可以独立配置：

- `mainCdnUrlTemplate`：主 CDN 地址模板。
- `fallbackCdnUrlTemplate`：备用 CDN 地址模板，启动时会校验它不能和主 CDN 完全相同。
- `requireHttps`：是否强制 HTTPS，正式环境建议开启。
- `allowedDomains`：允许访问的域名白名单，预留域名治理能力。
- `certificatePinningEnabled` / `certificatePublicKeyPin`：证书 pinning 预留字段。
- `enableGrayRelease` / `grayReleasePercent` / `grayMainCdnUrlTemplate`：CDN 灰度切换预留字段。

地址模板支持以下 token：

```text
{Environment}  # Development / Testing / Staging / Production
{Platform}     # Android / iOS / WebGL / Windows / macOS / Linux
{Channel}      # 渠道，例如 appstore、googleplay、tap、official
{Region}       # 地区，例如 cn、us、global
{PackageName}  # YooAsset 包名
```

示例：

```text
https://cdn.example.com/GameName/{Platform}/{Channel}/{Region}
https://cdn-backup.example.com/GameName/{Platform}/{Channel}/{Region}
```

运行时可以通过 PlayerPrefs 或命令行切换环境、渠道和地区，不需要修改代码：

```csharp
PlayerPrefs.SetString("Hotfix.Remote.Environment", "Production");
PlayerPrefs.SetString("Hotfix.Remote.Channel", "official");
PlayerPrefs.SetString("Hotfix.Remote.Region", "cn");
PlayerPrefs.Save();
```

也可以直接覆盖本次使用的主/备 CDN 模板：

```csharp
PlayerPrefs.SetString("Hotfix.Remote.MainUrl", "https://cdn.example.com/GameName/{Platform}/{Channel}/{Region}");
PlayerPrefs.SetString("Hotfix.Remote.FallbackUrl", "https://cdn-backup.example.com/GameName/{Platform}/{Channel}/{Region}");
PlayerPrefs.Save();
```

命令行示例：

```text
--hotfix-env=Production --hotfix-channel=official --hotfix-region=cn
--hotfix-main-url=https://cdn.example.com/GameName/{Platform}/{Channel}/{Region}
--hotfix-fallback-url=https://cdn-backup.example.com/GameName/{Platform}/{Channel}/{Region}
```

上传远端资源时，服务器目录需要和模板解析后的目录一致，并保持 YooAsset 构建输出中的文件结构和文件名不变。

## 启动阶段多语言

启动、资源检查、下载确认、失败重试、本地缓存降级、AOT 元数据加载和热更程序集加载等提示文案由 `Assets/AssetsPackage/Resources/HotfixLocalizationSettings.asset` 管理。

默认语言为 `FollowSystem`，运行时会跟随系统语言选择简体中文或英文。也可以通过 PlayerPrefs 或命令行覆盖：

```csharp
PlayerPrefs.SetString("Hotfix.Language", "English");
PlayerPrefs.Save();
```

```text
--hotfix-language=ChineseSimplified
--hotfix-language=English
```

新增启动阶段提示时，优先在 `HotfixTextKey` 中添加 key，并在 `HotfixLocalizationSettings.asset` 中补齐对应语言文本，再通过 `HotfixText.Get(...)` 使用，避免把用户可见文案继续写死在流程代码里。

## AOT 元数据策略

当前工程采用“首包固定 AOT 元数据”的策略：

- 首包构建时发布 `AOTCodes`。
- 热更构建时排除 `AOTCodes`。
- 运行时在 manifest 更新和启动下载策略处理后进入 AOT 元数据加载阶段；常规热更包不发布新的 AOT 元数据。

这个策略适合大多数业务热更，但要注意边界：

- 如果后续热更代码只是改业务逻辑、UI、资源、配表，通常只发热更包即可。
- 如果后续热更引入新的 AOT 泛型需求，且首包 AOT 元数据无法覆盖，可能需要重新发 App 包。
- 如果希望 AOT 元数据也能远端更新，需要调整当前流程：远端包不能清空 AOT 列表，也不能禁用 `AOTCodes` 收集器，并且运行时加载 AOT 的时机要能使用远端最新 manifest。

## AssemblyManifest 配置

`AssemblyManifest.asset` 字段说明：

- `AotMetadataAssemblies`：需要加载元数据的 AOT DLL 列表。
- `HotUpdateAssemblies`：需要加载的热更 DLL 列表。
- `EntrySceneAddress`：热更完成后加载的 YooAsset 场景地址，当前默认 `main`。
- `EntryTypeName`：可选，热更程序集里的静态入口类型完整名。
- `EntryMethodName`：可选，热更入口静态方法名。

通常不需要手动维护 DLL 列表，构建菜单会自动写入。入口场景或入口方法可以按项目需要手动配置。

## 按 Tag 下载和加载

当前框架已接入 YooAsset Tag 能力，可用于分阶段下载或按模块加载资源。

使用步骤：

1. 在 `YooAsset/AssetBundle Collector` 中给资源收集器配置 `AssetTags`，例如 `ui`、`battle`、`chapter_1`。
2. 重新执行 `Build/Build Hotfix YooAsset Package`，让最新 manifest 包含资源 Tag 信息。
3. 如希望启动热更阶段按 Tag 下载，在 `HotfixRuntimeSettings.asset` 中设置 `StartupDownloadMode = DownloadByTags`，并填写 `StartupDownloadTags`。
4. 业务运行时可通过 `YooAssetKit` 按 Tag 下载或加载。

启动阶段下载策略统一由 `HotfixRuntimeSettings.asset` 控制：

- `DownloadAll`：启动阶段下载全部差异资源。
- `DownloadByTags`：启动阶段只下载配置的 Tag；Tag 为空时不创建启动下载器。
- `Skip`：跳过启动阶段下载，业务可在进入游戏后按需下载。

按 Tag 下载：

```csharp
YooAssetKit.DownloadByTagsAsync(
    new[] { "ui", "battle" },
    onCompleted: downloader =>
    {
        if (downloader.Status != YooAsset.EOperationStatus.Succeed)
            UnityEngine.Debug.LogError(downloader.Error);
    },
    onUpdate: data =>
    {
        UnityEngine.Debug.Log($"download progress: {data.Progress}");
    });
```

按 Tag 加载：

```csharp
var prefabs = await YooAssetKit.LoadAssetsByTagAsync<UnityEngine.GameObject>("ui");
```

也可以只查询资源信息后自行加载：

```csharp
var assetInfos = YooAssetKit.GetAssetInfosByTag("ui");
foreach (var assetInfo in assetInfos)
{
    var handle = YooAsset.YooAssets.LoadAssetAsync(assetInfo);
}
```

注意：按 Tag 加载前，相关 bundle 必须已经在本地可用；如果是远端资源，先调用按 Tag 下载接口。

## 开发注意事项

- 热更代码必须放在热更 asmdef 管理的目录中，例如 `HotfixCommon`、`HotfixDemo`。
- 主工程代码可以引用稳定基础设施，但不要直接依赖热更业务实现。
- 热更资源必须被 YooAsset Collector 收集，否则远端 manifest 中不会出现。
- 需要按 Tag 下载或加载的资源，必须在 YooAsset Collector 中配置 `AssetTags`。
- 场景、Prefab、材质、Shader、配表等资源改动后，需要重新构建热更包。
- Shader 相关资源上线前建议做变体收集，避免运行时丢变体。
- 构建热更包前确保目标平台正确；不同平台的 DLL 和 AssetBundle 不能混用。

## 常用菜单

```text
HybridCLR/Installer/Install              # 安装 HybridCLR
HybridCLR/Generate/All                   # 生成 HybridCLR 必要数据
Build/Build Initial YooAsset Package     # 构建首包内置 YooAsset 包
Build/Build Hotfix YooAsset Package      # 构建后续远端热更包
Build/Hotfix/Sync Package Names From YooAsset Collector # 同步运行时主包和 RawFile 包名
Build/BuildAssetsAndCopyToAssetsPackage  # 只复制 AOT/热更 DLL 并刷新配置
Build/CopyAotDllsToAssetsPackage         # 只复制 AOT 元数据 DLL
Build/CopyHotUpdateDllsToAssetsPackage   # 只复制热更 DLL
Build/Win64                              # Windows 示例 Player 构建
```

## 常见问题

### 热更包是否是真正增量？

构建目录是完整版本目录，客户端更新是增量。YooAsset 会通过远端 manifest 对比本地缓存，只下载缺失或 hash 变化的 bundle。

### 为什么热更包里不包含 AOTCodes？

这是当前框架的设计：AOT 元数据随首包发布。热更构建时 `BuildHotfixYooAssetPackage` 会临时禁用 `AOTCodes` 收集器，并清空远端 manifest 中的 AOT 列表。

### 新增热更程序集后没有加载？

检查三处：

1. 新 asmdef 是否加入 `HybridCLR/Settings -> Hot Update Assembly Definitions`。
2. 是否执行了 `Build/Build Hotfix YooAsset Package`。
3. `AssemblyManifest.asset` 的 `HotUpdateAssemblies` 是否包含新增 DLL。

### 修改了资源但客户端下载不到？

检查资源是否在 `YooAsset/AssetBundle Collector` 中被收集，远端目录是否上传了最新版本文件和 bundle，`HotfixRemoteSettings.asset` 解析出的环境、平台、渠道、地区目录是否正确。

### 何时必须重新发 App 包？

以下情况通常需要重新发 App 包：

- 修改了首包主工程代码。
- HybridCLR、YooAsset、Unity 版本或构建平台配置发生关键变化。
- 新热更代码需要的 AOT 泛型元数据首包未覆盖。
- 启动流程、资源包初始化流程或平台原生能力发生变化。

## 致谢

- [QFramework](https://github.com/liangxiegame/QFramework)
- [YooAsset](https://github.com/tuyoogame/YooAsset)
- [HybridCLR](https://github.com/focus-creative-games/hybridclr)
