using UnityEngine;

namespace Cindy.Strings
{
    public abstract class StringSource : ScriptableObject, IStringSource
    {
        public abstract string Get(string key, string defaultValue = null);
    }
}
