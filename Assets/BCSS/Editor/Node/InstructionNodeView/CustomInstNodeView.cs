using BCSS.Editor;
using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor.Graphs;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BCSS.Editor
{
    [NodeCustomEditor(typeof(CustomInstNode))]
    public class CustomInstNodeView : BaseNodeView
    {
        CustomInstNode m_node;
        SkillGraphView m_view;

        public override void Enable()
        {
            this.m_node = nodeTarget as CustomInstNode;
            m_view = (SkillGraphView)owner;
            //DrawDefaultInspector();

            m_view.OnCustomNodeInputChanged += OnInputChanged;

            AddDeleteButtonToInputPorts();
            AddDeleteButtonToOutputPorts();

            var field_subGraph = new ObjectField("SubGraph")
            {
                objectType = typeof(SkillGraph),
                value = m_node.subGraph,
                allowSceneObjects = false
            };
            field_subGraph.RegisterValueChangedCallback(evt =>
            {
                m_node.subGraph = evt.newValue as SkillGraph;

                if (m_node.subGraph != null && m_node.subGraph.isSubGraph)
                {
                    foreach (var item in m_node.subGraph.dic_id_entryValue)
                        m_node.AddNewInputPort(item.Key, item.Value);
                }
            });

            string str_subGraph = "OpenOrCreateSubGraph";
            Button btn_subGraph = new Button(OnOpenOrCreateSubGraph)
            {
                name = str_subGraph,
                text = str_subGraph,
            };

            string str_addInput = "AddInput";
            Button btn_addInput = new Button(OnAddInputPort)
            {
                name = str_addInput,
                text = str_addInput,
                style = { flexGrow = 1 }
            };

            string str_addOutput = "AddOutput";
            Button btn_addOutput = new Button(OnAddOutputPort)
            {
                name = str_addOutput,
                text = str_addOutput,
                style = { flexGrow = 1 }
            };

            var buttonContainer = new VisualElement { style = { flexDirection = FlexDirection.Row } };
            buttonContainer.Add(btn_addInput);
            buttonContainer.Add(btn_addOutput);

            controlsContainer.Add(field_subGraph);
            controlsContainer.Add(btn_subGraph);
            controlsContainer.Add(buttonContainer);
        }

        public override void Disable()
        {
            base.Disable();

            m_view.OnCustomNodeInputChanged -= OnInputChanged;
        }

        public override bool RefreshPorts()
        {
            var result = base.RefreshPorts();
            AddDeleteButtonToInputPorts();
            AddDeleteButtonToOutputPorts();
            return result;
        }

        private void OnOpenOrCreateSubGraph()
        {
            if (m_node.subGraph == null)
            {
                SkillGraph asset = SkillAssetCallback.CreateGraphPorcessor_Skill();
                if (asset != null)
                {
                    owner.RegisterCompleteObjectUndo("Set Sub Graph");
                    asset.isSubGraph = true;
                    m_node.subGraph = asset;
                }
            }
            else
                SkillAssetCallback.OnBaseGraphOpened(m_node.subGraph.GetInstanceID(), 1);

            m_node.subGraph.preGraph = (SkillGraph)owner.graph;
            NotifyNodeChanged();
        }

        private void OnInputChanged(SkillGraph graph, bool isAdded)
        {
            if (m_node.subGraph != graph) return;

            m_node.SetIdentifierList(m_node.subGraph.dic_id_entryValue);
            m_node.UpdatePortsForField(nameof(m_node.list_input));
        }

        #region input
        void AddDeleteButtonToInputPorts()
        {
            foreach (var portView in inputPortViews)
            {
                var oldBtn = portView.Q<Button>("delete-port-btn");
                if (oldBtn != null)
                    portView.Remove(oldBtn);

                var btn = new Button(() => 
                { 
                    m_node.RemoveInputPort(portView.portData.identifier);
                    m_view.CustomNodeInputChangedInvoke(m_node.subGraph, false);
                })
                {
                    name = "delete-port-btn",
                    text = "x",
                    tooltip = "rmeove input port",

                    style = { width = 16, height = 16, marginLeft = 2, marginRight = 2 },
                };

                portView.Add(btn);
            }
        }
        public void OnAddInputPort()
        {
            if (m_node.subGraph == null)
            {
                m_view.window.ShowNotification(new GUIContent("请先赋值SubGraph"));
                return;
            }

            m_node.AddNewInputPort();
            m_view.CustomNodeInputChangedInvoke(m_node.subGraph, true);
        }

        #endregion

        #region output
        void AddDeleteButtonToOutputPorts()
        {
            foreach (var portView in outputPortViews)
            {
                var oldBtn = portView.Q<Button>("delete-port-btn");
                if (oldBtn != null)
                    portView.Remove(oldBtn);

                var btn = new Button(() => { m_node.RemoveOutputPort(portView.portData.identifier); })
                {
                    name = "delete-port-btn",
                    text = "x",
                    tooltip = "rmeove output port",
                    style = { width = 16, height = 16, marginLeft = 2, marginRight = 2 },
                };

                portView.Add(btn);
            }
        }
        private void OnAddOutputPort()
        {
            if (m_node.subGraph == null)
            {
                m_view.window.ShowNotification(new GUIContent("请先赋值SubGraph"));
                return;
            }

            m_node.AddNewOutputPort();
        }
        #endregion
    }
}
