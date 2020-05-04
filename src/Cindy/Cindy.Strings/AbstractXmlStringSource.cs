using System;
using System.Collections;
using System.Xml;
using UnityEngine;

namespace Cindy.Strings
{
    /// <summary>
    /// 抽象Xml文本源
    /// </summary>
    public abstract class AbstractXmlStringSource : StringSource
    {
        /// <summary>
        /// 节点搜索的xpath
        /// </summary>
        public string xpath = "/strings/string[@key='{key}']";

        /// <summary>
        /// xpath中key的占位符
        /// </summary>
        public string keyVariable = "{key}";

        /// <summary>
        /// 节点值类型
        /// </summary>
        public NodeValueType valueType = NodeValueType.InnerXml;

        private XmlDocument document;

        private bool preparing = false;

        protected abstract IEnumerator PrepareXmlDocument(ResultAction<XmlDocument, Exception> resultAction);

        public override IEnumerator DoGet(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction)
        {
            if(document == null)
            {
                if (!preparing)
                {
                    preparing = true;

                    Exception E = null;
                    bool S = false;
                    yield return context.StartCoroutine(PrepareXmlDocument((doc, e, s) =>
                    {
                        document = doc;
                        E = e;
                        S = s;
                        preparing = false;
                    }));
                    if(!S)
                    {
                        resultAction?.Invoke(null, E, false);
                        yield break;
                    }
                }
                else
                {
                    yield return new WaitWhile(() => preparing);
                }
            }
            if(document == null)
            {
                resultAction?.Invoke(null, null, false);
                yield break;
            }
            try
            {
                string path = xpath.Replace(keyVariable, key);
                try
                {
                    XmlNode tmp = document.SelectSingleNode(path);
                    string result = null;
                    switch (valueType)
                    {
                        default:
                        case NodeValueType.InnerXml:
                            result = tmp.InnerXml;
                            break;
                        case NodeValueType.InnerText:
                            result = tmp.InnerText;
                            break;
                        case NodeValueType.OuterXml:
                            result = tmp.OuterXml;
                            break;
                    }
                    resultAction?.Invoke(result, null, true);
                }catch(Exception e)
                {
                    Debug.LogWarning(e);
                    resultAction?.Invoke(null, null, true);
                }
            }
            catch(Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
        }

        public enum NodeValueType
        {
            InnerXml,
            InnerText,
            OuterXml
        }
    }
}
