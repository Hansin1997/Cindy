using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "StringSourcesSwitch", menuName = "Cindy/StringSources/StringSourcesSwitch", order = 99)]
    public class StringSourceSwitch : StringSource
    {
        public string defaultKey = "",currentKey = "";

        public string DefaultKey { get { return defaultKey; } set { defaultKey = value; } }
        public string CurrentKey { get { return currentKey; } set { currentKey = value; } }

        [SerializeField]
        public KV[] sourcesMap;
        protected IDictionary<string, StringSource> sources;

        public override string Get(string key, string defaultValue = null)
        {
            if (sources == null)
                sources = KV.ToDictionary(sourcesMap);
            string result;
            if (sources.ContainsKey(currentKey) && (result = sources[currentKey].Get(key, null)) != null)
                return result;
            if(sources.ContainsKey(defaultKey) && (result = sources[defaultKey].Get(key, null)) != null)
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
