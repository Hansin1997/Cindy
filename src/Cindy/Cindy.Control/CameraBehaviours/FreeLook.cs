using System;
using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{


    [AddComponentMenu("Cindy/Control/Camera/FreeLook", 1)]
    public class FreeLook : BaseCameraBehaviour
    {
        [Header("Free Look")]
        public AxesConfig axesConfig;
        public float distance = 1f;
        [Range(0, 90)]
        public float verticalAngleLimit = 80;

        protected Vector2 r;


        public override void OnCameraFocus(Camera camera)
        {
            if (target == null)
                return;
            Vector3 dir = (camera.transform.position - target.transform.position);
            Vector3 xz = dir;
            xz.y = 0;

            r.x = Vector3.SignedAngle(dir, Vector3.forward, -Vector3.up);
            r.y = Vector3.Angle(xz, dir);
        }

        public override void OnCameraUpdate(Camera camera, float deltaTime)
        {
            r.x += deltaTime * VirtualInput.GetAxis(axesConfig.horizontalAxis) * axesConfig.horizontalScale;
            r.y += deltaTime * VirtualInput.GetAxis(axesConfig.verticalAxis) * axesConfig.verticalScale;
            base.OnCameraUpdate(camera, deltaTime);
        }

        protected override Vector3 GetPosition(Camera camera)
        {
            if (r.y > verticalAngleLimit)
                r.y = verticalAngleLimit;
            if (r.y < -verticalAngleLimit)
                r.y = -verticalAngleLimit;
            Vector3 dir = Vector3.forward;
            dir = Quaternion.Euler(Vector3.up * r.x) * dir;
            Vector3 axis = Quaternion.Euler(0, -90, 0) * dir;
            dir = Quaternion.AngleAxis(r.y, axis) * dir;
            return target.transform.position + dir * distance;
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
