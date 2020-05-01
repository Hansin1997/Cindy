using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "StringSourceProcesser", menuName = "Cindy/StringSources/StringSourceProcesser", order = 99)]
    public class StringSourceProcesser : StringSource
    {

        public StringSource originStringSource;

        public KV[] parameterSources;

        protected Processer processer;
        public Processer Processer
        {
            get
            {
                if(processer == null)
                {
                    processer = new Processer();
                    if(parameterSources != null)
                        foreach(KV kv in parameterSources)
                        {
                            if(kv.Key == null || kv.Key.Length != 2)
                            {
                                Debug.LogError("StringSourceProcesser Error: Parameter sources key must be two char.");
                                continue;
                            }
                            if (kv.Value == null)
                                continue;
                            processer.AddHandler((stringKey) => kv.Value.Get(stringKey), kv.Key[0], kv.Key[1]);
                        }
                }

                return processer;
            }
        }

        public override string Get(string key, string defaultValue = default)
        {
            if (originStringSource == null)
                return defaultValue;
            string result = originStringSource.Get(key, defaultValue);
            if (result != null)
                return Processer.GetString(result);
            return defaultValue;
        }

        public string Process(string text)
        {
            return Processer.GetString(text);
        }

        [Serializable]
        public class KV : SerializedKeyValuePair<string,StringSource>
        {
            public KV(KeyValuePair<string, StringSource> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }
}
