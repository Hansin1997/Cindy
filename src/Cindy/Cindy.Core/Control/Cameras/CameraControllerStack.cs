using Cindy.Util;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    /// <summary>
    /// 摄像机控制栈
    /// </summary>
    [AddComponentMenu("Cindy/Control/CameraControllerStack", 1)]
    [RequireComponent(typeof(Camera))]
    [DisallowMultipleComponent]
    public class CameraControllerStack : AbstractControllerStack
    {
        protected Camera _camera;

        public Camera Camera { get { return _camera != null ? _camera : (_camera = GetComponent<Camera>()); } }

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is CameraController;
        }

        protected override bool IsAvailable(Attachment attachment)
        {
            if(attachment is CameraController a)
            {
                return a != null && a.enabled && a.gameObject.activeSelf;
            }
            return false;
        }
    }
}