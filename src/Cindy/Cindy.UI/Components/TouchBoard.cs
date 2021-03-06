﻿using Cindy.Logic.ReferenceValues;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cindy.UI.Components
{
    /// <summary>
    /// 触摸板
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Cindy/UI/Components/TouchBoard")]
    public class TouchBoard : Selectable
    {
        /// <summary>
        /// 虚拟横轴
        /// </summary>
        [Header("Virtual Axis")]
        public string horizontalAxisName = "Horizontal";
        /// <summary>
        /// 虚拟纵轴
        /// </summary>
        public string verticalAxisName = "Vertical";
        /// <summary>
        /// 虚拟按钮
        /// </summary>
        public string buttonKey = "";

        public bool horizontal = true, vertical = true, button = false; // 是否启用

        public InputType inputType = InputType.Delta; // 触摸板输入类型

        public ReferenceFloat horizontalSensitivity = new ReferenceFloat() { value = 100 } , verticalSensitivity = new ReferenceFloat() { value = 100 };

        private PointerEventData ed;
        private Vector2 dp;
        private VirtualAxis horizontalAxis, verticalAxis;
        private VirtualButton _btn;
        private int state;
        private RectTransform rectTransform;

        protected override void Start()
        {
            base.Start();
            rectTransform = GetComponent<RectTransform>();
            if (horizontal)
            {
                horizontalAxis = () =>
                {
                    if (ed == null || rectTransform == null)
                        return 0;
                    switch (inputType)
                    {
                        default:
                        case InputType.Delta:
                            return ed.delta.x / Screen.width * horizontalSensitivity.Value;
                        case InputType.Position:
                            return (ed.position.x - dp.x) / Screen.width * horizontalSensitivity.Value;
                    }
                };
                VirtualInput.Register(horizontalAxisName, horizontalAxis);
            }
            if (vertical)
            {
                verticalAxis = () =>
                {
                    if (ed == null || rectTransform == null)
                        return 0;
                    switch (inputType)
                    {
                        default:
                        case InputType.Delta:
                            return ed.delta.y / Screen.height * horizontalSensitivity.Value;
                        case InputType.Position:
                            return (ed.position.y - dp.y) / Screen.height * horizontalSensitivity.Value;
                    }
                };
                VirtualInput.Register(verticalAxisName, verticalAxis);
            }
            if (button && buttonKey != null && buttonKey.Length > 0)
            {
                _btn = () => GetState();
                VirtualInput.Register(buttonKey, _btn);
            }
        }

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
            if(ed != null)
            {
                if(ed.pointerId == eventData.pointerId)
                {
                    ed = eventData;
                    dp = ed.position;
                    state = 1;
                }
            }
            else
            {
                ed = eventData;
                dp = ed.position;
                state = 1;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if(ed != null)
            {
                if(eventData.pointerId == ed.pointerId)
                {
                    ed = null;
                    state = -1;
                }
            }
            else
            {
                state = -1;
            }
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

        public enum InputType
        {
            Delta,
            Position
        }
    }
}
