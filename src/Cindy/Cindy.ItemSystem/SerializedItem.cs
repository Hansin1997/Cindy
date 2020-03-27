using Cindy.Util;
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
            return Finder.Find<Item>(entityName, true, item => !item.serialized);
        }

        public Item Instantiate(Transform parent = null,bool worldPositionStay = false)
        {
            Item template = GetEntity();
            if (template == null)
                return null;
            Item instance = UnityEngine.Object.Instantiate(template, parent, worldPositionStay);
            instance.item = Clone();
            instance.serialized = true;
            return instance;
        }

        public Item Preview(Transform parent = null, bool worldPositionStay = false)
        {
            Item template = GetEntity();
            if (template == null)
                return null;
            Item instance = UnityEngine.Object.Instantiate(template, parent, worldPositionStay);
            instance.item = Clone();
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

            instance.serialized = true;
            return instance;
        }

        public SerializedItem Clone()
        {
            return new SerializedItem()
            {
                name = name,
                content = content,
                mergeable = mergeable,
                amount = amount,
                entityName = entityName,
                previewTag = previewTag
            };
        }
    }
}
