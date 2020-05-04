using Cindy.Logic;
using Cindy.Util;
using System;
using UnityEngine;

namespace Cindy.UI.Pages
{
    /// <summary>
    /// 页面
    /// </summary>
    [AddComponentMenu("Cindy/UI/Page/Page")]
    public class Page : Attachment, IPage<Page>
    {
        /// <summary>
        /// 所属容器
        /// </summary>
        protected PageContainer owner;
        /// <summary>
        /// 逻辑上下文
        /// </summary>
        protected Context context;
        /// <summary>
        /// 结束页面按钮
        /// </summary>
        public string[] cancelButtons = new string[] { "Cancel" };

        internal void SetContainer(PageContainer owner)
        {
            this.owner = owner;
        }

        internal void SetContext(Context context)
        {
            this.context = context;
        }

        public Context GetContext()
        {
            return context;
        }

        public virtual void OnPageStart()
        {

        }

        public virtual void OnPageFinish()
        {

        }

        public virtual void OnPageBlur()
        {

        }

        public virtual void OnPageFocus()
        {

        }

        public virtual void OnPageUpdate()
        {

        }

        public virtual void OnPageFixedUpdate()
        {

        }

        public virtual void OnPageGUI()
        {

        }

        public virtual void Finish()
        {
            if (owner != null)
                owner.FinishPage(this);
        }

        public virtual void ShowSingleton(Context context)
        {
            if (FindObjectOfType(GetType()) is Page p)
            {
                p.SetContext(context);
            }
            else
            {
                Show(context);
            }
        }

        public virtual void ShowSingleton()
        {
            ShowSingleton(null);
        }

        public virtual void Show(Context context)
        {
            if (FindTarget() is PageContainer pc)
            {
                pc.LoadPage(name, context);
            }
            else
            {
                PageContainer.Load<Page>(name, context);
            }
        }

        public virtual void Show()
        {
            Show(null);
        }

        public override void Attach()
        {
            Show();
        }

        public override void Detach()
        {
            Finish();
        }

        public virtual T ShowAndReturn<T>(Context context = null) where T : Page
        {
            if(FindTarget() is PageContainer pc)
            {
                return pc.LoadPage<T>(name, context);
            }
            return PageContainer.Load<T>(name, context);
        }

        public virtual bool IsActive()
        {
            return owner != null && owner.Pages != null && owner.Pages.Count > 0 && owner.Pages[owner.Pages.Count - 1] == this;
        }

        protected override Type GetTargetType()
        {
            return typeof(PageContainer);
        }
    }
}
