using Cindy.Control.CameraBehaviours;
using Cindy.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control
{

    [AddComponentMenu("Cindy/Control/Camera/CameraControlHandle", 1)]
    public class CameraControlHandler : Attachment<CameraBehaviour>
    {

        public Transform target;

        public override void Attach()
        {
            CameraController controller = FindTarget();
            if (controller != null)
                controller.Attach(this);
        }

        public override void Detach()
        {
            CameraController controller = FindTarget();
            if (controller != null)
                controller.Detach(this);
        }

        protected CameraController FindTarget()
        {
            CameraController[] controllers = FindObjectsOfType<CameraController>();
            foreach (CameraController controller in controllers)
                if (controller.gameObject.name.Equals(targetName))
                    return controller;
            return null;
        }

        protected override void OnGetParameters(IDictionary<string, object> parameters)
        {

        }

    }
}
