using System;
using UnityEngine;

namespace Cindy.Logic
{
    public class CombinedCondition : Condition
    {
        public Operate operate;
        public Condition[] conditions;
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

        [Serializable]
        public enum Operate
        {
            AND,
            OR
        }
    }
}
