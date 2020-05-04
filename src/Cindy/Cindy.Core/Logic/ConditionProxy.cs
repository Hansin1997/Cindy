using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic
{
    /// <summary>
    /// 条件代理
    /// </summary>
    [AddComponentMenu("Cindy/Logic/ConditionProxy")]
    public class ConditionProxy : Condition
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public Context context;
        /// <summary>
        /// 被代理条件的名称
        /// </summary>
        public ReferenceString targetName;

        protected Condition _target;

        public override bool Check()
        {
            if (_target == null || !_target.conditionName.Equals(targetName))
            {
                if (context == null)
                    return false;
                Condition[] targets = context.Find<Condition>();
                foreach (Condition target in targets)
                {
                    if (target.conditionName.Equals(targetName))
                    {
                        _target = target;
                        break;
                    }
                }
            }
            if (_target == null)
                return false;
            return _target.Check();
        }
    }
}
