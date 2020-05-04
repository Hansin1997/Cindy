using Cindy.Logic.ReferenceValues;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Cindy.Storages
{
    /// <summary>
    /// 抽象可存储对象
    /// </summary>
    public abstract class AbstractStorableObject : MonoBehaviour, IStorableObject
    {
        /// <summary>
        /// 存储选项
        /// </summary>
        [SerializeField]
        public StorableObjectOption StorableOptions;

        protected StringBuilder stringBuilder;

        public string GetStorableKey()
        {
            if (stringBuilder == null)
                stringBuilder = new StringBuilder();
            else
                stringBuilder.Clear();
            stringBuilder.Append(GetType().FullName)
                .Append("_")
                .Append(name);
            if(!StorableOptions.globalUniqueness)
            {
                stringBuilder.Append("_").Append(gameObject.scene.name);
            }
            return stringBuilder.ToString();


        }
        public abstract object GetStorableObject();
        public abstract Type GetStorableObjectType();
        public abstract void OnStorableObjectRestore(object obj);

        /// <summary>
        /// 存储选项类
        /// </summary>
        [Serializable]
        public class StorableObjectOption
        {
            /// <summary>
            /// 标签
            /// </summary>
            public ReferenceString[] labels = { new ReferenceString() { value = "default" } };
            /// <summary>
            /// 是否全局唯一
            /// </summary>
            public bool globalUniqueness = false;

            /// <summary>
            /// 判断是否包含标签
            /// </summary>
            /// <param name="labels"></param>
            /// <returns></returns>
            public bool ContainerLabel(params string[] labels)
            {
                HashSet<string> labelsSet = new HashSet<string>();
                foreach (ReferenceString label in this.labels)
                    labelsSet.Add(label.Value);
                if (labelsSet.Count == 0)
                    return false;
                foreach(string label in labels)
                {
                    if (labelsSet.Contains(label))
                        return true;
                }
                return false;
            }

            /// <summary>
            /// 判断是否包含标签
            /// </summary>
            /// <param name="labels"></param>
            /// <returns></returns>
            public bool ContainerLabel(params ReferenceString[] labels)
            {
                HashSet<string> labelsSet = new HashSet<string>();
                foreach (ReferenceString label in this.labels)
                    labelsSet.Add(label.Value);
                if (labelsSet.Count == 0)
                    return false;
                foreach (ReferenceString label in labels)
                {
                    if (labelsSet.Contains(label.Value))
                        return true;
                }
                return false;
            }
        }
    }
}
