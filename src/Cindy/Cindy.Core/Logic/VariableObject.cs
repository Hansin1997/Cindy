using Cindy.Logic.ReferenceValues;
using Cindy.Storages;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    /// <summary>
    /// 变量对象类，包装数据类型成MonoBehaviour。
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public abstract class VariableObject<T> : AbstractVariableObject,IStorableObject
    {
        /// <summary>
        /// 变量名
        /// </summary>
        [Header("Variable")]
        public string variableName;
        /// <summary>
        /// 变量值
        /// </summary>
        [SerializeField]
        protected T value;

        /// <summary>
        /// 代理上下文，当字段不为空时此对象为变量代理。
        /// </summary>
        [Header("Proxy")]
        public Context proxyContext;
        /// <summary>
        /// 被代理的变量名。
        /// </summary>
        public ReferenceString proxyTarget;

        /// <summary>
        /// 存储器，当字段不为空时将从存储器获取初始值
        /// </summary>
        [Header("Storage")]
        public AbstractStorage storage;
        /// <summary>
        /// 存储器键名
        /// </summary>
        public ReferenceString key;
        /// <summary>
        /// 是否在数据更改时存储
        /// </summary>
        public bool autoSave = true;
        /// <summary>
        /// 是否在每一帧从存储库获取最新值
        /// </summary>
        public bool updateFromStorage = false;
        /// <summary>
        /// 存储异常处理出口
        /// </summary>
        public ExceptionEvent exceptionEvent;

        /// <summary>
        /// 值更改事件
        /// </summary>
        [Header("Events")]
        public UnityEvent valueChangedEvent;

        protected object _value; // 旧变量值
        protected VariableObject<T> _proxyTarget, _proxyTargetOld; // 当前代理对象和旧的代理对象

        /// <summary>
        /// 存储异常出口
        /// </summary>
        public struct ExceptionEvent
        {
            public UnityEvent<Exception> onLoadException;
            public UnityEvent<Exception> onSaveException;
        }

        /// <summary>
        /// 变量值
        /// </summary>
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

        /// <summary>
        /// 从存储器加载数据
        /// </summary>
        protected virtual void LoadFromStorage()
        {
            if (storage != null)
            {
                storage.RestoreObjects(this,(s,e)=>
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

        /// <summary>
        /// 当从存储器加载值完毕后
        /// </summary>
        /// <param name="val">加载的数据值</param>
        protected virtual void OnValueLoad(T val)
        {
            if (val != null)
                value = val;
        }

        /// <summary>
        /// 当从存储器加载到空值
        /// </summary>
        protected virtual void OnValueLoadEmpty()
        {

        }

        /// <summary>
        /// 当从存储器加载时发生异常
        /// </summary>
        /// <param name="e">异常对象</param>
        protected virtual void OnValueLoadException(Exception e)
        {
            Debug.LogWarning(e, this);
            exceptionEvent.onLoadException.Invoke(e);
        }

        /// <summary>
        /// 当存储器存储值时发生异常
        /// </summary>
        /// <param name="e">异常对象</param>
        protected virtual void OnValueSaveException(Exception e)
        {
            Debug.LogWarning(e, this);
            exceptionEvent.onSaveException.Invoke(e);
        }

        /// <summary>
        /// 判断变量值是否更改
        /// </summary>
        /// <returns>变量值是否更改</returns>
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

        /// <summary>
        /// 当被代理变量值发生更改时
        /// </summary>
        protected virtual void OnProxyValueChanged()
        {
            if(_proxyTarget != null)
            {
                value = _proxyTarget.value;
            }
        }

        /// <summary>
        /// 当变量值发生更改时
        /// </summary>
        /// <param name="save">是否保存</param>
        /// <param name="notify">是否触发事件</param>
        protected virtual void OnValueChanged(bool save = true,bool notify = true)
        {
            if (autoSave && save)
                Save();
            if (valueChangedEvent != null && notify)
                valueChangedEvent.Invoke();
            _value = value;
        }

        /// <summary>
        /// 存储变量
        /// </summary>
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

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 刷新变量值
        /// </summary>
        public virtual void RefreshValue()
        {
            value = GetValue();
        }

        public override string ToString()
        {
            if (value == null)
                return "null";
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

        public virtual void OnStorableObjectRestore(object obj)
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

        /// <summary>
        /// 转换加载对象
        /// </summary>
        /// <param name="obj">从存储器加载的对象</param>
        /// <param name="output">转换后的对象</param>
        /// <returns>是否转换成功</returns>
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
