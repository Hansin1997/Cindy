﻿using Cindy.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Pages
{
    public class PageContainer : MonoBehaviour
    {
        public List<Page> pages;
        protected Page currentPage;
        public static int Count
        {
            get
            {
                PageContainer pc = FindObjectOfType<PageContainer>();
                if (pc == null)
                    return -1;
                return pc.pages.Count;
            }
        }

        protected virtual void Push(Page page,Context context = null)
        {
            if (!pages.Contains(page))
                pages.Add(page);
            ContextProxy[] contextProxies = page.GetComponentsInChildren<ContextProxy>();
            foreach(ContextProxy contextProxy in contextProxies)
            {
                contextProxy.realContext = context;
            }
            page.SetContainer(this);
            page.SetContext(context);
            page.OnPageStart();
        }

        protected virtual Page Pop()
        {
            if (pages.Count == 0)
                return null;
            Page page = pages[pages.Count - 1];
            pages.RemoveAt(pages.Count - 1);
            page.OnPageFinish();
            return page;
        }

        public virtual void Load(string name)
        {
            LoadPage(name);
        }

        public virtual Page LoadPage(string name, Context context = null)
        {
            return LoadPage<Page>(name, context);
        }

        public virtual T LoadPage<T>(string name,Context context = null) where T : Page
        {
            T[] pages = Resources.FindObjectsOfTypeAll<T>();
            T target = null;
            foreach (T p in pages)
            {
                if (p.name.Equals(name))
                {
                    target = Instantiate(p, transform);
                    target.name = p.name;
                    break;
                }
            }
            if (target != null)
                Push(target,context);
            return target;
        }

        public virtual void FinishPage(Page page)
        {
            if (pages.Contains(page))
            {
                while(pages.Count > 0)
                {
                    Page top = pages[pages.Count - 1];
                    Pop();
                    Destroy(top.gameObject);
                    if (currentPage == top)
                        currentPage = null;
                    if (page == top)
                        break;
                }
            }
        }

        protected virtual void Update()
        {
            if(currentPage != null)
                foreach (string key in currentPage.cancelButtons)
                {
                    if (VirtualInput.GetButtonDown(key))
                    {
                        currentPage.Finish();
                        break;
                    }
                }

            if (currentPage == null && pages.Count > 0)
            {
                currentPage = pages[pages.Count - 1];
                currentPage.OnPageFocus();
            }else if(currentPage != null && pages.Count > 0 && pages[pages.Count - 1] != currentPage)
            {
                currentPage.OnPageBlur();
                currentPage = pages[pages.Count - 1];
                currentPage.OnPageFocus();
            }
            if (currentPage != null)
            {
                currentPage.OnPageUpdate();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (currentPage != null)
                currentPage.OnPageFixedUpdate();
        }

        public static T Load<T>(string name,Context context = null) where T : Page
        {
            PageContainer c = FindObjectOfType<PageContainer>();
            if (c != null)
            {
                T page =  c.LoadPage<T>(name, context);
                if(page != null)
                {
                    page.gameObject.SetActive(true);
                }
                return page;
            }
            return null;
        }
    }
}
