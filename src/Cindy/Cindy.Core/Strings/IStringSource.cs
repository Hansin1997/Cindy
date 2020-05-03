using System;

namespace Cindy.Strings
{
    interface IStringSource
    {
        void Get(string key, UnityEngine.MonoBehaviour context, ResultAction<string, Exception> resultAction);
        void GetMultiple(string[] keys, UnityEngine.MonoBehaviour context, ResultAction<string[], Exception> resultAction);
    }
}
