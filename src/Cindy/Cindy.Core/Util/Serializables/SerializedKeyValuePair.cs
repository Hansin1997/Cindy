using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    [Serializable]
    public class SerializedKeyValuePair<K,V>
    {
        [SerializeField]
        public K Key;

        [SerializeField]
        public V Value;

        public SerializedKeyValuePair()
        {

        }

        public SerializedKeyValuePair(KeyValuePair<K,V> keyValuePair)
        {
            Key = keyValuePair.Key;
            Value = keyValuePair.Value;
        }

        public KeyValuePair<K,V> ToKeyValuePair()
        {
            return new KeyValuePair<K, V>(Key,Value);
        }

        public static IDictionary<K,V> ToDictionary(SerializedKeyValuePair<K,V>[] serializedKeyValuePairs)
        {
            IDictionary<K, V> result = new Dictionary<K, V>();
            if(serializedKeyValuePairs != null)
                foreach(SerializedKeyValuePair<K,V> serializedKeyValuePair in serializedKeyValuePairs)
                    result[serializedKeyValuePair.Key] = serializedKeyValuePair.Value;
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
