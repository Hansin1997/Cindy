using UnityEngine;

namespace Cindy.Logic.VariableObjects
{

    [AddComponentMenu("Cindy/Logic/VariableObject/ButtonValue")]
    public class ButtonValue : BoolObject
    {
        [Header("Button")]
        public string buttonName;

        protected override void Start()
        {
            value = VirtualInput.GetButton(buttonName);
        }

        protected override void Update()
        {
            value = VirtualInput.GetButton(buttonName);
            base.Update();
        }
    }
}
