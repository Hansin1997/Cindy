using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/CharacterControllerVelocity")]
    public class CharacterControllerVelocity : FloatObject
    {
        [Header("CharacterController")]
        public CharacterController characterController;
        public Direction direction = Direction.Y;

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
            value = GetValue();
        }

        protected override void Update()
        {
            value = GetValue();
            base.Update();
        }

        protected virtual float GetValue()
        {
            if (characterController == null)
                return 0;
            switch(direction)
            {
                case Direction.X:
                    return characterController.velocity.x;
                case Direction.Y:
                    return characterController.velocity.y;
                case Direction.Z:
                    return characterController.velocity.z;
                default:
                    return 0;
            }
        }
    }
}
