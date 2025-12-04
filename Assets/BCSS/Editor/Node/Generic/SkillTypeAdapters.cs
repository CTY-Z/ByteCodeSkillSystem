using GraphProcessor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BCSS.Editor
{
    public enum SkillDataType
    {
        None,
        Int,
        Float,
        Vector2,
        Vector3,
    }

    public class SkillTypeAdapters : ITypeAdapter
    {
        public static Dictionary<SkillDataType, Type> dic_enum_type = new()
        {
            { SkillDataType.Int,     typeof(int)     },
            { SkillDataType.Float,   typeof(float)   },
            { SkillDataType.Vector2, typeof(Vector2) },
            { SkillDataType.Vector3, typeof(Vector3) },
        };

        public static Dictionary<Type, SkillDataType> dic_type_enum = new()
        {
            { typeof(int)    , SkillDataType.Int      },
            { typeof(float)  , SkillDataType.Float    },
            { typeof(Vector2), SkillDataType.Vector2  },
            { typeof(Vector3), SkillDataType.Vector3  },
        };

        public static IPort GetInstanceByPortValueType(Type portValueType)
        {
            SkillDataType typeEnum = SkillDataType.None;
            dic_type_enum.TryGetValue(portValueType, out typeEnum);

            if (typeEnum == SkillDataType.None)
                return new Int();

            switch (typeEnum)
            {
                case SkillDataType.Int:     return new Int();
                case SkillDataType.Float:   return new Float();
                case SkillDataType.Vector2: return new V2();
                case SkillDataType.Vector3: return new V3();
            }

            return new Int();
        }

        //int
        public static IPort Convert_int_To_Int(int from) => new Int(from);
        public static int Convert_Int_To_int(IPort from) => (Int)from;
        //float
        public static IPort Convert_float_To_Float(float from) => new Float(from);
        public static float Convert_Float_To_float(IPort from) => (Float)from;
        //Vector2/V2
        public static IPort Convert_Vector2_To_V2(Vector2 from) => new V2(from);
        public static Vector2 Convert_V2_To_Vector2(IPort from) => (V2)from;
        //Vector3/V3
        public static IPort Convert_Vector3_To_V3(Vector3 from) => new V3(from);
        public static Vector3 Convert_V3_To_Vector3(IPort from) => (V3)from;
    }
}
