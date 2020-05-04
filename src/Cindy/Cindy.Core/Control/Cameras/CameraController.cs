using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    /// <summary>
    /// 抽象摄像机控制器
    /// </summary>
    public abstract class CameraController : AbstractController
    {
        private Dictionary<string, object> temp;
        public Dictionary<string, object> Temp { get { return temp == null ? (temp = new Dictionary<string, object>()) : temp; } }
        protected override Type GetTargetType()
        {
            return typeof(CameraControllerStack);
        }

        protected abstract CameraBehaviour GetCameraBehaviour();

        public override void OnControllerSelect()
        {
            CameraBehaviour behaviour = GetCameraBehaviour();
            if(behaviour != null)
            {
                if(ControllerStack is CameraControllerStack ccs)
                {
                    Camera camera = ccs.Camera;
                    if (camera == null)
                        Debug.LogWarning("Camera is null!", ccs);
                    behaviour.OnCameraFocus(camera, this);
                }
                else
                {
                    Debug.LogError("ControllerStack is null or not a CameraControllerStack!", this);
                }
            }
        }

        public override void OnControllerUpdate(float deltaTime)
        {
            CameraBehaviour behaviour = GetCameraBehaviour();
            if (behaviour != null)
            {
                if (ControllerStack is CameraControllerStack ccs)
                {
                    Camera camera = ccs.Camera;
                    if (camera == null)
                        Debug.LogWarning("Camera is null!", ccs);
                    behaviour.OnCameraUpdate(camera, this, deltaTime);
                }
                else
                {
                    Debug.LogError("ControllerStack is null or not a CameraControllerStack!", this);
                }
            }
        }

        public override void OnControllerUnselect()
        {
            CameraBehaviour behaviour = GetCameraBehaviour();
            if (behaviour != null)
            {
                if (ControllerStack is CameraControllerStack ccs)
                {
                    Camera camera = ccs.Camera;
                    if (camera == null)
                        Debug.LogWarning("Camera is null!", ccs);
                    behaviour.OnCameraBlur(camera, this);
                }
                else
                {
                    Debug.LogError("ControllerStack is null or not a CameraControllerStack!", this);
                }
            }
        }
    }
}
