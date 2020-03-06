using Cindy.Logic;
using UnityEngine;

namespace Cindy.UI.Pages
{
    public class Page : MonoBehaviour
    {
        protected PageContainer owner;
        protected Context context;
        public string[] cancelButtons = new string[] { "Cancel" };

        public void SetContainer(PageContainer owner)
        {
            this.owner = owner;
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }

        public Context GetContext()
        {
            return this.context;
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

        public void Finish()
        {
            if (owner != null)
                owner.FinishPage(this);
        }

        public void ShowSingleton(Context context)
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
        public void ShowSingleton()
        {
            ShowSingleton(null);
        }

        public void Show(Context context)
        {
            PageContainer.Load<Page>(name, context);
        }

        public void Show()
        {
            Show(null);
        }

        public T ShowAndReturn<T>(Context context = null) where T : Page
        {
            return PageContainer.Load<T>(name, context);
        }

        public bool IsActive()
        {
            return owner != null && owner.pages != null && owner.pages.Count > 0 && owner.pages[owner.pages.Count - 1] == this;
        }
    }
}
