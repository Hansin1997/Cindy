using System;
using UnityEngine;

namespace Cindy.UI.Binder
{
    /// <summary>
    /// 抽象绑定器
    /// </summary>
    public abstract class AbstractBinder : MonoBehaviour, IBinder
    {
        /// <summary>
        /// 数据源
        /// </summary>
        [Tooltip("Data source")]
        public AbstractDataSource dataSource;
        /// <summary>
        /// 绑定选项
        /// </summary>
        [Tooltip("Binder options")]
        public Options options;

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dataSource"></param>
        protected abstract void OnBind(AbstractDataSource dataSource);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dataSource"></param>
        protected abstract void OnApply(AbstractDataSource dataSource);

        /// <summary>
        /// 初始化方法，在Start()且数据绑定前时调用。
        /// </summary>
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