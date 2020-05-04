using Cindy.Util.Serializables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Cindy.Strings
{
    /// <summary>
    /// 字符串处理器，通过指定的起始终止符找到对应的文本源进行文本替换。
    /// </summary>
    [CreateAssetMenu(fileName = "StringSourceProcesser", menuName = "Cindy/StringSources/StringSourceProcesser", order = 99)]
    public class StringSourceProcesser : StringSource
    {
        /// <summary>
        /// 源文本源
        /// </summary>
        public StringSource originStringSource;

        /// <summary>
        /// 替换参数
        /// </summary>
        public KV[] parameterSources;

        private Dictionary<string, StringSource> map;

        private int oldHashCode;

        protected Dictionary<string, StringSource> Map
        {
            get
            {
                parameterSources.GetHashCode();
                if (map == null || parameterSources.GetHashCode() != oldHashCode)
                {
                    if (map == null)
                        map = new Dictionary<string, StringSource>();
                    else
                        map.Clear();
                    oldHashCode = parameterSources.GetHashCode();
                    foreach (KV kv in parameterSources)
                    {
                        if (kv.Key == null || kv.Key.Length != 2)
                        {
                            Debug.LogError("StringSourceProcesser Error: Parameter sources key must be two char.");
                            continue;
                        }
                        if (kv.Value == null)
                            continue;

                        if (kv.Key[0] == kv.Key[1])
                            throw new Exception("start char can't equal to end char!");
                        if (kv.Key[0] == 0 || kv.Key[1] == 0)
                            throw new Exception("start char or end char can't be empty!");
                        Map[("" + kv.Key[0] + kv.Key[1])] = kv.Value;
                    }
                }
                return map;
            }
        }

        class Parameters
        {
            public string input;
            public int start;
            public int length;
            public MonoBehaviour context;
            public ResultAction<string, Exception> resultAction;
        }

        IEnumerator Process(Parameters parameters)
        {
            StringBuilder builder = new StringBuilder(parameters.input.Length);
            StringSource[] handlerMap = new StringSource[256];
            char[] endCharMap = new char[256];
            try
            {
                if (Map == null || parameters.input == null || parameters.input.Trim().Length == 0)
                {
                    parameters.resultAction(parameters.input, null, true);
                    yield break;
                }
                foreach (KeyValuePair<string, StringSource> kv in map)
                {
                    char startChar = kv.Key[0], endChar = kv.Key[1];

                    handlerMap[startChar] = kv.Value;
                    endCharMap[startChar] = endChar;
                }
            }
            catch (Exception e)
            {
                parameters.resultAction(null, e, false);
                yield break;
            }

            int index = -1, count = 0;
            for (int i = parameters.start, right = parameters.start + parameters.length; i <= right; i++)
            {

                if (i == right)
                {
                    if (index != -1)
                        builder.Append(parameters.input, index, right - index);
                    break;
                }
                char c = parameters.input[i];

                if (index == -1)
                {
                    if (c <= 255 && handlerMap[c] != null)
                        index = i;
                    else
                        builder.Append(c);
                }
                else
                {
                    if (c == parameters.input[index])
                        count++;
                    else if (c == endCharMap[parameters.input[index]])
                    {

                        if (count == 0)
                        {
                            string tmp = null;
                            Exception tmp2 = null;
                            bool tmp3 = false;
                            ResultAction<string, Exception> ra = (r, e, isSuccess) =>
                            {
                                tmp = r;
                                tmp2 = e;
                                tmp3 = isSuccess;
                            };

                            Parameters p = new Parameters()
                            {
                                input = parameters.input,
                                start = index + 1,
                                length = i - index - 1,
                                context = parameters.context,
                                resultAction = ra
                            };

                            yield return parameters.context.StartCoroutine(Process(p));

                            if (!tmp3)
                            {
                                parameters.resultAction(tmp, tmp2, tmp3);
                                yield break;
                            }

                            StringSource s = handlerMap[parameters.input[index]];
                            yield return s.DoGet(tmp, parameters.context, (r, e, isSuccess) =>
                            {
                                tmp = r;
                                tmp2 = e;
                                tmp3 = isSuccess;
                            });

                            if (!tmp3)
                            {
                                parameters.resultAction(tmp, tmp2, tmp3);
                                yield break;
                            }
                            if(tmp != null)
                                builder.Append(tmp);
                            index = -1;
                        }
                        else
                            count--;
                    }
                }
            }
            parameters.resultAction(builder.ToString(), null, true);
        }

        public override IEnumerator DoGet(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction)
        {
            string result = null;
            Exception exception = null;
            bool isSuccess = false;
            yield return originStringSource.DoGet(key, context, (r, e, s) => { result = r;exception = e;isSuccess = s; });
            if (!isSuccess)
            {
                resultAction(result, exception, false);
                yield break;
            }
            yield return context.StartCoroutine(Process(new Parameters()
            {
                input = result,
                start = 0,
                length = result.Length,
                context = context,
                resultAction = resultAction
            }));
        }

        [Serializable]
        public class KV : SerializedKeyValuePair<string,StringSource>
        {
            public KV(KeyValuePair<string, StringSource> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }
}
