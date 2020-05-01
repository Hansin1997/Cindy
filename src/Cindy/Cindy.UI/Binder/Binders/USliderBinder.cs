using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder.Binders
{
    [AddComponentMenu("Cindy/UI/Binder/Binders/USliderBinder")]
    public class USliderBinder : AbstractBinder
    {
        public Slider slider;

        public ItemOption minValue;
        public ItemOption maxValue;
        public ItemOption value = new ItemOption() { bind = true };

        public bool applyValueToDataSource = true;

        protected override void Initialize()
        {
            if (slider == null)
                slider = GetComponent<Slider>();
            if(slider != null)
            {
                slider.onValueChanged.AddListener(ApplyValue);
            }
        }

        private void ApplyValue(float value)
        {
            if(applyValueToDataSource)
            {
                if(dataSource != null)
                {
                    OnApply(dataSource);
                }
                else
                {
                    Debug.LogWarning("DataSource is null!", this);
                }
            }
        }

        protected override void OnBind(AbstractDataSource dataSource)
        {
            if(slider == null)
            {
                Debug.LogWarning("Slider is null!");
                return;
            }
            if (minValue.bind)
                slider.minValue = dataSource.GetData(minValue.key.Value, slider.minValue);
            if (maxValue.bind)
                slider.maxValue = dataSource.GetData(maxValue.key.Value, slider.maxValue);
            if (value.bind)
                slider.value = dataSource.GetData(value.key.Value, slider.value);
        }

        protected override void OnApply(AbstractDataSource dataSource)
        {
            if (slider == null)
            {
                Debug.LogWarning("Slider is null!");
                return;
            }
            if (minValue.bind)
                dataSource.SetData(minValue.key.Value, slider.minValue);
            if (maxValue.bind)
                dataSource.SetData(maxValue.key.Value, slider.maxValue);
            if (value.bind)
                dataSource.SetData(value.key.Value, slider.value);
        }

        [Serializable]
        public class ItemOption
        {
            public ReferenceString key;
            public bool bind;
        }
    }
}
