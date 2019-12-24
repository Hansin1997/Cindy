using UnityEngine;

namespace Cindy.Control.Cameras.FreeLook
{

    [AddComponentMenu("Cindy/Control/Camera/FreeLook", 1)]
    public class FreeLook : CameraBehaviourAttachment
    {
        public FreeLookCamera freeLookCamera;
        protected override CameraBehaviour GetCameraBehaviour()
        {
            return freeLookCamera;
        }
    }
}
