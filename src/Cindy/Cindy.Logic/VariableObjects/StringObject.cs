using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/StringObject")]
    public class StringObject : VariableObject<string>
    {
        protected override string TransformTo(string value)
        {
            return value;
        }
    }
}
