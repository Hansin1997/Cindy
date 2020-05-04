using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages
{
    /// <summary>
    /// 抽象存储器
    /// </summary>
    public abstract class AbstractStorage : ScriptableObject, IStorage, IObjectStorage
    {
        /// <summary>
        /// 协程执行清除所有数据
        /// </summary>
        /// <param name="action">异步回调</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoClear(BoolAction<Exception> action);
        /// <summary>
        /// 协程执行获取数据
        /// </summary>
        /// <param name="key">数据键名</param>
        /// <param name="resultAction">异步回调</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoGet(string key, ResultAction<string, Exception> resultAction);
        /// <summary>
        /// 协程执行存储数据
        /// </summary>
        /// <param name="key">数据键名</param>
        /// <param name="value">数据值</param>
        /// <param name="action">异步回调</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoPut(string key, object value, BoolAction<Exception> action);
        /// <summary>
        /// 协程执行获取多个数据
        /// </summary>
        /// <param name="resultAction">异步回调</param>
        /// <param name="progess">进度回调</param>
        /// <param name="keys">数据键名数组</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoGetMultiple(ResultAction<string[], Exception> resultAction, Action<float> progess, string[] keys);
        /// <summary>
        /// 协程执行存储多个数据
        /// </summary>
        /// <param name="keyValuePairs">数据字典</param>
        /// <param name="action">异步回调</param>
        /// <param name="progess">进度回调</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoPutMultiple(IDictionary<string, object> keyValuePairs, BoolAction<Exception> action, Action<float> progess);
        /// <summary>
        /// 协程执行恢复对象
        /// </summary>
        /// <param name="action">异步回调</param>
        /// <param name="progess">进度回调</param>
        /// <param name="storableObjects">可存储物体数组</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoRestoreObjects(BoolAction<Exception> action, Action<float> progess, IStorableObject[] storableObjects);
        /// <summary>
        /// 协程执行存储对象
        /// </summary>
        /// <param name="action">异步回调</param>
        /// <param name="progress">进度回调</param>
        /// <param name="storableObjects">可存储物体数组</param>
        /// <returns>协程迭代器</returns>
        public abstract IEnumerator DoPutObjects(BoolAction<Exception> action, Action<float> progress, IStorableObject[] storableObjects);
       
        public void Clear(MonoBehaviour context, BoolAction<Exception> action)
        {
            try
            {
                context.StartCoroutine(DoClear(action));
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
        }

        public void Get(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction)
        {
            if (resultAction == null)
            {
                Debug.Log("Action is null!");
                return;
            }
            try
            {
                context.StartCoroutine(DoGet(key, resultAction));
            }catch(Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
        }

        public void Put(string key, MonoBehaviour context, object value, BoolAction<Exception> action)
        {
            try
            {
                context.StartCoroutine(DoPut(key, value, action));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }

        public void GetMultiple(MonoBehaviour context, ResultAction<string[], Exception> resultAction, Action<float> progess, params string[] keys)
        {
            if (resultAction == null)
            {
                Debug.Log("Action is null!");
                return;
            }
            try
            {
                context.StartCoroutine(DoGetMultiple(resultAction, progess, keys));
            }
            catch (Exception e)
            {
                resultAction?.Invoke(null, e, false);
            }
        }

        public void PutMultiple(string[] keys, object[] values, MonoBehaviour context, BoolAction<Exception> action, Action<float> progess)
        {
            try
            {
                int len = Math.Min(keys.Length, values.Length);
                Dictionary<string, object> map = new Dictionary<string, object>();
                for (int i = 0; i < len; i++)
                    map[keys[i]] = values[i];
                PutMultiple(map, context, action, progess);
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }

        public void PutMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action, Action<float> progess)
        {
            try
            {
                context.StartCoroutine(DoPutMultiple(keyValuePairs, action, progess));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }

        public void RestoreObjects(MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects)
        {
            if(action == null)
            {
                Debug.Log("Action is null!");
                return;
            }
            try
            {
                context.StartCoroutine(DoRestoreObjects(action, progess, storableObjects));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }

        }
        
        public void PutObjects(MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects)
        {
            try
            {
                context.StartCoroutine(DoPutObjects(action, progess, storableObjects));
            }catch(Exception e)
            {
                action?.Invoke(false, e);
            }
        }
    }
}
