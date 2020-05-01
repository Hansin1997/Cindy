using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.UI.Binder
{
    public abstract class SingleValueBinder<V, T> : AbstractBinder
    {
        public ReferenceString dataKey;

        public T target;

        protected abstract void BindData(T target, V value);

        protected abstract V TargetData(T target);

        protected override void Initialize()
        {
            if (target == null)
            {
                target = GetComponent<T>();
            }
        }

        protected override void OnBind(AbstractDataSource dataSource)
        {
            if(target != null)
            {
                BindData(target, OnGetValue(dataSource, dataKey.Value));
            }
            else
            {
                Debug.LogWarning("Target is null!");
            }
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

        protected virtual V OnGetValue(AbstractDataSource dataSource, string key)
        {
            return dataSource.GetData<V>(key, GetDefaultValue());
        }

        protected virtual V GetDefaultValue()
        {
            return default;
        }

        protected void SetValue(V value)
        {
            if (dataSource != null)
                dataSource.SetData(dataKey.Value, value);
            else
                Debug.LogWarning("DataSource is null!", this);
        }
    }
}