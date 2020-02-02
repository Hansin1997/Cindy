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
        protected T value;

        [Header("Storage")]
        public AbstractStorage storage;
        public ReferenceString key;
        public bool autoSave = true;
        public bool updateFromStorage = false;

        [Header("Events")]
        public UnityEvent valueChangedEvent;

        protected object _value;

        public T Value { get { return GetValue(); }  set { SetValue(value); } }

        protected virtual void Start()
        {
            LoadFromStorage();
        }

        protected virtual void LoadFromStorage()
        {
            if (storage != null)
            {
                string val = storage.Get(key.Value);
                if (val != null)
                    OnValueLoad(TransformTo(val));
                else
                    OnValueLoad(default);
            }
            _value = value;
        }

        protected virtual void OnValueLoad(T val)
        {
            if (val != default)
                value = val;
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
            if (updateFromStorage)
                LoadFromStorage();
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

        public virtual void SetValue(T value)
        {
            this.value = value;
        }

        public virtual T GetValue()
        {
            return value;
        }
        protected abstract T TransformTo(string value);

    }
}
