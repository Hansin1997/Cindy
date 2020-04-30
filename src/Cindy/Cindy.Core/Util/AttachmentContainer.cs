using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util
{
    public abstract class AttachmentContainer : MonoBehaviour
    {
        [Header("Attachment Container")]
        [SerializeField]
        public List<Attachment> attachments;

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

        public virtual bool Remove(Attachment attachment)
        {
            if (!attachments.Contains(attachment))
                return false;
            return attachments.Remove(attachment);
        }

        public Attachment Peek()
        {
            for (int i = attachments.Count - 1; i >= 0; i--)
            {
                Attachment attachment = attachments[i];
                if (IsAvailable(attachment))
                    return attachment;
            }
            return null;
        }

        public T Peek<T>() where T : Attachment
        {
            Attachment tmp = Peek();
            if (tmp == null)
                return default;
            return tmp as T;
        }

        protected abstract bool CheckAttachment(Attachment attachment);
        protected abstract bool IsAvailable(Attachment attachment);
    }
}
