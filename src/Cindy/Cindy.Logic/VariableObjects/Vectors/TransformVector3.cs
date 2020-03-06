using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/TransformVector3")]
    public class TransformVector3 : Vector3Object
    {
        public ReferenceTransfrom target;
        public TransfromType transfromType = TransfromType.Position;

        public override void SetValue(Vector3 value)
        {

        }

        public override Vector3 GetValue()
        {
            Transform t = target != null && target.Value != null ? target.Value : transform;
            switch (transfromType)
            {
                case TransfromType.Position:
                    value = t.position;
                    break;
                case TransfromType.Rotation:
                    value = t.rotation.eulerAngles;
                    break;
                case TransfromType.LocalScale:
                    value = t.localScale;
                    break;
                case TransfromType.lossyScale:
                    value = t.lossyScale;
                    break;
                case TransfromType.Forward:
                    value = t.forward;
                    break;
                case TransfromType.Back:
                    value = -t.forward;
                    break;
                case TransfromType.Left:
                    value = -t.right;
                    break;
                case TransfromType.Right:
                    value = t.right;
                    break;
                case TransfromType.Up:
                    value = t.up;
                    break;
                case TransfromType.Bottom:
                    value = -t.up;
                    break;
                default:
                    value = Vector3.zero;
                    break;
            }
            return base.GetValue();
        }

        public enum TransfromType
        {
            Position,
            Rotation,
            LocalScale,
            lossyScale,
            Forward,
            Back,
            Right,
            Left,
            Up,
            Bottom
        }
    }
}
