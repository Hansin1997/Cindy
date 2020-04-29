using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/TransformObject")]
    public class TransformObject : VariableObject<Transform>
    {
        protected override Transform TransformFrom(string value)
        {
            return null;
        }

        protected override object TransfromTo(Transform value)
        {
            return null;
        }

        protected override void LoadFromStorage()
        {

        }

        public override void Save()
        {

        }
    }
}
