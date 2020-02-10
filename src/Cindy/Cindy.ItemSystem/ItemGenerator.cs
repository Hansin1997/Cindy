using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.ItemSystem
{

    [AddComponentMenu("Cindy/ItemSystem/ItemGenerator", 1)]
    public class ItemGenerator : LogicNode
    {
        public ReferenceInt count;
        public ReferenceInt amount;
        public Item template;
        public Transform[] transforms;

        protected HashSet<Item> items;

        public UnityEvent emptyListener;

        protected override void Run()
        {
            if (items == null)
                items = new HashSet<Item>();
            if(template != null)
            {
                for (int i = 0;i < count.Value; i++){
                    Item item = template.item.Instantiate(RandomTransform());
                    item.pickListener.AddListener(() =>
                    {
                        if (items != null)
                        {
                            items.Remove(item);
                            if (items.Count == 0)
                                emptyListener.Invoke();
                        }
                    });
                    items.Add(item);
                }
            }
        }

        protected virtual Transform RandomTransform()
        {
            if (transforms.Length == 0)
                return transform;
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}
