using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder
{
    /// <summary>
    /// 抽象数据源
    /// </summary>
    public abstract class AbstractDataSource : MonoBehaviour, IDataSource
    {
        public abstract bool IsReadable();
        public abstract bool IsWriteable();
        protected abstract IEnumerator DoGetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction);
        protected abstract IEnumerator DoSetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action);
        protected virtual IEnumerator DoGetDataMultiple<T>(string[] keys, MonoBehaviour context, ResultAction<T[], Exception> resultAction)
        {
            List<T> result = new List<T>();
            foreach (string key in keys)
            {
                Coroutine c;
                T t1 = default;
                Exception t2 = null;
                bool t3 = false;
                try
                {
                    c = context.StartCoroutine(DoGetData<T>(key, context, (r, e, s) =>
                    {
                        t1 = r;
                        t2 = e;
                        t3 = s;
                    }
                    ));
                }
                catch (Exception e)
                {
                    resultAction?.Invoke(null, e, false);
                    yield break;
                }
                yield return c;
                if (!t3)
                {
                    resultAction?.Invoke(null, t2, false);
                    yield break;
                }
                result.Add(t1);
            }
            resultAction?.Invoke(result.ToArray(), null, true);
        }
        protected virtual IEnumerator DoSetDataMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action)
        {
            foreach (KeyValuePair<string, object> kv in keyValuePairs)
            {
                bool t1 = false;
                Exception t2 = null;
                Coroutine c;
                try
                {
                    c = context.StartCoroutine(DoSetData(kv.Key, kv.Value, context, (s, e) =>
                    {
                        t1 = s;
                        t2 = e;
                    }));
                }
                catch (Exception e)
                {
                    action?.Invoke(false, e);
                    yield break;
                }
                yield return c;
                if (!t1)
                {
                    action?.Invoke(false, t2);
                    yield break;
                }
            }
        }

        public void GetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction)
        {
            try
            {
                context.StartCoroutine(DoGetData(key, context, resultAction));
            }
            catch (Exception e)
            {
                resultAction?.Invoke(default, e, false);
            }
        }

        public void SetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action)
        {
            try
            {
                context.StartCoroutine(DoSetData(key, value, context, action));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }

        public void GetDataMultiple<T>(string[] keys, MonoBehaviour context, ResultAction<T[], Exception> resultAction)
        {
            try
            {
                context.StartCoroutine(DoGetDataMultiple(keys, context, resultAction));
            }
            catch (Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
        }

        public void SetDataMultiple(string[] keys, object[] values, MonoBehaviour context, BoolAction<Exception> action)
        {
            try
            {
                int len = Math.Min(keys.Length, values.Length);
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                for (int i = 0; i < len; i++)
                {
                    keyValuePairs[keys[i]] = values[i];
                }
                SetDataMultiple(keyValuePairs, context, action);
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }

        public void SetDataMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action)
        {
            try
            {
                context.StartCoroutine(DoSetDataMultiple(keyValuePairs, context, action));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }
    }
}