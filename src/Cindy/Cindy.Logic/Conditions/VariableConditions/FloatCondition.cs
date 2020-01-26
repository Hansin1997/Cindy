using Cindy.Logic.VariableObjects;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/FloatCondition", 0)]
    public class FloatCondition : NumberVariableCondition<FloatObject,float>
    {
        protected override bool Eq(float variableValue, float value)
        {
            return variableValue == value;
        }

        protected override bool Ge(float variableValue, float value)
        {
            return variableValue >= value;
        }

        protected override bool Gt(float variableValue, float value)
        {
            return variableValue > value;
        }

        protected override bool Le(float variableValue, float value)
        {
            return variableValue <= value;
        }

        protected override bool Lt(float variableValue, float value)
        {
            return variableValue < value;
        }
    }
}
