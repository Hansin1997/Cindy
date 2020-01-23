using Cindy.Strings;
using Cindy.UI.Components;
using UnityEngine;

namespace Cindy.ItemSystem.UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/UI/ItemListView", 1)]
    public class ItemListView : ListView<ItemView, ItemContainer>
    {
        public bool refreshOnDataChange = true;

        [Header("Strings")]
        public StringSource stringSource;

        protected override void Start()
        {
            base.Start();
            source.dataChangeListener.AddListener(RefreshOnDataChange);
        }

        protected virtual void RefreshOnDataChange()
        {
            if (refreshOnDataChange)
            {
                Refresh();
            }
        }

        protected override void GenerateItem(ItemContainer source)
        {
            foreach(SerializedItem item in source.items)
            {
                ItemView tmp = InstantiateTemplate();
                tmp.SetItem(item, source, stringSource);
            }
        }

        protected void OnDestroy()
        {
            source.dataChangeListener.RemoveListener(RefreshOnDataChange);
        }
    }
}
