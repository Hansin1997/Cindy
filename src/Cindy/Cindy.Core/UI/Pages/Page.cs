using UnityEngine;

namespace Cindy.UI.Pages
{
    public class Page : MonoBehaviour
    {
        protected PageContainer owner;
        public string[] cancelButtons = new string[] { "Cancel" };

        public void SetContainer(PageContainer owner)
        {
            this.owner = owner;
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

        public void Show()
        {
            PageContainer.Load<Page>(name);
        }

        public T Show<T>() where T : Page
        {
            return PageContainer.Load<T>(name);
        }

        public bool IsActive()
        {
            return owner != null && owner.pages != null && owner.pages.Count > 0 && owner.pages[owner.pages.Count - 1] == this;
        }
    }
}
