using Cindy.UI.Structs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Components
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public abstract class ListView<T, S> : MonoBehaviour where T : Component where S : Component
    {
        [Serializable]
        public enum ListType
        {
            Horizontal,
            Vertical
        }

        public T template;

        [Header("Style")]
        public ListType type;
        public LRTB padding;
        public float space;

        [Header("Option")]
        public bool templateActived;
        public bool refreshOnStart = true;

        [Header("Data")]
        public S source;
        [HideInInspector]
        public List<T> items;

        protected ScrollRect scrollRect;

        public ScrollRect ScrollRect { get { return scrollRect == null ? (scrollRect = GetComponent<ScrollRect>()) : scrollRect;  } }

        public RectTransform Content { get { return ScrollRect == null ? null : ScrollRect.content; } }
        public RectTransform Viewport { get { return Content == null || Content.parent == null ? null : Content.parent.GetComponent<RectTransform>(); } }

        protected virtual void Start()
        {
            if(template != null)
                template.gameObject.SetActive(templateActived);
            if (refreshOnStart)
                Refresh();
            if (items == null)
                items = new List<T>();
        }

        public virtual void Refresh()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Destroy(items[i].transform.gameObject);
            }
            items.Clear();
            GenerateItem(source);
            ReAdapte();
        }

        protected virtual T InstantiateTemplate()
        {
            if (template == null)
                throw new Exception("Template is null!");
            if (Content == null)
                return null;
            T result = Instantiate(template, Content.transform);
            if (result == null)
                return null;
            result.gameObject.SetActive(true);
            result.gameObject.name = "Item" + items.Count;
            items.Add(result);
            return result;
        }

        protected abstract void GenerateItem(S source);

        public virtual void ReAdapte()
        {
            switch (type)
            {
                case ListType.Horizontal:
                    HorizontalAdapte();
                    scrollRect.vertical = false;
                    scrollRect.horizontal = true;
                    break;
                case ListType.Vertical:
                    VerticalAdapte();
                    scrollRect.vertical = true;
                    scrollRect.horizontal = false;
                    break;
            }
        }

        protected virtual void HorizontalAdapte()
        {
            float width = padding.left, w;
            Content.anchorMin = new Vector2(0, 0);
            Content.anchorMax = new Vector2(0, 1);
            for (int i = 0, len = items.Count; i < len; i++)
            {
                Transform child = items[i].transform;
                RectTransform rectTransform;
                if (child.TryGetComponent(out rectTransform))
                {
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(0, 1);

                    w = rectTransform.rect.width;
                    Vector2 tmp = rectTransform.anchoredPosition;
                    tmp.x = Viewport.anchoredPosition.x + w / 2 + width;

                    width += w;

                    if (i > 0)
                    {
                        tmp.x += space;
                        width += space;
                    }

                    rectTransform.anchoredPosition = tmp;

                    Vector2 offsetMin = rectTransform.offsetMin, offsetMax = rectTransform.offsetMax;
                    offsetMin.y = padding.bottom;
                    offsetMax.y = -padding.top;
                    rectTransform.offsetMin = offsetMin;
                    rectTransform.offsetMax = offsetMax;
                }
            }
            width += padding.right;
            Content.offsetMin = new Vector2(0, 0);
            Content.offsetMax = new Vector2(width, 0);
        }

        protected virtual void VerticalAdapte()
        {
            float height = padding.top, h;
            Content.anchorMin = new Vector2(0, 1);
            Content.anchorMax = new Vector2(1, 1);
            for (int i = 0, len = items.Count; i < len; i++)
            {
                Transform child = items[i].transform;
                if (!child.gameObject.activeSelf)
                    continue;
                RectTransform rectTransform;
                if (child.TryGetComponent(out rectTransform))
                {
                    rectTransform.anchorMin = new Vector2(0, 1);
                    rectTransform.anchorMax = new Vector2(1, 1);

                    h = rectTransform.rect.height;
                    Vector2 tmp = rectTransform.anchoredPosition;
                    tmp.y = Viewport.anchoredPosition.y - h / 2 - height;

                    height += h;

                    if (i > 0)
                    {
                        tmp.y -= space;
                        height += space;
                    }

                    rectTransform.anchoredPosition = tmp;

                    Vector2 offsetMin = rectTransform.offsetMin, offsetMax = rectTransform.offsetMax;
                    offsetMin.x = padding.left;
                    offsetMax.x = -padding.right;
                    rectTransform.offsetMin = offsetMin;
                    rectTransform.offsetMax = offsetMax;
                }
            }
            height += padding.bottom;
            Content.offsetMin = new Vector2(0, -height);
            Content.offsetMax = new Vector2(0, 0);
        }
    }
}
