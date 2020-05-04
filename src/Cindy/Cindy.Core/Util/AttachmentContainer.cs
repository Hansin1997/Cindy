using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util
{
    /// <summary>
    /// AttachmentContainer类是Attachment的容器类。
    /// </summary>
    public abstract class AttachmentContainer : MonoBehaviour
    {
        [Header("Attachment Container")]
        [SerializeField]
        [Tooltip("Attachments attached to container.")]
        public List<Attachment> attachments; // 提交到容器的Attachment

        /// <summary>
        /// 附加一个Attachment到容器。
        /// </summary>
        /// <param name="attachment">被附加的Attachment</param>
        /// <returns>是否添加成功</returns>
        public virtual bool Add(Attachment attachment)
        {
            if (attachments.Contains(attachment) || !CheckAttachment(attachment))
                return false;
            if (attachments.Count == 0)
                attachments.Add(attachment);
            else
                for (int i = attachments.Count - 1; i >= 0; i--)
                {
                    Attachment tmp = attachments[i];
                    if (tmp.order <= attachment.order)
                    {
                        attachments.Insert(i + 1, attachment);
                        break;
                    }
                    else if (i == 0)
                    {
                        attachments.Insert(i, attachment);
                        break;
                    }
                }
            return true;
        }

        /// <summary>
        /// 从容器移除一个Attachment。
        /// </summary>
        /// <param name="attachment">被移除的Attachment</param>
        /// <returns>是否成功移除</returns>
        public virtual bool Remove(Attachment attachment)
        {
            if (!attachments.Contains(attachment))
                return false;
            return attachments.Remove(attachment);
        }

        /// <summary>
        /// 获取有效的栈顶Attachment。
        /// </summary>
        /// <returns>有效的Attachment</returns>
        public virtual Attachment Peek()
        {
            for (int i = attachments.Count - 1; i >= 0; i--)
            {
                Attachment attachment = attachments[i];
                if (IsAvailable(attachment))
                    return attachment;
            }
            return null;
        }

        /// <summary>
        /// 获取有效的栈顶Attachment。
        /// </summary>
        /// <typeparam name="T">Attachment类或者其子类</typeparam>
        /// <returns>有效的Attachment</returns>
        public virtual T Peek<T>() where T : Attachment
        {
            Attachment tmp = Peek();
            if (tmp == null)
                return default;
            return tmp as T;
        }

        /// <summary>
        /// 检查Attachment是否允许添加到容器中。
        /// </summary>
        /// <param name="attachment">被检查的Attachment</param>
        /// <returns>是否能够添加到容器</returns>
        protected abstract bool CheckAttachment(Attachment attachment);
        /// <summary>
        /// 检查容器内的某个Attachment是否有效
        /// </summary>
        /// <param name="attachment">被检查的Attachment</param>
        /// <returns>是否有效</returns>
        protected abstract bool IsAvailable(Attachment attachment);
    }
}
