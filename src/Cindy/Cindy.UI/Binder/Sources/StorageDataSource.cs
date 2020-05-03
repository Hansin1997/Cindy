using Cindy.Storages;
using System;
using System.Collections;
using UnityEngine;

namespace Cindy.UI.Binder.Sources
{
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

        protected override IEnumerator DoGetData<T>(string key, ResultAction<T, Exception> resultAction)
        {
            yield return StartCoroutine(storage.DoGet(key, (temp, e, isSuccess) =>
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

        protected override IEnumerator DoSetData(string key, object value, BoolAction<Exception> action)
        {
            string json;
            try
            {
                json = JSON.ToJson(value);
            }catch(Exception e)
            {
                action?.Invoke(false, e);
                yield break;
            }
            yield return StartCoroutine(storage.DoPut(key, json, action));
        }
    }
}
