using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.VariableObjects.Vectors
{
    /// <summary>
    /// Vector3投影值
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Vectors/ProjectVector3Value (Float)")]
    public class ProjectVector3Value : FloatObject
    {
        /// <summary>
        /// 向量
        /// </summary>
        public ReferenceVector3 vector;
        /// <summary>
        /// 法线
        /// </summary>
        public ReferenceVector3 normal;

        protected virtual void Start()
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
