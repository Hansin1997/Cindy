using Cindy.Strings;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy.ItemSystem.UI
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/UI/ItemView", 1)]
    public class ItemView : MonoBehaviour
    {
        [Header("UI Component")]
        public Text itemName;
        public Text itemContent;
        public Text itemAmount;

        protected SerializedItem item;
        protected ItemContainer container;
        protected StringSource stringSource;

        public SerializedItem Item { get => item; }
        public ItemContainer Container { get => container; }

        public void SetItem(SerializedItem item,ItemContainer container,StringSource stringSource = null)
        {
            this.item = item;
            this.container = container;
            this.stringSource = stringSource;
            if(item != null)
            {
                SetText(itemName, item.name, stringSource);
                SetText(itemContent, item.content, stringSource);
                SetText(itemAmount, item.amount.ToString());
            }
        }

        protected void SetText(Text target, string str, StringSource stringSource = null)
        {
            if (target != null)
            {
                if (stringSource == null)
                    target.text = str;
                else
                    target.text = stringSource.Get(str, str);
            }
        }

        public Item Abandon(int count = 1,Transform position = null)
        {
            if (count < 0)
                return null;
            Item instance = container.AbandonItem(item.name, count);
            if (instance != null)
            {
                if (position == null)
                    position = container.transform;
                instance.transform.position = position.position;
            }
            return instance;
        }

        public void AbandonWithoutResult(int amount = 1)
        {
            Abandon(amount,null);
        }

        public void ShowDetail(ItemDetailPage itemDetailPage)
        {
            itemDetailPage.itemContainer = container;
            itemDetailPage.item = item;
            itemDetailPage.ShowAndReturn<ItemDetailPage>();
        }

        public void ShowPreview(ItemPreview itemPreview)
        {
            itemPreview.SetItem(item);
        }

        public void ShowItemName(Text text)
        {
            SetText(text, item.name, stringSource);
        }

        public void ShowItemContent(Text text)
        {
            SetText(text, item.content, stringSource);
        }
    }
}
