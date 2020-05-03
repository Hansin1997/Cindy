using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/BoolObject (Bool)")]
    public class BoolObject : VariableObject<bool>
    {
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
