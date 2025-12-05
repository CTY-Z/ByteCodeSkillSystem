using GraphProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;

namespace BCSS.Editor
{
    public class EntryNode : BaseNode
    {
        [Output]
        public List<IPort> list_output;

        public override string name => "Entry";

        public override void OnNodeCreated()
        {
            base.OnNodeCreated();

        }

        public override void InitializePorts()
        {
            if (list_output == null)
                list_output = new();
            base.InitializePorts();
        }

        protected override void Process()
        {

        }

        public override void TryGetOutputValue<T>(NodePort outputPort, NodePort inputPort, ref T value)
        {

        }

        [CustomPortBehavior(nameof(list_output))]
        IEnumerable<PortData> ListOutputPortBehavior(List<SerializableEdge> edges)
        {
            //while (outputPortIdentifiers.Count < portCount)
            //    outputPortIdentifiers.Add(System.Guid.NewGuid().ToString());
            //while (outputPortIdentifiers.Count > portCount)
            //    outputPortIdentifiers.RemoveAt(outputPortIdentifiers.Count - 1);

            SkillGraph m_graph = (SkillGraph)graph;

            foreach (var item in m_graph.dic_id_entryValue)
            {
                Type t = TypeConvert.GetTypeBySkillDataType(item.Value.type);
                yield return new PortData
                {
                    displayName = t.Name,
                    displayType = t,
                    identifier = item.Key,
                    acceptMultipleEdges = false
                };
            }
        }

        public void RefreshOutputPort(List<string> list_identifier)
        {
            UpdatePortsForField(nameof(list_output));
        }

        public void AddNewOutputPort(IPort portValue)
        {
            //list_output.Add(portValue);
            UpdatePortsForField(nameof(list_output));
        }

        public void RemoveOutputPort(IPort portValue)
        {
            //list_output.Remove(portValue);
        }
    }
}
