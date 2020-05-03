using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    public abstract class NumberObject<T> : VariableObject<T>
    {
        public abstract void Add(T number);

        public abstract void Sub(T number);
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/IntObject (Int)")]
    public class IntObject : NumberObject<int>
    {
        public override void Add(int number)
        {
            value += number;
        }

        public override void Sub(int number)
        {
            value -= number;
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/FloatObject (Float)")]
    public class FloatObject : NumberObject<float>
    {
        public override void Add(float number)
        {
            value += number;
        }

        public override void Sub(float number)
        {
            value -= number;
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/DoubleObject (Double)")]
    public class DoubleObject : NumberObject<double>
    {
        public override void Add(double number)
        {
            value += number;
        }

        public override void Sub(double number)
        {
            value -= number;
        }
    }
}
