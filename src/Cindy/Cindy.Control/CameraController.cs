using Cindy.Control.CameraBehaviours;
using Cindy.Util;
using UnityEngine;

namespace Cindy.Control
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : Attachable<CameraControlHandler, CameraBehaviour>
    {

        protected Camera _camera;

        protected CameraControlHandler focusedAttachment;

        public Camera Camera
        {
            get
            {
                if (_camera == null)
                    _camera = GetComponent<Camera>();
                return _camera;
            }
        }

        protected virtual void FixedUpdate()
        {
            CameraControlHandler top = Peek();
            if (top != null)
            {
                if (top != focusedAttachment)
                {
                    if (focusedAttachment != null)
                    {
                        focusedAttachment.attachment.OnCameraBlur(Camera, top.target, top.GetParameters());
                    }
                    top.attachment.OnCameraFocus(Camera, top.target, top.GetParameters());
                }
                top.attachment.OnCameraUpdate(Camera, top.target, Time.fixedDeltaTime, top.GetParameters());
            }
            focusedAttachment = top;
        }

        protected override bool IsPeek(CameraControlHandler attachment)
        {
            CameraBehaviour behaviour = attachment.attachment;
            if (behaviour == null || !attachment.enabled || !attachment.gameObject.activeSelf)
                return false;
            return true;
        }
    }
}
