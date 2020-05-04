using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// Transform变量
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/TransformObject (Transform)")]
    public class TransformObject : VariableObject<Transform>
    {
        protected override void LoadFromStorage()
        {
            Debug.LogWarning("TransformObject not allow to load!");
        }

        public override void Save()
        {
            Debug.LogWarning("TransformObject not allow to save!");
        }
    }
}
