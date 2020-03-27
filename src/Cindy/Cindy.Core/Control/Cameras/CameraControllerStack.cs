using Cindy.Util;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    [AddComponentMenu("Cindy/Control/CameraController", 1)]
    [RequireComponent(typeof(Camera))]
    public class CameraControllerStack : Attachable
    {
        public UpdateType updateType;

        protected Camera _camera;

        protected CameraController focusedAttachment;

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
            return attachment is CameraController;
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
            CameraController top = Peek<CameraController>();
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
            if(attachment is CameraController a)
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