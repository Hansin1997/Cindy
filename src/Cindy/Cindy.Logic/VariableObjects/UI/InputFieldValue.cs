using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 输入框值
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/UI/InputFieldValue (String)")]
    public class InputFieldValue : StringObject
    {
        [Header("InputFieldValue")]
        public InputField inputField;

        public override string GetValue()
        {
            if (inputField != null)
                value = inputField.text;
            return base.GetValue();
        }

        public override void SetValue(string value)
        {
            if (inputField != null)
                inputField.text = value;
            base.SetValue(value);
        }
    }
}
