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
                if (variable.Value == null || value == null)
                    return false;
                switch(@operator)
                {
                    case Operator.Equlas:
                        return Eq(variable.Value, value);
                    case Operator.NotEqulas:
                        return !Eq(variable.Value, value);
                    case Operator.GreaterThan:
                        return Gt(variable.Value, value);
                    case Operator.GreaterThanOrEqulas:
                        return Ge(variable.Value, value);
                    case Operator.LessThan:
                        return Lt(variable.Value, value);
                    case Operator.LessThanOrEqulas:
                        return Le(variable.Value, value);
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
