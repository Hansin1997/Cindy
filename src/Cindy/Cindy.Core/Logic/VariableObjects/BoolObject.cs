using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 布尔型变量对象
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/BoolObject (Bool)")]
    public class BoolObject : VariableObject<bool>
    {
        /// <summary>
        /// 取反
        /// </summary>
        public void Invert()
        {
            value = !value;
        }

        public override string ToString()
        {
            return value ? "true" : "false";
        }
    }
}
