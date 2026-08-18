using UnityEditor;
using UnityEngine;

namespace Framework.Luban.Editor
{
    [CustomEditor(typeof(LubanBuildProfile))]
    public sealed class LubanBuildProfileInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("建议通过 Build/Luban/数据构建中心 编辑、扫描、预览和执行此 Profile。", MessageType.Info);
            if (GUILayout.Button("打开 Luban 数据构建中心")) LubanBuildCenterWindow.Open();
            EditorGUILayout.Space();
            DrawDefaultInspector();
        }
    }
}
