using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/CharacterControllerIsGrounded")]
    public class CharacterControllerIsGrounded : BoolObject
    {
        [Header("CharacterController")]
        public CharacterController characterController;

        protected override void Start()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
            if (characterController != null)
                value = characterController.isGrounded;
            else
                value = false;
        }

        public override bool GetValue()
        {
            if (characterController != null)
                value = characterController.isGrounded;
            else
                value = false;
            return base.GetValue();
        }

        public override void SetValue(bool value)
        {

        }
    }
}
