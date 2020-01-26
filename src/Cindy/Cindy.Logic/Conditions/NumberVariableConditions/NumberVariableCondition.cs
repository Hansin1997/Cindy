using System;

namespace Cindy.Logic.Conditions.NumberVariableConditions
{
    public abstract class NumberVariableCondition<V,T> : Condition where V : VariableObject<T>
    {
        public V variable;
        public Operator @operator;
        public V targetValue;
        public T staticValue;

        public override bool Check()
        {
            if(variable != null)
            {
                T value = targetValue != null && targetValue.value != null ? targetValue.value : staticValue;
                if (variable.value == null || value == null)
                    return false;
                switch(@operator)
                {
                    case Operator.Equlas:
                        return Eq(variable.value, value);
                    case Operator.NotEqulas:
                        return !Eq(variable.value, value);
                    case Operator.GreaterThan:
                        return Gt(variable.value, value);
                    case Operator.GreaterThanOrEqulas:
                        return Ge(variable.value, value);
                    case Operator.LessThan:
                        return Gt(variable.value, value);
                    case Operator.LessThanOrEqulas:
                        return Eq(variable.value, value);
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected abstract bool Eq(T variableValue,T value);
        protected abstract bool Gt(T variableValue, T value);
        protected abstract bool Ge(T variableValue, T value);
        protected abstract bool Lt(T variableValue, T value);
        protected abstract bool Le(T variableValue, T value);

        [Serializable]
        public enum Operator
        {
            Equlas,
            NotEqulas,
            GreaterThan,
            GreaterThanOrEqulas,
            LessThan,
            LessThanOrEqulas
        }
    }
}
