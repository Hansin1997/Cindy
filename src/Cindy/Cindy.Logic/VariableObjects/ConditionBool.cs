using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/ConditionBool")]
    public class ConditionBool : BoolObject
    {
        [Header("Condition")]
        public Condition condition;

        protected override void Start()
        {
            value = condition != null && condition.Check();
        }

        protected override void Update()
        {
            base.Update();
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
