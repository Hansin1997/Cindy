using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/ButtonCondition", 1)]
    public class ButtonCondition : Condition
    {
        public ReferenceString buttonKey;
        public Type state;

        public override bool Check()
        {
            if (buttonKey == null || buttonKey.Value.Trim().Length == 0)
                return false;
            switch (state)
            {
                case Type.Normal:
                    return VirtualInput.GetButton(buttonKey.Value);
                case Type.Down:
                    return VirtualInput.GetButtonDown(buttonKey.Value);
                case Type.Up:
                    return VirtualInput.GetButtonUp(buttonKey.Value);
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
