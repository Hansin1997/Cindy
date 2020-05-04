using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    /// <summary>
    /// 逻辑触发器，用于触发事件。
    /// </summary>
    public abstract class LogicTrigger : MonoBehaviour
    {
        /// <summary>
        /// 事件逻辑节点
        /// </summary>
        public LogicNode[] targetNodes;
        /// <summary>
        /// UnityEvent
        /// </summary>
        public UnityEvent events;

        /// <summary>
        /// 执行触发事件
        /// </summary>
        protected virtual void Execute()
        {
            if (!Handle())
                return;
            if(targetNodes != null)
            {
                foreach(LogicNode node in targetNodes)
                {
                    try
                    {
                        node.Execute();
                    }catch(Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
            events.Invoke();
        }

        /// <summary>
        /// 触发事件前的检查
        /// </summary>
        /// <returns>是否执行触发事件</returns>
        protected abstract bool Handle();
    }
}
