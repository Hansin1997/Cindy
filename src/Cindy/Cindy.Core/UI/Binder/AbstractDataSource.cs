using UnityEngine;

namespace Cindy.UI.Binder
{
    public abstract class AbstractDataSource : MonoBehaviour, IDataSource
    {
        public abstract T GetData<T>(string key, T defaultValue = default);
        public abstract void SetData(string key, object value);
        public abstract bool IsReadable();
        public abstract bool IsWriteable();
    }
}
