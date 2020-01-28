using System;

namespace Cindy.Logic.Conditions
{
    public abstract class NumberVariableCondition<V,T,R> : Condition where V : VariableObject<T> where R : ReferenceValue<T,V>
    {
        public V variable;
        public Operator @operator;
        public R target;

        public override bool Check()
        {
            if(variable != null)
            {
                T value = target.Value;
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
                        return Lt(variable.value, value);
                    case Operator.LessThanOrEqulas:
                        return Le(variable.value, value);
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
