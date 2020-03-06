using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/BoolObject")]
    public class BoolObject : VariableObject<bool>
    {
        protected override bool TransformFrom(string value)
        {
            bool result;
            if (bool.TryParse(value, out result))
                return result;
            return false;
        }

        public void Invert()
        {
            value = !value;
        }
    }
}
