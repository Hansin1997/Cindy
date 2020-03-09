using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic
{
    public abstract class Condition : MonoBehaviour
    {
        public ReferenceString conditionName;

        public abstract bool Check();

    }
}
