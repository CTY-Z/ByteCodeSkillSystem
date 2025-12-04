using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BCSS.Editor
{
    public class SkillGraphWindow : UniversalGraphWindow
    {
        protected override void OnEnable()
        {
            titleContent = new GUIContent("Skill Graph");
            base.OnEnable();
        }

        protected override void InitializeWindow(BaseGraph graph)
        {
            graphView = new SkillGraphView(this);

            m_MiniMap = new MiniMap() { anchored = true };
            graphView.Add(m_MiniMap);

            m_ToolbarView = new SkillToolbarView(this, graphView, m_MiniMap, (SkillGraph)graph);
            graphView.Add(m_ToolbarView);
        }

        protected override void InitializeGraphView(BaseGraphView view)
        {
            base.InitializeGraphView(view);
            
            view.initialized += OnGraphViewInitialized;
        }

        private void OnGraphViewInitialized()
        {
            SkillGraph skillGraph = (SkillGraph)graph;
            bool hasEntryNode = skillGraph.nodes.Exists(n => n is EntryNode);
            if (skillGraph.isSubGraph && !hasEntryNode)
            {
                Vector2 pos = new Vector2(300, 300);
                EntryNode en = (EntryNode)BaseNode.CreateFromType(typeof(EntryNode), pos);
                graphView.AddNode(en);


            }
        }
    }
}
