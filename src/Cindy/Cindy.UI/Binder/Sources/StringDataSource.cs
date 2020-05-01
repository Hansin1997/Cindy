using Cindy.Strings;
using UnityEngine;

namespace Cindy.UI.Binder
{
    [AddComponentMenu("Cindy/UI/Binder/DataSources/StringDataSource")]
    public class StringDataSource : AbstractDataSource
    {
        public StringSource stringSource;

        public override T GetData<T>(string key, T defaultValue = default)
        {
            string temp = stringSource.Get(key);
            if (temp == null)
                return defaultValue;
            if (temp is T r)
                return r;
            return JSON.FromJson<T>(temp);
        }

        public override void SetData(string key, object value)
        {
            Debug.LogWarning("StringDataSource is readonly", this);
        }

        public override bool IsReadable()
        {
            return true;
        }

        public override bool IsWriteable()
        {
            return false;
        }
    }
}
