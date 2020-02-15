using Cindy.Logic;
using Cindy.Logic.ReferenceValues;
using Cindy.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.ItemSystem
{
    [AddComponentMenu("Cindy/ItemSystem/ItemCustomer", 1)]
    public class ItemCustomer : LogicNode
    {
        [Header("ItemCustomer")]
        public ReferenceString itemContainerName;
        public ReferenceString itemName;
        public ReferenceInt amount;
        public UnityEvent onSucess;
        public UnityEvent onFail;

        protected ItemContainer container;


        protected override void Run()
        {
            if(container == null)
                container = Finder.Find<ItemContainer>(itemContainerName.Value);
            if(container != null)
            {
                SerializedItem item = container.Custom(itemName.Value, amount.Value);
                if (item == null)
                    onFail.Invoke();
                else
                {
                    onSucess.Invoke();
                }
            }
            else
            {
                onFail.Invoke();
            }
        }
    }
}
