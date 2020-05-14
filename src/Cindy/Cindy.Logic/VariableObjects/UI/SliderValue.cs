using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// Slider值
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/UI/SliderValue (Float)")]
    public class SliderValue : FloatObject
    {
        [Header("Slider Value")]
        public Slider slider;
        public ValueType valueType = ValueType.Value;

        protected virtual void Start()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
        }

        protected override void OnValueChanged(bool save = true,bool notify = true)
        {
            base.OnValueChanged(save, notify);
            SetSliderValue(value, slider);
        }

        public override float GetValue()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
            value = GetSliderValue(slider);
            return base.GetValue();
        }

        public override void SetValue(float value)
        {
            if (slider == null)
                slider = GetComponent<Slider>();
            SetSliderValue(value, slider);
            base.SetValue(value);
        }

        private void SetSliderValue(float val, Slider slider)
        {
            if (slider == null)
                return;
            switch (valueType)
            {
                default:
                case ValueType.Value:
                    slider.value = val;
                    break;
                case ValueType.MinValue:
                    slider.minValue = val;
                    break;
                case ValueType.MaxValue:
                    slider.maxValue = val;
                    break;
            }
        }
        private float GetSliderValue(Slider slider)
        {
            if (slider == null)
                return 0;
            switch (valueType)
            {
                default:
                case ValueType.Value:
                    return slider.value;
                case ValueType.MinValue:
                    return slider.minValue;
                case ValueType.MaxValue:
                    return slider.maxValue;
            }
        }

        public enum ValueType
        {
            Value,
            MinValue,
            MaxValue
        }
    }
}
