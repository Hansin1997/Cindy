using System;
using UnityEngine;

namespace Cindy.Control
{
    public abstract class Controller : MonoBehaviour
    {
        [Header("Controllable")]
        public bool isControllable = true;

        public virtual bool IsControllable()
        {
            return isControllable && enabled;
        }

        public virtual void Move(Vector3 direction)
        {
            if (IsControllable() && enabled)
                DoMove(direction);
        }

        protected abstract void DoMove(Vector3 direction);
    }
}
