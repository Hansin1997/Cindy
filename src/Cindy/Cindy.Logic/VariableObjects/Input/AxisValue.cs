using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 轴输入
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Input/AxisValue (Float)")]
    public class AxisValue : FloatObject
    {
        [Header("Axis")]
        public ReferenceString axisName;

        protected virtual void Start()
        {
            GetValue();
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
