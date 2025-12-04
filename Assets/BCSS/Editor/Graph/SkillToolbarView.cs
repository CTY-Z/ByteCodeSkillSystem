using BCSS.Editor;
using GraphProcessor;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;
using static BCSS.Editor.SkillToolbarView;

namespace BCSS.Editor
{
    public class SkillToolbarView : UniversalToolbarView
    {
        private SkillGraphWindow skillGraphWindow;
        private SkillGraph m_skillGraph;

        public class BlackboardInspectorViewer : SerializedScriptableObject
        {
            public SkillBlackBoard blackboard;
        }

        private static BlackboardInspectorViewer m_blackboardInspectorViewer;
        private static BlackboardInspectorViewer blackboardInspectorViewer
        {
            get
            {
                if (m_blackboardInspectorViewer == null)
                {
                    m_blackboardInspectorViewer = ScriptableObject.CreateInstance<BlackboardInspectorViewer>();
                }

                return m_blackboardInspectorViewer;
            }
        }

        public SkillToolbarView(SkillGraphWindow skillGraphWindow, BaseGraphView graphView, MiniMap miniMap, SkillGraph skillGraph)
            : base(graphView, miniMap, skillGraph)
        {
            this.skillGraphWindow = skillGraphWindow;
            m_skillGraph = skillGraph;
        }

        protected override void AddButtons()
        {
            base.AddButtons();

            AddSeparator(5);
            
            bool hasPreGraph = m_skillGraph.preGraph != null;
            string toolTips_preGraph = hasPreGraph ? $"返回{m_skillGraph.preGraph.name}" : "返回上一级界面(暂无)";

            AddButton(new GUIContent("back", toolTips_preGraph), () =>
            {
                if (hasPreGraph)
                {
                    SkillAssetCallback.OnBaseGraphOpened(m_skillGraph.preGraph.GetInstanceID(), 1);
                    m_skillGraph.preGraph = null;
                }
                else
                    skillGraphWindow.ShowNotification(new GUIContent("无上一级界面"));

            });
        }
    }
}
