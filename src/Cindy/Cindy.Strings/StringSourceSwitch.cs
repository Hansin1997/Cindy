using Cindy.Storages.serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "StringSourcesSwitch", menuName = "Cindy/StringSources/StringSourcesSwitch", order = 99)]
    public class StringSourceSwitch : StringSource
    {
        public string defaultKey = "",currentKey = "";
        [SerializeField]
        public KV[] sourcesMap;
        protected IDictionary<string, StringSource> sources;

        public override string GetString(string key, string defaultValue = null)
        {
            if (sources == null)
                sources = KV.ToDictionary(sourcesMap);
            string result;
            if (sources.ContainsKey(currentKey) && (result = sources[currentKey].GetString(key, null)) != null)
                return result;
            if(sources.ContainsKey(defaultKey) && (result = sources[defaultKey].GetString(key, null)) != null)
                return result;
            return defaultValue;
        }

        [Serializable]
        public class KV : SerializedKeyValuePair<string, StringSource>
        {
            public KV(KeyValuePair<string,StringSource> keyValuePair) : base(keyValuePair)
            {

            }

            
        }
    }
}
