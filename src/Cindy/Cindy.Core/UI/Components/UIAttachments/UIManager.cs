using Cindy.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Components.UIAttachments
{
    [RequireComponent(typeof(RectTransform))]
    public class UIManager : Attachable
    {
        public Camera targetCamera;
        protected RectTransform rectTransform;
        protected List<GameObject> roots;

        protected Camera Camera { get { return targetCamera != null ? targetCamera : Camera.main; } } 

        public override bool Attach(Attachment attachment)
        {
            if (!base.Attach(attachment))
                return false;
            UIAttachment a = attachment as UIAttachment;
            if (roots == null)
                roots = new List<GameObject>();
            GameObject root = new GameObject((attachments.Count - 1).ToString());
            a.GenerateComponents(root);
            roots.Add(root);
            return true;
        }

        public override bool Detach(Attachment attachment)
        {
            int index = attachments.IndexOf(attachment);
            if (!base.Detach(attachment))
                return false;
            Destroy(roots[index]);
            roots.RemoveAt(index);
            return true;
        }

        protected void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;
        }

        protected virtual void Update()
        {
            int i = 0;
            foreach(Attachment attachment in attachments)
            {
                UIAttachment a = attachment as UIAttachment;
                
                if (a.IsActived())
                {
                    a.OnShow(roots[i].GetComponentsInChildren<RectTransform>());
                    Vector3 position = Camera.WorldToScreenPoint(a.transform.position);
                    a.OnAdapte(roots[i].GetComponentsInChildren<RectTransform>(), position);
                }
                else
                {
                    a.OnHide(roots[i].GetComponentsInChildren<RectTransform>());
                }
                i++;
            }
        }

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is UIAttachment;
        }

        protected override bool IsPeek(Attachment attachment)
        {
            return attachment != null;
        }
    }
}
