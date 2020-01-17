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

        public void SetItem(SerializedItem item,StringSource stringSource = null)
        {
            this.item = item;
            if(item == null)
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
    }
}
