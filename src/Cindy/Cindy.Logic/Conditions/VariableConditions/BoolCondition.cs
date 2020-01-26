using Cindy.Logic.VariableObjects;
using System;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/BoolCondition", 0)]
    public class BoolCondition : Condition
    {
        public BoolObject variable;
        public Operator @operator;
        public BoolObject targetVariable;
        public bool staticValue;

        public override bool Check()
        {
            if (variable == null)
                return false;
            bool value = targetVariable != null ? targetVariable.value : staticValue;

            switch (@operator)
            {
                case Operator.Equlas:
                    return variable.value == value;
                case Operator.NotEqulas:
                    return variable.value != value;
                default:
                    return false;
            }
        }


        [Serializable]
        public enum Operator
        {
            Equlas,
            NotEqulas
        }
    }
}
