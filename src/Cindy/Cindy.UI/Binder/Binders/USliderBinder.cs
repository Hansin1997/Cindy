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
                dataSource.GetData<float>(minValue.key.Value, this, (val, e, s) =>
                  {
                      if (s)
                          slider.minValue = val;
                      else
                          Debug.LogWarning(e);
                  });
            if (maxValue.bind)
                dataSource.GetData<float>(maxValue.key.Value, this, (val, e, s) =>
                 {
                     if (s)
                         slider.maxValue = val;
                     else
                         Debug.LogWarning(e);
                 });
            if (value.bind)
                dataSource.GetData<float>(value.key.Value, this, (val, e, s) =>
                 {
                     if (s)
                         slider.value = val;
                     else
                         Debug.LogWarning(e);
                 });
        }

        protected override void OnApply(AbstractDataSource dataSource)
        {
            if (slider == null)
            {
                Debug.LogWarning("Slider is null!");
                return;
            }
            if (minValue.bind)
                dataSource.SetData(minValue.key.Value, slider.minValue, this, (s,e)=> { if (!s) Debug.LogWarning(e); });
            if (maxValue.bind)
                dataSource.SetData(maxValue.key.Value, slider.maxValue, this, (s, e) => { if (!s) Debug.LogWarning(e); });
            if (value.bind)
                dataSource.SetData(value.key.Value, slider.value, this, (s, e) => { if (!s) Debug.LogWarning(e); });
        }

        [Serializable]
        public class ItemOption
        {
            public ReferenceString key;
            public bool bind;
        }
    }
}
