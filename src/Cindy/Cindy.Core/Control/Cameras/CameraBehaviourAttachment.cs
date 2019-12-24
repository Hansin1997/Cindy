using Cindy.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    public abstract class CameraBehaviourAttachment : Attachment
    {
        [Header("Camera Behaviour")]
        [Tooltip("Target that camera focus on, it will be self if not set.")]
        public string cameraTargetName = "";

        private CameraBehaviour _behaviour;
        public CameraBehaviour Behaviour
        {
            get
            {
                if (_behaviour == null)
                    _behaviour = GetCameraBehaviour();
                return _behaviour;
            }

        }

        private Transform _cameraTarget;
        public Transform CameraTarget
        {
            get
            {
                if (cameraTargetName.Trim().Length > 0)
                {
                    if(_cameraTarget == null)
                    {
                        GameObject obj = GameObject.Find(cameraTargetName);
                        if (obj != null)
                            _cameraTarget = obj.transform;
                    }
                    return _cameraTarget;
                }
                else
                    return transform;
            }
        }

        private Dictionary<string, object> temp;
        public Dictionary<string,object> Temp
        {
            get
            {
                if (temp == null)
                    temp = new Dictionary<string, object>();
                return temp;
            }
        }
        protected override Type GetAttachableType()
        {
            return typeof(CameraController);
        }

        protected abstract CameraBehaviour GetCameraBehaviour();
    }
}
