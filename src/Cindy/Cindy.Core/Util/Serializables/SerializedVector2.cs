using System;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    [Serializable]
    public class SerializedVector2
    {
        public double X;
        public double Y;

        public SerializedVector2()
        {

        }

        public SerializedVector2(Vector2 vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public Vector3 ToVector2()
        {
            return new Vector2((float)X, (float)Y);
        }

        public override string ToString()
        {
            return ToVector2().ToString();
        }
    }
}
