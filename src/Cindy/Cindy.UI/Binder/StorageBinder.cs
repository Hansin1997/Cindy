using Cindy.Storages;

namespace Cindy.UI.Binder
{
    public abstract class StorageBinder<T> : AbstractBinder<AbstractStorage, T>
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
