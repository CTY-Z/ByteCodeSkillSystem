using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class PushValueInst : ISkillInstruction
    {
        public int pool_idx { get; set; }
        private float value;
        public PushValueInst(float value)
        {
            this.value = value;
        }

        public void Execute(SkillExecutionContext context)
        {
            context.Stack.Push(value);
            //Debug.Log($"[Push] --- stack push : {value} - stack count : {context.Stack.Count}");
        }

        public void Undo(SkillExecutionContext context)
        {

        }

        public void SetValue(float value)
        {
            this.value = value;
        }
    }
}
