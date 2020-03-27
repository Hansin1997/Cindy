using Cindy.Util;
using System;

namespace Cindy.Control
{
    public abstract class Controller : Attachment
    {
        public abstract void OnControllerSelect();

        public abstract void OnControllerUpdate(float deltaTime);

        public abstract void OnControllerUnselect();

        protected override Type GetAttachableType()
        {
            return typeof(ControllerStack);
        }
    }
}