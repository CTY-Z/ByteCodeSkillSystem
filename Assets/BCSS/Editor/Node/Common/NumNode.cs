using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable, NodeMenuItem("Common/Num")]
public class NumNode : BaseNode
{
    public float num;

    [Output(name = "num")]
    public float output;

    protected override void Process()
    {
        output = num;
    }

    public override void TryGetOutputValue<T>(NodePort outputPort, NodePort inputPort, ref T value)
    {
        if (output is T finalValue)
        {
            value = finalValue;
        }
    }
}
