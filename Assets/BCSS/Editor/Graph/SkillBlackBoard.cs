using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS.Editor
{
    [HideReferenceObjectPicker]
    [BoxGroup]
    public class SkillBlackBoard
    {
        public Dictionary<string, string> TestEvent = new Dictionary<string, string>();

        public Dictionary<long, long> TestId = new Dictionary<long, long>();
    }
}
