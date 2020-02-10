using Cindy.UI.Pages;
using UnityEngine;

namespace Cindy.ItemSystem.UI
{
    [AddComponentMenu("Cindy/ItemSystem/UI/ItemDetailPage", 1)]
    public class ItemDetailPage : Page
    {
        public ItemContainer itemContainer;
        public SerializedItem item;

    }
}
