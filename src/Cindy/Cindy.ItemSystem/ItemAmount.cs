using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using Cindy.Util;
using UnityEngine;

namespace Cindy.ItemSystem
{
    [AddComponentMenu("Cindy/ItemSystem/ItemAmount", 1)]
    public class ItemAmount : IntObject
    {
        [Header("ItemAmount")]
        public ReferenceString itemContainerName;
        public ReferenceString itemName;

        protected ItemContainer c;

        public override int GetValue()
        {
            if(c == null)
                c = Finder.Find<ItemContainer>(itemContainerName.Value);
            SerializedItem serializedItem = null;
            if (c != null && (serializedItem = c.GetItem(itemName.Value) ) != null)
            {
                value = serializedItem.amount;
            }
            else
            {
                value = 0;
            }
            return base.GetValue();
        }

        public override void SetValue(int value)
        {

        }
    }
}
