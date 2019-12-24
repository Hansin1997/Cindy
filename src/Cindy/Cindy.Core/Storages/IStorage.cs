using System.Collections.Generic;

namespace Cindy.Storages
{
    public interface IStorage
    {
        string Get(string key);

        void Put(string key, object value);

        string[] GetMultiple(params string[] keys);

        void PutMultiple(string[] keys,object[] values);

        void PutMultiple(IDictionary<string, object> keyValuePairs);

        void Clear();
        
    }
}
