﻿using System;
using UnityEngine;

namespace Cindy.Control.Controller
{
    public class ThirdPersonController : MonoBehaviour
    {
        [Serializable]
        public class AxesMap
        {
            public string Horizontal = "Horizontal";
            public string Vertical = "Vertical";
        }

        public Controllable target;
        public AxesMap axesMap;

        public virtual void Start()
        {
            if (target == null)
                target = GetComponent<Controllable>();
        }

        public virtual void FixedUpdate()
        {
            Camera camera = Camera.main;
            if (camera == null || target == null || !target.IsControllable())
                return;
            Vector3 dir = Vector3.forward * VirtualInput.GetAxis(axesMap.Vertical) + Vector3.right * VirtualInput.GetAxis(axesMap.Horizontal);
            dir = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * dir;

            target.Move(dir.normalized);
        }

    }
}
