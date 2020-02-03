using UnityEngine;
using UnityEngine.Events;

namespace Cindy.Logic
{
    [AddComponentMenu("Cindy/Logic/Caller/Caller")]
    public class Caller : MonoBehaviour
    {
        public UnityEvent events;

        public CallType callType;

        public bool justOnce = false;

        protected bool done = false;

        protected virtual void DoCall()
        {
            if (justOnce && done)
                return;
            events.Invoke();
            done = true;
        }

        protected virtual void Start()
        {
            if (callType == CallType.OnStart)
                DoCall();
        }
        protected virtual void Update()
        {
            if (callType == CallType.OnUpdate)
                DoCall();
        }
        protected virtual void FixedUpdate()
        {
            if (callType == CallType.OnFixedUpdate)
                DoCall();
        }
        protected virtual void OnDestroy()
        {
            if (callType == CallType.OnDestroy)
                DoCall();
        }
        protected virtual void OnApplicationPause()
        {
            if (callType == CallType.OnApplicationPause)
                DoCall();
        }
        protected virtual void OnApplicationQuit()
        {
            if (callType == CallType.OnApplicationQuit)
                DoCall();
        }

        public enum CallType
        {
            OnStart,
            OnUpdate,
            OnFixedUpdate,
            OnDestroy,
            OnApplicationPause,
            OnApplicationQuit
        }

    }
}
