using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using Cindy.Strings;
using Cindy.Util;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Cindy.UI.Pages.DialogStruct;

namespace Cindy.UI.Pages
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/UI/Dialog/Dialog(Page)")]
    public class Dialog : Page
    {
        private Dictionary<string, Text> _map;

        public StringSource source;

        public RectTransform ButtonGroup;

        protected DialogStruct last;
        protected List<Button> buttons;

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

        public virtual void Enqueue(DialogStruct[] data)
        {
            queue.AddRange(data);
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
            DoDispose();
        }

        protected virtual void DoDispose()
        {
            if (buttons == null)
                buttons = new List<Button>();
            foreach (Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
            if (last != null)
                last.events.Invoke();
            if (!HasMore())
                return;
            DialogStruct data = queue[0];
            last = data;
            queue.RemoveAt(0);
            Dictionary<string, Text> map = Texts;
            Dictionary<string, string> sm = ComputedData(data);
            foreach (KeyValuePair<string, string> kv in sm)
            {
                if (map.ContainsKey(kv.Key))
                    map[kv.Key].text = kv.Value;
            }
            foreach (ButtonMap btnMap in data.buttonMap)
            {
                Button btn = Instantiate<Button>(btnMap.key, ButtonGroup);
                buttons.Add(btn);
                btn.onClick.AddListener(() => { btnMap.value.Invoke(); });
            }
        }

        public override void OnPageFinish()
        {
            if (last != null)
                last.events.Invoke();
        }

        public virtual Dictionary<string, string> ComputedData(DialogStruct data)
        {

            Dictionary<string, string> result = new Dictionary<string, string>();
            if (source != null)
            {
                foreach (SerializedKeyValuePair<string, string> kv in data.stringMap)
                    result[kv.key] = source.Get(kv.value, kv.value);
            }
            else
            {
                foreach (SerializedKeyValuePair<string, string> kv in data.stringMap)
                    result[kv.key] = kv.value;
            }
            return result;
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
        public ButtonMap[] buttonMap;
        public UnityEvent events;

        [Serializable]
        public class StringMap : SerializedKeyValuePair<string,string>
        {
            public StringMap(KeyValuePair<string,string> keyValuePair) : base(keyValuePair)
            {

            }
        }

        [Serializable]
        public class ButtonMap : SerializedKeyValuePair<Button,UnityEvent>
        {
            public ButtonMap(KeyValuePair<Button, UnityEvent> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }

    [AddComponentMenu("Cindy/UI/Dialog/DialogObject")]
    public class DialogObject : MonoBehaviour
    {
        public DialogStruct[] data;
    }

    [CreateAssetMenu(fileName = "DialogAsset", menuName = "Cindy/UI/Dialog/DialogAsset", order = 1)]
    public class DialogAsset : ScriptableObject
    {
        public DialogStruct[] data;
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
        public bool autoDispose = true;

        protected override void Run()
        {
            if (dialog == null)
                return;
            Dialog instance = null;
            if (singleton)
            {
                instance = Finder.Find<Dialog>(dialog.name);
            }
            if (instance == null)
            {
                instance = dialog.ShowAndReturn<Dialog>();
            }
            foreach (DialogObject dialogObject in dialogObjects)
                instance.Enqueue(dialogObject);
            if (autoDispose)
                instance.Dispose();
        }
    }
    [AddComponentMenu("Cindy/UI/Dialog/DialogAssetShower")]
    public class DialogAssetShower : LogicNode
    {
        public Dialog dialog;
        public bool singleton = true;
        public DialogAsset[] dialogAssets;
        public bool autoDispose = true;

        protected override void Run()
        {
            if (dialog == null)
                return;
            Dialog instance = null;
            if (singleton)
            {
                instance = Finder.Find<Dialog>(dialog.name);
            }
            if (instance == null)
            {
                instance = dialog.ShowAndReturn<Dialog>();
            }
            foreach (DialogAsset dialogAsset in dialogAssets)
                instance.Enqueue(dialogAsset);
            if (autoDispose)
                instance.Dispose();
        }
    }
}
