using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
using Cindy.Strings;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.UI.Pages
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/UI/Page/MessageBox")]
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

    [AddComponentMenu("Cindy/Logic/Methods/MessageShower")]
    public class MessageShower : LogicNode
    {
        public MessageBox messageBox;
        public ReferenceString titleKey;
        public ReferenceString contentKey;
        public StringSource stringSource;

        protected override void Run()
        {
            string title = null, content = null;
            if(stringSource != null)
            {
                title = stringSource.GetString(titleKey.Value,titleKey.Value);
                content = stringSource.GetString(contentKey.Value, contentKey.Value);
            }
            else
            {
                title = titleKey.Value;
                content = contentKey.Value;
            }
            if(messageBox != null)
            {
                messageBox.SetText(title, content);
                messageBox.Show();
            }
            else
            {
                Debug.LogError("MessageBox is null!\n" + title + "\n" + content);
            }
        }
    }
}
