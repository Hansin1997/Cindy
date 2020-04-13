using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/UI/InputFieldValue (String)")]
    public class InputFieldValue : StringObject
    {
        [Header("InputFieldValue")]
        public InputField inputField;

        protected override void Start()
        {
            if (inputField == null)
                inputField = GetComponent<InputField>();
            base.Start();
        }

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
