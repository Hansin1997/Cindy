using Cindy.Logic;
using Cindy.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.PositionBinders
{
    [AddComponentMenu("Cindy/UI/PositionBinders/PositionBinderManager", 99)]
    [RequireComponent(typeof(RectTransform))]
    public class PositionBinderManager : Attachable
    {
        public Camera targetCamera;
        protected RectTransform rectTransform;

        public List<RectTransformGroup> transformGroups;

        public Dictionary<Attachment, RectTransformGroup> map;

        protected Camera Camera { get { return targetCamera == null ?  Camera.main : targetCamera; } } 

        public override bool Attach(Attachment attachment)
        {
            if (!base.Attach(attachment))
                return false;
            AbstractPositionBinder a = attachment as AbstractPositionBinder;
            if (map == null)
                map = new Dictionary<Attachment, RectTransformGroup>();
            RectTransformGroup group = new RectTransformGroup(a.GenerateComponents(gameObject));
            foreach(RectTransform rectTransform in group.rectTransforms)
            {
                ContextProxy[] contexts = rectTransform.GetComponentsInChildren<ContextProxy>();
                foreach (ContextProxy context in contexts)
                    context.realContext = a.context;
            }
            transformGroups.Add(group);
            map[attachment] = group;
            return true;
        }

        public override bool Detach(Attachment attachment)
        {
            if (!base.Detach(attachment))
                return false;

            if (map == null)
                map = new Dictionary<Attachment, RectTransformGroup>();
            if (map.ContainsKey(attachment))
            {
                RectTransformGroup group = map[attachment];
                if(group != null)
                {
                    foreach (RectTransform rect in group.rectTransforms)
                    {
                        Destroy(rect.gameObject);
                    }

                    transformGroups.Remove(group);
                }
            }
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
            foreach(Attachment attachment in attachments)
            {
                AbstractPositionBinder a = attachment as AbstractPositionBinder;
                float angle = Vector3.Angle(Camera.transform.forward, a.transform.position - Camera.transform.position);
                RectTransformGroup group = map[attachment];
                if (a.IsActived() && angle <= Camera.fieldOfView)
                {
                    a.OnShow(group.rectTransforms);
                    Vector3 position = Camera.WorldToScreenPoint(a.transform.position);

                    a.OnAdapte(group.rectTransforms, position);
                }
                else
                {
                    a.OnHide(group.rectTransforms);
                }
            }
        }

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is AbstractPositionBinder;
        }

        protected override bool IsPeek(Attachment attachment)
        {
            return attachment != null && attachment is AbstractPositionBinder positionBinder
                && positionBinder.enabled;
        }

        [Serializable]
        public class RectTransformGroup
        {
            public RectTransform[] rectTransforms;

            public RectTransformGroup(RectTransform[] rectTransforms)
            {
                this.rectTransforms = rectTransforms;
            }
        }
    }
}
