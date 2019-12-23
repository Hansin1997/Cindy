using Cindy.Strings;
using UnityEngine;
using UnityEngine.UI;

namespace Cindy
{
    public class TextTest : MonoBehaviour
    {
        public Text text;
        [Multiline]
        public string key;
        public StringSource stringSource;



        protected void Update()
        {
            if(stringSource != null && text != null && key != null)
            {
                text.text = stringSource.GetString(key, key);
            }
        }

    } 
}
