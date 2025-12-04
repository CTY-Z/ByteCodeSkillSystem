using BCSS_1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class BranchIfLessInst : ISkillInstruction
    {
        public int pool_idx { get; set; }
        public int JumpOffset { get; set; }

        public void Execute(SkillExecutionContext context)
        {
            var value = (float)context.Stack.Pop();
            var condition = (float)context.Stack.Pop();

            if (condition < value)
            {
                var vm = context.Variables["VM"] as SkillVM;
                vm.Jump(JumpOffset);
            }
        }

        public void Undo(SkillExecutionContext context) { }
    }
}

