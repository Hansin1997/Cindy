using Cindy.Storages;
using UnityEngine;

namespace Cindy.Strings
{

    [CreateAssetMenu(fileName = "StorageStringSource", menuName = "Cindy/StringSources/StorageStringSource", order = 1)]
    public class StorageStringSource : StringSource
    {
        public AbstractStorage storage;
        public override string GetString(string key, string defaultValue = null)
        {
            if (storage == null)
                return defaultValue;
            string result = storage.Get(key);
            return result != null ? result : defaultValue;
        }
    }
}
