using System;

namespace Cindy.Logic
{
    public abstract class AbstractVariableObject : NamedObject
    {
        public abstract object GetVariableValue();

        public abstract void SetVariableValue(object value);

        public abstract Type GetValueType();
    }
}
