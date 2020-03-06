using System;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    [Serializable]
    public class SerializedVector2
    {
        public double x, y;

        public SerializedVector2(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        public Vector3 ToVector2()
        {
            return new Vector2((float)x, (float)y);
        }

        public override string ToString()
        {
            return ToVector2().ToString();
        }
    }


}
