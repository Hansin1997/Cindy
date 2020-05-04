using Cindy.Logic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder
{
    /// <summary>
    /// 变量数据源
    /// </summary>
    [AddComponentMenu("Cindy/UI/Binder/DataSources/VariableDataSource")]
    public class VariableDataSource : AbstractDataSource
    {
        public Context context;

        protected Dictionary<string, AbstractVariableObject> cache;

        protected virtual void Awake()
        {
            if (context == null)
                context = GetComponent<Context>();
        }

        protected AbstractVariableObject FindTarget(string name)
        {
            if (name == null)
                return null;
            if (cache == null)
                cache = new Dictionary<string, AbstractVariableObject>();
            AbstractVariableObject target;
            if (cache.ContainsKey(name) && (target = cache[name]) != null && name.Equals(target.Name))
                return target;
            target = context.GetNamedObject<AbstractVariableObject>(name);
            if (target != null)
                cache[name] = target;
            return target;
        }

        public override bool IsReadable()
        {
            return true;
        }

        public override bool IsWriteable()
        {
            return true;
        }

        protected override IEnumerator DoGetData<T>(string key, MonoBehaviour context, ResultAction<T, Exception> resultAction)
        {
            AbstractVariableObject temp = FindTarget(key);
            if (temp == null)
            {
                resultAction?.Invoke(default, null, false);
                yield break;
            }

            object value = temp.GetVariableValue();
            if (value == null)
            {
                resultAction?.Invoke(default, null, false);
                yield break;
            }

            if (value is T r)
            {
                resultAction?.Invoke(r, null, true);
                yield break;
            }
            try
            {
                resultAction?.Invoke(JsonConvert.DeserializeObject<T>(value.ToString()), null, true);
            }
            catch (Exception e)
            {
                resultAction?.Invoke(default, e, false);
            }
        }

        protected override IEnumerator DoSetData(string key, object value, MonoBehaviour context, BoolAction<Exception> action)
        {
            AbstractVariableObject temp = FindTarget(key);
            if (temp == null)
            {
                action?.Invoke(false, null);
                yield break;
            }
            try
            {
                temp.SetVariableValue(value);
                action?.Invoke(true, null);
            }
            catch (Exception e)
            {
                action?.Invoke(false, e);
            }
        }
    }
}
