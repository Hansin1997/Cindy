using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/ProjectVector3Value (Float)")]
    public class ProjectVector3Value : FloatObject
    {
        public ReferenceVector3 vector, normal;

        protected override void Start()
        {
            GetValue();
        }

        public override void SetValue(float value)
        {

        }

        public override float GetValue()
        {
            Vector3 v = Vector3.Project(vector.Value, normal.Value);
            if (Vector3.Angle(v, normal.Value) > 90)
            {
                value = -v.magnitude;
            }
            else
            {
                value = v.magnitude;
            }
            return base.GetValue();
        }
    }
}
