using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace BCSS_1
{
    public class SkillTest : MonoBehaviour
    {
        public struct Poold : IDisposable
        {
            private readonly int idx;
            public readonly int value;

            internal Poold(int idx, int value)
            {
                this.idx = idx;
                this.value = value;
            }

            public void Dispose()
            {
            }
        }

        public SkillData skillData;
        SkillManager mgr;

        // Start is called before the first frame update
        void Start()
        {
            //Profiler.enableAllocationCallstacks = true;
            //Profiler.enabled = true;


            //mgr = GetComponent<SkillManager>();

            //mgr.RegisterSkill("Fireball", skillData);
        }

        // Update is called once per frame
        void Update()
        {
            //if(Input.GetMouseButtonDown(0))
            //{
            //    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            //
            //    for (int i = 0; i < 100000; i++)
            //        mgr.CastSkill("Fireball", this.gameObject);
            //
            //    stopwatch.Stop();
            //    Debug.Log($"创建100000个指令耗时: {stopwatch.ElapsedMilliseconds}ms");
            //}
        }
    }
}
