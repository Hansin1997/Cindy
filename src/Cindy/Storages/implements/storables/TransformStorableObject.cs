using System;
using Cindy.serializables;

namespace Cindy.Storages
{
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
            if (obj is SerializedTransform serializedTransform)
                serializedTransform.SetTransform(transform, transformOptions.position, transformOptions.rotation, transformOptions.scale);

        }
    }

}
