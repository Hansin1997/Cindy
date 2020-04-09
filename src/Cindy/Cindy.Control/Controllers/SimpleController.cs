using Cindy.Logic.ReferenceValues;
using System;
using UnityEngine;

namespace Cindy.Control.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Cindy/Control/Controllers/SimpleController", 1)]
    public class SimpleController : Controller
    {
        [Serializable]
        public class AxisValue
        {
            public ReferenceFloat Horizontal;
            public ReferenceFloat Vertical;
            public ReferenceBool Jump;
        }

        [Header("Simple Controller")]
        public AxisValue axesValue;

        [Header("Physics")]
        public ReferenceBool useGravity = new ReferenceBool() {  value = true };
        public ReferenceFloat mass = new ReferenceFloat() { value = 1f };
        public ReferenceFloat movingPower = new ReferenceFloat() { value = 1f };
        public ReferenceFloat jumpPower = new ReferenceFloat() { value = 1f };

        protected Vector3 direction;
        protected bool jumping;

        protected bool selected;
        protected CharacterController characterController;

        protected virtual void FixedUpdate()
        {
            if (characterController == null)
                return;
            if (useGravity.Value)
            {
                if (characterController.isGrounded)
                {
                    direction.y = 0;
                    jumping = false;
                }
                else
                {
                    direction += Physics.gravity * Time.fixedDeltaTime;
                    jumping = true;
                }
            }

            if (selected)
            {
                if (axesValue.Jump.Value && !jumping)
                {
                    jumping = true;
                    direction.y = jumpPower.Value / mass.Value;
                }
            }
            characterController.Move(direction * Time.fixedDeltaTime);
        }

        public override void OnControllerSelect()
        {
            characterController = GetComponent<CharacterController>();
            selected = true;
        }

        protected virtual void Update()
        {
        }

        public override void OnControllerUpdate(float deltaTime)
        {
            if (characterController == null)
                return;
            if (characterController == null || !characterController.enabled)
                return;
            Vector3 dir = Vector3.forward * axesValue.Vertical.Value + Vector3.right * axesValue.Horizontal.Value;
            dir = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * dir;

            float y = direction.y;
            direction = dir * (movingPower.Value / mass.Value);
            direction.y = y;
        }

        public override void OnControllerUnselect()
        {
            selected = false;
        }

    }
}