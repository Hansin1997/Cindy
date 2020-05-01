using Cindy.Logic.ReferenceValues;
using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    [AddComponentMenu("Cindy/Logic/LogicNode")]
    public class LogicNode : NamedObject
    {
        [Header("LogicNode")]
        public ReferenceString nodeName;
        public UnityEvent events;
        public Switch[] nextNodes;

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
                if (nextNode.value != null && (nextNode.key == null || nextNode.key.Check()))
                    try
                    {
                        nextNode.value.Execute();
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

        protected virtual void Run()
        {

        }

        [Serializable]
        public class Switch : SerializedKeyValuePair<Condition, LogicNode>
        {
            public Switch(KeyValuePair<Condition,LogicNode> keyValuePair) : base(keyValuePair)
            {

            }
        }
    }
}
