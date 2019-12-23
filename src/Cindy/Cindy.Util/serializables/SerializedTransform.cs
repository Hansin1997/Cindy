using System;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    [Serializable]
    public class SerializedTransform
    {
        public SerializedVector3 position, rotation, scale;

        public SerializedTransform(Transform transform)
        {
            position = new SerializedVector3(transform.position);
            rotation = new SerializedVector3(transform.rotation.eulerAngles);
            scale = new SerializedVector3(transform.localScale);
        }

        public void SetTransform(Transform target, bool position = true, bool rotation = true, bool scale = true)
        {
            if (position)
                target.position = this.position.ToVector3();
            if (rotation)
                target.rotation = Quaternion.Euler(this.rotation.ToVector3());
            if (scale)
                target.localScale = this.scale.ToVector3();
        }
    }
}
