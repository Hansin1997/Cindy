using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/ToggleValue")]
    public class ToggleValue : BoolObject
    {
        public Toggle toggle;

        protected override void Start()
        {
            if (toggle == null)
                toggle = GetComponent<Toggle>();
            base.Start();
        }

        protected override void OnValueLoad(bool val)
        {
            base.OnValueLoad(val);
            toggle.isOn = val;
        }

        protected override void Update()
        {
            value = toggle.isOn;
            base.Update();
        }

        protected override void OnValueChanged()
        {
            base.OnValueChanged();
            toggle.isOn = value;
        }

        public override bool GetValue()
        {
            value = toggle.isOn;
            return base.GetValue();
        }

        public override void SetValue(bool value)
        {
            toggle.isOn = value;
            base.SetValue(value);
        }
    }
}
