using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 条件转布尔型变量
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Util/ConditionBool (Bool)")]
    public class ConditionBool : BoolObject
    {
        [Header("Condition")]
        public Condition condition;

        protected virtual void Start()
        {
            value = condition != null && condition.Check();
        }

        public override void SetValue(bool value)
        {

        }

        public override bool GetValue()
        {
            value = condition != null && condition.Check();
            return base.GetValue();
        }
    }
}
