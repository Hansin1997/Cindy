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

        protected abstract Type GetTargetType();

        public virtual void Attach()
        {
            AttachmentContainer attachable = FindTarget();
            if (attachable != null)
                attachable.Add(this);
        }

        public virtual void Detach()
        {
            AttachmentContainer attachable = FindTarget();
            if (attachable != null)
                attachable.Remove(this);
        }

        protected AttachmentContainer FindTarget()
        {
            return Finder.Find<AttachmentContainer>(targetName.Value, false, null, GetTargetType());
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
