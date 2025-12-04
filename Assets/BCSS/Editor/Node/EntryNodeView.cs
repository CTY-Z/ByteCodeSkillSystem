using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                OnAddOutputPort(item.Key);
        }

        public override void Enable()
        {
            m_node = nodeTarget as EntryNode;
            m_graph = (SkillGraph)owner.graph;
            //DrawDefaultInspector();

            m_node.RefreshOutputPort(m_graph.dic_id_entryValue.Keys.ToList());
        }

        private void OnAddOutputPort(string identifier)
        {
            m_node.AddNewOutputPort(identifier);
        }
    }
}
