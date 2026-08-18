#!/bin/bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
PROJECT_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"
LUBAN_DLL="$PROJECT_ROOT/LubanConfig/DataTables/Luban/Luban.dll"

dotnet "$LUBAN_DLL" -t client -d bin -c cs-bin \
  --conf "$SCRIPT_DIR/luban.conf" --validationFailAsError \
  -x "bin.outputDataDir=$PROJECT_ROOT/Assets/AssetsPackage/AssetsHotFix/Datas/Localization" \
  -x "cs-bin.outputCodeDir=$PROJECT_ROOT/Assets/AssetsPackage/Scripts/Main/Runtime/Localization/Generated"

mkdir -p "$PROJECT_ROOT/Assets/AssetsPackage/Resources/Localization"
cp "$PROJECT_ROOT/Assets/AssetsPackage/AssetsHotFix/Datas/Localization/tbbootstraptext.bytes" \
   "$PROJECT_ROOT/Assets/AssetsPackage/Resources/Localization/bootstrap.bytes"

# 业务文本由 Unity 同步器按 Locale 独立生成；主生成流程只保留其解析代码。
rm -f "$PROJECT_ROOT/Assets/AssetsPackage/AssetsHotFix/Datas/Localization/tblocaletext.bytes"
