﻿using Cindy.Util;
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

        protected Camera Camera { get { return targetCamera == null ?  Camera.main : targetCamera; } } 

        public override bool Attach(Attachment attachment)
        {
            if (!base.Attach(attachment))
                return false;
            AbstractPositionBinder a = attachment as AbstractPositionBinder;
            transformGroups.Add(new RectTransformGroup(a.GenerateComponents(gameObject)));
            return true;
        }

        public override bool Detach(Attachment attachment)
        {
            int index = attachments.IndexOf(attachment);
            if (!base.Detach(attachment))
                return false;
            foreach(RectTransform rect in transformGroups[index].rectTransforms)
            {
                Destroy(rect.gameObject);
            }
            transformGroups.RemoveAt(index);
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
                AbstractPositionBinder a = attachment as AbstractPositionBinder;
                float angle = Vector3.Angle(Camera.transform.forward, a.transform.position - Camera.transform.position);

                if (a.IsActived() && angle <= Camera.fieldOfView)
                {
                    a.OnShow(transformGroups[i].rectTransforms);
                    Vector3 position = Camera.WorldToScreenPoint(a.transform.position);

                    a.OnAdapte(transformGroups[i].rectTransforms, position);
                }
                else
                {
                    a.OnHide(transformGroups[i].rectTransforms);
                }
                i++;
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
