using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Components
{
    public abstract class SliderSetter<T,K,V> : MonoBehaviour where T : NumberObject<V> where K : ReferenceValue<V,T>
    {
        public Slider slider;
        public T valueVariable;

        public K minValue,maxValue;

        protected virtual void Start()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
            if (valueVariable == null)
                valueVariable = GetComponent<T>();

            if (slider != null)
            {
                float v;
                if (minValue.Value != null)
                {
                    if (float.TryParse(minValue.Value.ToString(), out v))
                        slider.minValue = v;
                }

                if (maxValue.Value != null)
                {
                    if (float.TryParse(maxValue.Value.ToString(), out v))
                        slider.maxValue = v;
                }
            }

            if (slider != null && valueVariable != null)
            {
                OnVariableChanged();
                slider.onValueChanged.AddListener(OnSliderValueChanged);
                valueVariable.valueChangedEvent.AddListener(OnVariableChanged);
            }
            else
            {
                if (slider == null)
                    Debug.LogWarning("Slider can not be found!");
                if (valueVariable == null)
                    Debug.LogWarning("Variable can not be found!");
            }

        }

        protected virtual void OnSliderValueChanged(float value)
        {
            if (valueVariable != null)
                valueVariable.SetValue(Transform(value));
        }

        protected abstract V Transform(float value);

        protected virtual void OnVariableChanged()
        {
            if (valueVariable == null || slider == null)
                return;
            if (valueVariable.GetValue() == null)
                slider.value = 0;
            else
            {
                try
                {
                    float v = float.Parse(valueVariable.GetValue().ToString());
                    slider.value = v;
                }catch(Exception e)
                {
                    Debug.LogWarning(e);
                    slider.value = 0;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (slider != null)
                slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            if (valueVariable != null)
                valueVariable.valueChangedEvent.RemoveListener(OnVariableChanged);
        }
    }

    [AddComponentMenu("Cindy/UI/Components/SliderSetter/SliderIntSetter")]
    public class SliderIntSetter : SliderSetter<IntObject, ReferenceInt, int>
    {
        protected override int Transform(float value)
        {
            return (int)value;
        }
    }

    [AddComponentMenu("Cindy/UI/Components/SliderSetter/SliderFloatSetter")]
    public class SliderFloatSetter : SliderSetter<FloatObject, ReferenceFloat, float>
    {
        protected override float Transform(float value)
        {
            return value;
        }
    }
}
