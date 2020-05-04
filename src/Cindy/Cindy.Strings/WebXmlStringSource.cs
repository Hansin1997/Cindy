using System;
using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

namespace Cindy.Strings
{
    /// <summary>
    /// Web Xml文本源
    /// </summary>
    [CreateAssetMenu(fileName = "WebXmlStringSource", menuName = "Cindy/StringSources/WebXmlStringSource", order = 1)]
    public class WebXmlStringSource : AbstractXmlStringSource
    {
        public string url;

        protected override IEnumerator PrepareXmlDocument(ResultAction<XmlDocument, Exception> resultAction)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    resultAction?.Invoke(null, new Exception(webRequest.error), false);
                    yield break;
                }
                else
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(webRequest.downloadHandler.text);
                        resultAction?.Invoke(doc, null, true);
                    }
                    catch (Exception e)
                    {
                        resultAction?.Invoke(null, e, false);
                    }
                    yield break;
                }

            }
        }
    }
}