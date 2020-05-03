using Cindy.Logic.ReferenceValues;
using Cindy.Util.Serializables;
using System;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Vector3Object (Vector3)")]
    public class Vector3Object : VariableObject<Vector3>
    {
        public override Type GetStorableObjectType()
        {
            return typeof(SerializedVector3);
        }

        public override object GetStorableObject()
        {
            return new SerializedVector3(value);
        }

        protected override bool TramsformValue(object obj, out Vector3 output)
        {
            if(obj is SerializedVector3 s)
            {
                output = s.ToVector3();
                return true;
            }
            else
            {
                output = default;
                return false;
            }
        }

        public void SetPosition(Transform transform)
        {
            if (transform != null)
                SetValue(transform.position);
        }

        public void SetRotation(Transform transform)
        {
            if (transform != null)
                SetValue(transform.rotation.eulerAngles);
        }

        public void SetScale(Transform transform)
        {
            if (transform != null)
                SetValue(transform.localScale);
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector3Value (Float)")]
    public class Vector3Value : FloatObject
    {
        [Header("Vector3Value")]
        public ReferenceVector3 vector;
        public Vector3Components component;

        protected void ApplyToVector(float value)
        {
            Vector3 v = vector.Value;
            switch (component)
            {
                case Vector3Components.X:
                    v.x = value;
                    break;
                case Vector3Components.Y:
                    v.y = value;
                    break;
                case Vector3Components.Z:
                    v.z = value;
                    break;
            }
            vector.Value = v;
        }

        public override void SetValue(float value)
        {
            ApplyToVector(value);
            base.SetValue(value);
        }

        protected override void OnValueChanged(bool save = true, bool notify = true)
        {
            ApplyToVector(value);
            base.OnValueChanged(save, notify);
        }

        protected override void OnValueLoad(float val)
        {
            ApplyToVector(val);
            base.OnValueLoad(val);
        }

        protected override void OnValueLoadEmpty()
        {
            GetValue();
        }

        public override float GetValue()
        {
            switch (component)
            {
                case Vector3Components.X:
                    value = vector.Value.x;
                    break;
                case Vector3Components.Y:
                    value = vector.Value.y;
                    break;
                case Vector3Components.Z:
                    value = vector.Value.z;
                    break;
            }
            return base.GetValue();
        }

        public enum Vector3Components
        {
            X,Y,Z
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector3Magnitude (Float)")]
    public class Vector3Magnitude : FloatObject
    {
        [Header("Vector3Magnitude")]
        public ReferenceVector3 vector;

        public override void SetValue(float value)
        {

        }

        public override float GetValue()
        {
            value = vector.Value.magnitude;
            return base.GetValue();
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/RelativedVector3 (Vector3)")]
    public class RelativedVector3 : Vector3Object
    {
        [Header("RelativedVector3")]
        public ReferenceVector3 start, end;

        public override void SetValue(Vector3 value)
        {

        }

        public override Vector3 GetValue()
        {
            value = end.Value - start.Value;
            return base.GetValue();
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector2Object (Vector2)")]
    public class Vector2Object : VariableObject<Vector2>
    {
        public override Type GetStorableObjectType()
        {
            return typeof(SerializedVector2);
        }

        public override object GetStorableObject()
        {
            return new SerializedVector2(value);
        }

        protected override bool TramsformValue(object obj, out Vector2 output)
        {
            if(obj is SerializedVector2 v)
            {
                output = v.ToVector2();
                return true;
            }
            else
            {
                output = default;
                return false;
            }
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/Vector2Value (Vector2)")]
    public class Vector2Value : FloatObject
    {
        [Header("Vector2Value")]
        public ReferenceVector2 vector;
        public Vector2Components component;

        protected void ApplyToVector(float value)
        {
            Vector2 v = vector.Value;
            switch (component)
            {
                case Vector2Components.X:
                    v.x = value;
                    break;
                case Vector2Components.Y:
                    v.y = value;
                    break;
            }
            vector.Value = v;
        }

        public override void SetValue(float value)
        {
            ApplyToVector(value);
            base.SetValue(value);
        }

        protected override void OnValueChanged(bool save = true, bool notify = true)
        {
            ApplyToVector(value);
            base.OnValueChanged(save, notify);
        }

        protected override void OnValueLoad(float val)
        {
            ApplyToVector(value);
            base.OnValueLoad(val);
        }

        protected override void OnValueLoadEmpty()
        {
            GetValue();
        }

        public override float GetValue()
        {
            switch (component)
            {
                case Vector2Components.X:
                    value = vector.Value.x;
                    break;
                case Vector2Components.Y:
                    value = vector.Value.y;
                    break;
            }
            return base.GetValue();
        }

        public enum Vector2Components
        {
            X, Y
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/RelativedVector2 (Vector2)")]
    public class RelativedVector2 : Vector2Object
    {
        [Header("RelativedVector2")]
        public ReferenceVector2 start, end;

        public override void SetValue(Vector2 value)
        {

        }

        public override Vector2 GetValue()
        {
            value = end.Value - start.Value;
            return base.GetValue();
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector2Magnitude (Float)")]
    public class Vector2Magnitude : FloatObject
    {
        [Header("Vector2Magnitude")]
        public ReferenceVector2 vector;

        public override void SetValue(float value)
        {

        }

        public override float GetValue()
        {
            value = vector.Value.magnitude;
            return base.GetValue();
        }
    }
}
