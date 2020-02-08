using Cindy.Logic.ReferenceValues;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.PositionBinders
{
    [AddComponentMenu("Cindy/UI/PositionBinders/SimplePositionBinder")]
    public class SimplePositionBinder : AbstractPositionBinder
    {
        [Header("Simple UI Attachment")]
        public ReferenceBool actived = new ReferenceBool() { value = true };
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
            return actived.Value;
        }
    }
}
