using Cindy.Logic.ReferenceValues;
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
            if (axisName != null && axisName.Value != null && axisName.Value.Trim().Length > 0)
                value = VirtualInput.GetAxis(axisName.Value);
        }

        protected override void Update()
        {
            if (axisName != null && axisName.Value != null && axisName.Value.Trim().Length > 0)
                value = VirtualInput.GetAxis(axisName.Value);
            base.Update();
        }
    }
}
