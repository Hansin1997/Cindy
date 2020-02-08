using UnityEngine;

namespace Cindy.ItemSystem
{
    [AddComponentMenu("Cindy/ItemSystem/ItemPreviewer", 1)]
    public class ItemPreviewer : MonoBehaviour
    {
        public string previewTag;

        protected SerializedItem item;

        protected Item previewObject;

        public bool IsEmpty { get{ return previewObject == null; } }

        public void Preview(SerializedItem item)
        {
            if (!IsEmpty)
                CancelPreview();
            this.item = item;
            if(item != null)
            {
                previewObject = item.Preview(transform);
            }
        }

        public void CancelPreview()
        {
            if (previewObject == null)
                return;
            Destroy(previewObject.gameObject);
            item = null;
            previewObject = null;
        }

        public static ItemPreviewer FindPreviewer(string previewTag,Transform parent = null,bool requireEmpty = true)
        {
            ItemPreviewer[] itemPreviewers;
            if (parent == null)
                itemPreviewers = FindObjectsOfType<ItemPreviewer>();
            else
                itemPreviewers = parent.GetComponentsInChildren<ItemPreviewer>();
            foreach(ItemPreviewer previewer in itemPreviewers)
            {
                if (previewer.previewTag.Equals(previewTag) && (!requireEmpty || previewer.IsEmpty))
                    return previewer;
            }
            return null;
        }
    }
}
