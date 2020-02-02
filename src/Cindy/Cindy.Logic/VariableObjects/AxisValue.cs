﻿using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/AxisValue")]
    public class AxisValue : FloatObject
    {
        [Header("Axis")]
        public ReferenceString axisName;

        protected override void Start()
        {
            GetValue();
        }

        protected override void Update()
        {
            GetValue();
            base.Update();
        }

        public override float GetValue()
        {
            if (axisName != null && axisName.Value != null && axisName.Value.Trim().Length > 0)
                value = VirtualInput.GetAxis(axisName.Value);
            return base.GetValue();
        }

        public override void SetValue(float value)
        {

        }
    }
}
