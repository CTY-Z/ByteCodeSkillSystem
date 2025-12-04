using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace BCSS.Editor
{
    public class SkillProcessorView : PinnedElementView
    {
        SkillProcessor processor;

        public SkillProcessorView() : base()
        {
            title = "SkillPanel";
        }

        protected override void Initialize(BaseGraphView graphView)
        {
            processor = new SkillProcessor(graphView.graph);

            graphView.computeOrderUpdated += processor.UpdateComputeOrder;

            var field_graph = new ObjectField("Graph To Open")
            {
                objectType = typeof(BaseGraph), value = processor.graphToOpen, allowSceneObjects = false
            };
            field_graph.RegisterValueChangedCallback(evt =>
            {
                processor.graphToOpen = evt.newValue as BaseGraph;
            });

            Button btn_play = new Button(OnPlay) { name = "ActionButton", text = "Play" };

            content.Add(field_graph);
            content.Add(btn_play);
        }

        void OnPlay()
        {
            processor.Run();
        }
    }
}
