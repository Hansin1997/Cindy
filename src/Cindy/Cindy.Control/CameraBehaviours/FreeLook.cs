using System;
using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{
    public class FreeLook : CameraBehaviour
    {
        public Transform target;
        public AxesConfig axesConfig;
        public float distance = 1f;

        protected Vector2 r;
        [Range(0,90)]
        public float verticalAngleLimit = 80;
        public override void OnCameraBlur(Camera camera)
        {

        }

        public override void OnCameraFocus(Camera camera)
        {
            if (target == null)
                return;
            Vector3 dir = (camera.transform.position - target.transform.position);
            Vector3 xz = dir;
            xz.y = 0;
            Vector3 y = dir;
            y.x = y.z = 0;

            r.x = Vector3.Angle(xz, -Vector3.forward);
            r.y = Vector3.Angle(xz, y);
        }

        public override void OnCameraUpdate(Camera camera, float deltaTime)
        {
            if (target == null)
                return;
            r.x += deltaTime * VirtualInput.GetAxis(axesConfig.horizontalAxis) * axesConfig.horizontalScale;
            r.y += deltaTime * VirtualInput.GetAxis(axesConfig.verticalAxis) * axesConfig.verticalScale;
            if (r.y > verticalAngleLimit)
                r.y = verticalAngleLimit;
            if (r.y < -verticalAngleLimit)
                r.y = -verticalAngleLimit;
            Vector3 dir = -Vector3.forward;
            dir = Quaternion.Euler(Vector3.up * r.x) * dir;
            Vector3 axis = Quaternion.Euler(0, -90, 0) * dir;
            dir = Quaternion.AngleAxis(r.y, axis) * dir;
            camera.transform.position = target.transform.position + dir * distance;
            camera.transform.LookAt(target);
        }

        [Serializable]
        public class AxesConfig
        {
            public string horizontalAxis = "Mouse X";
            public float horizontalScale = 1;
            public string verticalAxis = "Mouse Y";
            public float verticalScale = 1;

        }

    }
}
