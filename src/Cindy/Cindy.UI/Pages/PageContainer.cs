using Cindy.Logic;
using Cindy.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Pages
{
    /// <summary>
    /// 页面容器
    /// </summary>
    [AddComponentMenu("Cindy/UI/Page/PageContainer")]
    public class PageContainer : AttachmentContainer , IPageContainer<Page>
    {
        public List<Attachment> Pages { get { return attachments; } }
        protected Page currentPage;

        public override bool Add(Attachment attachment)
        {
            if (base.Add(attachment) && attachment is Page p)
            {
                Push(p);
                return true;
            }else
                return false;
        }

        public override bool Remove(Attachment attachment)
        {
            if (Pages.Contains(attachment) && attachment is Page p)
            {
                FinishPage(p);
                return true;
            }
            else
                return false;
        }

        public virtual void Push(Page page,Context context = null)
        {
            base.Add(page);
            ContextProxy[] contextProxies = page.GetComponentsInChildren<ContextProxy>();
            foreach(ContextProxy contextProxy in contextProxies)
            {
                contextProxy.RealContext = context;
            }
            page.SetContainer(this);
            page.SetContext(context);
            page.OnPageStart();
        }

        public virtual Page Pop()
        {
            if (Pages.Count == 0)
                return null;
            Page page = Pages[Pages.Count - 1] as Page;
            Pages.RemoveAt(Pages.Count - 1);
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
            if (Pages.Contains(page))
            {
                while(Pages.Count > 0)
                {
                    Page top = Pages[Pages.Count - 1] as Page;
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

            if (currentPage == null && Pages.Count > 0)
            {
                currentPage = Pages[Pages.Count - 1] as Page;
                currentPage.OnPageFocus();
            }else if(currentPage != null && Pages.Count > 0 && Pages[Pages.Count - 1] != currentPage)
            {
                currentPage.OnPageBlur();
                currentPage = Pages[Pages.Count - 1] as Page;
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

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is Page;
        }

        protected override bool IsAvailable(Attachment attachment)
        {
            return attachment != null && attachment.gameObject.activeSelf && attachment.enabled;
        }
    }
}
