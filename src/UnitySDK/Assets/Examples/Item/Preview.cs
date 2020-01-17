using Cindy.ItemSystem;
using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour
{

    public RawImage image;
    public ItemContainer container;
    public Camera cam;
    public string previewLayer = "UI";
    protected RenderTexture texture;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(container.items.Count > 0)
        {
            if(texture == null)
            {

                Vector2 size = image.rectTransform.sizeDelta;
                texture = new RenderTexture((int)image.rectTransform.rect.width * 2, (int)image.rectTransform.rect.height * 2, 10);
                image.texture = texture;
                GameObject preview = container.items[0].Preview();
                if (preview == null)
                    return;
                preview.layer = LayerMask.NameToLayer(previewLayer);
                preview.transform.position = Camera.main.transform.position - Camera.main.transform.forward * 5;
                
                cam.transform.LookAt(preview.transform);
                cam.targetTexture = texture;
            }
        }
    }
}
