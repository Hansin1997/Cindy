using Cindy.Storages;
using UnityEngine;

namespace Cindy.Logic
{
    public abstract class VariableObject<T> : MonoBehaviour
    {
        [Header("Variable")]
        [SerializeField]
        public T value;

        [Header("Storage")]
        public AbstractStorage storage;
        public string key;
        public bool autoSave = true;

        protected object _value;

        protected virtual void Start()
        {
            if(storage != null)
            {
                string val = storage.Get(key);
                if (val != null)
                    value = TransformTo(val);
            }
            _value = value;
        }

        protected virtual void Update()
        {
            if(autoSave)
            {
                if (value != null)
                {
                    if (!value.Equals(_value))
                        Save();
                }
                else if (_value != null)
                {
                    if (!_value.Equals(value))
                        Save();
                }
            }
        }

        public void Save()
        {
            if (storage != null)
                storage.Put(key, value);
        }

        public void SetValue(T value)
        {
            this.value = value;
        }

        protected abstract T TransformTo(string value);
    }
}
