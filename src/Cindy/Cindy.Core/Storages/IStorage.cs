using System;
using System.Collections.Generic;

namespace Cindy.Storages
{
    public interface IStorage
    {
        void Get(string key, UnityEngine.MonoBehaviour context, ResultAction<string, Exception> resultAction);

        void Put(string key, UnityEngine.MonoBehaviour context, object value, BoolAction<Exception> action);

        void GetMultiple(UnityEngine.MonoBehaviour context, ResultAction<string[], Exception> resultAction, Action<float> progess, params string[] keys);

        void PutMultiple(string[] keys, object[] values, UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess);

        void PutMultiple(IDictionary<string, object> keyValuePairs, UnityEngine.MonoBehaviour context, BoolAction<Exception> action, Action<float> progess);

        void Clear(UnityEngine.MonoBehaviour context, BoolAction<Exception> action);
    }
}
