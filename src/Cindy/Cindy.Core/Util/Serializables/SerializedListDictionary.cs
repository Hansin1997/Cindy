using System;
using System.Collections.Generic;

namespace Cindy.Util.Serializables
{
    /// <summary>
    /// 序列化列表字典
    /// </summary>
    [Serializable]
    public class SerializedListDictionary
    {
        public string[] paramsKey;
        public SerializedList[] paramsVal;

        public SerializedListDictionary()
        {

        }

        public SerializedListDictionary(Dictionary<string, List<string>> dictonary)
        {
            if (dictonary == null)
            {
                paramsKey = new string[0];
                paramsVal = new SerializedList[0];
                return;
            }
            paramsKey = new string[dictonary.Keys.Count];
            paramsVal = new SerializedList[paramsKey.Length];
            int index = 0;
            foreach (KeyValuePair<string, List<string>> kv in dictonary)
            {
                paramsKey[index] = kv.Key;
                paramsVal[index] = new SerializedList(kv.Value);
                index++;
            }
        }

        public Dictionary<string, List<string>> ToDictonary(Dictionary<string, List<string>> dictionary = null, bool clearOldData = false)
        {
            if (dictionary == null)
                dictionary = new Dictionary<string, List<string>>();
            else if (clearOldData)
                dictionary.Clear();
            for (int i = 0, len = Math.Min(paramsKey.Length, paramsVal.Length); i < len; i++)
            {
                if (dictionary.ContainsKey(paramsKey[i]))
                    dictionary[paramsKey[i]] = new List<string>(paramsVal[i].array);
                else
                    dictionary.Add(paramsKey[i], new List<string>(paramsVal[i].array));
            }
            return dictionary;
        }
    }
}
