using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class GetAttributeInst : ISkillInstruction
    {
        public int pool_idx { get ; set; }

        public void Execute(SkillExecutionContext context)
        {

        }

        public void Undo(SkillExecutionContext context)
        {

        }
    }
}

