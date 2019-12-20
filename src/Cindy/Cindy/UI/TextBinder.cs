using Cindy.Strings;
using UnityEngine;

namespace Cindy.UI
{
    [AddComponentMenu("Cindy/Test/TextBinder", 1)]
    public class TextBinder : MonoBehaviour
    {
        public string key;
        public StringSource source;

        public void Start()
        {
            if (source != null && key != null)
                Debug.Log(source.GetString(key));
        }
    }
}
