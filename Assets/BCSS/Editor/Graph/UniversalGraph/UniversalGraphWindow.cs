using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphProcessor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace BCSS.Editor
{
    public abstract class UniversalGraphWindow : BaseGraphWindow
    {
        protected UniversalToolbarView m_ToolbarView;
        protected MiniMap m_MiniMap;

        private bool m_HasInitGUIStyles;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_HasInitGUIStyles = false;
        }

        protected override void InitializeWindow(BaseGraph graph)
        {
            graphView = new UniversalGraphView(this);

            m_MiniMap = new MiniMap() { anchored = true };
            graphView.Add(m_MiniMap);

            m_ToolbarView = new UniversalToolbarView(graphView, m_MiniMap, graph);
            graphView.Add(m_ToolbarView);
        }

        /// <summary>
        /// 初始化绘制此GraphView需要用到的GUIStyle
        /// </summary>
        private void InitGUIStyles(ref bool result)
        {
            if (!result)
            {
                EditorGUIStyleHelper.SetGUIStylePadding(nameof(EditorStyles.toolbarButton), new RectOffset(15, 15, 0, 0));
                result = true;
            }
        }

        private void OnGUI()
        {
            InitGUIStyles(ref m_HasInitGUIStyles);

            m_MiniMap?.SetPosition(new Rect(this.position.size.x - 205, this.position.size.y - 205, 200, 200));
        }

        protected override void InitializeGraphView(BaseGraphView view)
        {
            // graphView.OpenPinned< ExposedParameterView >();
            // toolbarView.UpdateButtonStatus();
        }
    }
}