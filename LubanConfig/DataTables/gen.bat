set WORKSPACE=..\..
set LUBAN_DLL=%WORKSPACE%\LubanConfig\DataTables\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -d bin ^
    -c cs-bin ^
    --conf %CONF_ROOT%\luban.conf ^
    -x bin.outputDataDir=%WORKSPACE%\Assets\AssetsPackage\AssetsHotfix\Datas\bin^
    -x cs-bin.outputCodeDir=%WORKSPACE%\Assets\AssetsPackage\GenCodes\Bin

pause