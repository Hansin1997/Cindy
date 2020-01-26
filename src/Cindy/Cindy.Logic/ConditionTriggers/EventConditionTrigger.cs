using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic.ConditionTriggers
{

    [AddComponentMenu("Cindy/Logic/ConditionTriggers/EventConditionTrigger")]
    public class EventConditionTrigger : ConditionTrigger
    {
        public LogicNode startNode;
        public UnityEvent events;

        protected override void Handle(bool val)
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
