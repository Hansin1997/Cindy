using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Logic
{
    public abstract class Context : MonoBehaviour
    {
        public enum ScanScope
        {
            All,
            Self,
            SelfAndChildren,
            SelfAndParent
        }

        public ScanScope scanScope = ScanScope.SelfAndChildren;

        public GameObject GameObject { get { return GetContext(); } }

        public abstract GameObject GetContext();
        public abstract T Get<T>(string key);
        public abstract void Put(string key, object value);

        public virtual V[] Find<V>() where V : Object
        {
            V[] results = null;
            switch (scanScope)
            {
                case ScanScope.All:
                    results = FindObjectsOfType<V>();
                    break;
                case ScanScope.Self:
                    results = GetContext().GetComponents<V>();
                    break;
                case ScanScope.SelfAndChildren:
                    results = GetContext().GetComponentsInChildren<V>();
                    break;
                case ScanScope.SelfAndParent:
                    results = GetContext().GetComponentsInParent<V>();
                    break;
            }
            return results;
        }

        public virtual V FindOne<V>() where V : Object
        {
            V result = null;
            switch (scanScope)
            {
                case ScanScope.All:
                    result = FindObjectOfType<V>();
                    break;
                case ScanScope.Self:
                    result = GetContext().GetComponent<V>();
                    break;
                case ScanScope.SelfAndChildren:
                    result = GetContext().GetComponentInChildren<V>();
                    break;
                case ScanScope.SelfAndParent:
                    result = GetContext().GetComponentInParent<V>();
                    break;
            }
            return result;
        }

        public V GetNamedObject<V>(string nodeName = null) where V : NamedObject
        {

            V[] results = Find<V>();
            if (nodeName == null)
            {
                if (results.Length > 0)
                    return results[0];
                return null;
            }
            foreach (V v in results)
                if (nodeName.Equals(v.Name))
                    return v;
            return null;
        }

        public V[] GetNamedObjects<V>(string nodeName = null) where V : NamedObject
        {
            if (nodeName == null)
                return Find<V>();
            V[] results = Find<V>();
            List<V> collection = new List<V>();
            foreach (V v in results)
                if (nodeName.Equals(v.Name))
                    collection.Add(v);
            return collection.ToArray();
        }

        public V GetVariable<V, T>(string variableName = null) where V : VariableObject<T>
        {
            return GetNamedObject<V>(variableName);
        }

        public V[] GetVariables<V, T>(string variableName = null) where V : VariableObject<T>
        {
            return GetNamedObjects<V>(variableName);
        }

        public V GetLogicNode<V>(string nodeName = null) where V : LogicNode
        {
            return GetNamedObject<V>(nodeName);
        }

        public V[] GetLogicNodes<V>(string nodeName = null) where V : LogicNode
        {
            return GetNamedObjects<V>(nodeName);
        }

    }

    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Logic/Context/LocalContext")]
    public class LocalContext : Context
    {
        protected Dictionary<string, object> data;

        public override T Get<T>(string key)
        {
            if (data == null || !data.ContainsKey(key))
                return default;
            if (data[key] is T r)
                return r;
            return default;
        }

        public override void Put(string key, object value)
        {
            if (data == null)
                data = new Dictionary<string, object>();
            data[key] = value;
        }

        public override GameObject GetContext()
        {
            return gameObject;
        }

    }

    [AddComponentMenu("Cindy/Logic/Context/ContextProxy")]
    public class ContextProxy : Context
    {
        public Context RealContext { get; set; }

        public override T Get<T>(string key)
        {
            return RealContext == null ? default : RealContext.Get<T>(key);
        }

        public override void Put(string key, object value)
        {
            if (RealContext != null)
                RealContext.Put(key, value);
        }

        public override GameObject GetContext()
        {
            return RealContext == null ? gameObject : RealContext.GetContext();
        }
    }
}
