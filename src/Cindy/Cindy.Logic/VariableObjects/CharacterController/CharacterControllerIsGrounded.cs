using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 角色控制器是否在地上
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/CharacterController/CharacterControllerIsGrounded (Bool)")]
    public class CharacterControllerIsGrounded : BoolObject
    {
        [Header("CharacterController")]
        public CharacterController characterController;

        protected virtual void Start()
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
