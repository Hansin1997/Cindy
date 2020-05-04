using UnityEngine;

namespace Cindy
{
    /// <summary>
    /// 支持命名的MonoBehaviour
    /// </summary>
    public abstract class NamedBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Behaviour的名称。
        /// </summary>
        public string Name { get { return GetName(); } }
        /// <summary>
        /// 获取名称。
        /// </summary>
        /// <returns></returns>
        protected abstract string GetName();
    }
}
