using System;
using UnityEngine;

namespace Cindy.Control
{
    public abstract class Controllable : MonoBehaviour
    {
        public bool isControllable = true;
        public ControllableRigbody rigbody;

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


    [Serializable]
    public class ControllableRigbody
    {
        public float mass = 1f;
        public float movingPower = 1f;
    }
}
