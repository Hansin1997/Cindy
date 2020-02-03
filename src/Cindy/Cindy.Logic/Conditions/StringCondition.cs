using Cindy.Logic.VariableObjects;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    public abstract class StringCondition : Condition
    {
        public StringObject stringObject;
    }


    [AddComponentMenu("Cindy/Logic/Conditions/StringNotEmptyCondition")]
    public class StringNotEmptyCondition : StringCondition
    {
        public bool trim;

        public override bool Check()
        {
            return stringObject != null && stringObject.Value != null &&
                (trim ? stringObject.Value.Trim() : stringObject.Value).Length > 0;
        }
    }
}
