﻿using System;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    [CreateAssetMenu(fileName = "FreeLookCamera", menuName = "Cindy/Control/Camera/FreeLook", order = 1)]
    public class FreeLookCamera : BaseCameraBehaviour
    {
        [Header("Free Look")]
        public AxesConfig axesConfig;
        public float distance = 5f;
        [Range(0, 90)]
        public float verticalAngleLimit = 80;

        public override void OnCameraFocus(Camera camera, CameraBehaviourAttachment attachment)
        {
            Vector3 dir = (camera.transform.position - attachment.CameraTarget.transform.position);
            Vector3 xz = dir;
            xz.y = 0;

            Vector2 r;

            r.x = Vector3.SignedAngle(dir, Vector3.forward, -Vector3.up);
            r.y = Vector3.Angle(xz, dir);
            attachment.Temp["r"] = r;
        }

        protected override Vector3 GetPosition(Camera camera, CameraBehaviourAttachment attachment, float deltaTime)
        {

            Vector2 r = Vector2.zero;
            object R;
            if (attachment.Temp.TryGetValue("r", out R) && R is Vector2 tmp)
                r = tmp;

            r.x += deltaTime * VirtualInput.GetAxis(axesConfig.horizontalAxis) * axesConfig.horizontalScale;
            r.y += deltaTime * VirtualInput.GetAxis(axesConfig.verticalAxis) * axesConfig.verticalScale;
            if (r.y > verticalAngleLimit)
                r.y = verticalAngleLimit;
            if (r.y < -verticalAngleLimit)
                r.y = -verticalAngleLimit;
            attachment.Temp["r"] = r;

            Vector3 dir = Vector3.forward;
            dir = Quaternion.Euler(Vector3.up * r.x) * dir;
            Vector3 axis = Quaternion.Euler(0, -90, 0) * dir;
            dir = Quaternion.AngleAxis(r.y, axis) * dir;
            return attachment.CameraTarget.transform.position + dir * distance;
        }

        [Serializable]
        public class AxesConfig
        {
            public string horizontalAxis = "Mouse X";
            public float horizontalScale = 10;
            public string verticalAxis = "Mouse Y";
            public float verticalScale = 10;
        }
    }
}