using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder
{
    public interface IDataSource
    {
        void GetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction);
        void GetDataMultiple<T>(string[] keys, MonoBehaviour context, ResultAction<T[], Exception> resultAction);
        void SetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action);
        void SetDataMultiple(string[] keys, object[] values, MonoBehaviour context, BoolAction<Exception> action);
        void SetDataMultiple(IDictionary<string, object> keyValuePairs, MonoBehaviour context, BoolAction<Exception> action);

        bool IsReadable();

        bool IsWriteable();
    }
}