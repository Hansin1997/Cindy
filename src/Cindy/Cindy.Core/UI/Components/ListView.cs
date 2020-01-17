﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Components
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public abstract class ListView<T,S> : MonoBehaviour where T : Component where S : Component
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

        public RectTransform Content { get { return scrollRect == null ? null : scrollRect.content; } }

        protected virtual void Start()
        {
            scrollRect = GetComponent<ScrollRect>();
            template.gameObject.SetActive(templateActived);
            if (refreshOnStart)
                Refresh();
            if (items == null)
                items = new List<T>();
        }

        public virtual void Refresh()
        {
            for(int i = 0;i < items.Count; i++)
            {
                Destroy(items[i].gameObject);
            }
            items.Clear();
            GenerateItem(source);
            ReAdapte();
        }

        protected virtual T InstantiateTemplate()
        {
            if (template == null)
                throw new Exception("Template is null!");
            T result =  Instantiate(template, Content.transform);
            result.gameObject.SetActive(true);
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
            float width = padding.left,w;
            for(int i = 0, len = items.Count; i < len; i++)
            {
                Transform child = items[i].transform;
                RectTransform rectTransform;
                if(child.TryGetComponent(out rectTransform))
                {
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(0, 1);
                    w = rectTransform.rect.width;
                    Vector2 tmp = rectTransform.anchoredPosition;
                    tmp.x = Content.anchoredPosition.x + w / 2 + width;

                    width += w;

                    if (i > 0)
                    {
                        tmp.x += space;
                        width += space;
                    }

                    rectTransform.anchoredPosition = tmp;
                }
            }

            Vector2 s = Content.sizeDelta;
            s.x = width + padding.right;
            Content.sizeDelta = s;
        }

        protected virtual void VerticalAdapte()
        {
            float height = padding.top, h;
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
                    tmp.y = Content.anchoredPosition.y - h / 2 - height;

                    height += h;

                    if (i > 0)
                    {
                        tmp.y += space;
                        height += space;
                    }

                    rectTransform.anchoredPosition = tmp;
                }
            }

            Vector2 s = Content.sizeDelta;
            s.y = height + padding.bottom;
            Content.sizeDelta = s;
        }
    }
}
