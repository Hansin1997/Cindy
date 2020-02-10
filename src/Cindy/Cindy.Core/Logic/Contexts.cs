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

        public GameObject context { get { return GetContext(); } }
        public abstract GameObject GetContext();
        public abstract T Get<T>(string key);
        public abstract void Put(string key, object value);

        public V GetVariable<V, T>(string variableName) where V : VariableObject<T>
        {
            V[] results = Find<V>();
            foreach (V v in results)
                if (v.variableName.Equals(variableName))
                    return v;
            return null;
        }
        public V[] GetVariables<V, T>(string variableName = null) where V : VariableObject<T>
        {
            if (variableName == null)
                return Find<V>();
            V[] results = Find<V>();
            List<V> collection = new List<V>();
            foreach (V v in results)
                if (v.variableName.Equals(variableName))
                    collection.Add(v);
            return collection.ToArray();
        }

        public V GetLogicNode<V>(string nodeName) where V : LogicNode
        {
            V[] results = Find<V>();
            foreach (V v in results)
                if (v.nodeName.Equals(nodeName))
                    return v;
            return null;
        }
        public V[] GetLogicNodes<V>(string nodeName = null) where V : LogicNode
        {
            if (nodeName == null)
                return Find<V>();
            V[] results = Find<V>();
            List<V> collection = new List<V>();
            foreach (V v in results)
                if (v.nodeName.Equals(nodeName))
                    collection.Add(v);
            return collection.ToArray();
        }

        protected virtual V[] Find<V>() where V : UnityEngine.Object
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
        public Context realContext { get; set; }

        public override T Get<T>(string key)
        {
            return realContext == null ? default : realContext.Get<T>(key);
        }

        public override void Put(string key, object value)
        {
            if (realContext != null)
                realContext.Put(key, value);
        }

        public override GameObject GetContext()
        {
            return realContext == null ? gameObject : realContext.GetContext();
        }
    }
}
