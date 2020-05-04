using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    /// <summary>
    /// 鼠标位置
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/MouseVector (Vector2)")]
    public class MouseVector : Vector2Object
    {
        [Header("MouseVector")]
        public ValueType valueType;

        public override Vector2 GetValue()
        {
            switch (valueType)
            {
                case ValueType.Position:
                    value = VirtualInput.GetMousePosition();
                    break;
                case ValueType.ScrollDelta:
                    value = VirtualInput.GetMouseScrollDelta();
                    break;
                default:
                    value = Vector2.zero;
                    break;
            }
            return base.GetValue();
        }

        public override void SetValue(Vector2 value)
        {

        }

        public enum ValueType
        {
            Position, ScrollDelta
        }
    }
}
