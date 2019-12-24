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

        public CameraBehaviour Behaviour
        {
            get
            {
                return GetCameraBehaviour();
            }

        }

        private Transform _cameraTarget;
        private string _cameraTargetName;
        public Transform CameraTarget
        {
            get
            {
                if (cameraTargetName.Trim().Length > 0)
                {
                    if(!cameraTargetName.Equals(_cameraTargetName) || _cameraTarget == null)
                    {
                        GameObject obj = GameObject.Find(cameraTargetName);
                        _cameraTargetName = cameraTargetName;
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
