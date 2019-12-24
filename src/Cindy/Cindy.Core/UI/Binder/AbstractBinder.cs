using UnityEngine;

namespace Cindy.UI.Binder
{
    public abstract class AbstractBinder<S,T> : MonoBehaviour
    {
        [Header("Data Source")]
        [SerializeField]
        public S source;
        [Multiline]
        public string key;

        [Header("Option")]
        public bool allowUpdate;
        public bool keyAsDefaultValue = true;

        [Header("Target")]
        [Tooltip("Auto find target from component if not set")]
        [SerializeField]
        public T target;

        protected abstract void OnBind(T target,string value);

        protected abstract string GetValue(S source,string key, string defaultValue = default); 

        private void Bind()
        {
            if (source != null && target != null)
            {
                if (keyAsDefaultValue)
                    OnBind(target, GetValue(source, key, key));
                else
                    OnBind(target, GetValue(source,key));
            }
        }

        protected virtual void Start()
        {
            if (target == null)
                target = GetComponent<T>();
            Bind();
        }

        protected virtual void Update()
        {
            if (allowUpdate)
                Bind();
        }
    }
}
