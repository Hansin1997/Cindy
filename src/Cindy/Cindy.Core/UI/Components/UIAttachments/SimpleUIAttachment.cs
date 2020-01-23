using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Components.UIAttachments
{
    [AddComponentMenu("Cindy/UI/Components/UIAttachments/SimpleUIAttachment")]
    public class SimpleUIAttachment : UIAttachment
    {
        [Header("Simple UI Attachment")]
        public bool actived = true;
        public RectTransform[] templates;

        public override RectTransform[] GenerateComponents(GameObject root)
        {
            List<RectTransform> instances = new List<RectTransform>();
            foreach(RectTransform template in templates)
            {
                GameObject instance =  Instantiate(template.gameObject,root.transform);
                instances.Add(instance.GetComponent<RectTransform>());
            }
            return instances.ToArray();
        }

        public override bool IsActived()
        {
            return actived;
        }
    }
}
