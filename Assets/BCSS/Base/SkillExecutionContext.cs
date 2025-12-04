using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public struct ContextData
    {

    }

    public class SkillExecutionContext
    {
        public GameObject Caster { get; set; }
        public GameObject Target { get; set; }
        public Vector3 Position { get; set; }
        public SkillData SkillData { get; set; }
        public Stack<float> Stack { get; } = new Stack<float>();
        public Dictionary<string, object> Variables { get; } = new Dictionary<string, object>();

        public void SetData(GameObject caster, GameObject target, SkillData data)
        {
            this.Caster = caster;
            this.Target = target;
            this.SkillData = data;
        }
    }
}

