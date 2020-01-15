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

        [Header("Preview")]
        public string entityName;

        public string bindTag;

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
                bindTag = bindTag
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
    }
}
