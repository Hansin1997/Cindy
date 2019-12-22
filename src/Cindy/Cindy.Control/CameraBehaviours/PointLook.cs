using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{

    [AddComponentMenu("Cindy/Control/Camera/PointLook", 1)]
    public class PointLook : CameraBehaviour
    {
        public override void OnCameraBlur(Camera camera)
        {

        }

        public override void OnCameraFocus(Camera camera)
        {

        }

        public override void OnCameraUpdate(Camera camera, float deltaTime)
        {
            if(target != null)
            {
                camera.transform.position = transform.position;
                camera.transform.LookAt(target.transform);
            }
        }
    }
}
