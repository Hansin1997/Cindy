using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/CharacterController/CharacterControllerVelocity (Vector3)")]
    public class CharacterControllerVelocity : Vector3Object
    {
        [Header("CharacterController")]
        public CharacterController characterController;

        public enum Direction
        {
            X,
            Y,
            Z
        }

        protected override void Start()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
            value = GetVelocityValue();
        }

        protected virtual Vector3 GetVelocityValue()
        {
            if (characterController == null)
                return Vector3.zero;
            return characterController.velocity;

        }

        public override Vector3 GetValue()
        {
            value = GetVelocityValue();
            return base.GetValue();
        }

        public override void SetValue(Vector3 value)
        {

        }
    }
}
