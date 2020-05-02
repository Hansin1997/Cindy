using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic.ConditionTriggers
{
    [Obsolete]
    [AddComponentMenu("Cindy/Logic/Triggers/EventConditionTrigger")]
    public class EventConditionTrigger : ConditionTrigger
    {
        public LogicNode startNode;
        public UnityEvent events;

        protected override void OnConditionValueChanged(bool val)
        {
            if (val)
            {
                if(startNode != null)
                {
                    startNode.Execute();
                }
                if(events != null)
                {
                    events.Invoke();
                }
            }
        }
    }
}
