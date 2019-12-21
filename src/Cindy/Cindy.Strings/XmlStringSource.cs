using System.Xml;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "XmlStringSource", menuName = "Cindy/StringSources/XmlStringSource", order = 1)]
    public class XmlStringSource : StringSource
    {
        public TextAsset asset;

        public string xpath = "/strings/string[@key='{key}']";

        public string keyVariable = "{key}";

        protected XmlDocument document;

        protected XmlDocument Document
        {
            get
            {
                if (document == null && asset != null)
                {
                    document = new XmlDocument();
                    document.LoadXml(asset.text);
                }
                return document;
            }
        }

        public override string GetString(string key, string defaultValue = null)
        {
            if (asset == null)
                return defaultValue;
            string path = xpath.Replace(keyVariable, key);
            XmlNode tmp = Document.SelectSingleNode(path);
            if (tmp == null)
                return defaultValue;
            return tmp.InnerXml;
        }
    }
}
