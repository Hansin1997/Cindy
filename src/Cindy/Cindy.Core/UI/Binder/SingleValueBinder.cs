using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;

namespace Cindy.UI.Binder
{
    /// <summary>
    /// 单数据值绑定器
    /// </summary>
    /// <typeparam name="V">数据类型</typeparam>
    /// <typeparam name="T">被绑定的目标类型</typeparam>
    public abstract class SingleValueBinder<V, T> : AbstractBinder
    {
        /// <summary>
        /// 数据键名
        /// </summary>
        [Tooltip("Key of data.")]
        public ReferenceString dataKey;
        /// <summary>
        /// 被绑定的对象
        /// </summary>
        [Tooltip("Bound object.")]
        public T target;

        /// <summary>
        /// 绑定数据到对象。
        /// </summary>
        /// <param name="target">被绑定的对象</param>
        /// <param name="value">数据</param>
        protected abstract void BindData(T target, V value);
        /// <summary>
        /// 获取被绑定的对象的数据值。
        /// </summary>
        /// <param name="target">被绑定的对象</param>
        /// <returns>被绑定的对象的数据值</returns>
        protected abstract V TargetData(T target);
        /// <summary>
        /// 绑定数据异常时调用此方法。
        /// </summary>
        /// <param name="e">异常对象</param>
        protected virtual void BindDataException(Exception e) { if (e != null) Debug.LogError(e); }
        /// <summary>
        /// 更新数据异常时调用此方法。
        /// </summary>
        /// <param name="e">异常对象</param>
        protected virtual void ApplyDataException(Exception e) { if (e != null) Debug.LogError(e); }

        protected override void Initialize()
        {
            if (target == null)
            {
                target = GetComponent<T>();
            }
        }

        protected override void OnBind(AbstractDataSource dataSource)
        {
            dataSource.GetData<V>(dataKey.Value, this, (val, e, isSuccess) =>
            {
                if (isSuccess)
                {
                    if (target != null)
                    {
                        try
                        {
                            BindData(target, val);
                        }
                        catch (Exception e1)
                        {
                            BindDataException(e1);
                        }
                    }
                    else
                        Debug.LogWarning("Target is null!");
                }
                else
                {
                    BindDataException(e);
                }
            });
        }

        protected override void OnApply(AbstractDataSource dataSource)
        {
            if (target != null)
            {
                SetValue(TargetData(target));
            }
            else
            {
                Debug.LogWarning("Target is null!");
            }
        }

        /// <summary>
        /// 获取默认数据。
        /// </summary>
        /// <returns>默认数据</returns>
        protected virtual V GetDefaultValue()
        {
            return default;
        }

        /// <summary>
        /// 设置数据。
        /// </summary>
        /// <param name="value">数据值</param>
        protected void SetValue(V value)
        {
            if (dataSource != null)
            {
                dataSource.SetData(dataKey.Value, value,this,(isSuccess,e)=>
                {
                    if (!isSuccess)
                    {
                        ApplyDataException(e);
                    }
                });
            }
            else
                Debug.LogWarning("DataSource is null!", this);
        }
    }
}