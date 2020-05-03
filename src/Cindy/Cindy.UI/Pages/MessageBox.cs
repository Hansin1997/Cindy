﻿using Cindy.Logic;
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
        public Context context;

        protected override void Run()
        {
            if (messageBox == null)
                return;
            string title, content;

            title = titleKey.Value;
            content = contentKey.Value;
            messageBox.SetText(title, content);
            MessageBox instance = messageBox.ShowAndReturn<MessageBox>(context);
            if (stringSource != null)
            {
                stringSource.Get(titleKey.Value,this,(v,e,s)=>
                {
                    if (s)
                    {
                        if (instance != null)
                            instance.SetTitle(v);
                    }    
                    else
                        Debug.Log(e);
                });
                stringSource.Get(contentKey.Value, this, (v, e, s) =>
                {
                    if (s)
                    {
                        if (instance != null)
                            instance.SetContent(v);
                    }
                    else
                        Debug.Log(e);
                });
            }
        }
    }
}
