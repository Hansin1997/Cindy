using Cindy.Logic.ReferenceValues;
using Cindy.Util.Serializables;
using System;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Vector3Object")]
    public class Vector3Object : VariableObject<Vector3>
    {
        protected override Vector3 TransformFrom(string value)
        {
            try
            {
                return JsonUtility.FromJson<SerializedVector3>(value).ToVector3();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return Vector3.zero;
            }
        }

        protected override object TramsfromTo(Vector3 value)
        {
            return new SerializedVector3(value);
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

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector3Magnitude")]
    public class Vector3Magnitude : FloatObject
    {
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

    [AddComponentMenu("Cindy/Logic/VariableObject/RelativedVector3")]
    public class RelativedVector3 : Vector3Object
    {
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

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector2Object")]
    public class Vector2Object : VariableObject<Vector2>
    {
        protected override Vector2 TransformFrom(string value)
        {
            try
            {
                return JsonUtility.FromJson<SerializedVector2>(value).ToVector2();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return Vector3.zero;
            }
        }

        protected override object TramsfromTo(Vector2 value)
        {
            return new SerializedVector2(value);
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/RelativedVector2")]
    public class RelativedVector2 : Vector2Object
    {
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

    [AddComponentMenu("Cindy/Logic/VariableObject/Vector2Magnitude")]
    public class Vector2Magnitude : FloatObject
    {
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
