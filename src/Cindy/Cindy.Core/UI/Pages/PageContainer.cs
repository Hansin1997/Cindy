using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Pages
{
    public class PageContainer : MonoBehaviour
    {
        public List<Page> pages;
        protected Page currentPage;

        protected virtual void Push(Page page)
        {
            if (!pages.Contains(page))
                pages.Add(page);
            page.SetContainer(this);
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

        public virtual Page LoadPage(string name)
        {
            return LoadPage<Page>(name);
        }

        public virtual T LoadPage<T>(string name) where T : Page
        {
            T[] pages = Resources.FindObjectsOfTypeAll<T>();
            T target = null;
            foreach (T p in pages)
            {
                if (p.name.Equals(name))
                {
                    target = Instantiate<T>(p, transform);
                    break;
                }
            }
            if (target != null)
                Push(target);
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

    }
}
