using HybridCLR.Editor;
using QFramework;
using UnityEditor;

public static class AOTMetaAssembliesHelper
{
    public static void AOTMetaAssembliesActiveBuildTarget()
    {
        DoCopyAOTAssemblies(EditorUserBuildSettings.activeBuildTarget);
        LogKit.I("AOT metadata assemblies copied.");
    }

    public static void DoCopyAOTAssemblies(BuildTarget buildTarget)
    {
        BuildAssetsCommand.CopyAotMetaDataDlls(buildTarget);
    }
}
