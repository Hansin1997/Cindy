using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    /// <summary>
    /// Vector3投影
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/ProjectVector3 (Vector3)")]
    public class ProjectVector3 : Vector3Object
    {
        /// <summary>
        /// 向量
        /// </summary>
        public ReferenceVector3 vector;
        /// <summary>
        /// 法线
        /// </summary>
        public ReferenceVector3 normal;

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