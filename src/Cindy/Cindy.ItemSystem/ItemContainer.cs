using Cindy.Storages;
using System;
using System.Collections.Generic;

namespace Cindy.ItemSystem
{
    public class ItemContainer : AbstractStorableObject
    {
        public List<SerializedItem> items;
        protected Dictionary<string, SerializedItem> map;

        public void AddItem(SerializedItem item)
        {
            if (item == null)
                return;
            if (map.ContainsKey(item.name))
                map[item.name].Add(item.amount);
            else
            {
                map[item.name] = item;
                items.Add(item);
            }
        }

        public override object GetStorableObject()
        {
            throw new NotImplementedException();
        }

        public override Type GetStorableObjectType()
        {
            throw new NotImplementedException();
        }

        public override void OnPutStorableObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
