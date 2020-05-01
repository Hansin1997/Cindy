using Cindy.Strings;
using System;

namespace Cindy.UI.Binder
{
    [Obsolete]
    public abstract class StringSourceBinder<T> : ObsoletedBinder<StringSource,T>
    {
        protected override string GetValue(StringSource source, string key, string defaultValue = null)
        {
            return source.Get(key, defaultValue);
        }

    }
}
