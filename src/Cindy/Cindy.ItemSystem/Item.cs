using Cindy.Logic.VariableObjects;
using Cindy.Util;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.ItemSystem
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/Item", 2)]
    public class Item : MonoBehaviour
    {
        public SerializedItem item;
        [Header("Actions After Pick")]
        public bool destroySelf = true;
        public GameObject[] destroyList = new GameObject[] { };
        public UnityEvent pickListener;

        [NonSerialized]
        public bool serialized;

        protected virtual void Start()
        {
            if (item.entityName == null || item.entityName.Trim().Length == 0)
                item.entityName = gameObject.name;
        }

        public virtual void AfterPick()
        {
            foreach (GameObject obj in destroyList)
                Destroy(obj);
            if (destroySelf)
                Destroy(gameObject);
        }

        public virtual void Pick(ItemContainer container)
        {
            if (container == null)
                return;
            container.AddItem(this,name);
            AfterPick();
            pickListener.Invoke();
        }

        public virtual void Pick(string containerName)
        {
            Pick(Finder.Find<ItemContainer>(containerName));
        }

        public virtual void Pick(StringObject containerName)
        {
            Pick(Finder.Find<ItemContainer>(containerName.Value));
        }
    }
}
