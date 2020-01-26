using Cindy.Logic.VariableObjects;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/DoubleCondition",0)]
    public class DoubleCondition : NumberVariableCondition<DoubleObject,double>
    {
        protected override bool Eq(double variableValue, double value)
        {
            return variableValue == value;
        }

        protected override bool Ge(double variableValue, double value)
        {
            return variableValue >= value;
        }

        protected override bool Gt(double variableValue, double value)
        {
            return variableValue > value;
        }

        protected override bool Le(double variableValue, double value)
        {
            return variableValue <= value;
        }

        protected override bool Lt(double variableValue, double value)
        {
            return variableValue < value;
        }
    }
}
