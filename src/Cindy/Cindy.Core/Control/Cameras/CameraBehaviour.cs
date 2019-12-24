using UnityEngine;

namespace Cindy.Control.Cameras
{
    public abstract class CameraBehaviour : ScriptableObject
    {
        public abstract void OnCameraFocus(Camera camera, CameraBehaviourAttachment attachment);
        public abstract void OnCameraUpdate(Camera camera, CameraBehaviourAttachment attachment, float deltaTime);
        public abstract void OnCameraBlur(Camera camera, CameraBehaviourAttachment attachment);
    }
}
