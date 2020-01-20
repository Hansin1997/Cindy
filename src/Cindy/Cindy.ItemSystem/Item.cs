using UnityEngine;

namespace Cindy.ItemSystem
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/Item", 2)]
    public class Item : MonoBehaviour
    {
        public SerializedItem item;
        [Header("Actions After Pick")]
        public bool destroySelf = true;
        public GameObject[] destroyList = new GameObject[] { };

        protected virtual void Start()
        {
            if (item.entityName == null || item.entityName.Trim().Length == 0)
                item.entityName = gameObject.name;
        }

        public virtual void AfterPick()
        {
            foreach (GameObject obj in destroyList)
                Destroy(obj);
            if (destroySelf)
                Destroy(gameObject);
        }

        public virtual void Pick(ItemContainer container)
        {
            if (container == null)
                return;
            container.AddItem(this,name);
            AfterPick();
        }

    }
}
