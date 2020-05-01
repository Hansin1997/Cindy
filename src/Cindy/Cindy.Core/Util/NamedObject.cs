using UnityEngine;

namespace Cindy
{
    public abstract class NamedObject : MonoBehaviour
    {
        public string Name { get { return GetName(); } }
        protected abstract string GetName();
    }
}
