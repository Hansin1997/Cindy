using System;
using UnityEngine;

namespace Cindy.UI.Binder
{
    public abstract class AbstractBinder : MonoBehaviour, IBinder
    {
        public AbstractDataSource dataSource;
        public Options options;

        protected abstract void OnBind(AbstractDataSource dataSource);
        protected abstract void OnApply(AbstractDataSource dataSource);

        protected virtual void Initialize() { }

        public void Bind()
        {
            if (dataSource != null)
            {
                if (dataSource.IsReadable())
                    OnBind(dataSource);
                else
                    Debug.LogWarning("Data source not allow to read!", this);
            }
            else
                Debug.LogWarning("Data source is null!", this);
        }

        public void Apply()
        {
            if (dataSource != null)
            {
                if (dataSource.IsWriteable())
                    OnApply(dataSource);
                else
                    Debug.LogWarning("Data source not allow to write!", this);
            }
            else
                Debug.LogWarning("Data source is null!", this);
        }

        protected void Start()
        {
            Initialize();
            if (options.bindOnStart)
                Bind();
        }

        protected virtual void Update()
        {
            if (options.bindOnUpdate)
                Bind();
        }

        [Serializable]
        public class Options
        {
            public bool bindOnStart = true;

            public bool bindOnUpdate;
        }
    }
}
