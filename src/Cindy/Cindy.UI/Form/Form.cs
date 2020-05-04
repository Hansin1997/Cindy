using Cindy.UI.Binder;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Form
{
    /// <summary>
    /// 表单
    /// </summary>
    [AddComponentMenu("Cindy/UI/Form/Form")]
    public class Form : MonoBehaviour, IForm
    {
        /// <summary>
        /// 用于搜索的物体
        /// </summary>
        [Tooltip("Objects for searching.")]
        public GameObject[] contexts;
        /// <summary>
        /// 是否搜索自身
        /// </summary>
        [Tooltip("Search itself or not.")]
        public bool includingSelf = true;
        /// <summary>
        /// 预置的绑定器
        /// </summary>
        [Tooltip("Preset binder.")]
        public AbstractBinder[] binders;

        /// <summary>
        /// 获取目标绑定器
        /// </summary>
        /// <returns>绑定器数组</returns>
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