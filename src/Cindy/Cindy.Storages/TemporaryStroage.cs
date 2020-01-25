using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages
{
    [CreateAssetMenu(fileName = "TemporaryStroage", menuName = "Cindy/Storage/TemporaryStroage", order = 1)]
    public class TemporaryStroage : AbstractStorage
    {
        protected Dictionary<string, object> map;
        protected Dictionary<string, object> Map { get { return map != null ? map : (map = new Dictionary<string, object>()); } }

        public override void Clear()
        {
            Map.Clear();
        }

        public override string Get(string key)
        {
            if (Map.ContainsKey(key))
                return Map[key] as string;
            return null;
        }

        public override string[] GetMultiple(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
                return new string[0];
            string[] result = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                if (map.ContainsKey(keys[i]))
                    result[i] = map[keys[i]] as string;
            return result;
        }

        public override void LoadObjects(params IStorableObject[] storableObjects)
        {
            foreach (IStorableObject storableObject in storableObjects)
            {
                if (Map.ContainsKey(storableObject.GetStorableKey()))
                    storableObject.OnPutStorableObject(Map[storableObject.GetStorableKey()]);
            }
        }

        public override void Put(string key, object value)
        {
            Map[key] = value;
        }

        public override void PutMultiple(string[] keys, object[] values)
        {
            int len = Math.Min(keys.Length, values.Length);
            for (int i = 0; i < len; i++)
                Map[keys[i]] = values[i];
        }

        public override void PutMultiple(IDictionary<string, object> keyValuePairs)
        {
            foreach (KeyValuePair<string, object> kv in keyValuePairs)
                Map[kv.Key] = kv.Value;
        }

        public override void PutObjects(params IStorableObject[] storableObjects)
        {
            foreach (IStorableObject storableObject in storableObjects)
                Map[storableObject.GetStorableKey()] = storableObject.GetStorableObject();
        }
    }
}
