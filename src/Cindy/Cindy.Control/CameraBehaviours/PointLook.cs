using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{

    [AddComponentMenu("Cindy/Control/Camera/PointLook", 1)]
    public class PointLook : BaseCameraBehaviour
    {
        [Header("PointLook")]
        public bool keepOriginPosition;

        protected Vector3 originPosition;

        public override void OnCameraFocus(Camera camera)
        {
            originPosition = camera.transform.position;
        }

        protected override Vector3 GetPosition(Camera camera)
        {
            return keepOriginPosition ? originPosition : transform.position;
        }
    }
}
