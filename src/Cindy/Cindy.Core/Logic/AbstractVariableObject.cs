using System;

namespace Cindy.Logic
{
    /// <summary>
    /// 抽象变量物体，负责包装数据类型。
    /// </summary>
    public abstract class AbstractVariableObject : NamedBehaviour
    {
        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <returns></returns>
        public abstract object GetVariableValue();
        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="value"></param>
        public abstract void SetVariableValue(object value);
        /// <summary>
        /// 获取变量类型
        /// </summary>
        /// <returns></returns>
        public abstract Type GetValueType();
    }
}
