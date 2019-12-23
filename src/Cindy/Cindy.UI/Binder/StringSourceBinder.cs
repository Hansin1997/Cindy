using Cindy.Strings;

namespace Cindy.UI.Binder
{
    public abstract class StringSourceBinder<T> : AbstractBinder<StringSource,T>
    {
        protected override string GetValue(StringSource source, string key, string defaultValue = null)
        {
            return source.GetString(key, defaultValue);
        }

    }
}
