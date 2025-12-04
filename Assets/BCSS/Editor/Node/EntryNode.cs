using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS.Editor
{
    public class EntryNode : BaseNode
    {
        [Output]
        public List<IPort> list_output;
        [SerializeField]
        private List<string> list_outputPortIdentifier;

        public override string name => "Entry";

        public override void OnNodeCreated()
        {
            base.OnNodeCreated();

        }

        public override void InitializePorts()
        {
            if (list_outputPortIdentifier == null)
                list_outputPortIdentifier = new List<string>();
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

            for (int i = 0; i < list_outputPortIdentifier.Count; i++)
            {
                yield return new PortData
                {
                    displayName = $"Output  {i}",
                    displayType = typeof(IPort),
                    identifier = list_outputPortIdentifier[i],
                    acceptMultipleEdges = false
                };
            }
        }

        public void AddNewOutputPort()
        {
            list_outputPortIdentifier.Add(System.Guid.NewGuid().ToString());
            UpdatePortsForField(nameof(list_output));
        }

        public void RemoveOutputPort(string identifier)
        {
            int idx = list_outputPortIdentifier.IndexOf(identifier);
            if (idx >= 0)
            {
                list_outputPortIdentifier.RemoveAt(idx);
                UpdatePortsForField(nameof(list_output));
            }
        }
    }
}
