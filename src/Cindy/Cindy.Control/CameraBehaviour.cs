using System;
using UnityEngine;

namespace Cindy.Control
{
    public abstract class CameraBehaviour : MonoBehaviour
    {
        public CameraBehaviourConfig config;

        public Transform target;

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
            if (config.attachOnStart)
                Attach();
        }

        protected virtual void OnDestroy()
        {
            if (config.detachOnDestroy)
                Detach();
        }

        public bool Detach()
        {
            CameraController controller = Controller;
            if (controller != null)
            {
                controller.Remove(this);
                return true;
            }
            return false;
        }

        public bool Attach()
        {

            CameraController controller = Controller;
            if (controller != null)
            {
                controller.Add(this);
                return true;
            }
            return false;
        }

        public abstract void OnCameraFocus(Camera camera);
        public abstract void OnCameraUpdate(Camera camera,float deltaTime);
        public abstract void OnCameraBlur(Camera camera);

        [Serializable]
        public class CameraBehaviourConfig
        {
            public string controllerName = "Main Camera";
            public int order = 0;

            public bool attachOnStart = false;
            public bool detachOnDestroy = false;
        }
    }
}
