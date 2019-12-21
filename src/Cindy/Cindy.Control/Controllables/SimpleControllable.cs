using System;
using UnityEngine;

namespace Cindy.Control.Controllables
{
    [RequireComponent(typeof(CharacterController))]
    public class SimpleControllable : Controllable
    {
        protected CharacterController characterController;

        protected Vector3 direction;

        protected virtual void OnEnable()
        {
            characterController = GetComponent<CharacterController>();
            characterController.enabled = true;
        }

        protected virtual void FixedUpdate()
        {
            if(IsControllable())
            {
                characterController.SimpleMove(direction * Time.fixedDeltaTime);
            }

        }

        protected virtual void OnControllerColliderHit(ControllerColliderHit hit)
        {
            float offsetY = (hit.point - transform.position).y;
            Rigidbody r;
            if ((r = hit.gameObject.GetComponent<Rigidbody>()) != null && hit.controller.collisionFlags != CollisionFlags.Below)
            {
                if (offsetY > characterController.stepOffset)
                {
                    // m*v + M*V = (m + M)V' , V' = (m*v + M*V) / (m + M)
                    Vector3 vector = (r.mass * r.velocity + rigbody.mass * characterController.velocity) / (r.mass + rigbody.mass);

                    r.velocity = vector;
                    direction = vector;
                }
            }
        }

        protected override void DoMove(Vector3 direction)
        {
            this.direction = direction * (rigbody.movingPower / rigbody.mass);
        }

        protected virtual void OnDisable()
        {
            characterController.enabled = false;
        }
    }
}
