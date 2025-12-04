using BCSS_1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class MultByInst : ISkillInstruction
    {
        public int pool_idx { get; set; }
        float value;

        public MultByInst(float value)
        {
            this.value = value;
        }

        public void Execute(SkillExecutionContext context)
        {
            var b = context.Stack.Pop();
            context.Stack.Push(value * (float)b);
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
