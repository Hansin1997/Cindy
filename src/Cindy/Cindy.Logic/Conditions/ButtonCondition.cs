using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/ButtonCondition", 1)]
    public class ButtonCondition : Condition
    {
        public string buttonKey;
        public Type state;

        public override bool Check()
        {
            if (buttonKey == null || buttonKey.Trim().Length == 0)
                return false;
            switch (state)
            {
                case Type.Normal:
                    return VirtualInput.GetButton(buttonKey);
                case Type.Down:
                    return VirtualInput.GetButtonDown(buttonKey);
                case Type.Up:
                    return VirtualInput.GetButtonUp(buttonKey);
                default:
                    return false;
            }
        }

        public enum Type
        {
            Normal,
            Down,
            Up,
        }
    }
}
