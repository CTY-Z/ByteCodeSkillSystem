using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    public class DamageInst : ISkillInstruction
    {
        public int pool_idx { get; set; }
        public void Execute(SkillExecutionContext context)
        {
            float damage = (float)context.Stack.Pop();
            //GameObject target = (GameObject)context.Stack.Pop();
            //Debug.Log($"Take {damage} Damage");
        }

        public void Undo(SkillExecutionContext context)
        {
            // »Ö¸´ÉúÃüÖµ
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}