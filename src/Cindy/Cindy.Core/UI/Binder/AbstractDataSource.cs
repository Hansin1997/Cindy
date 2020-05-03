using System;
using System.Collections;
using UnityEngine;

namespace Cindy.UI.Binder
{
    public abstract class AbstractDataSource : MonoBehaviour, IDataSource
    {
        public abstract bool IsReadable();
        public abstract bool IsWriteable();
        protected abstract IEnumerator DoGetData<T>(string key, ResultAction<T, Exception> resultAction);
        protected abstract IEnumerator DoSetData(string key, object value, BoolAction<Exception> action);

        public void GetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction)
        {
            try
            {
                context.StartCoroutine(DoGetData<T>(key, resultAction));
            }catch(Exception e)
            {
                resultAction?.Invoke(default, e, false);
            }
        }
        public void SetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action)
        {
            try
            {
                context.StartCoroutine(DoSetData(key, value, action));
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }
    }
}