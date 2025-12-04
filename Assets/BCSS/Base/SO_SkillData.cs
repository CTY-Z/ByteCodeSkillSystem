using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS_1
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill Data")]
    public class SkillData : ScriptableObject
    {
        public string SkillName;
        public float Cooldown;
        public List<SkillInstructionData> list_instData = new();
    }

    [System.Serializable]
    public class SkillInstructionData
    {
        public InstructionType InstructionType;
        public float data;
    }
}

