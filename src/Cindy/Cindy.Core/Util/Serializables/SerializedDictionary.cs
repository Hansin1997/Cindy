using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    /// <summary>
    /// 序列化字典
    /// </summary>
    [Serializable]
    public class SerializedDictionary
    {
        public string[] paramsKey;

        [Multiline]
        public string[] paramsVal;

        public SerializedDictionary()
        {

        }

        public SerializedDictionary(Dictionary<string, string> dictonary)
        {
            if (dictonary == null)
            {
                paramsKey = new string[0];
                paramsVal = new string[0];
                return;
            }
            paramsKey = new string[dictonary.Keys.Count];
            paramsVal = new string[paramsKey.Length];
            int index = 0;
            foreach (KeyValuePair<string, string> kv in dictonary)
            {
                paramsKey[index] = kv.Key;
                paramsVal[index] = kv.Value;
                index++;
            }
        }

        public Dictionary<string, string> ToDictonary(Dictionary<string, string> dictionary = null, bool clearOldData = false)
        {
            if (dictionary == null)
                dictionary = new Dictionary<string, string>();
            else if (clearOldData)
                dictionary.Clear();
            for (int i = 0, len = Math.Min(paramsKey.Length, paramsVal.Length); i < len; i++)
            {
                if (dictionary.ContainsKey(paramsKey[i]))
                    dictionary[paramsKey[i]] = paramsVal[i];
                else
                    dictionary.Add(paramsKey[i], paramsVal[i]);
            }
            return dictionary;
        }
    }
}
