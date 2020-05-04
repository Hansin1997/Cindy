using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder.Binders
{
    /// <summary>
    /// InputField数据绑定器
    /// </summary>
    [AddComponentMenu("Cindy/UI/Binder/Binders/UInputFieldBinder")]
    public class UInputFieldBinder : SingleValueBinder<string, InputField>
    {
        public DefaultValue defaultValue = DefaultValue.EmptyString;

        public bool applyToDataSource = true;

        protected override void Initialize()
        {
            base.Initialize();
            if(target != null)
            {
                target.onValueChanged.AddListener(ApplyValue);
            }
        }

        private void ApplyValue(string value)
        {
            if (applyToDataSource)
                SetValue(value);
        }

        protected override void BindData(InputField target, string value)
        {
            if (value == null)
            {
                target.text = GetDefaultValue();
            }
            else
            {
                target.text = value;
            }
        }

        protected override string GetDefaultValue()
        {
            switch (defaultValue)
            {
                case DefaultValue.EmptyString:
                    return "";
                case DefaultValue.NullString:
                    return "Null";
                default:
                case DefaultValue.OriginalString:
                    return target != null ? target.text : "";
            }
        }

        protected override string TargetData(InputField target)
        {
            return target.text;
        }

        public enum DefaultValue
        {
            EmptyString,
            NullString,
            OriginalString
        }
    }
}
