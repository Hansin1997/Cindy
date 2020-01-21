using UnityEngine;

namespace Cindy.Control.Cameras
{

    [AddComponentMenu("Cindy/Control/Camera/BlockedCamera", 0)]
    public class BlockedCamera : CameraBehaviourAttachment
    {
        protected override CameraBehaviour GetCameraBehaviour()
        {
            return null;
        }
    }
}
