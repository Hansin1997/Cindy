using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{
    public abstract class CameraBehaviour : ScriptableObject
    {
        public abstract void OnCameraFocus(Camera camera, Transform target, IDictionary<string, object> parameters);
        public abstract void OnCameraUpdate(Camera camera, Transform target, float deltaTime, IDictionary<string, object> parameters);
        public abstract void OnCameraBlur(Camera camera, Transform target, IDictionary<string, object> parameters);

    }
}
