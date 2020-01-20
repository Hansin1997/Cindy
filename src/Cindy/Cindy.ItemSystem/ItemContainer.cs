using Cindy.Storages;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.ItemSystem
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/ItemContainer", 1)]
    public class ItemContainer : AbstractStorableObject
    {
        public List<SerializedItem> items;
        public List<SceneItem> abandonedItems;
        protected Dictionary<string, List<string>> picked;

        protected Dictionary<string, SerializedItem> map;
        private readonly string ABANDONED_ITEMS = "ABANDONED_ITEMS";

        protected virtual void Remap()
        {
            map = new Dictionary<string, SerializedItem>();
            foreach (SerializedItem i in items)
                map[i.name] = i;
        }

        public virtual void AddItem(SerializedItem item,string name = null)
        {
            if (item == null)
                return;
            if (map == null)
            {
                Remap();
            }
            if (picked == null)
                picked = new Dictionary<string, List<string>>();
            if (!item.isSerialized)
            {
                if (!picked.ContainsKey(gameObject.scene.name) || picked[gameObject.scene.name] == null)
                    picked[gameObject.scene.name] = new List<string>();
                if (name != null && !picked.ContainsKey(name))
                    picked[gameObject.scene.name].Add(name);
            }
            if (map.ContainsKey(item.name))
                map[item.name].Add(item.amount);
            else
            {
                map[item.name] = item;
                items.Add(item);
            }
        }

        public virtual SceneItem AbandonItem(string item,int amount = 1)
        {
            if (!map.ContainsKey(item) || map[item] == null)
                return null;
            if (amount < 0)
                amount = 0;
            SceneItem result = new SceneItem(map[item].Sub(amount), gameObject.scene.name);
            if (map[item].amount <= 0)
            {
                items.Remove(map[item]);
                map.Remove(item);
            }
            abandonedItems.Add(result);
            GenerateAbandonedItem(result.item);
            return result;
        }

        protected virtual void GenerateAbandonedItem(SerializedItem item,bool WorldPositionStay = false)
        {
            GameObject root = GameObject.Find(ABANDONED_ITEMS);
            Transform parent;
            if (root == null)
                root = new GameObject(ABANDONED_ITEMS);
            parent = root.transform.Find(name + "_" + ABANDONED_ITEMS);
            if(parent == null)
            {
                parent = new GameObject(name + "_" + ABANDONED_ITEMS).transform;
                parent.SetParent(root.transform);
            }
            item.Instantiate(parent, WorldPositionStay);
        }

        public override object GetStorableObject()
        {
            return new ItemContainerPackage(this);
        }

        public override Type GetStorableObjectType()
        {
            return typeof(ItemContainerPackage);
        }

        public override void OnPutStorableObject(object obj)
        {
            if (obj is ItemContainerPackage p) { 
                p.Resume(this);

                if (picked != null && picked.ContainsKey(gameObject.scene.name) && picked[gameObject.scene.name] != null)
                {

                    foreach (string n in picked[gameObject.scene.name])
                    {
                        GameObject target = GameObject.Find(n);
                        Item i = null;
                        if(target != null && (i = target.GetComponent<Item>()) != null)
                        {
                            i.AfterPick();
                        }

                    }
                }
            }
        }


        [Serializable]
        public class ItemContainerPackage
        {
            public SerializedItem[] items;
            public SceneItem[] abandoned;
            public SerializedListDictionary picked;

            public ItemContainerPackage(ItemContainer container)
            {
                items = container.items.ToArray();
                abandoned = container.abandonedItems.ToArray();
                picked = new SerializedListDictionary(container.picked);
            }

            public void Resume(ItemContainer container)
            {
                container.items.Clear();
                container.items.AddRange(items);

                container.abandonedItems.Clear();
                container.abandonedItems.AddRange(abandoned);

                container.picked = picked.ToDictonary();
                container.Remap();
            }
        }
    }
}
