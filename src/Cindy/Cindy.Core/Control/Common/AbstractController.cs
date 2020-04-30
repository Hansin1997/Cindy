using Cindy.Util;

namespace Cindy.Control
{
    public abstract class AbstractController : Attachment
    {
        public AbstractControllerStack ControllerStack { get; internal set; }
        public abstract void OnControllerSelect();
        public abstract void OnControllerUpdate(float deltaTime);
        public abstract void OnControllerUnselect();
    }
}