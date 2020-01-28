using Cindy.Logic.ReferenceValues;
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
        public ReferenceBool target;

        public override bool Check()
        {
            if (variable == null)
                return false;
            bool value = target.Value;

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
