using UnityEngine;

namespace Cindy.Strings
{
    public abstract class StringSources : ScriptableObject
    {
        public string defaultLanguage = "";

        public abstract string GetString(string key, string language, string defaultValue = default);

        public string GetStringDefaultLanguage(string key,string defaultValue = default)
        {
            return GetString(key,defaultLanguage,defaultValue);
        }
    }
}
