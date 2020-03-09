using System;

namespace Cindy.Logic
{
    [Serializable]
    public class ReferenceValue<T,V> where V : VariableObject<T>
    {
        public V reference;
        public T value;

        public T Value { get { return GetValue(); } set { SetValue(value); } }

        public T GetValue()
        {
            return reference != null && reference.Value != null ? reference.Value : value;
        }

        public void SetValue(T value)
        {
            if (reference != null) { 
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
    }
}
