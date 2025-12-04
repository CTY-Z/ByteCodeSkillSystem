using Codice.CM.Common;
using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace BCSS.Editor
{
    public class NumNodeView : BaseNodeView
    {
        NumNode m_node;

        public override void Enable()
        {
            m_node = nodeTarget as NumNode;
            //DrawDefaultInspector();

            FloatField field_num = new FloatField()
            {
                value = m_node.num,
            };
            field_num.RegisterValueChangedCallback(evt =>
            {
                m_node.num = evt.newValue;
            });
        }
    }
}
