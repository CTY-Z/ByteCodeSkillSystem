using BCSS_1;
using CMFramework.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using static UnityEngine.GraphicsBuffer;

namespace BCSS_1
{
    public class SkillManager : MonoBehaviour
    {
        private Dictionary<string, SkillData> _skills = new Dictionary<string, SkillData>();
        private SkillVM _vm = new SkillVM();

        private Dictionary<InstructionType, Func<ISkillInstruction>> dic_instType_return = new();

        private ObjectPool<AddInst> pool_add;
        private ObjectPool<MultInst> pool_mult;
        private ObjectPool<MultByInst> pool_multBy;
        private ObjectPool<PushValueInst> pool_pushValue;
        private ObjectPool<DamageInst> pool_damage;

        private ObjectPool<SkillExecutionContext> pool_context;
        private void Start()
        {
            pool_add = new ObjectPool<AddInst>(100, () => { return new AddInst(); }, false, null, null, true);
            pool_mult = new ObjectPool<MultInst>(100, () => { return new MultInst(); }, false, null, null, true);
            pool_multBy = new ObjectPool<MultByInst>(100, () => { return new MultByInst(0); }, false, null, null, true);
            pool_pushValue = new ObjectPool<PushValueInst>(100, () => { return new PushValueInst(0); }, false, null, null, true);
            pool_damage = new ObjectPool<DamageInst>(100, () => { return new DamageInst(); }, false, null, null, true);

            pool_context = new ObjectPool<SkillExecutionContext>(100, () => { return new SkillExecutionContext(); }, false, null, null, true);

            dic_instType_return[InstructionType.Add] = () =>
            {
                using (var poole = pool_add!.Rent())
                {
                    return poole.value;
                }
            };
            dic_instType_return[InstructionType.Mult] = () =>
            {
                using (var poole = pool_mult!.Rent())
                {
                    return poole.value;
                }
            };
            dic_instType_return[InstructionType.MultBy] = () =>
            {
                using (var poole = pool_multBy!.Rent())
                {
                    return poole.value;
                }
            };
            dic_instType_return[InstructionType.Push] = () =>
            {
                using (var poole = pool_pushValue!.Rent())
                {
                    return poole.value;
                }
            };
            dic_instType_return[InstructionType.Damage] = () =>
            {
                using (var poole = pool_damage!.Rent())
                {
                    return poole.value;
                }
            };
        }

        public void RegisterSkill(string skillId, SkillData data)
        {
            _skills[skillId] = data;
        }

        public void CastSkill(string skillId, GameObject caster, GameObject target = null)
        {
            if (_skills.TryGetValue(skillId, out var skillData))
            {
                using (var pooled = pool_context!.Rent())
                {
                    pooled.value.SetData(caster, target, skillData);
                    var instructions = CompileSkill(skillData);
                    _vm.ExecuteSkill(instructions, pooled.value);
                }
            }
        }

        Dictionary<string, List<ISkillInstruction>> dic_cache = new();

        private List<ISkillInstruction> CompileSkill(SkillData skillData)
        {
            List<ISkillInstruction> list_instructions;
            if (dic_cache.TryGetValue(skillData.SkillName, out list_instructions))
            {
                return list_instructions;
            }

            list_instructions = new();
            foreach (var instructionData in skillData.list_instData)
            {
                var instruction = CreateInstruction(instructionData);
                if (instruction != null)
                    list_instructions.Add(instruction);
            }

            dic_cache[skillData.SkillName] = list_instructions;
            return list_instructions;
        }

        private ISkillInstruction CreateInstruction(SkillInstructionData data)
        {
            dic_instType_return.TryGetValue(data.InstructionType, out Func<ISkillInstruction> fac);

            return fac();
            /*
            switch(data.InstructionType)
            {
                case InstructionType.Add:
                    using (var poole = pool_add!.Rent())
                    {
                        //poole.value.pool_idx = poole.
                        return poole.value;
                    }
                case InstructionType.Mult:
                    using (var poole = pool_mult!.Rent())
                    {
                        return poole.value;
                    }
                case InstructionType.MultBy:
                    using (var poole = pool_multBy!.Rent())
                    {
                        poole.value.SetValue(data.data);
                        return poole.value;
                    }
                case InstructionType.Push:
                    using (var poole = pool_pushValue!.Rent())
                    {
                        poole.value.SetValue(data.data);
                        return poole.value;
                    }
                case InstructionType.Damage:
                    using (var poole = pool_damage!.Rent())
                    {
                        return poole.value;
                    }
                case InstructionType.GetAttribute:
                    return new GetAttributeInst();
                case InstructionType.ApplyEffect:
                    return new ApplyEffectInst();
                default: return null;
            }
            */
        }
    }
}
