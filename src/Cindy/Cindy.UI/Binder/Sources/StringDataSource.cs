using Cindy.Strings;
using System;
using System.Collections;
using UnityEngine;

namespace Cindy.UI.Binder
{
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

        protected override IEnumerator DoGetData<T>(string key, ResultAction<T, Exception> resultAction)
        {
            yield return StartCoroutine(stringSource.DoGet(key, this, (temp, e, isSuccess) =>
            {
                if (isSuccess)
                {
                    try
                    {
                        if(temp is T r)
                        {
                            resultAction(r, e, true);
                        }
                        else
                        {
                            resultAction?.Invoke(JSON.FromJson<T>(temp), null, true);
                        }
                    }catch(Exception e1)
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
            Debug.LogWarning("StringDataSource is readonly", this);
            action?.Invoke(false, null);
            yield return null;
        }
    }
}
