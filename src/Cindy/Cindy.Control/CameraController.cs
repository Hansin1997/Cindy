using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        public List<CameraBehaviour> behaviours;

        protected Camera _camera;

        protected CameraBehaviour focusedBehaviour;

        public Camera Camera
        {
            get
            {
                if (_camera == null)
                    _camera = GetComponent<Camera>();
                return _camera;
            }
        }

        public virtual void Add(CameraBehaviour behaviour)
        {
            if (behaviours.Contains(behaviour))
                return;
            if (behaviours.Count == 0)
                behaviours.Add(behaviour);
            else
                for (int i = behaviours.Count - 1; i >= 0; i--)
                {
                    CameraBehaviour tmp = behaviours[i];
                    if (tmp.config.order <= behaviour.config.order)
                    {
                        behaviours.Insert(i + 1, behaviour);
                        break;
                    }
                    else if (i == 0)
                    {
                        behaviours.Insert(i, behaviour);
                        break;
                    }
                }
        }

        public virtual void Remove(CameraBehaviour behaviour)
        {
            if (!behaviours.Contains(behaviour))
                return;
            behaviours.Remove(behaviour);
        }

        protected virtual void FixedUpdate()
        {
            CameraBehaviour top = null;
            for (int i = behaviours.Count - 1;i >= 0;i--)
            {
                CameraBehaviour behaviour = behaviours[i];
                if (behaviour == null || !behaviour.enabled || !behaviour.gameObject.activeSelf)
                    continue;
                top = behaviour;
                break;
            }
            if(top != null)
            {
                if (top != focusedBehaviour)
                {
                    if (focusedBehaviour != null)
                    {
                        focusedBehaviour.OnCameraBlur(Camera);
                    }
                    top.OnCameraFocus(Camera);
                }
                top.OnCameraUpdate(Camera,Time.fixedDeltaTime);
            }
            focusedBehaviour = top;
        }
    }
}
