using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 抽象数字变量
    /// </summary>
    /// <typeparam name="T">数字类型</typeparam>
    public abstract class NumberObject<T> : VariableObject<T>
    {
        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="number"></param>
        public abstract void Add(T number);

        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="number"></param>
        public abstract void Sub(T number);
    }

    /// <summary>
    /// 整型变量
    /// </summary>
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

    /// <summary>
    /// 浮点型变量
    /// </summary>
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

    /// <summary>
    /// 双精度浮点型变量
    /// </summary>
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
