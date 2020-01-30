using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/ButtonCondition", 1)]
    public class ButtonCondition : Condition
    {
        public ReferenceString buttonKey;
        public ButtonType state;

        public override bool Check()
        {
            return GetValue(state, buttonKey.Value);
        }

        public static bool GetValue(ButtonType state, string buttonKey)
        {
            if (buttonKey == null || buttonKey.Trim().Length == 0)
                return false;
            switch (state)
            {
                case ButtonType.Normal:
                    return VirtualInput.GetButton(buttonKey);
                case ButtonType.Down:
                    return VirtualInput.GetButtonDown(buttonKey);
                case ButtonType.Up:
                    return VirtualInput.GetButtonUp(buttonKey);
                default:
                    return false;
            }
        }

        public enum ButtonType
        {
            Normal,
            Down,
            Up,
        }
    }
}
