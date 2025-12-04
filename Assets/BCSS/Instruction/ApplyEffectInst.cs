using BCSS_1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class ApplyEffectInst : ISkillInstruction
    {
        public int pool_idx { get; set; }
        public void Execute(SkillExecutionContext context)
        {
            Debug.Log("apply some effect");
        }

        public void Undo(SkillExecutionContext context)
        {

        }
    }
}
