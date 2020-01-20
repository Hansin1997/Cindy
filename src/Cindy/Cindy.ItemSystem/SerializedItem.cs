using System;
using UnityEngine;

namespace Cindy.ItemSystem
{
    [Serializable]
    public class SerializedItem
    {
        [Header("Keys")]
        public string name;
        [Multiline]
        public string content;

        [Header("Amount")]
        public bool mergeable = true;

        public int amount = 1;

        [Header("Entity")]
        public string entityName;

        public string previewTag;

        [NonSerialized]
        public bool isSerialized;

        public void Add(int count = 1)
        {
            amount += count;
        }

        public SerializedItem Sub(int count = 1)
        {
            if (amount < count)
                count = amount;
            amount -= count;
            return new SerializedItem
            {
                name = name,
                content = content,
                mergeable = mergeable,
                amount = count,
                entityName = entityName,
                previewTag = previewTag
            };
        }

        public Item GetEntity()
        {
            Item[] items = Resources.FindObjectsOfTypeAll<Item>();
            foreach(Item item in items)
            {
                if (item.name.Equals(entityName))
                    return item;
            }
            return null;
        }

        public Item Instantiate(Transform parent = null,bool worldPositionStay = false)
        {
            Item template = GetEntity();
            if (template == null)
                return null;
            Item instance = UnityEngine.Object.Instantiate(template, parent, worldPositionStay);
            instance.item = this;
            instance.item.isSerialized = true;
            return instance;
        }

        public GameObject Preview(Transform parent = null, bool worldPositionStay = false)
        {
            Item template = GetEntity();
            if (template == null)
                return null;
            Item instance = UnityEngine.Object.Instantiate(template, parent, worldPositionStay);
            instance.item = this;
            instance.item.isSerialized = true;
            if (instance != null)
            {
                Behaviour[] behaviours = instance.GetComponentsInChildren<Behaviour>();
                foreach (Behaviour behaviour in behaviours)
                {
                    if (behaviour is Animator)
                        continue;
                    behaviour.enabled = false;
                }
                Collider[] colliders = instance.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    collider.enabled = false;
                }
                Rigidbody rigidbody = instance.GetComponent<Rigidbody>();
                if (rigidbody != null)
                    UnityEngine.Object.Destroy(rigidbody);
            }
            return instance.gameObject;
        }
    }
}
