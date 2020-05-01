using System;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    [Serializable]
    public class SerializedTransform
    {
        public SerializedVector3 Position { get; set; }
        public SerializedVector3 Rotation { get; set; }
        public SerializedVector3 Scale { get; set; }

        public SerializedTransform()
        {
            
        }

        public SerializedTransform(Transform transform)
        {
            Position = new SerializedVector3(transform.position);
            Rotation = new SerializedVector3(transform.rotation.eulerAngles);
            Scale = new SerializedVector3(transform.localScale);
        }

        public void SetTransform(Transform target, bool position = true, bool rotation = true, bool scale = true)
        {
            if (position)
                target.position = this.Position.ToVector3();
            if (rotation)
                target.rotation = Quaternion.Euler(this.Rotation.ToVector3());
            if (scale)
                target.localScale = this.Scale.ToVector3();
        }
    }
}
