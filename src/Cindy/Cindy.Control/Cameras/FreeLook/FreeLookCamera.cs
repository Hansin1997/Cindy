using System;
using UnityEngine;

namespace Cindy.Control.Cameras
{
    /// <summary>
    /// 第三人称自由视角摄像机行为
    /// </summary>
    [CreateAssetMenu(fileName = "FreeLookCamera", menuName = "Cindy/Control/Camera/FreeLook", order = 1)]
    public class FreeLookCamera : BaseCameraBehaviour
    {
        /// <summary>
        /// 输入轴配置
        /// </summary>
        [Header("Free Look")]
        public AxesConfig axesConfig;
        /// <summary>
        /// 距离目标的距离
        /// </summary>
        public float distance = 5f;
        /// <summary>
        /// 上下角度限制
        /// </summary>
        [Range(0, 90)]
        public float verticalAngleLimit = 80;

        public override void OnCameraFocus(Camera camera, CameraController attachment)
        {
            Vector3 dir = (camera.transform.position - attachment.transform.position);
            Vector3 xz = dir;
            xz.y = 0;

            Vector2 r;

            r.x = Vector3.SignedAngle(dir, Vector3.forward, -Vector3.up);
            r.y = Vector3.Angle(xz, dir);
            attachment.Temp["r"] = r;
        }

        protected override Vector3 GetPosition(Camera camera, CameraController attachment, float deltaTime)
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
            return attachment.transform.position + dir * distance;
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
