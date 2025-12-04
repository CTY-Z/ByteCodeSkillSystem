using BCSS_1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class MultInst : ISkillInstruction
    {
        public int pool_idx { get; set; }
        public void Execute(SkillExecutionContext context)
        {
            var a = context.Stack.Pop();
            var b = context.Stack.Pop();

            context.Stack.Push((float)a * (float)b);
        }

        public void Undo(SkillExecutionContext context)
        {

        }
    }
}
