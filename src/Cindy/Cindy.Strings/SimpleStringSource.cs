using Cindy.Storages.serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "SimpleStringSource", menuName = "Cindy/StringSources/SimpleStringSource", order = 1)]
    public class SimpleStringSource : StringSource
    {
        [SerializeField]
        public KV[] strings;

        protected IDictionary<string,string> stringMap;

        public override string GetString(string key, string defaultValue = null)
        {
            if (stringMap == null)
                stringMap = KV.ToDictionary(strings);
            string result;
            if (stringMap.TryGetValue(key, out result))
                return result;
            return defaultValue;
        }

        [Serializable]
        public class KV : SerializedKeyValuePair<string, string>
        {
            public KV(KeyValuePair<string,string> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }
}
