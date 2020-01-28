using Cindy.Util;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    [AddComponentMenu("Cindy/Control/CameraController", 1)]
    [RequireComponent(typeof(Camera))]
    public class CameraController : Attachable
    {
        public UpdateType updateType;

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

        protected virtual void Update()
        {
            if (updateType == UpdateType.OnUpdate)
                DoUpdate(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            if (updateType == UpdateType.OnFixedUpdate)
                DoUpdate(Time.fixedDeltaTime);
        }

        protected virtual void DoUpdate(float deltaTime)
        {
            CameraBehaviourAttachment top = Peek<CameraBehaviourAttachment>();
            if (top != null)
            {
                if (top != focusedAttachment)
                {
                    if (focusedAttachment != null && focusedAttachment.Behaviour != null)
                    {
                        focusedAttachment.Behaviour.OnCameraBlur(Camera, top);
                    }
                    if (top.Behaviour != null)
                        top.Behaviour.OnCameraFocus(Camera, top);
                }
                if (top.Behaviour != null)
                    top.Behaviour.OnCameraUpdate(Camera, top, deltaTime);
            }
            focusedAttachment = top;
        }

        protected override bool IsPeek(Attachment attachment)
        {
            if(attachment is CameraBehaviourAttachment a)
            {
                return a != null && a.enabled && a.gameObject.activeSelf;
            }
            return false;
        }

        public enum UpdateType
        {
            OnUpdate,
            OnFixedUpdate
        }
    }
}