using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/TriggerCondition")]
    [RequireComponent(typeof(Collider))]
    public class TriggerCondition : Condition
    {
        [Header("Trigger Condition")]
        public TriggerType triggerType;
        public ReferenceString[] targets;

        protected bool value;

        public override bool Check()
        {
            if (value)
            {
                value = false;
                return true;
            }
            return false;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (IsTarget(other.gameObject) && triggerType == TriggerType.Enter)
            {
                value = true;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (IsTarget(other.gameObject) && triggerType == TriggerType.Exit)
            {
                value = true;
            }
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (IsTarget(other.gameObject) && triggerType == TriggerType.Stay)
            {
                value = true;
            }
        }

        protected virtual bool IsTarget(GameObject gObject)
        {
            if (gObject == null)
                return false;
            foreach (ReferenceString target in targets)
            {
                if (gObject.name.Equals(target.Value))
                    return true;
            }
            return false;
        }

        public enum TriggerType
        {
            Enter,
            Stay,
            Exit
        }
    }
}
