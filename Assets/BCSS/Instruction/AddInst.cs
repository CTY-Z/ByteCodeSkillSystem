using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class AddInst : ISkillInstruction
    {
        public int pool_idx { get; set; }

        public void Execute(SkillExecutionContext context)
        {
            var b = context.Stack.Pop();
            var a = context.Stack.Pop();
            context.Stack.Push((float)a + (float)b);
        }

        public void Undo(SkillExecutionContext context)
        {
            context.Stack.Pop();
        }
    }
}
