using System;
using UnityEngine;

namespace Cindy.Control.Controllers
{
    [AddComponentMenu("Cindy/Control/Controllers/SimpleController", 1)]
    public class SimpleController : ControllerAttachment
    {
        [Serializable]
        public class AxesMap
        {
            public string Horizontal = "Horizontal";
            public string Vertical = "Vertical";
        }

        [Header("Simple Controller")]
        public AxesMap axesMap;
        public string jumpButton = "Jump";

        [Header("Physics")]
        public bool useGravity = true;
        public float mass = 1f;
        public float movingPower = 1f;
        public float jumpPower = 1f;

        protected Vector3 direction;
        protected bool jump;

        protected CharacterController characterController;

        protected virtual void FixedUpdate()
        {
            if (Target == null || (characterController = Target.GetComponent<CharacterController>()) == null)
                return;
            if (useGravity)
            {
                if (characterController.isGrounded)
                {
                    if (jump)
                    {
                        direction.y = jumpPower / mass;
                        jump = false;
                    }
                    else
                        direction.y = 0;
                }
                else
                {
                    direction += Physics.gravity * Time.fixedDeltaTime;
                    jump = false;
                }
            }
            characterController.Move(direction * Time.fixedDeltaTime);
        }

        public override void OnControllerSelect()
        {

        }

        public override void OnControllerUpdate(float deltaTime)
        {
            if (characterController == null)
                return;
            Camera camera = Camera.main;
            if (camera == null || characterController == null)
                return;
            Vector3 dir = Vector3.forward * VirtualInput.GetAxis(axesMap.Vertical) + Vector3.right * VirtualInput.GetAxis(axesMap.Horizontal);
            dir = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * dir;

            float y = direction.y;
            direction = dir * (movingPower / mass);
            direction.y = y;
            if (VirtualInput.GetButtonDown(jumpButton))
                jump = true;
                
        }

        public override void OnControllerUnselect()
        {

        }

    }
}
