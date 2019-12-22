using System;
using UnityEngine;

namespace Cindy.Control
{
    public abstract class CameraBehaviour : MonoBehaviour
    {
        public CameraBehaviourConfig config;

        protected CameraController Controller
        {
            get
            {
                CameraController[] controllers = FindObjectsOfType<CameraController>();
                foreach (CameraController controller in controllers)
                    if (controller.gameObject.name.Equals(config.controllerName))
                        return controller;
                return null;
            }
        }
        protected virtual void Start()
        {
            CameraController controller = Controller;
            if (controller != null)
                controller.Add(this);
        }

        protected virtual void OnDestroy()
        {
            CameraController controller = Controller;
            if (controller != null)
                controller.Remove(this);
        }

        public abstract void OnCameraFocus(Camera camera);
        public abstract void OnCameraUpdate(Camera camera,float deltaTime);
        public abstract void OnCameraBlur(Camera camera);

        [Serializable]
        public class CameraBehaviourConfig
        {
            public string controllerName = "Main Camera";
            public int order = 0;
        }
    }
}
