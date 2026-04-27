using QFramework;
using YooAsset;

namespace Framework.Procedure
{
    public enum ResPackageStates
    {
        InitializePackage,
        RequestPackageVersion,
        UpdatePackageManifest,
        CreateDownloader,
        DownloadPackageFiles,
        DownloadPackageOver,
        LoadAssemblies,
        ClearCacheBundle,
        StartGame
    }

    public class ProcedureManager : GameAsyncOperation
    {
        public const string DefaultEntrySceneAddress = "main";

        public readonly string _packageName;
        public readonly string _rawfilwPkgName;
        public readonly EPlayMode _playMode;
        public string _packageVersion;
        public bool _isIncludeRawFile;
        public string _rawfilePkgVersion;
        public ResourceDownloaderOperation _downloaderOperation;
        public ResourceDownloaderOperation _downloaderRawfile;

        public string EntrySceneAddress { get; private set; } = DefaultEntrySceneAddress;
        public string EntryTypeName { get; private set; } = string.Empty;
        public string EntryMethodName { get; private set; } = string.Empty;

        public FSM<ResPackageStates> _mFSM = new FSM<ResPackageStates>();

        public ProcedureManager(string packageName, EPlayMode playMode, bool IsIncludeRawFile = false)
        {
            _packageName = packageName;
            _playMode = playMode;
            _isIncludeRawFile = IsIncludeRawFile;
            if (_isIncludeRawFile)
            {
                _rawfilwPkgName = Boot.rawfilePackageName;
            }

            _mFSM.AddState(ResPackageStates.InitializePackage, new ProcedureInitializePackage(_mFSM, this));
            _mFSM.AddState(ResPackageStates.RequestPackageVersion, new ProcedureRequestPackageVersion(_mFSM, this));
            _mFSM.AddState(ResPackageStates.UpdatePackageManifest, new ProcedureUpdatePackageManifest(_mFSM, this));
            _mFSM.AddState(ResPackageStates.CreateDownloader, new ProcedureCreateDownloader(_mFSM, this));
            _mFSM.AddState(ResPackageStates.DownloadPackageFiles, new ProcedureDownloadPackageFiles(_mFSM, this));
            _mFSM.AddState(ResPackageStates.DownloadPackageOver, new ProcedureDownloadPackageOver(_mFSM, this));
            _mFSM.AddState(ResPackageStates.LoadAssemblies, new ProcedureLoadAssembly(_mFSM, this));
            _mFSM.AddState(ResPackageStates.ClearCacheBundle, new ProcedureClearCacheBundle(_mFSM, this));
            _mFSM.AddState(ResPackageStates.StartGame, new ProcedureStartGame(_mFSM, this));
        }

        protected override void OnAbort()
        {
        }

        protected override void OnStart()
        {
            _mFSM.StartState(ResPackageStates.InitializePackage);
        }

        protected override void OnUpdate()
        {
            if (IsDone)
            {
                return;
            }

            _mFSM.Update();
        }

        public void SetFinish()
        {
            Status = EOperationStatus.Succeed;
        }

        public void SetFailed(string error)
        {
            if (IsDone)
            {
                return;
            }

            Error = string.IsNullOrEmpty(error) ? "Hot update procedure failed." : error;
            Status = EOperationStatus.Failed;
            LogKit.E(Error);
        }

        public void SetHotfixEntry(string sceneAddress, string typeName, string methodName)
        {
            EntrySceneAddress = string.IsNullOrWhiteSpace(sceneAddress) ? DefaultEntrySceneAddress : sceneAddress;
            EntryTypeName = typeName ?? string.Empty;
            EntryMethodName = methodName ?? string.Empty;
        }
    }
}
