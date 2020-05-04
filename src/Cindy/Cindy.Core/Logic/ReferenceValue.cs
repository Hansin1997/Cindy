using System;

namespace Cindy.Logic
{
    /// <summary>
    /// 引用值类型，方便变量对象值和静态值切换。
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="V">变量对象类型</typeparam>
    [Serializable]
    public class ReferenceValue<T,V> where V : VariableObject<T>
    {
        /// <summary>
        /// 引用对象
        /// </summary>
        public V reference;
        /// <summary>
        /// 静态值
        /// </summary>
        public T value;

        /// <summary>
        /// 当引用对象存在时操作其变量值，否则操作静态值。
        /// </summary>
        public T Value { get { return GetValue(); } set { SetValue(value); } }

        /// <summary>
        /// 当引用对象存在时获取其变量值，否则获取静态值。
        /// </summary>
        /// <returns>数据值</returns>
        public virtual T GetValue()
        {
            T V = reference != null && reference.Value != null ? reference.Value : value;
            return V;
        }

        /// <summary>
        /// 当引用对象存在时设置其变量值，否则设置静态值。
        /// </summary>
        /// <param name="value">数据值</param>
        public virtual void SetValue(T value)
        {
            if (reference != null)
            {
                reference.Value = value;
            }
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return Value == null;
            if(obj is ReferenceValue<T,V> a)
            {
                if (Value == null)
                    return a.Value == null;
                if (a == null)
                    return false;
                return Value.Equals(a.Value);
            }else if(obj is T b)
            {
                if (Value == null)
                    return b == null;
                return Value.Equals(b);
            }else if(obj is V c)
            {
                if (Value == null)
                    return c == null || c.Value == null;
                if (c == null)
                    return false;
                return Value.Equals(c.Value);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            if(Value == null)
                return base.GetHashCode();
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            if (Value != null)
                return Value.ToString();
            else
                return base.ToString();
        }
    }
}
