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
    }
}
