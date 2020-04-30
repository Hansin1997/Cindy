﻿using UnityEngine;

namespace Cindy.Control.Cameras
{
    [AddComponentMenu("Cindy/Control/Camera/BlockedCamera", 0)]
    public class BlockedCamera : CameraController
    {
        protected override CameraBehaviour GetCameraBehaviour()
        {
            return null;
        }

        public override void OnControllerSelect()
        {

        }

        public override void OnControllerUpdate(float deltaTime)
        {

        }

        public override void OnControllerUnselect()
        {

        }
    }
}