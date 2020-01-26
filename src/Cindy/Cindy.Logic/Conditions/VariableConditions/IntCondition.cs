using Cindy.Logic.VariableObjects;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/IntCondition", 0)]
    public class IntCondition : NumberVariableCondition<IntObject,int>
    {
        protected override bool Eq(int variableValue, int value)
        {
            return variableValue == value;
        }

        protected override bool Ge(int variableValue, int value)
        {
            return variableValue >= value;
        }

        protected override bool Gt(int variableValue, int value)
        {
            return variableValue > value;
        }

        protected override bool Le(int variableValue, int value)
        {
            return variableValue <= value;
        }

        protected override bool Lt(int variableValue, int value)
        {
            return variableValue < value;
        }
    }
}
