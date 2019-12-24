using UnityEngine;

namespace Cindy.Control.Cameras.PointLook
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/Control/Camera/PointLook", 1)]
    public class PointLook : CameraBehaviourAttachment
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
