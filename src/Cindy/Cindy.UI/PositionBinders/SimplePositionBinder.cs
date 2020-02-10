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
        public bool keepRelativePosition = true;
        protected Vector3 orginVector;

        protected override void Start()
        {
            if(transform.parent != null)
            {
                orginVector = transform.position - transform.parent.position;
            }
            base.Start();
        }

        public override RectTransform[] GenerateComponents(GameObject root)
        {
            List<RectTransform> instances = new List<RectTransform>();
            foreach(RectTransform template in templates)
            {
                GameObject instance =  Instantiate(template.gameObject,root.transform);
                instance.name = template.name;
                instances.Add(instance.GetComponent<RectTransform>());
            }
            return instances.ToArray();
        }

        protected virtual void Update()
        {
            if (keepRelativePosition && transform.parent != null)
                transform.position = transform.parent.position + orginVector;
        }

        public override bool IsActived()
        {
            return actived.Value;
        }
    }
}
