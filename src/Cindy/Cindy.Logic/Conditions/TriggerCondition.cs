using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/TriggerCondition")]
    [RequireComponent(typeof(Collider))]
    public class TriggerCondition : Condition
    {
        [Header("Trigger Condition")]
        public TriggerType triggerType;
        public ReferenceString[] targets;

        [Header("Listener")]
        public UnityEvent onConditionMatch;

        protected bool value;

        protected virtual void OnConditionMatch()
        {
            try
            {
                onConditionMatch.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

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
                OnConditionMatch();
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (IsTarget(other.gameObject) && triggerType == TriggerType.Exit)
            {
                value = true;
                OnConditionMatch();
            }
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (IsTarget(other.gameObject) && triggerType == TriggerType.Stay)
            {
                value = true;
                OnConditionMatch();
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
