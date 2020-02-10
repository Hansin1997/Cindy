using Cindy.Logic.ReferenceValues;
using Cindy.Storages;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    public abstract class VariableObject<T> : MonoBehaviour
    {
        [Header("Variable")]
        public string variableName;
        [SerializeField]
        protected T value;

        [Header("Proxy")]
        public Context proxyContext;
        public ReferenceString proxyTarget;

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
                if (val != null && val.Length > 0)
                    OnValueLoad(TransformTo(val));
                else
                    OnValueLoadEmpty();
            }
            _value = value;
        }

        protected virtual void OnValueLoad(T val)
        {
            if (val != default)
                value = val;
        }

        protected virtual void OnValueLoadEmpty()
        {

        }

        protected virtual void Update()
        {
            if(proxyContext != null)
            {
                VariableObject<T> target = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                if (target != null)
                    value = target.GetValue();
            }
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
            if (proxyContext != null)
            {
                VariableObject<T> target = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                if (target != null)
                    target.SetValue(value);
            }
            this.value = value;
        }

        public virtual T GetValue()
        {
            if(proxyContext != null)
            {
                VariableObject<T> target = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                if (target != null)
                    value = target.GetValue();
            }
            return value;
        }
        protected abstract T TransformTo(string value);
    }
}
