using UnityEngine;

namespace Cindy.Control.Cameras.PointLook
{
    [CreateAssetMenu(fileName = "PointLookCamera", menuName = "Cindy/Control/Camera/PointLook", order = 1)]
    public class PointLookCamera : BaseCameraBehaviour
    {

        protected override Vector3 GetPosition(Camera camera, CameraController attachment, float deltaTime)
        {
            if(attachment is PointLook pointLook && pointLook.point != null)
            {
                return pointLook.point.position;
            }
            else
            {
                return camera.transform.position;
            }
                
        }
    }
}
