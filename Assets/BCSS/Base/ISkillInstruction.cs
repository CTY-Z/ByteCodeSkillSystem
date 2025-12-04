using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public interface ISkillInstruction
    {
        public int pool_idx { get; set; }
        void Execute(SkillExecutionContext context);
        void Undo(SkillExecutionContext context);
    }
}
