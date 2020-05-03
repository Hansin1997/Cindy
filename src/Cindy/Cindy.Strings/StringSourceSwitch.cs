using Cindy.Util.Serializables;
using System;
using System.Collections;
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

        public override IEnumerator DoGet(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction)
        {
            try
            {
                if (sources == null)
                    sources = KV.ToDictionary(sourcesMap);
                if (sources.ContainsKey(currentKey))
                {
                    sources[currentKey].Get(key, context, (result, excetion, isSuccess) =>
                    {
                        if (isSuccess)
                        {
                            resultAction(result, excetion, true);
                        }
                        else
                        {
                            if (sources[defaultKey] != null)
                                sources[defaultKey].Get(key, context, resultAction);
                            else
                                resultAction(null, null, false);
                        }
                    });
                }
                else
                {
                    if (sources[defaultKey] != null)
                        sources[defaultKey].Get(key, context, resultAction);
                    else
                        resultAction(null, null, false);
                }
            }
            catch (Exception e)
            {
                resultAction(null, e, false);
            }
            yield return null;
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