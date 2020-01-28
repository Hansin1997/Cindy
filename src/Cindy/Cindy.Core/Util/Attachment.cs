using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;

namespace Cindy.Util
{
    public abstract class Attachment : MonoBehaviour
    {
        [Header("Attachment")]
        public ReferenceString targetName;
        public int order = 0;

        public bool attachOnStart = false;
        public bool detachOnDestroy = false;

        protected abstract Type GetAttachableType();

        public virtual void Attach()
        {
            Attachable attachable = FindTarget();
            if (attachable != null)
                attachable.Attach(this);
        }

        public virtual void Detach()
        {
            Attachable attachable = FindTarget();
            if (attachable != null)
                attachable.Detach(this);
        }

        protected Attachable FindTarget()
        {
            UnityEngine.Object[] objs = FindObjectsOfType(GetAttachableType());
            foreach (UnityEngine.Object obj in objs)
            {
                if (obj is Attachable attachable && attachable.gameObject.name.Equals(targetName.Value))
                {
                    return attachable;
                }
            }
            return null;
        }

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
    }
}
