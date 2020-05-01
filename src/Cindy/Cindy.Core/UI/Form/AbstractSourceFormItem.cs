using System;
using UnityEngine;

namespace Cindy.UI.Form
{
    [Obsolete]
    public abstract class AbstractSourceFormItem<T> : AbstractFormItem
    {
        [Tooltip("Item value source, it will find from component if not set.")]
        public T Source;

        protected virtual void Start()
        {
            if (Source == null)
                Source = GetComponent<T>();
        }

        protected override string GetValue(string key)
        {
            if (Source == null)
                return null;
            return OnGetValue(Source, key);
        }

        protected abstract string OnGetValue(T source,string key);

    }
}
