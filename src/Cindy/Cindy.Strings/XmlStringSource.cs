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

        public ValueType valueType = ValueType.InnerXml;

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

        public override string Get(string key, string defaultValue = null)
        {
            if (asset == null)
                return defaultValue;
            string path = xpath.Replace(keyVariable, key);
            XmlNode tmp = Document.SelectSingleNode(path);
            if (tmp == null)
                return defaultValue;
            switch (valueType)
            {
                case ValueType.InnerXml:
                    return tmp.InnerXml;
                case ValueType.InnerText:
                    return tmp.InnerText;
                case ValueType.OuterXml:
                    return tmp.OuterXml;
                default:
                    return tmp.InnerXml;
            }
        }

        public enum ValueType
        {
            InnerXml,
            InnerText,
            OuterXml,
            OuterText
        }
    }
}
