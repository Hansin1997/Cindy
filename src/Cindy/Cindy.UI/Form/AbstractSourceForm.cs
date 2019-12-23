using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Form
{
    public abstract class AbstractSourceForm : MonoBehaviour
    {
        [Header("Storage")]
        [Tooltip("targets who has form item need to save.")]
        public List<AbstractFormItem> items;
        public List<GameObject> scanTargets;
        public bool scanSelf = true;

        protected virtual void Scan()
        {
            if (scanSelf && !scanTargets.Contains(gameObject))
            {
                scanTargets.Add(gameObject);
            }

            foreach (GameObject gameObject in scanTargets)
            {
                AbstractFormItem[] tmp = gameObject.GetComponentsInChildren<AbstractFormItem>();
                foreach (AbstractFormItem item in tmp)
                {
                    if (!items.Contains(item))
                        items.Add(item);
                }
            }
        }

        public virtual IDictionary<string, object> GetForm()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            Scan();
            foreach(AbstractFormItem item in items) {
                result[item.Key] = item.Value;
            }
            return result;
        }

        public virtual bool Submit()
        {
            return DoSubmit(GetForm());
        }

        public void SubmitNoReturn()
        {
            Submit();
        }

        protected abstract bool DoSubmit(IDictionary<string, object> form);
    }
}
