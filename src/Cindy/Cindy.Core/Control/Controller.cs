using System;

namespace Cindy.Control
{
    /// <summary>
    /// 通用控制器
    /// </summary>
    public abstract class Controller : AbstractController
    {
        protected override Type GetTargetType()
        {
            return typeof(ControllerStack);
        }
    }
}