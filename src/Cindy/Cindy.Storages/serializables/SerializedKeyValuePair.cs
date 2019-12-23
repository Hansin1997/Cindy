using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages.serializables
{
    [Serializable]
    public class SerializedKeyValuePair<K,V>
    {
        [SerializeField]
        public K key;

        [SerializeField]
        public V value;

        public SerializedKeyValuePair(KeyValuePair<K,V> keyValuePair)
        {
            key = keyValuePair.Key;
            value = keyValuePair.Value;
        }

        public KeyValuePair<K,V> ToKeyValuePair()
        {
            return new KeyValuePair<K, V>(key,value);
        }

        public static IDictionary<K,V> ToDictionary(SerializedKeyValuePair<K,V>[] serializedKeyValuePairs)
        {
            IDictionary<K, V> result = new Dictionary<K, V>();
            if(serializedKeyValuePairs != null)
                foreach(SerializedKeyValuePair<K,V> serializedKeyValuePair in serializedKeyValuePairs)
                    result[serializedKeyValuePair.key] = serializedKeyValuePair.value;
            return result;
        }

        public static IDictionary<K, V> ToDictionary(K[] keys,V[] values)
        {
            IDictionary<K, V> result = new Dictionary<K, V>();
            if (keys == null || values == null)
                return result;
            int len = Math.Min(keys.Length, values.Length);
            for(int i = 0;i < len;i++)
            {
                result[keys[i]] = values[i];
            }
            return result;
        }
    }
}
