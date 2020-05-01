using Cindy.Storages;
using System;

namespace Cindy.UI.Binder
{
    [Obsolete]
    public abstract class StorageBinder<T> : ObsoletedBinder<AbstractStorage, T>
    {
        protected override string GetValue(AbstractStorage source, string key, string defaultValue = null)
        {
            string result;
            if ((result = source.Get(key)) != null)
                return result;
            return defaultValue;
        }

    }
}
