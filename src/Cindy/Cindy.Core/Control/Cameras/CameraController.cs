﻿using Cindy.Util;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : Attachable
    {

        protected Camera _camera;

        protected CameraBehaviourAttachment focusedAttachment;

        public Camera Camera
        {
            get
            {
                if (_camera == null)
                    _camera = GetComponent<Camera>();
                return _camera;
            }
        }

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is CameraBehaviourAttachment;
        }

        protected virtual void FixedUpdate()
        {
            CameraBehaviourAttachment top = Peek<CameraBehaviourAttachment>();
            if (top != null)
            {
                if (top != focusedAttachment)
                {
                    if (focusedAttachment != null)
                    {
                        focusedAttachment.Behaviour.OnCameraBlur(Camera, top);
                    }
                    top.Behaviour.OnCameraFocus(Camera, top);
                }
                top.Behaviour.OnCameraUpdate(Camera, top, Time.fixedDeltaTime);
            }
            focusedAttachment = top;
        }

        protected override bool IsPeek(Attachment attachment)
        {
            if(attachment is CameraBehaviourAttachment a)
            {
                return a != null && a.Behaviour != null && a.CameraTarget != null
                    && a.enabled && a.gameObject.activeSelf;
            }
            return false;
        }
    }
}