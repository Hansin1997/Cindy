using Cindy.Strings;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Pages
{
    public class Dialog : Page
    {
        private Dictionary<string, Text> _map;

        public virtual Dictionary<string,Text> Texts
        {
            get
            {
                if (_map == null)
                    _map = new Dictionary<string, Text>();
                else
                    _map.Clear();
                Text[] arr = GetComponentsInChildren<Text>();
                foreach(Text text in arr)
                {
                    _map[text.gameObject.name] = text;
                }
                return _map;
            }
        }

        public List<DialogStruct> queue;

        public virtual void Enqueue(DialogStruct data)
        {
            queue.Add(data);
        }

        public virtual void Enqueue(DialogObject dialogObject)
        {
            Enqueue(dialogObject.data);
        }

        public virtual void Enqueue(DialogAsset dialogAsset)
        {
            Enqueue(dialogAsset.data);
        }

        public virtual void Dispose()
        {
            if (!HasMore())
                return;
            DialogStruct data = queue[0];
            queue.RemoveAt(0);
            Dictionary<string, Text> map = Texts;
            foreach (KeyValuePair<string,string> kv in data.ComputedData)
            {
                if (map.ContainsKey(kv.Key))
                    map[kv.Key].text = kv.Value;
            }
        }

        public virtual bool HasMore()
        {
            return queue != null && queue.Count > 0;
        }
    }

    [Serializable]
    public class DialogStruct
    {
        public StringMap[] stringMap;
        public StringSource source;

        public virtual Dictionary<string, string> ComputedData
        {
            get
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                if (source != null)
                {
                    foreach (SerializedKeyValuePair<string, string> kv in stringMap)
                        result[kv.key] = source.GetString(kv.value, kv.value);
                }
                else
                {
                    foreach (SerializedKeyValuePair<string, string> kv in stringMap)
                        result[kv.key] = kv.value;
                }
                return result;
            }
        }

        [Serializable]
        public class StringMap : SerializedKeyValuePair<string,string>
        {
            public StringMap(KeyValuePair<string,string> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }

    public class DialogObject : MonoBehaviour
    {
        public DialogStruct data;
    }

    public class DialogAsset : ScriptableObject
    {
        public DialogStruct data;
    }
}
