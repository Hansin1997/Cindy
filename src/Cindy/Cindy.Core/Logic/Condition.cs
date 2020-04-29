using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic
{
    public abstract class Condition : MonoBehaviour
    {
        public ReferenceString conditionName;

        /// <summary>
        /// 检查条件是否成立
        /// </summary>
        /// <returns>条件是否成立</returns>
        public abstract bool Check();

    }
}
