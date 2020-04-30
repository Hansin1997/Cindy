using System;

namespace Cindy.Control
{
    public abstract class Controller : AbstractController
    {
        protected override Type GetTargetType()
        {
            return typeof(ControllerStack);
        }
    }
}