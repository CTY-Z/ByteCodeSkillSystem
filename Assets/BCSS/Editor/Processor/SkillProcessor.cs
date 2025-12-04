using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace BCSS.Editor
{
    public class SkillProcessor : ProcessGraphProcessor
    {
        public BaseGraph graphToOpen;

        public SkillProcessor(BaseGraph graph) : base(graph)
        {

        }

        public override void Run()
        {
            base.Run();
        }

        public void Open()
        {
            SkillAssetCallback.OnBaseGraphOpened(graphToOpen.GetInstanceID(), 1);
        }
    }
}
