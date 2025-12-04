using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS.Editor
{

    [System.Serializable, NodeMenuItem("InstructionNode/Add")]
    public class AddInstNode : BaseNode, IBaseNode
    {
        [Input(name = "A")]
        public float inputA;
        [Input(name = "B")]
        public float inputB;

        [Output(name = "Out")]
        public float output;

        public override string name => "Sub";

        protected override void Process()
        {
            TryGetInputValue(nameof(inputA), ref inputA);
            TryGetInputValue(nameof(inputB), ref inputB);
            output = inputA + inputB;
        }

        public override void TryGetOutputValue<T>(NodePort outputPort, NodePort inputPort, ref T value)
        {
            if (output is T finalValue)
            {
                value = finalValue;
            }
        }

        public void OnPanel()
        {

        }
    }
}
