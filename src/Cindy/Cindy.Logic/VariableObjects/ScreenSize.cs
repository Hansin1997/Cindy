using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 屏幕大小
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Util/ScreenSize (Vector2)")]
    public class ScreenSize : Vector2Object
    {
        protected override void LoadFromStorage()
        {

        }

        public override void Save()
        {

        }

        public override Vector2 GetValue()
        {
            value = new Vector2(Screen.width,Screen.height);
            return base.GetValue();
        }

        public override void SetValue(Vector2 value)
        {

        }
    }
}
