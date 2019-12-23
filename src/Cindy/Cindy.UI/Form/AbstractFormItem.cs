using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Form
{
    public abstract class AbstractFormItem : MonoBehaviour
    {
        [Header("Form Item")]
        [Tooltip("Item key will be target's gameobject name if not set.")]
        public string Key;
        public string Value
        {
            get { return GetValue(Key); }
        }
        public KeyValuePair<string, string> KeyValuePair
        {
            get
            {
                return new KeyValuePair<string, string>(Key, Value);
            }
        }

        protected abstract string GetValue(string key);
    }
}
