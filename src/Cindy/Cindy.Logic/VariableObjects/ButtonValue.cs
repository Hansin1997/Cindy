﻿using Cindy.Logic.Conditions;
using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{

    [AddComponentMenu("Cindy/Logic/VariableObject/Bool/ButtonValue")]
    public class ButtonValue : BoolObject
    {
        [Header("Button")]
        public ReferenceString buttonKey;
        public ButtonCondition.ButtonType state;

        protected override void Start()
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
