using Cindy.Strings;
using Cindy.UI.Components;
using UnityEngine;

namespace Cindy.ItemSystem.UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/UI/ItemListView", 1)]
    public class ItemListView : ListView<ItemView, ItemContainer>
    {
        [Header("Strings")]
        public StringSource stringSource;

        protected override void GenerateItem(ItemContainer source)
        {
            foreach(SerializedItem item in source.items)
            {
                ItemView tmp = InstantiateTemplate();
                tmp.SetItem(item, source, stringSource);
            }
        }
    }
}
