using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    public abstract class NumberObject<T> : VariableObject<T>
    {
        public abstract void Add(T number);

        public abstract void Sub(T number);
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/IntObject")]
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

        protected override int TransformFrom(string value)
        {
            int result;
            if (int.TryParse(value, out result))
                return result;
            return 0;
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/FloatObject")]
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
        protected override float TransformFrom(string value)
        {
            float result;
            if (float.TryParse(value, out result))
                return result;
            return 0;
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/DoubleObject")]
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

        protected override double TransformFrom(string value)
        {
            double result;
            if (double.TryParse(value, out result))
                return result;
            return 0;
        }
    }
}
