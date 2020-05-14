using Cindy.Logic.Conditions;
using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 按钮值
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Input/ButtonValue (Bool)")]
    public class ButtonValue : BoolObject
    {
        [Header("Button Value")]
        public ReferenceString buttonKey;
        public ButtonCondition.ButtonType state;

        protected virtual void Start()
        {
            GetValue();
        }

        public override bool GetValue()
        {
            value = ButtonCondition.GetValue(state, buttonKey.Value);
            return value;
        }

        public override void SetValue(bool value)
        {

        }
    }
}
