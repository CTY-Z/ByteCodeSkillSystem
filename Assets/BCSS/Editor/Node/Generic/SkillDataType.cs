using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS.Editor
{
    public interface IPort
    {
        public SkillDataType type { get; set; }
    }
    public interface IPortValue<T> : IPort
    {
        public T value { get; set; }
    }

    public struct Int : IPortValue<int>
    {
        public int value { get; set; }
        public SkillDataType type { get { return TypeConvert.GetSkillDataTypeByType(value.GetType()); } set { } }

        public Int(int value) => this.value = value;

        public static implicit operator int(Int d) => d.value;
        public static explicit operator Int(int b) => new Int(b);
    }

    public struct Float : IPortValue<float>
    {
        public float value { get; set; }
        public SkillDataType type { get { return TypeConvert.GetSkillDataTypeByType(value.GetType()); } set { } }

        public Float(float value) => this.value = value;

        public static implicit operator float(Float d) => d.value;
        public static explicit operator Float(float b) => new Float(b);
    }

    public struct V2 : IPortValue<Vector2>
    {
        public Vector2 value { get; set; }
        public SkillDataType type { get { return TypeConvert.GetSkillDataTypeByType(value.GetType()); } set { } }

        public V2(Vector3 value) => this.value = value;

        public static implicit operator Vector2(V2 d) => d.value;
        public static explicit operator V2(Vector2 b) => new V2(b);
    }

    public struct V3 : IPortValue<Vector3>
    {
        public Vector3 value { get; set; }
        public SkillDataType type { get { return TypeConvert.GetSkillDataTypeByType(value.GetType()); } set { } }

        public V3(Vector3 value) => this.value = value;

        public static implicit operator Vector3(V3 d) => d.value;
        public static explicit operator V3(Vector3 b) => new V3(b);
    }
}