using UnityEngine;

namespace Cindy.Control.Cameras.PointLook
{
    /// <summary>
    /// 第三人称固定点摄像机控制器
    /// </summary>
    [AddComponentMenu("Cindy/Control/Camera/PointLook", 1)]
    public class PointLook : CameraController
    {
        /// <summary>
        /// 摄像机行为
        /// </summary>
        public PointLookCamera pointLookCamera;

        /// <summary>
        /// 点
        /// </summary>
        [Header("PointLook")]
        public Transform point;

        protected override CameraBehaviour GetCameraBehaviour()
        {
            return pointLookCamera;
        }

    }
}
