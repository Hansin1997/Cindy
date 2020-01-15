using UnityEngine.UI;

namespace Cindy.UI.Pages
{
    public class MessageBox : Page
    {
        public Text title, content;

        public void SetText(string title,string content)
        {
            if (this.title != null)
                this.title.text = title;
            if (this.content != null)
                this.content.text = content;

        }

        public void SetTitle(string title)
        {
            this.title.text = title;
        } 

        public void SetContent(string content)
        {
            this.content.text = content;
        }


    }
}
