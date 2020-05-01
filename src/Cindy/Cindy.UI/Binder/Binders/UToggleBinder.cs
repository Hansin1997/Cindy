using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder.Binders
{
    [AddComponentMenu("Cindy/UI/Binder/Binders/UToggleBinder")]
    public class UToggleBinder : SingleValueBinder<bool, Toggle>
    {
        public bool applyToDataSource = true;

        protected override void Initialize()
        {
            base.Initialize();
            if(target != null)
            {
                target.onValueChanged.AddListener(ApplyValue);
            }
        }

        private void ApplyValue(bool value)
        {
            if (applyToDataSource)
                SetValue(value);
        }

        protected override bool GetDefaultValue()
        {
            return target != null ? target.isOn : false;
        }

        protected override void BindData(Toggle target, bool value)
        {
            target.isOn = value;
        }

        protected override bool TargetData(Toggle target)
        {
            return target.isOn;
        }
    }
}
