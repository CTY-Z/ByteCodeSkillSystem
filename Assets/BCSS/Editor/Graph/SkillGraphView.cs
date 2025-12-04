using BCSS.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;

namespace BCSS.Editor
{
    public delegate void OnCustomNodeInputChanged(SkillGraph graph, bool isAdded);
    public class SkillGraphView : UniversalGraphView
    {
        private SkillGraphWindow m_window;
        public SkillGraphWindow window { get { return m_window; } }

        public event OnCustomNodeInputChanged OnCustomNodeInputChanged;

        public SkillGraphView(SkillGraphWindow window) : base(window)
        {
            m_window = window;
        }

        public void CustomNodeInputChangedInvoke(SkillGraph graph, bool isAdded)
        {
            OnCustomNodeInputChanged?.Invoke(graph, isAdded);
        }
    }
}
