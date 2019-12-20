using UnityEngine;

namespace Cindy.Strings
{
    public abstract class StringSource : ScriptableObject
    {
        public abstract string GetString(string key, string defaultValue = default);

    }
}
