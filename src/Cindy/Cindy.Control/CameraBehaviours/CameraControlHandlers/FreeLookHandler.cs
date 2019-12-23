using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.CameraBehaviours.CameraControlHandlers
{
    [AddComponentMenu("Cindy/Control/Camera/FreeLookHandler", 1)]
    public class FreeLookHandler : CameraControlHandler
    {
        [Header("FreeLook")]
        public Transform point;

        protected override void OnGetParameters(IDictionary<string, object> parameters)
        {
            parameters[PointLook.POINT_KEY] = point;
        }
    }
}
