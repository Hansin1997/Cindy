using System;
using UnityEngine;

namespace Cindy.Logic
{
    public abstract class ConditionTrigger : MonoBehaviour
    {
        [Serializable]
        public enum UpdateType
        {
            Manual,
            OnStart,
            OnUpdate,
            OnFixedUpdate
        }

        [Header("Condition Trigger")]
        public Condition condition;
        public bool invert;
        public UpdateType updateType;

        protected abstract void Handle(bool val);

        public virtual void CallHandle()
        {
            if (condition != null)
            {
                bool val = condition.Check();
                if (invert)
                    val = !val;
                Handle(val);
            }
        }

        protected virtual void Start()
        {
            if (updateType == UpdateType.OnStart)
                CallHandle();
        }

        protected virtual void Update()
        {
            if (updateType == UpdateType.OnUpdate)
                CallHandle();
        }

        public virtual void FixedUpdate()
        {
            if (updateType == UpdateType.OnFixedUpdate)
                CallHandle();
        }
    }


}
