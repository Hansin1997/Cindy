using UnityEngine;

namespace Cindy.Control.Cameras.PointLook
{
    [AddComponentMenu("Cindy/Control/Camera/PointLook", 1)]
    public class PointLook : CameraController
    {
        public PointLookCamera pointLookCamera;

        [Header("PointLook")]
        public Transform point;

        protected override CameraBehaviour GetCameraBehaviour()
        {
            return pointLookCamera;
        }

    }
}
