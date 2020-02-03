using Cindy.Util.Serializables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    public class LogicNode : MonoBehaviour
    {
        public UnityEvent events;
        public Switch[] nextNodes;

        public void Execute()
        {
            try
            {
                Run();
            }catch(Exception e)
            {
                Debug.LogError(e);
            }
            events.Invoke();
            foreach(Switch nextNode in nextNodes)
            {
                if (nextNode.value != null && (nextNode.key == null || nextNode.key.Check()))
                    nextNode.value.Execute();
            }
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
