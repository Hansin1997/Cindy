using UnityEngine;

namespace Cindy.Control.Cameras
{
    public abstract class CameraBehaviour : ScriptableObject
    {
        public abstract void OnCameraFocus(Camera camera, CameraController attachment);
        public abstract void OnCameraUpdate(Camera camera, CameraController attachment, float deltaTime);
        public abstract void OnCameraBlur(Camera camera, CameraController attachment);
    }
}
