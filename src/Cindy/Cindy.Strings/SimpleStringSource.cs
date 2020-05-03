using Cindy.Util.Serializables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "SimpleStringSource", menuName = "Cindy/StringSources/SimpleStringSource", order = 1)]
    public class SimpleStringSource : StringSource
    {
        [SerializeField]
        public KV[] strings;

        protected IDictionary<string, string> stringMap;

        public override IEnumerator DoGet(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction)
        {
            try
            {
                if (stringMap == null)
                    stringMap = KV.ToDictionary(strings);
                if (stringMap.TryGetValue(key, out string result))
                    resultAction(result, null, true);
                else
                    resultAction(null, null, false);
            }catch(Exception e)
            {
                resultAction(null, e, false);
            }
            yield return null;
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