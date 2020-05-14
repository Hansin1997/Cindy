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
        /// 值更改事件
        /// </summary>
        [Header("Events")]
        public UnityEvent valueChangedEvent;

        protected object _value; // 旧变量值
        protected VariableObject<T> _proxyTarget, _proxyTargetOld; // 当前代理对象和旧的代理对象

        /// <summary>
        /// 变量值
        /// </summary>
        public T Value { get { return GetValue(); }  set { SetValue(value); } }

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
                SetValue(v);
            else if (value != null)
            {
                SetValue(JSON.FromJson<T>(value.ToString()));
            }
            else
                SetValue(default);
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
            if (valueChangedEvent != null && notify)
                valueChangedEvent.Invoke();
            _value = value;
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetValue(T value)
        {
            if (_proxyTarget != _proxyTargetOld)
            {
                if (_proxyTargetOld != null)
                    _proxyTargetOld.valueChangedEvent.RemoveListener(OnProxyValueChanged);
                _proxyTarget.valueChangedEvent.AddListener(OnProxyValueChanged);
                _proxyTargetOld = _proxyTarget;
            }
            if (proxyContext != null)
            {
                if (_proxyTarget == null || !_proxyTarget.variableName.Equals(proxyTarget.Value))
                    _proxyTarget = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                VariableObject<T> target = _proxyTarget;
                if (target != null)
                    target.SetValue(value);
            }
            this.value = value;

            if (IsValueChanged())
                OnValueChanged();
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <returns></returns>
        public virtual T GetValue()
        {
            if (_proxyTarget != _proxyTargetOld)
            {
                if (_proxyTargetOld != null)
                    _proxyTargetOld.valueChangedEvent.RemoveListener(OnProxyValueChanged);
                _proxyTarget.valueChangedEvent.AddListener(OnProxyValueChanged);
                _proxyTargetOld = _proxyTarget;
            }
            if (proxyContext != null)
            {
                if (_proxyTarget == null || !_proxyTarget.variableName.Equals(proxyTarget.Value))
                    _proxyTarget = proxyContext.GetVariable<VariableObject<T>, T>(proxyTarget.Value);
                VariableObject<T> target = _proxyTarget;
                if (target != null)
                    value = target.GetValue();
            }

            if (IsValueChanged())
                OnValueChanged();
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
            return variableName;
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
            if (TramsformValue(obj, out T v))
            {
                SetValue(v);
            }
            else
            {
                Debug.LogWarning(string.Format("obj({0}) can not tramsfrom to {1}", obj.GetType().Name, typeof(T).Name));
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
