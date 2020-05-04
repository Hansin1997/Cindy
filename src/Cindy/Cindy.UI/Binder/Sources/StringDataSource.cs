using Cindy.Strings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder
{
    /// <summary>
    /// 文本数据源
    /// </summary>
    [AddComponentMenu("Cindy/UI/Binder/DataSources/StringDataSource")]
    public class StringDataSource : AbstractDataSource
    {
        public StringSource stringSource;

        public override bool IsReadable()
        {
            return true;
        }

        public override bool IsWriteable()
        {
            return false;
        }

        protected override IEnumerator DoGetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction)
        {
            Coroutine c;
            try
            {

                c = context.StartCoroutine(stringSource.DoGet(key, this, (temp, e, isSuccess) =>
                {
                    if (isSuccess)
                    {
                        try
                        {
                            if (temp is T r)
                            {
                                resultAction(r, e, true);
                            }
                            else
                            {
                                resultAction?.Invoke(JSON.FromJson<T>(temp), null, true);
                            }
                        }
                        catch (Exception e1)
                        {
                            resultAction?.Invoke(default, e1, false);
                        }
                    }
                    else
                    {
                        resultAction?.Invoke(default, e, false);
                    }
                }));
            }
            catch (Exception e)
            {
                resultAction?.Invoke(default, e, false);
                yield break;
            }
            yield return c;
        }

        protected override IEnumerator DoGetDataMultiple<T>(string[] keys, MonoBehaviour context, ResultAction<T[], Exception> resultAction)
        {
            Coroutine c;
            try
            {
                c = context.StartCoroutine(stringSource.DoGetMultiple(keys, this, (arr, e, isSuccess) =>
                {
                    if (isSuccess)
                    {
                        try
                        {
                            List<T> results = new List<T>();
                            foreach(string temp in arr)
                            {
                                if (temp is T r)
                                {
                                    results.Add(r);
                                }
                                else
                                {
                                    results.Add(JSON.FromJson<T>(temp));
                                }
                            }
                            resultAction?.Invoke(results.ToArray(), null, true);
                        }
                        catch (Exception e1)
                        {
                            resultAction?.Invoke(default, e1, false);
                        }
                    }
                    else
                    {
                        resultAction?.Invoke(default, e, false);
                    }
                }));
            }
            catch (Exception e)
            {
                resultAction?.Invoke(default, e, false);
                yield break;
            }
            yield return c;
        }

        protected override IEnumerator DoSetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action)
        {
            Debug.LogWarning("StringDataSource is readonly", this);
            action?.Invoke(false, null);
            yield return null;
        }

        protected override IEnumerator DoSetDataMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action)
        {
            Debug.LogWarning("StringDataSource is readonly", this);
            action?.Invoke(false, null);
            yield return null;
        }
    }
}
