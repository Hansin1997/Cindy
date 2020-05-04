using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Strings
{
    /// <summary>
    /// 抽象文本源
    /// </summary>
    public abstract class StringSource : ScriptableObject, IStringSource
    {
        /// <summary>
        /// 获取一个字符串
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="context">上下文</param>
        /// <param name="resultAction">异步回调</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoGet(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction);

        /// <summary>
        /// 获取多个字符串
        /// </summary>
        /// <param name="keys">键名数组</param>
        /// <param name="context">上下文param>
        /// <param name="resultAction">异步回调</param>
        /// <returns></returns>
        public virtual IEnumerator DoGetMultiple(string[] keys, MonoBehaviour context, ResultAction<string[], Exception> resultAction)
        {
            List<string> results = new List<string>();
            foreach (string key in keys)
            {
                string t1 = null;
                Exception t2 = null;
                bool t3 = false;
                Coroutine c;
                try
                {
                    c = context.StartCoroutine(DoGet(key, context, (v, e, s) =>
                    {
                        t1 = v;
                        t2 = e;
                        t3 = s;
                    }));
                } catch (Exception e)
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
                results.Add(t1);
            }
            resultAction?.Invoke(results.ToArray(), null, true);
        }

        public void Get(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction)
        {
            if (resultAction == null)
            {
                Debug.LogWarning("ResultAction is null.");
                return;
            }
            try
            {
                context.StartCoroutine(DoGet(key, context, resultAction));
            }
            catch (Exception e)
            {
                resultAction(null, e, false);
            }
        }

        public void GetMultiple(string[] keys, MonoBehaviour context, ResultAction<string[], Exception> resultAction)
        {
            if (resultAction == null)
            {
                Debug.LogWarning("ResultAction is null.");
                return;
            };
            try
            {
                context.StartCoroutine(DoGetMultiple(keys, context, resultAction));
            }
            catch (Exception e)
            {
                resultAction(null, e, false);
            }
        }
    }
}