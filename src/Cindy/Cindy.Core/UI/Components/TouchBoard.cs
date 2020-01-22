using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cindy.UI.Components
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/UI/Components/TouchBoard")]
    public class TouchBoard : Selectable
    {
        [Header("Virtual Axis")]
        public string horizontalAxisName = "Horizontal";
        public string verticalAxisName = "Vertical";
        public string buttonKey = "";

        public bool horizontal = true, vertical = true, button = true;

        Vector2 lastPos;
        private PointerEventData ed;
        private VirtualAxis horizontalAxis, verticalAxis;
        private VirtualButton _btn;
        private int state;

        protected override void Start()
        {
            base.Start();
            if (horizontal)
            {
                horizontalAxis = () => ed == null ? 0 : ed.delta.x / Screen.width * 100;
                VirtualInput.Register(horizontalAxisName, horizontalAxis);
            }
            if (vertical)
            {
                verticalAxis = () => ed == null ? 0 : ed.delta.y / Screen.height * 100;
                VirtualInput.Register(verticalAxisName, verticalAxis);
            }
            if (button && buttonKey != null && buttonKey.Length > 0)
            {
                _btn = () => GetState();
                VirtualInput.Register(buttonKey, _btn);
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (state == 1)
                state = 2;
            else if (state == 2)
                state = 3;
            else if (state == -1)
                state = -2;
            else if (state == -2)
                state = 0;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            ed = eventData;
            state = 1;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            ed = null;
            state = -1;
        }

        protected override void OnDestroy()
        {
            if (horizontalAxis != null)
                VirtualInput.Remove(horizontalAxisName, horizontalAxis);
            if (verticalAxis != null)
                VirtualInput.Remove(verticalAxisName, verticalAxis);
            if (_btn != null)
                VirtualInput.Remove(buttonKey, _btn);
        }

        protected virtual ButtonState GetState()
        {
            ButtonState result = new ButtonState();
            result.down = (state == 2);
            result.value = state > 0;
            result.up = (state == -2);
            return result;
        }
    }
}
