using System;
using System.Collections.Generic;
using UnityEngine;
using Cindy.serializables;

namespace Cindy.Storages
{
    [CreateAssetMenu(fileName = "Storage", menuName = "Cindy/Storage/PlayerPrefsStorage", order = 1)]
    public class PlayerPrefsStorage : AbstractStorage
    {
        public string prefix = "PREFIX_";

        protected const string keysKey = "#KEYS#";

        protected IList<string> GetKeys()
        {
            IList<string> result = null;
            string json = PlayerPrefs.GetString(prefix + keysKey);
            if (json != null)
            {
                SerializedList serializedList = JsonUtility.FromJson<SerializedList>(json);
                if (serializedList != null)
                    result = serializedList.ToList();
            }
            if (result != null)
                return result;
            return new List<string>();
        }

        protected void SetKeys(IList<string> keys)
        {
            if (keys == null || keys.Count == 0)
                PlayerPrefs.DeleteKey(prefix + keysKey);
            else
            {
                PlayerPrefs.SetString(prefix + keysKey, JsonUtility.ToJson(new SerializedList(keys)));
            }
                
        }

        public override void Clear()
        {
            IList<string> keys = GetKeys();
            foreach(string key in keys)
            {
                PlayerPrefs.DeleteKey(prefix + key);
            }
            PlayerPrefs.Save();
        }

        public override string Get(string key)
        {
            return PlayerPrefs.GetString(prefix + key);
        }

        public override string[] GetMultiple(params string[] keys)
        {
            string[] results = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                results[i] = PlayerPrefs.GetString(prefix + keys[i]);
            return results;
        }

        public override void LoadObjects(params IStorableObject[] storableObjects)
        {
            foreach (IStorableObject storableObject in storableObjects)
            {
                string json = PlayerPrefs.GetString(prefix + storableObject.GetStorableKey());
                if (json == null)
                    continue;
                storableObject.OnPutStorableObject(JsonUtility.FromJson(json, storableObject.GetStorableObjectType()));
            }
        }

        public override void Put(string key, object value)
        {
            IList<string> keys = GetKeys();
            if (value == null)
            {
                PlayerPrefs.DeleteKey(prefix + key);
                keys.Remove(key);
            }
            else
            {
                PlayerPrefs.SetString(prefix + key, value.ToString());
                if (!keys.Contains(key))
                    keys.Add(key);
            }
            SetKeys(keys);
            PlayerPrefs.Save();
        }

        public override void PutMultiple(string[] keys, object[] values)
        {
            IList<string> _keys = GetKeys();
            int len = Math.Min(keys.Length, values.Length);
            for (int i = 0; i < len; i++)
            {
                if (values[i] == null)
                {
                    PlayerPrefs.DeleteKey(prefix + keys[i]);
                    _keys.Remove(keys[i]);
                }
                else
                {
                    PlayerPrefs.SetString(prefix + keys[i], values[i].ToString());
                    if (!_keys.Contains(keys[i]))
                        _keys.Add(keys[i]);
                }
            }
            SetKeys(_keys);
            PlayerPrefs.Save();
        }

        public override void PutMultiple(IDictionary<string, object> keyValuePairs)
        {
            IList<string> keys = GetKeys();
            foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
            {
                if (keyValuePair.Value == null)
                {
                    PlayerPrefs.DeleteKey(prefix + keyValuePair.Key);
                    keys.Remove(keyValuePair.Key);
                }
                else
                {
                    PlayerPrefs.SetString(prefix + keyValuePair.Key, keyValuePair.Value.ToString());
                    if (!keys.Contains(keyValuePair.Key))
                        keys.Add(keyValuePair.Key);
                }
            }
            SetKeys(keys);
            PlayerPrefs.Save();
        }

        public override void PutObjects(params IStorableObject[] storableObjects)
        {
            IList<string> keys = GetKeys();
            foreach (IStorableObject storableObject in storableObjects)
            {
                string key = storableObject.GetStorableKey(), value = JsonUtility.ToJson(storableObject.GetStorableObject());
                PlayerPrefs.SetString(prefix + key, value);
               
                if (!keys.Contains(key))
                    keys.Add(key);
            }
            SetKeys(keys);
            PlayerPrefs.Save();
        }
    }
}
