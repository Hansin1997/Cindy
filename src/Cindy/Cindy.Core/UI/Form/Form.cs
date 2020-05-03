using Cindy.UI.Binder;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Form
{
    [AddComponentMenu("Cindy/UI/Form/Form")]
    public class Form : MonoBehaviour, IForm
    {
        public GameObject[] contexts;
        public bool includingSelf = true;

        public AbstractBinder[] binders;

        protected virtual ICollection<AbstractBinder> GetTargets()
        {
            HashSet<AbstractBinder> binders = new HashSet<AbstractBinder>();
            foreach (AbstractBinder binder in this.binders)
                binders.Add(binder);
            HashSet<GameObject> contexts = new HashSet<GameObject>();
            foreach (GameObject context in this.contexts)
                contexts.Add(context);
            if(includingSelf)
                contexts.Add(gameObject);
            foreach(GameObject gameObject in contexts)
            {
                AbstractBinder[] bs = gameObject.GetComponentsInChildren<AbstractBinder>();
                foreach (AbstractBinder b in bs)
                    binders.Add(b);
            }    
            return binders;
        }

        public void Restore()
        {
            ICollection<AbstractBinder> bs = GetTargets();
            foreach (AbstractBinder binder in bs)
            {
                binder.Bind();
            }
        }

        public void Submit()
        {
            ICollection<AbstractBinder> bs = GetTargets();
            foreach (AbstractBinder binder in bs)
            {
                binder.Apply();
            }
        }
    }
}