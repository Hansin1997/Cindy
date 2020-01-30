using Cindy.Logic.ReferenceValues;
using UnityEngine;
using static Cindy.Logic.Conditions.ButtonCondition;

namespace Cindy.Logic.VariableObjects
{

    [AddComponentMenu("Cindy/Logic/VariableObject/ButtonValue")]
    public class ButtonValue : BoolObject
    {
        [Header("Button")]
        public ReferenceString buttonKey;
        public ButtonType state;

        protected override void Start()
        {
            value = GetValue(state, buttonKey.Value);
        }

        protected override void Update()
        {
            value = GetValue(state, buttonKey.Value);
            base.Update();
        }
    }
}
