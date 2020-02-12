using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using Cindy.Strings;
using Cindy.Util;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Pages
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/UI/Dialog/Dialog(Page)")]
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

    [AddComponentMenu("Cindy/UI/Dialog/DialogObject")]
    public class DialogObject : MonoBehaviour
    {
        public DialogStruct data;
    }

    [CreateAssetMenu(fileName = "DialogAsset", menuName = "Cindy/UI/Dialog/DialogAsset", order = 1)]
    public class DialogAsset : ScriptableObject
    {
        public DialogStruct data;
    }

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Dialog))]
    [AddComponentMenu("Cindy/UI/Dialog/DialogQueueLength")]
    public class DialogQueueLength : IntObject
    {
        public ReferenceString dialogName = new ReferenceString() { value = "Dialog" };
        protected Dialog dialog;

        public override int GetValue()
        {
            if (dialog == null)
                dialog = Finder.Find<Dialog>(dialogName.Value);
            value = dialog != null && dialog.queue != null ? dialog.queue.Count : 0;
            return base.GetValue();
        }

        public override void SetValue(int value)
        {

        }
    }

    [AddComponentMenu("Cindy/UI/Dialog/DialogNotEmptyCondition")]
    public class DialogNotEmptyCondition : Condition
    {
        public ReferenceString dialogName = new ReferenceString() { value = "Dialog" };
        protected Dialog dialog;

        public override bool Check()
        {
            if (dialog == null)
                dialog = Finder.Find<Dialog>(dialogName.Value);
            if (dialog == null)
                return false;
            return dialog.HasMore();
        }
    }

    [AddComponentMenu("Cindy/UI/Dialog/DialogObjectShower")]
    public class DialogObjectShower : LogicNode
    {
        public Dialog dialog;
        public bool singleton = true;
        public DialogObject[] dialogObjects;

        protected override void Run()
        {
            if (dialog == null)
                return;
            Dialog instance = null;
            if (singleton)
            {
                instance = FindObjectOfType(dialog.GetType()) as Dialog;
            }
            if (instance == null)
            {
                instance = dialog.ShowAndReturn<Dialog>();
            }
            foreach (DialogObject dialogObject in dialogObjects)
                instance.Enqueue(dialogObject);
        }
    }
    [AddComponentMenu("Cindy/UI/Dialog/DialogAssetShower")]
    public class DialogAssetShower : LogicNode
    {
        public Dialog dialog;
        public bool singleton = true;
        public DialogAsset[] dialogAssets;

        protected override void Run()
        {
            if (dialog == null)
                return;
            Dialog instance = null;
            if (singleton)
            {
                instance = FindObjectOfType(dialog.GetType()) as Dialog;
            }
            if (instance == null)
            {
                instance = dialog.ShowAndReturn<Dialog>();
            }
            foreach (DialogAsset dialogAsset in dialogAssets)
                instance.Enqueue(dialogAsset);
        }
    }
}
