using Cindy.Logic.ReferenceValues;
using Cindy.Storages;
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
        protected VariableObject<T> _proxyTarget, _proxyTargetOld;

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
                string val = storage.Get(key.Value);
                if (val != null && val.Length > 0)
                    OnValueLoad(TransformFrom(val));
                else
                    OnValueLoadEmpty();
            }
            if (IsValueChanged())
                OnValueChanged(false);
        }

        protected virtual void OnValueLoad(T val)
        {
            if (val != default)
                value = val;
        }

        protected virtual void OnValueLoadEmpty()
        {

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
                storage.Put(key.Value, TramsfromTo(value));
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
        protected abstract T TransformFrom(string value);

        protected virtual object TramsfromTo(T value)
        {
            return value;
        }
    }
}
