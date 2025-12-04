using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace BCSS.Editor
{
    public class SkillAssetCallback
    {
        [MenuItem("Assets/Create/GraphProcessor_Skill", false, 10)]
        public static SkillGraph CreateGraphPorcessor_Skill()
        {
            string defaultPath = "Assets";
            string assetPath = EditorUtility.SaveFilePanelInProject(
                "创建 Skill Graph",
                "SkillGraph",
                "asset",
                "选择保存位置并输入资源名称",
                defaultPath);

            if (string.IsNullOrEmpty(assetPath))
                return null;

            SkillGraph graph = ScriptableObject.CreateInstance<SkillGraph>();
            AssetDatabase.CreateAsset(graph, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = graph;
            return graph;
        }

        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            SkillGraph skillGraph = EditorUtility.InstanceIDToObject(instanceID) as SkillGraph;
            return InitializeGraph(skillGraph);
        }

        public static bool InitializeGraph(SkillGraph skillGraph)
        {
            if (skillGraph == null) return false;
            EditorWindow.GetWindow<SkillGraphWindow>().InitializeGraph(skillGraph);
            return true;
        }
    }
}
