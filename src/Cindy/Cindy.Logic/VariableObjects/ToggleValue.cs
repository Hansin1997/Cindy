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

            if (toggle == null)
                toggle = GetComponent<Toggle>();
            if (toggle != null)
                toggle.isOn = val;
        }

        protected override void OnValueChanged()
        {
            base.OnValueChanged();
            if(toggle != null)
             toggle.isOn = value;
        }

        public override bool GetValue()
        {
            if (toggle == null)
                toggle = GetComponent<Toggle>();
            if(toggle != null)
            {
                value = toggle.isOn;
                return base.GetValue();
            }
            else
            {
                return false;
            }
        }

        public override void SetValue(bool value)
        {
            if (toggle == null)
                toggle = GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.isOn = value;

                base.SetValue(value);
            }
            else
            {
                base.SetValue(false);
            }
        }
    }
}
