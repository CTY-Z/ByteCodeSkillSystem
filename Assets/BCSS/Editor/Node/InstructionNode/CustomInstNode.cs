using BCSS.Editor;
using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

namespace BCSS.Editor
{

    [System.Serializable, NodeMenuItem("InstructionNode/Custom")]
    public class CustomInstNode : BaseNode
    {
        //public List<IBaseNode> list_baseNode = new();

        [Input]
        public List<IPort> list_input;

        [Output]
        public List<IPort> list_output;

        [SerializeField]
        private List<string> list_inputPortIdentifier;
        [SerializeField]
        private List<string> list_outputPortIdentifier;

        public SkillGraph subGraph;
        public override string name => "Custom";

        protected override void Process()
        {
            foreach (NodePort data in GetAllPorts())
            {
                IPort i = null;
                data.GetInputValue(ref i);

                switch (i.type)
                {
                    case SkillDataType.Int:     ProcessIportData<Int, int>(i);     break;
                    case SkillDataType.Float:   ProcessIportData<Float, float>(i); break;
                    case SkillDataType.Vector2: ProcessIportData<V2, Vector2>(i);  break;
                    case SkillDataType.Vector3: ProcessIportData<V3, Vector3>(i);  break;
                }
            }
        }

        public void ProcessIportData<T, K>(IPort data) where T : IPortValue<K>
        {
            T value = (T)data;
            Debug.Log(value.value);
        }

        public override void InitializePorts()
        {
            if (list_inputPortIdentifier == null)
                list_inputPortIdentifier = new List<string>();
            if (list_outputPortIdentifier == null)
                list_outputPortIdentifier = new List<string>();
            if (list_input == null)
                list_input = new List<IPort>();
            if (list_output == null)
                list_output = new List<IPort>();
            base.InitializePorts();
        }

        public override void TryGetOutputValue<T>(NodePort outputPort, NodePort inputPort, ref T value)
        {

        }

        #region input

        [CustomPortBehavior(nameof(list_input))]
        IEnumerable<PortData> ListInputPortBehavior(List<SerializableEdge> edges)
        {
            //while (outputPortIdentifiers.Count < portCount)
            //    outputPortIdentifiers.Add(System.Guid.NewGuid().ToString());
            //while (outputPortIdentifiers.Count > portCount)
            //    outputPortIdentifiers.RemoveAt(outputPortIdentifiers.Count - 1);

            for (int i = 0; i < list_inputPortIdentifier.Count; i++)
            {
                yield return new PortData
                {
                    displayName = $"Iutput  {i}",
                    displayType = typeof(IPort),
                    identifier = list_inputPortIdentifier[i],
                    acceptMultipleEdges = false
                };
            }
        }

        public void AddNewInputPort()
        {
            var parameterType = new GenericMenu();

            foreach (var paramType in SkillTypeAdapters.dic_convert.Keys)
            {
                parameterType.AddItem(new GUIContent(paramType.Name), false, () =>
                {
                    var identifier = System.Guid.NewGuid().ToString();
                    list_inputPortIdentifier.Add(identifier);
                    list_input.Add(null);
                    
                    UpdatePortsForField(nameof(list_input));
                    subGraph.AddPortValue(identifier, GetPortValue(identifier));
                });
            }

            parameterType.ShowAsContext();
        }

        public void AddNewInputPort(string identifier)
        {
            list_inputPortIdentifier.Add(identifier);
            list_input.Add(null);

            UpdatePortsForField(nameof(list_input));
        }

        public void SetIdentifierList(List<string> list_identifier)
        {
            if (list_identifier == null) return;

            list_inputPortIdentifier.Clear();
            list_inputPortIdentifier = list_identifier;
        }

        public void RemoveInputPort(string identifier)
        {
            int idx = list_inputPortIdentifier.IndexOf(identifier);
            if (idx >= 0)
            {
                if (list_input != null && idx < list_input.Count)
                    list_input.RemoveAt(idx);

                subGraph.RemovePortValue(identifier);
            }
            else
                subGraph.RemovePortValue(identifier);
        }

        #endregion

        #region output

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
        #endregion

        private IPort GetPortValue(string dentifier)
        {
            IPort value = null;

            NodePort port = GetPort(nameof(list_input), dentifier);
            TryGetInputValue(port.fieldName, ref value);
            //port.GetInputValue(ref value);

            return value;
        }

        private void SetPortValue(string dentifier, IPort value)
        {
            IPort v = null;
            NodePort port = GetPort(nameof(list_input), dentifier);
            port.GetInputValue(ref v);
            v = value;
        }
    }

}