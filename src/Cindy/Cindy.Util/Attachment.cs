using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util
{
    public abstract class Attachment<T> : MonoBehaviour
    {
        [Header("Attachment")]
        public string targetName = "";
        public int order = 0;

        public bool attachOnStart = false;
        public bool detachOnDestroy = false;

        public T attachment;

        private IDictionary<string, object> parameters;

        protected abstract void OnGetParameters(IDictionary<string, object> parameters);


        public abstract void Attach();

        public abstract void Detach();

        protected virtual void Start()
        {
            if (attachOnStart)
                Attach();
        }

        protected virtual void OnDestroy()
        {
            if (detachOnDestroy)
                Detach();
        }

        public IDictionary<string,object> GetParameters()
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();
            OnGetParameters(parameters);
            return parameters;
        }
    }
}
