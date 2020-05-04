using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;

namespace Cindy.Util
{
    /// <summary>
    /// Attachment类，指定AttachmentContaier的名称后可以将自身附加到AttachmentContaier中。
    /// </summary>
    public abstract class Attachment : MonoBehaviour
    {
        [Header("Attachment")]
        [Tooltip("Name of AttachmentContaier.")]
        public ReferenceString targetName; // 目标名称
        [Tooltip("Sort values, the higher the value, the higher the container.")]
        public int order = 0; // 排序值，值越高在容器越靠前
        
        [Tooltip("Whether to attach itself at Start().")]
        public bool attachOnStart = false; // 是否在Start()时附加自身。
        [Tooltip("Whether to detach itself at OnDestroy().")]
        public bool detachOnDestroy = false;// 是否在Detach()时从容器移除自身。

        /// <summary>
        /// 获取目标类型，用于寻找AttachmentContaier。
        /// </summary>
        /// <returns>AttachmentContaier的类型</returns>
        protected abstract Type GetTargetType();

        /// <summary>
        /// 将自身附加到容器。
        /// </summary>
        public virtual void Attach()
        {
            AttachmentContainer attachable = FindTarget();
            if (attachable != null)
                attachable.Add(this);
        }

        /// <summary>
        /// 从容器移除自身。
        /// </summary>
        public virtual void Detach()
        {
            AttachmentContainer attachable = FindTarget();
            if (attachable != null)
                attachable.Remove(this);
        }

        /// <summary>
        /// 寻找目标容器。
        /// </summary>
        /// <returns></returns>
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
