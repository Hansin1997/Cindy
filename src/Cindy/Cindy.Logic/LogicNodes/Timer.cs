using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes
{
    [AddComponentMenu("Cindy/Logic/LogicNodes/Timer")]
    public class Timer : LogicNode
    {
        [Header("Timer")]
        public ReferenceFloat time = new ReferenceFloat() { value = 1 };
        public ReferenceFloat currentTime = new ReferenceFloat() { value = 0 };
        public ReferenceBool loop;

        protected bool started;

        public override void Execute()
        {
            started = true;
            currentTime.Value = 0;
        }

        protected void Update()
        {
            if (started)
            {
                currentTime.Value += Time.deltaTime;
                if (currentTime.Value >= time.Value)
                {
                    base.Execute();
                    started = false;
                    if (loop.Value)
                        Execute();
                }
            }
        }
    }
}
