using Cindy.Logic.ReferenceValues;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    /// <summary>
    /// 逻辑节点，负责封装游戏的逻辑、一些方法等，可以已链表的形式指定一条逻辑链。
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNode")]
    public class LogicNode : NamedBehaviour
    {
        /// <summary>
        /// 节点名
        /// </summary>
        [Header("LogicNode")]
        public ReferenceString nodeName;
        /// <summary>
        /// 节点事件，将在节点执行完毕后执行
        /// </summary>
        public UnityEvent events;
        /// <summary>
        /// 下一批节点，在节点执行完毕且节点事件执行完毕后根据条件执行。
        /// </summary>
        public Switch[] nextNodes;

        /// <summary>
        /// 执行节点
        /// </summary>
        public virtual void Execute()
        {
            try
            {
                Run();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            try
            {
                events.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            foreach (Switch nextNode in nextNodes) // 遍历下一批逻辑节点
            {
                // 当条件为空或者条件为真时执行该节点
                if (nextNode.Value != null && (nextNode.Key == null || nextNode.Key.Check()))
                    try
                    {
                        nextNode.Value.Execute();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
            }
        }

        protected override string GetName()
        {
            return nodeName.Value;
        }

        /// <summary>
        /// 执行节点具体业务，在此方法封装具体业务。
        /// </summary>
        protected virtual void Run()
        {

        }

        /// <summary>
        /// 序列化(条件-逻辑节点)键值对
        /// </summary>
        [Serializable]
        public class Switch : SerializedKeyValuePair<Condition, LogicNode>
        {
            public Switch(KeyValuePair<Condition,LogicNode> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }
}
