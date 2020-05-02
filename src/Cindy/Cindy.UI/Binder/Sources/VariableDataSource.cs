using Cindy.Logic;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.UI.Binder
{
    [AddComponentMenu("Cindy/UI/Binder/DataSources/VariableDataSource")]
    public class VariableDataSource : AbstractDataSource
    {
        public Context context;

        protected Dictionary<string, AbstractVariableObject> cache;

        protected virtual void Start()
        {
            if (context == null)
                context = GetComponent<Context>();
        }

        public override T GetData<T>(string key, T defaultValue = default)
        {
            AbstractVariableObject temp = FindTarget(key);
            if (temp == null)
                return defaultValue;
            object value = temp.GetVariableValue();
            if (value == null)
                return defaultValue;
            if (value is T r)
                return r;
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

        public override void SetData(string key, object value)
        {
            AbstractVariableObject temp = FindTarget(key);
            if (temp == null)
                return;
            temp.SetVariableValue(value);
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
    }
}
