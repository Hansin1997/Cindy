using Cindy.Logic.ReferenceValues;
using Cindy.Storages;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    public abstract class VariableObject<T> : AbstractVariableObject,IStorableObject
    {
        [Header("Variable")]
        public string variableName;
        [SerializeField]
        protected T value;
        protected object _value;

        [Header("Proxy")]
        public Context proxyContext;
        public ReferenceString proxyTarget;
        protected VariableObject<T> _proxyTarget, _proxyTargetOld;

        [Header("Storage")]
        public AbstractStorage storage;
        public ReferenceString key;
        public bool autoSave = true;
        public bool updateFromStorage = false;
        public ExceptionEvent exceptionEvent;

        [Header("Events")]
        public UnityEvent valueChangedEvent;

        public struct ExceptionEvent
        {
            public UnityEvent<Exception> onLoadException;
            public UnityEvent<Exception> onSaveException;
        }

        public T Value { get { return GetValue(); }  set { SetValue(value); } }

        protected virtual void Start()
        {
            LoadFromStorage();
        }

        protected override string GetName()
        {
            return variableName;
        }

        public override object GetVariableValue()
        {
            return GetValue();
        }

        public override void SetVariableValue(object value)
        {
            if (value is T v)
                this.value = v;
            else if (value != null)
            {
                string json;
                if (value is string s)
                    json = s;
                else
                    json = JSON.ToJson(value);
                this.value = JSON.FromJson<T>(json);
            }
            else
                this.value = default;
        }

        public override Type GetValueType()
        {
            return typeof(T);
        }

        protected virtual void OnDestroy()
        {
            if (_proxyTarget != null)
                _proxyTarget.valueChangedEvent.RemoveListener(OnProxyValueChanged);
            if(_proxyTargetOld != null)
                _proxyTargetOld.valueChangedEvent.RemoveListener(OnProxyValueChanged);
        }

        protected virtual void LoadFromStorage()
        {
            if (storage != null)
            {
                storage.LoadObjects(this,(s,e)=>
                {
                    if (!s && e != null)
                        OnValueLoadException(e);
                }, (p) => { }, this);                    
            }
            else
            {
                OnValueLoadEmpty();
                if (IsValueChanged())
                    OnValueChanged(false);
            }
        }

        protected virtual void OnValueLoad(T val)
        {
            if (val != null)
                value = val;
        }

        protected virtual void OnValueLoadEmpty()
        {

        }

        protected virtual void OnValueLoadException(Exception e)
        {
            Debug.LogWarning(e, this);
            exceptionEvent.onLoadException.Invoke(e);
        }

        protected virtual void OnValueSaveException(Exception e)
        {
            Debug.LogWarning(e, this);
            exceptionEvent.onSaveException.Invoke(e);
        }

        protected bool IsValueChanged()
        {
            if (value == null || _value == null)
            {
                if (!(value == null && _value == null))
                    return true;
            }
            else
            {
                if (!value.Equals(_value))
                    return true;
            }
            return false;
        }

        protected virtual void Update()
        {
            if(_proxyTarget != _proxyTargetOld)
            {
                if (_proxyTargetOld != null)
                    _proxyTargetOld.valueChangedEvent.RemoveListener(OnProxyValueChanged);
                _proxyTarget.valueChangedEvent.AddListener(OnProxyValueChanged);
                _proxyTargetOld = _proxyTarget;
            }
            if(IsValueChanged())
                OnValueChanged();
            if (updateFromStorage)
                LoadFromStorage();
        }

        protected virtual void OnProxyValueChanged()
        {
            if(_proxyTarget != null)
            {
                value = _proxyTarget.value;
            }
        }

        protected virtual void OnValueChanged(bool save = true,bool notify = true)
        {
            if (autoSave && save)
                Save();
            if (valueChangedEvent != null && notify)
                valueChangedEvent.Invoke();
            _value = value;
        }

        public virtual void Save()
        {
            if (storage != null)
            {
                storage.PutObjects(this, (isSuccess, exception) =>
                {
                    if (!isSuccess)
                        OnValueSaveException(exception);
                }, (p) => { }, this);
            }
        }

        public virtual void SetValue(T value)
        {
            if (proxyContext != null)
            {
                if (_proxyTarget == null || !_proxyTarget.variableName.Equals(proxyTarget.Value))
                    _proxyTarget = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                VariableObject<T> target = _proxyTarget;
                if (target != null)
                    target.SetValue(value);
            }
            this.value = value;
        }

        public virtual T GetValue()
        {
            if(proxyContext != null)
            {
                if (_proxyTarget == null || !_proxyTarget.variableName.Equals(proxyTarget.Value))
                    _proxyTarget = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                VariableObject<T> target = _proxyTarget;
                if (target != null)
                    value = target.GetValue();
            }
            return value;
        }

        public virtual void RefreshValue()
        {
            value = GetValue();
        }

        public override string ToString()
        {
            if (value == null)
                return "NULL";
            return value.ToString();
        }

        public virtual string GetStorableKey()
        {
            return key.Value;
        }

        public virtual object GetStorableObject()
        {
            return value;
        }

        public virtual Type GetStorableObjectType()
        {
            return typeof(T);
        }

        public virtual void OnPutStorableObject(object obj)
        {
            if (obj == null)
                OnValueLoadEmpty();
            else
            {
                if (TramsformValue(obj,out T v))
                {
                    OnValueLoad(v);
                }
                else
                {
                    Debug.LogWarning(string.Format("obj({0}) can not tramsfrom to {1}", obj.GetType().Name, typeof(T).Name));
                }
            }
            if (IsValueChanged())
                OnValueChanged(false);
        }

        protected virtual bool TramsformValue(object obj,out T output)
        {
            if (obj is T v)
            {
                output = v;
                return true;
            }
            output = default;
            return false;
        }
    }
}
