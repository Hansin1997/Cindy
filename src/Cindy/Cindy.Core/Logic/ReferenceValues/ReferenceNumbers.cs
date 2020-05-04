using Cindy.Logic.VariableObjects;
using System;

namespace Cindy.Logic.ReferenceValues
{
    /// <summary>
    /// 整型应用
    /// </summary>
    [Serializable]
    public class ReferenceInt : ReferenceValue<int, IntObject>
    {

    }

    /// <summary>
    /// 浮点型引用
    /// </summary>
    [Serializable]
    public class ReferenceFloat : ReferenceValue<float, FloatObject>
    {

    }

    /// <summary>
    /// 双精度浮点型引用
    /// </summary>
    [Serializable]
    public class ReferenceDouble : ReferenceValue<double, DoubleObject>
    {

    }
}
