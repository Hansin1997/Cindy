using UnityEngine;
using UnityEngine.UI;

namespace Cindy.ItemSystem.UI
{

    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/ItemSystem/UI/ItemPreviews", 1)]
    public class ItemPreview : MonoBehaviour
    {
        [Header("UI Components")]
        public RawImage targetImage;
        public Camera sourceCamera;

        [Header("Others")]
        public string previewLayer = "UI";

        protected RenderTexture texture;
        protected SerializedItem item;
        protected bool reset = true;
        protected Item preview;

        // Start is called before the first frame update
        protected void Start()
        {
            ResetRendererTexture();
        }

        public void ResetRendererTexture()
        {
            if (texture != null)
                texture.Release();
            texture = new RenderTexture((int)targetImage.rectTransform.rect.width * 2, (int)targetImage.rectTransform.rect.height * 2, 10);
            targetImage.texture = texture;
            sourceCamera.targetTexture = texture;
        }

        public void SetItem(SerializedItem item)
        {
            if (item != null)
            {
                if (preview != null)
                {
                    Destroy(preview.gameObject);
                }
                preview = item.Preview(transform);
                preview.gameObject.layer = LayerMask.NameToLayer(previewLayer);
            }
        }
    }
}
