using UnityEngine;

namespace Cindy.Logic
{
    [AddComponentMenu("Cindy/Logic/Triggers/ConditionTrigger")]
    public class ConditionTrigger : AbstractMonoMethodTrigger
    {
        [Header("Condition Trigger")]
        public Condition condition;
        public bool invert;

        private bool lastValue, firstTime = true;

        protected virtual void OnConditionValueChanged(bool val) { }

        protected override bool Handle()
        {
            if (condition == null)
                return false; 
            bool val = condition.Check();
            if (invert)
                val = !val;
            if (firstTime)
            {
                OnConditionValueChanged(val);
                lastValue = val;
                firstTime = false;
                return true;
            }
            else if (lastValue != val)
            {
                OnConditionValueChanged(val);
                lastValue = val;
                return true;
            }
            return false;
        }
    }
}