using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;

namespace Cindy.UI.Binder
{
    public abstract class SingleValueBinder<V, T> : AbstractBinder
    {
        public ReferenceString dataKey;

        public T target;

        protected abstract void BindData(T target, V value);
        protected abstract V TargetData(T target);
        protected virtual void BindDataException(Exception e) { if (e != null) Debug.LogError(e); }
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

        protected virtual V GetDefaultValue()
        {
            return default;
        }

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