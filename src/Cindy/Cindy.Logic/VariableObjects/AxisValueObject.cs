using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/AxisValueObject")]
    public class AxisValueObject : FloatObject
    {
        public string axisName;

        protected override void Start()
        {
            if (axisName != null && axisName.Trim().Length > 0)
                value = VirtualInput.GetAxis(axisName);
        }

        protected override void Update()
        {
            if (axisName != null && axisName.Trim().Length > 0)
                value = VirtualInput.GetAxis(axisName);
            base.Update();
        }
    }
}
