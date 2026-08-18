@echo off
set SCRIPT_DIR=%~dp0
set PROJECT_ROOT=%SCRIPT_DIR%..\..
set LUBAN_DLL=%PROJECT_ROOT%\LubanConfig\DataTables\Luban\Luban.dll

dotnet "%LUBAN_DLL%" -t client -d bin -c cs-bin --conf "%SCRIPT_DIR%luban.conf" --validationFailAsError ^
 -x bin.outputDataDir=%PROJECT_ROOT%\Assets\AssetsPackage\AssetsHotFix\Datas\Localization ^
 -x cs-bin.outputCodeDir=%PROJECT_ROOT%\Assets\AssetsPackage\Scripts\Main\Runtime\Localization\Generated
if errorlevel 1 exit /b %errorlevel%

if not exist "%PROJECT_ROOT%\Assets\AssetsPackage\Resources\Localization" mkdir "%PROJECT_ROOT%\Assets\AssetsPackage\Resources\Localization"
copy /Y "%PROJECT_ROOT%\Assets\AssetsPackage\AssetsHotFix\Datas\Localization\tbbootstraptext.bytes" "%PROJECT_ROOT%\Assets\AssetsPackage\Resources\Localization\bootstrap.bytes"
if exist "%PROJECT_ROOT%\Assets\AssetsPackage\AssetsHotFix\Datas\Localization\tblocaletext.bytes" del /Q "%PROJECT_ROOT%\Assets\AssetsPackage\AssetsHotFix\Datas\Localization\tblocaletext.bytes"
