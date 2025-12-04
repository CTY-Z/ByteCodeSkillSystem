using BCSS.Editor;
using GraphProcessor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEditor.LightingExplorerTableColumn;

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
        private Dictionary<string, PortData> dic_inputPortID_portData;
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
            if (dic_inputPortID_portData == null)
                dic_inputPortID_portData = new();
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

            foreach (PortData item in dic_inputPortID_portData.Values)
                yield return item;
        }

        public void AddNewInputPort()
        {
            var parameterType = new GenericMenu();

            foreach (var paramType in SkillTypeAdapters.dic_type_enum.Keys)
            {
                parameterType.AddItem(new GUIContent(paramType.Name), false, () =>
                {
                    var identifier = System.Guid.NewGuid().ToString();

                    PortData data = GetPortData(identifier, paramType);

                    dic_inputPortID_portData.Add(identifier, data);
                    list_input.Add(SkillTypeAdapters.GetInstanceByPortValueType(paramType));

                    UpdatePortsForField(nameof(list_input));
                    subGraph.AddPortValue(identifier, GetPortValue(identifier));
                });
            }

            parameterType.ShowAsContext();
        }

        public void AddNewInputPort(string identifier, IPort portValue)
        {
            PortData data = GetPortData(identifier, portValue);
            dic_inputPortID_portData.Add(identifier, data);
            list_input.Add(portValue);

            UpdatePortsForField(nameof(list_input));
        }

        public void SetIdentifierList(Dictionary<string, IPort> dic_identifier)
        {
            if (dic_identifier == null) return;

            foreach (var item in dic_identifier)
            {
                if (dic_inputPortID_portData.ContainsKey(item.Key))
                    continue;

                PortData data = GetPortData(item.Key, item.Value);
                dic_inputPortID_portData.Add(item.Key, data);
            }
        }

        public void RemoveInputPort(string identifier)
        {
            int idx = -1;

            foreach (var item in dic_inputPortID_portData)
            {
                if (item.Key == identifier)
                    break;
                idx++;
            }

            if (idx >= 0)
            {
                if (list_input != null && idx < list_input.Count)
                    list_input.RemoveAt(idx);

                dic_inputPortID_portData.Remove(identifier);
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

        private PortData GetPortData(string identifier, IPort portValue)
        {
            if (portValue == null)
                portValue = new Int();

            Type dataType = null;
            SkillTypeAdapters.dic_enum_type.TryGetValue(portValue.type, out dataType);
            PortData data = new PortData()
            {
                displayName = dataType.Name,
                displayType = dataType,
                identifier = identifier,
                acceptMultipleEdges = false
            };

            return data;
        }
        private PortData GetPortData(string identifier, Type valueType)
        {
            PortData data = new PortData()
            {
                displayName = valueType.Name,
                displayType = valueType,
                identifier = identifier,
                acceptMultipleEdges = false
            };

            return data;
        }
    }

}