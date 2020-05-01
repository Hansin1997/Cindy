using Cindy.Storages;
using System;
using UnityEngine;

namespace Cindy.UI.Binder.Sources
{
    [AddComponentMenu("Cindy/UI/Binder/DataSources/StorageDataSource")]
    public class StorageDataSource : AbstractDataSource
    {
        public AbstractStorage storage;

        public override T GetData<T>(string key, T defaultValue = default)
        {
            string temp = storage.Get(key);
            if (temp == null)
                return defaultValue;
            if (temp is T r)
                return r;
            try
            {
                return JSON.FromJson<T>(temp);
            }catch(Exception e)
            {
                Debug.LogWarning(e, this);
                return default;
            }
            
        }

        public override void SetData(string key, object value)
        {
            storage.Put(key, JSON.ToJson(value));
        }

        public override bool IsReadable()
        {
            return true;
        }

        public override bool IsWriteable()
        {
            return true;
        }
    }
}
