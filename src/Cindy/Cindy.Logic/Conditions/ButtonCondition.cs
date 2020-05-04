using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    /// <summary>
    /// 按钮状态条件
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Conditions/ButtonCondition", 1)]
    public class ButtonCondition : Condition
    {
        /// <summary>
        /// 按钮名
        /// </summary>
        public ReferenceString buttonKey;
        /// <summary>
        /// 按钮状态
        /// </summary>
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
            /// <summary>
            /// 按钮值
            /// </summary>
            Normal,
            /// <summary>
            /// 按钮按下
            /// </summary>
            Down,
            /// <summary>
            /// 按钮放开
            /// </summary>
            Up,
        }
    }
}
