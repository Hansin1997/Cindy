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

        public SerializedItem Item { get => item; }
        public ItemContainer Container { get => container; }

        public void SetItem(SerializedItem item,ItemContainer container,StringSource stringSource = null)
        {
            this.item = item;
            this.container = container;
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
                    target.text = stringSource.GetString(str, str);
            }
        }

        public void Abandon(int count = 1)
        {
            if (count < 0)
                return;
            container.AbandonItem(item.name, count);
        }
    }
}
