using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    public abstract class LogicTrigger : MonoBehaviour
    {
        public LogicNode[] targetNodes;
        public UnityEvent events;

        protected void Execute()
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

        protected abstract bool Handle();
    }
}
