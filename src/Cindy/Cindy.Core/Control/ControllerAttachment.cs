using Cindy.Util;
using System;
using UnityEngine;

namespace Cindy.Control
{
    public abstract class ControllerAttachment : Attachment
    {

        [Header("Controllable")]
        public string controllerTargetName;

        private GameObject _target;
        private string _targetName;
        public GameObject Target { 
            get {
                if(!controllerTargetName.Equals(_targetName) || _target == null)
                {
                    _target = GameObject.Find(controllerTargetName);
                    _targetName = controllerTargetName;
                    OnTargetChange(_target);
                }
                return _target; 
            } 
        }

        public abstract void OnControllerSelect();

        public abstract void OnControllerUpdate(float deltaTime);

        public abstract void OnControllerUnselect();

        protected virtual void OnTargetChange(GameObject target)
        {

        }

        protected override Type GetAttachableType()
        {
            return typeof(Controller);
        }
    }
}
