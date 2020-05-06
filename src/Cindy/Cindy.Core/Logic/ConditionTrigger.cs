using UnityEngine;

namespace Cindy.Logic
{
    /// <summary>
    /// 条件触发器
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Triggers/ConditionTrigger")]
    public class ConditionTrigger : AbstractMonoMethodTrigger
    {
        /// <summary>
        /// 触发条件
        /// </summary>
        [Header("Condition Trigger")]
        public Condition condition;
        /// <summary>
        /// 是否取反
        /// </summary>
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
            }
            else if (lastValue != val)
            {
                OnConditionValueChanged(val);
                lastValue = val;
            }
            return val;
        }
    }
}