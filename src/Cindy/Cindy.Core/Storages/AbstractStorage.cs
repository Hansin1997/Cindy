using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Storages
{
    public abstract class AbstractStorage : ScriptableObject, IStorage, IObjectStorage
    {
        public abstract IEnumerator DoClear(BoolAction<Exception> action);
        public abstract IEnumerator DoGet(string key, ResultAction<string, Exception> resultAction);
        public abstract IEnumerator DoPut(string key, object value, BoolAction<Exception> action);
        public abstract IEnumerator DoGetMultiple(ResultAction<string[], Exception> resultAction, Action<float> progess, string[] keys);
        public abstract IEnumerator DoPutMultiple(IDictionary<string, object> keyValuePairs, BoolAction<Exception> action, Action<float> progess);
        public abstract IEnumerator DoLoadObjects(BoolAction<Exception> action, Action<float> progess, IStorableObject[] storableObjects);
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

        public void LoadObjects(MonoBehaviour context, BoolAction<Exception> action, Action<float> progess, params IStorableObject[] storableObjects)
        {
            if(action == null)
            {
                Debug.Log("Action is null!");
                return;
            }
            try
            {
                context.StartCoroutine(DoLoadObjects(action, progess, storableObjects));
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
