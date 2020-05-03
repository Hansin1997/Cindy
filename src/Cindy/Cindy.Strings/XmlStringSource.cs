using System;
using System.Collections;
using System.Xml;
using UnityEngine;

namespace Cindy.Strings
{
    [CreateAssetMenu(fileName = "XmlStringSource", menuName = "Cindy/StringSources/XmlStringSource", order = 1)]
    public class XmlStringSource : AbstractXmlStringSource
    {
        public TextAsset asset;

        protected override IEnumerator PrepareXmlDocument(ResultAction<XmlDocument, Exception> resultAction)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(asset.text);
                resultAction?.Invoke(doc, null, true);
            }catch(Exception e)
            {
                resultAction?.Invoke(null,e,false);
            }
            yield return null;
        }
    }
}