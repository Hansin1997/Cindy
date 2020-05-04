using UnityEngine;

namespace Cindy.Control.Cameras.FreeLook
{
    /// <summary>
    /// 第三人称自由视角摄像机控制器
    /// </summary>
    [AddComponentMenu("Cindy/Control/Camera/FreeLook", 1)]
    public class FreeLook : CameraController
    {
        /// <summary>
        /// 摄像机行为
        /// </summary>
        public FreeLookCamera freeLookCamera;

        protected override CameraBehaviour GetCameraBehaviour()
        {
            return freeLookCamera;
        }
    }
}
