using Cindy.Logic;
using UnityEngine;

namespace Cindy.UI.Pages
{
    /// <summary>
    /// 页面
    /// </summary>
    [AddComponentMenu("Cindy/UI/Page/Page")]
    public class Page : MonoBehaviour,IPage<Page>
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
            PageContainer.Load<Page>(name, context);
        }

        public virtual void Show()
        {
            Show(null);
        }

        public virtual T ShowAndReturn<T>(Context context = null) where T : Page
        {
            return PageContainer.Load<T>(name, context);
        }

        public virtual bool IsActive()
        {
            return owner != null && owner.pages != null && owner.pages.Count > 0 && owner.pages[owner.pages.Count - 1] == this;
        }
    }
}
