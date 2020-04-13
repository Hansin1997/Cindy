using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/ProjectVector3 (Vector3)")]
    public class ProjectVector3 : Vector3Object
    {
        public ReferenceVector3 vector, normal;

        public override void SetValue(Vector3 value)
        {

        }

        public override Vector3 GetValue()
        {
            value = Vector3.Project(vector.Value, normal.Value);
            return base.GetValue();
        }
    }
}
