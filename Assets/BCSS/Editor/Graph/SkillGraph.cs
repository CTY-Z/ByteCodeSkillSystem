using BCSS.Editor;
using GraphProcessor;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BCSS.Editor
{
    public class SkillGraph : BaseGraph
    {
        [HideInInspector]
        public SkillBlackBoard skillBlackBoard = new SkillBlackBoard();

        #region SubGraph
        public bool isSubGraph = false;
        public SkillGraph preGraph = null;

        public Dictionary<string, IPort> dic_id_entryValue = new();
        #endregion

        [Button("ExpoertSkillData")]
        public void ExpoertSkillData()
        {

        }

        public bool AddPortValue(string dentifier, IPort value)
        {
            if (!dic_id_entryValue.ContainsKey(dentifier))
            {
                dic_id_entryValue.Add(dentifier, value);
                return true;
            }

            return false;
        }

        public void RemovePortValue(string dentifier)
        {
            if (dic_id_entryValue.ContainsKey(dentifier))
                dic_id_entryValue.Remove(dentifier);
        }
    }
}
