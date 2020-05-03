using System;
using UnityEngine;

namespace Cindy.UI.Binder
{
    public interface IDataSource
    {
        void GetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction);

        void SetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action);

        bool IsReadable();

        bool IsWriteable();
    }
}