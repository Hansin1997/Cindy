using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Logic
{
    /// <summary>
    /// 逻辑上下文
    /// </summary>
    public abstract class Context : MonoBehaviour
    {
        /// <summary>
        /// 扫描区域
        /// </summary>
        public enum ScanScope
        {
            /// <summary>
            /// 整个场景
            /// </summary>
            All,
            /// <summary>
            /// 自身
            /// </summary>
            Self,
            /// <summary>
            /// 自身及子物体
            /// </summary>
            SelfAndChildren,
            /// <summary>
            /// 自身及父物体
            /// </summary>
            SelfAndParent
        }

        /// <summary>
        /// 扫描区域，相关搜索将在扫描区域内进行搜索。
        /// </summary>
        public ScanScope scanScope = ScanScope.SelfAndChildren;

        /// <summary>
        /// 上下文对象
        /// </summary>
        public GameObject ContextGameObject { get { return GetContext(); } }

        /// <summary>
        /// 获取上下文对象
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetContext();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">数据键名</param>
        /// <returns>数据值</returns>
        public abstract T Get<T>(string key);

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key">数据键名</param>
        /// <param name="value">数据值</param>
        public abstract void Put(string key, object value);

        /// <summary>
        /// 获取上下文内全部对象
        /// </summary>
        /// <typeparam name="V">对象类型</typeparam>
        /// <returns>对象数组</returns>
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

        /// <summary>
        /// 获取上下文内一个对象
        /// </summary>
        /// <typeparam name="V">对象类型</typeparam>
        /// <returns>对象</returns>
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

        /// <summary>
        /// 获取已命名对象
        /// </summary>
        /// <typeparam name="V">对象类型</typeparam>
        /// <param name="name">对象名称</param>
        /// <returns>对象</returns>
        public V GetNamedObject<V>(string name = null) where V : NamedBehaviour
        {

            V[] results = Find<V>();
            if (name == null)
            {
                if (results.Length > 0)
                    return results[0];
                return null;
            }
            foreach (V v in results)
                if (name.Equals(v.Name))
                    return v;
            return null;
        }

        /// <summary>
        /// 获取已命名对象数组
        /// </summary>
        /// <typeparam name="V">对象类型</typeparam>
        /// <param name="name">对象名称</param>
        /// <returns>对象数组</returns>
        public V[] GetNamedObjects<V>(string name = null) where V : NamedBehaviour
        {
            if (name == null)
                return Find<V>();
            V[] results = Find<V>();
            List<V> collection = new List<V>();
            foreach (V v in results)
                if (name.Equals(v.Name))
                    collection.Add(v);
            return collection.ToArray();
        }

        /// <summary>
        /// 获取变量对象
        /// </summary>
        /// <typeparam name="V">变量对象类型</typeparam>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量对象</returns>
        public V GetVariable<V, T>(string variableName = null) where V : VariableObject<T>
        {
            return GetNamedObject<V>(variableName);
        }

        /// <summary>
        /// 获取变量对象数组
        /// </summary>
        /// <typeparam name="V">变量对象类型</typeparam>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="variableName">变量名称</param>
        /// <returns>变量对象数组</returns>
        public V[] GetVariables<V, T>(string variableName = null) where V : VariableObject<T>
        {
            return GetNamedObjects<V>(variableName);
        }

        /// <summary>
        /// 获取逻辑节点
        /// </summary>
        /// <typeparam name="V">逻辑节点类型</typeparam>
        /// <param name="nodeName">节点名称</param>
        /// <returns>逻辑节点</returns>
        public V GetLogicNode<V>(string nodeName = null) where V : LogicNode
        {
            return GetNamedObject<V>(nodeName);
        }

        /// <summary>
        /// 获取逻辑节点数组
        /// </summary>
        /// <typeparam name="V">逻辑节点类型</typeparam>
        /// <param name="nodeName">节点名称</param>
        /// <returns>逻辑节点数组</returns>
        public V[] GetLogicNodes<V>(string nodeName = null) where V : LogicNode
        {
            return GetNamedObjects<V>(nodeName);
        }

    }

    /// <summary>
    /// 本地上下文
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Logic/Context/LocalContext")]
    public class LocalContext : Context
    {
        /// <summary>
        /// 临时数据字典
        /// </summary>
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

    /// <summary>
    /// 代理上下文
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Context/ContextProxy")]
    public class ContextProxy : Context
    {
        /// <summary>
        /// 目标上下文
        /// </summary>
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
