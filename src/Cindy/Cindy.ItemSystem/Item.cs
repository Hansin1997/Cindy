using UnityEngine;

namespace Cindy.ItemSystem
{
    public class Item : MonoBehaviour
    {
        public SerializedItem item;

        protected virtual void Start()
        {
            if (item.entityName == null || item.entityName.Trim().Length == 0)
                item.entityName = gameObject.name;
        }
    }
}
