using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

namespace BCSS_1
{
    public class SkillVM
    {
        private List<ISkillInstruction> list_inst = new();
        private int programCounter;
        private SkillExecutionContext context;

        public void ExecuteSkill(List<ISkillInstruction> instructions, SkillExecutionContext context)
        {
            list_inst = instructions;
            this.context = context;
            programCounter = 0;
            //context.Variables["VM"] = this;

            while (programCounter < list_inst.Count)
            {
                list_inst[programCounter].Execute(context);
                programCounter++;
            }
        }

        public void Jump(int offset)
        {
            programCounter += offset;
        }
    }
}
