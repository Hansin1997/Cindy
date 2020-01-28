using Cindy.Logic.ReferenceValues;
using Cindy.Storages;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    public abstract class VariableObject<T> : MonoBehaviour
    {
        [Header("Variable")]
        [SerializeField]
        public T value;

        [Header("Storage")]
        public AbstractStorage storage;
        public ReferenceString key;
        public bool autoSave = true;

        [Header("Events")]
        public UnityEvent valueChangedEvent;

        protected object _value;

        protected virtual void Start()
        {
            if(storage != null)
            {
                string val = storage.Get(key.Value);
                if (val != null)
                    value = TransformTo(val);
            }
            _value = value;
        }

        protected virtual void Update()
        {
            if(value == null || _value == null)
            {
                if(!(value == null && _value == null))
                    OnValueChanged();
            }
            else
            {
                if (!value.Equals(_value))
                    OnValueChanged();
            }
        }

        protected virtual void OnValueChanged()
        {
            if (autoSave)
                Save();
            if (valueChangedEvent != null)
                valueChangedEvent.Invoke();
        }

        public virtual void Save()
        {
            if (storage != null)
            {
                storage.Put(key.Value, value);
                _value = value;
            }
        }

        public void SetValue(T value)
        {
            this.value = value;
        }

        protected abstract T TransformTo(string value);
    }
}
