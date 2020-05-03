using System;
using System.Collections;
using UnityEngine;

namespace Cindy.Strings
{
    public abstract class StringSource : ScriptableObject, IStringSource
    {
        public abstract IEnumerator DoGet(string key, MonoBehaviour context, ResultAction<string, Exception> resultAction);
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
    }
}
