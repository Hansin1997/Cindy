using Cindy.Storages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder.Sources
{
    /// <summary>
    /// 存储器数据源
    /// </summary>
    [AddComponentMenu("Cindy/UI/Binder/DataSources/StorageDataSource")]
    public class StorageDataSource : AbstractDataSource
    {
        public AbstractStorage storage;

        public override bool IsReadable()
        {
            return true;
        }

        public override bool IsWriteable()
        {
            return true;
        }

        protected override IEnumerator DoGetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction)
        {
            Coroutine c;
            try
            {
                c = context.StartCoroutine(storage.DoGet(key, (temp, e, isSuccess) =>
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
                c = context.StartCoroutine(storage.DoGetMultiple((arr, e, isSuccess) =>
                {
                    if (isSuccess)
                    {
                        try
                        {
                            List<T> results = new List<T>();
                            foreach (string temp in arr)
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
                }, (p) => { }, keys));
            }catch(Exception e)
            {
                resultAction?.Invoke(null, e, false);
                yield break;
            }
            yield return c;
        }

        protected override IEnumerator DoSetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action)
        {
            Coroutine c;
            try
            {
                c = context.StartCoroutine(storage.DoPut(key, value.ToString(), action));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
                yield break;
            }
            yield return c;
        }

        protected override IEnumerator DoSetDataMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Coroutine c = null;
            try
            {
                foreach(KeyValuePair<string,object> kv in keyValuePairs)
                {
                    dic[kv.Key] = kv.Value.ToString();
                }
                c = context.StartCoroutine(storage.DoPutMultiple(dic, action, (p) => { }));
            }
            catch(Exception e)
            {
                action?.Invoke(false, e);
                yield break;
            }
            yield return c;
        }
    }
}