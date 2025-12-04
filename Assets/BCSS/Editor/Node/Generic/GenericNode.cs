using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS.Editor
{
    public abstract class GenericNode<T> : BaseNode
    {
        [Input] 
        public T input;
        [Output] 
        public IPort output;

        protected abstract IPort ProcessValue(T value);

        protected override void Process()
        {
            TryGetInputValue<T>(nameof(input), ref input);
            output = ProcessValue(input);
        }

        public override void TryGetOutputValue<K>(NodePort outputPort, NodePort inputPort, ref K value)
        {
            if (output is K finalValue)
                value = finalValue;
        }
    }

    [NodeMenuItem("Generic/IntNode")]
    public class IntNode : GenericNode<int>
    {
        protected override IPort ProcessValue(int value) { return new Int(value); }
    }

    [NodeMenuItem("Generic/FloatNode")]
    public class FloatNode : GenericNode<float>
    {
        protected override IPort ProcessValue(float value) { return new Float(value); }
    }

    [NodeMenuItem("Generic/Vector2Node")]
    public class Vector2Node : GenericNode<Vector2>
    {
        protected override IPort ProcessValue(Vector2 value) { return new V2(value); }
    }

    [NodeMenuItem("Generic/Vector3Node")]
    public class Vector3Node : GenericNode<Vector3>
    {
        protected override IPort ProcessValue(Vector3 value) { return new V3(value); }
    }

}