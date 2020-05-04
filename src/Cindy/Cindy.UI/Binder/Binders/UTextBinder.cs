using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Binder.Binders
{
    /// <summary>
    /// Text数据绑定器
    /// </summary>
    [AddComponentMenu("Cindy/UI/Binder/Binders/UTextBinder")]
    public class UTextBinder : SingleValueBinder<string, Text>
    {
        public DefaultValue defaultValue = DefaultValue.EmptyString;

        protected override void BindData(Text target, string value)
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

        protected override string TargetData(Text target)
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
