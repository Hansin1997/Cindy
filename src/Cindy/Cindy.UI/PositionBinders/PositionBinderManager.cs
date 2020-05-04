using Cindy.Logic;
using Cindy.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.PositionBinders
{
    /// <summary>
    /// 位置绑定管理器
    /// </summary>
    [AddComponentMenu("Cindy/UI/PositionBinders/PositionBinderManager", 99)]
    [RequireComponent(typeof(RectTransform))]
    public class PositionBinderManager : AttachmentContainer
    {
        public Camera targetCamera;
        protected RectTransform rectTransform;

        public List<RectTransformGroup> transformGroups;

        public Dictionary<Attachment, RectTransformGroup> map;

        protected int block;

        protected Camera Camera { get { return targetCamera == null ?  Camera.main : targetCamera; } } 

        public override bool Add(Attachment attachment)
        {
            if (!base.Add(attachment))
                return false;
            AbstractPositionBinder a = attachment as AbstractPositionBinder;
            if (a is BlockedPositionBinder)
                block++;
            if (map == null)
                map = new Dictionary<Attachment, RectTransformGroup>();
            RectTransformGroup group = new RectTransformGroup(a.GenerateComponents(gameObject));
            foreach(RectTransform rectTransform in group.rectTransforms)
            {
                ContextProxy[] contexts = rectTransform.GetComponentsInChildren<ContextProxy>();
                foreach (ContextProxy context in contexts)
                    context.RealContext = a.context;
            }
            transformGroups.Add(group);
            map[attachment] = group;
            return true;
        }

        public override bool Remove(Attachment attachment)
        {
            if (!base.Remove(attachment))
                return false;
            if (attachment is BlockedPositionBinder)
                block--;
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
            HashSet<Attachment> nullSet = new HashSet<Attachment>();
            foreach(Attachment attachment in attachments)
            {
                if (attachment == null)
                {
                    nullSet.Add(attachment);
                    continue;
                }
                AbstractPositionBinder a = attachment as AbstractPositionBinder;
                float angle = Vector3.Angle(Camera.transform.forward, a.transform.position - Camera.transform.position);
                RectTransformGroup group = map[attachment];
                if (a.IsActived() && angle <= Camera.fieldOfView && block == 0)
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
            foreach(Attachment attachment in nullSet)
            {
                Remove(attachment);
            }
        }

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is AbstractPositionBinder;
        }

        protected override bool IsAvailable(Attachment attachment)
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
