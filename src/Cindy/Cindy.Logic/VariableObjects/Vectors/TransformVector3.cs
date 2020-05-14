using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    /// <summary>
    /// Transform各种向量: 位置、旋转、缩放
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/TransformVector3 (Vector3)")]
    public class TransformVector3 : Vector3Object
    {
        public ReferenceTransfrom target;
        public TransfromType transfromType = TransfromType.Position;

        protected void Apply(Vector3 value)
        {
            Transform t = target != null && target.Value != null ? target.Value : transform;
            switch (transfromType)
            {
                case TransfromType.Position:
                    t.position = value;
                    break;
                case TransfromType.Rotation:
                    t.rotation = Quaternion.Euler(value);
                    break;
                case TransfromType.LocalScale:
                    t.localScale = value;
                    break;
                case TransfromType.lossyScale:
                    break;
                case TransfromType.Forward:
                    t.forward = value;
                    break;
                case TransfromType.Back:
                    t.forward = -value;
                    break;
                case TransfromType.Left:
                    t.right = -value;
                    break;
                case TransfromType.Right:
                    t.right = value;
                    break;
                case TransfromType.Up:
                    t.up = value;
                    break;
                case TransfromType.Bottom:
                    t.up = -value;
                    break;
            }
        }

        public override void SetValue(Vector3 value)
        {
            Apply(value);
            base.SetValue(value);
        }

        protected override void OnValueChanged(bool save = true, bool notify = true)
        {
            Apply(value);
            base.OnValueChanged(save, notify);
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
