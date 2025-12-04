using BCSS.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BCSS.Editor
{
    public static class TypeConvert
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

        public static Type GetTypeBySkillDataType(SkillDataType sdt)
        {
            Type type = null;
            dic_enum_type.TryGetValue(sdt, out type);
            return type;
        }

        public static SkillDataType GetSkillDataTypeByType(Type type)
        {
            SkillDataType sdt = SkillDataType.None;
            dic_type_enum.TryGetValue(type, out sdt);
            return sdt;
        }

        public static IPort GetInstanceByPortValueType(Type portValueType)
        {
            SkillDataType typeEnum = SkillDataType.None;
            dic_type_enum.TryGetValue(portValueType, out typeEnum);

            if (typeEnum == SkillDataType.None)
                return new Int();

            switch (typeEnum)
            {
                case SkillDataType.Int: return new Int();
                case SkillDataType.Float: return new Float();
                case SkillDataType.Vector2: return new V2();
                case SkillDataType.Vector3: return new V3();
            }

            return new Int();
        }

        public static bool HasImplicitConversion(Type from, Type to)
        {
            return from.GetMethods(BindingFlags.Static | BindingFlags.Public)
                       .Union(to.GetMethods(BindingFlags.Static | BindingFlags.Public))
                       .Any(mi => (mi.Name == "op_Implicit" || mi.Name == "op_Explicit")
                               && mi.ReturnType == to
                               && mi.GetParameters().FirstOrDefault()?.ParameterType == from);
        }

    }

}

