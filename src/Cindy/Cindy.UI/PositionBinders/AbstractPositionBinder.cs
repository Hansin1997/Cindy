using Cindy.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.PositionBinders
{
    public abstract class AbstractPositionBinder : Attachment
    {
        public abstract RectTransform[] GenerateComponents(GameObject root);
        public abstract bool IsActived();
        public virtual void OnHide(IList<RectTransform> rectTransforms)
        {
            foreach (RectTransform rectTransform in rectTransforms)
                rectTransform.gameObject.SetActive(false);
        }

        public virtual void OnShow(IList<RectTransform> rectTransforms)
        {
            foreach (RectTransform rectTransform in rectTransforms)
                rectTransform.gameObject.SetActive(true);
        }

        public virtual void OnAdapte(IList<RectTransform> rectTransforms,Vector3 position)
        {
            float h = 0;
            foreach (RectTransform rectTransform in rectTransforms)
            {
                h += rectTransform.rect.height;
            }

            for (int i = 0;i < rectTransforms.Count; i++)
            {
                rectTransforms[i].anchorMin = rectTransforms[i].anchorMax = Vector2.zero;
                rectTransforms[i].anchoredPosition = position - Vector3.up * (h - rectTransforms[i].rect.height);
                h -= rectTransforms[i].rect.height;
            }
        }

        protected override Type GetAttachableType()
        {
            return typeof(PositionBinderManager);
        }
    }
}
