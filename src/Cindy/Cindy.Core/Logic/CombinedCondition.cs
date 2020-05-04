using System;
using UnityEngine;

namespace Cindy.Logic
{
    /// <summary>
    /// 组合条件
    /// </summary>
    [AddComponentMenu("Cindy/Logic/CombinedCondition")]
    public class CombinedCondition : Condition
    {
        /// <summary>
        /// 操作符
        /// </summary>
        public Operate operate;
        /// <summary>
        /// 条件数组
        /// </summary>
        public Condition[] conditions;
        /// <summary>
        /// 当条件数组为空时，此条件的默认值。
        /// </summary>
        [Tooltip("Value when conditions is empty.")]
        public bool emptyValue = true;

        public override bool Check()
        {
            if (conditions.Length == 0)
                return emptyValue;
            switch (operate)
            {
                case Operate.AND:
                    foreach (Condition condition in conditions)
                        if (!condition.Check())
                            return false;
                    return true;
                case Operate.OR:
                    foreach (Condition condition in conditions)
                        if (condition.Check())
                            return true;
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 操作符
        /// </summary>
        [Serializable]
        public enum Operate
        {
            /// <summary>
            /// 且
            /// </summary>
            AND,
            /// <summary>
            /// 或者
            /// </summary>
            OR
        }
    }
}
