using UnityEngine;

namespace Cindy.Control
{
    /// <summary>
    /// 控制器组
    /// </summary>
    [AddComponentMenu("Cindy/Control/Controllers/ControllerGroup", 0)]
    public class ControllerGroup : Controller
    {
        /// <summary>
        /// 控制器数组
        /// </summary>
        public Controller[] controllers;

        public override void OnControllerSelect()
        {
            if (controllers != null)
                foreach (Controller controller in controllers)
                    if (controller != null)
                        controller.OnControllerSelect();
        }

        public override void OnControllerUnselect()
        {
            if (controllers != null)
                foreach (Controller controller in controllers)
                    if (controller != null)
                        controller.OnControllerUnselect();
        }

        public override void OnControllerUpdate(float deltaTime)
        {
            if (controllers != null)
                foreach (Controller controller in controllers)
                    if (controller != null)
                        controller.OnControllerUpdate(deltaTime);
        }
    }
}