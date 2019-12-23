using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Util
{
    public abstract class Attachable<A,T> : MonoBehaviour where A : Attachment<T>
    {
        [Header("Attachable")]
        [SerializeField]
        public List<A> attachments;

        public virtual void Attach(A attachment)
        {
            if (attachments.Contains(attachment))
                return;
            if (attachments.Count == 0)
                attachments.Add(attachment);
            else
                for (int i = attachments.Count - 1; i >= 0; i--)
                {
                    A tmp = attachments[i];
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
        }

        public virtual void Detach(A attachment)
        {
            if (!attachments.Contains(attachment))
                return;
            attachments.Remove(attachment);
        }

        public A Peek()
        {
            for (int i = attachments.Count - 1; i >= 0; i--)
            {
                A attachment = attachments[i];
                if (IsPeek(attachment))
                    return attachment;
            }
            return null;
        }

        protected abstract bool IsPeek(A attachment);
    }
}
