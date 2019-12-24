using Cindy.Util.Serializables;
using System;
using UnityEngine;

namespace Cindy.Storages.Storables
{
    [AddComponentMenu("Cindy/Storage/StorableObject/TransformStorableObject", 1)]
    [DisallowMultipleComponent]
    public class TransformStorableObject : AbstractStorableObject
    {

        [Serializable]
        public class TransformOption
        {
            public bool position = true, rotation = true, scale = true;
        }

        public TransformOption transformOptions;

        public override object GetStorableObject()
        {
            return new SerializedTransform(transform);
        }

        public override Type GetStorableObjectType()
        {
            return typeof(SerializedTransform);
        }

        public override void OnPutStorableObject(object obj)
        {
            bool active = gameObject.activeSelf;
            gameObject.SetActive(false);
            if (obj is SerializedTransform serializedTransform)
                serializedTransform.SetTransform(transform, transformOptions.position, transformOptions.rotation, transformOptions.scale);
            gameObject.SetActive(active);
        }
    }

}
