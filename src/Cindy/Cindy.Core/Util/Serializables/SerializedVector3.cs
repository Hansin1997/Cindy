using System;
using UnityEngine;

namespace Cindy.Util.Serializables
{
    [Serializable]
    public class SerializedVector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public SerializedVector3()
        {

        }

        public SerializedVector3(Vector3 vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3((float)X, (float)Y, (float)Z);
        }

        public override string ToString()
        {
            return ToVector3().ToString();
        }
    }
}
