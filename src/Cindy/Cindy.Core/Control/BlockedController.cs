using UnityEngine;

namespace Cindy.Control
{
    /// <summary>
    /// 阻塞控制器（空控制器）
    /// </summary>
    [AddComponentMenu("Cindy/Control/Controllers/BlockedController", 0)]
    public class BlockedController : Controller
    {
        public override void OnControllerSelect()
        {
            
        }

        public override void OnControllerUnselect()
        {

        }

        public override void OnControllerUpdate(float deltaTime)
        {

        }
    }
}