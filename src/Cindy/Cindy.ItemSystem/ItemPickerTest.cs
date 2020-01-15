using UnityEngine;

namespace Cindy.ItemSystem
{
    public class ItemPickerTest : MonoBehaviour
    {
        public ItemContainer container;
        public string pickUpButton = "Submit";

        protected virtual void Start()
        {
            if (container == null)
                container = GetComponent<ItemContainer>();
            if (container == null)
                container = FindObjectOfType<ItemContainer>();
        }
        protected virtual void Update()
        {
            if (VirtualInput.GetButtonDown(pickUpButton) && container != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(VirtualInput.GetMousePosition());
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    Item item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        container.AddItem(item.item);
                    }
                }
            }
        }
    }
}
