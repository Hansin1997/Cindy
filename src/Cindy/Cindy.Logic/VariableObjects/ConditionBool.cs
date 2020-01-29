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
            value = condition != null && condition.Check();
            base.Update();
        }
    }
}
