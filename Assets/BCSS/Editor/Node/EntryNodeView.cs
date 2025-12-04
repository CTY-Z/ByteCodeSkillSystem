using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BCSS.Editor
{
    [NodeCustomEditor(typeof(EntryNode))]
    public class EntryNodeView : BaseNodeView
    {
        EntryNode m_node;
        SkillGraph m_graph;

        public override void OnCreated()
        {
            base.OnCreated();

            foreach (var item in m_graph.dic_id_entryValue)
                OnAddOutputPort();
        }

        public override void Enable()
        {
            m_node = nodeTarget as EntryNode;
            m_graph = (SkillGraph)owner.graph;
            //DrawDefaultInspector();
        }

        private void OnAddOutputPort()
        {
            m_node.AddNewOutputPort();
        }
    }
}
