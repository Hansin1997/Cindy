using Cindy.Util;
using UnityEngine;

namespace Cindy.Control
{
    public abstract class AbstractControllerStack : AttachmentContainer
    {
        [Header("Controller Stack")]
        public UpdateType updateType = UpdateType.OnFixedUpdate;

        protected AbstractController selectedController;

        protected virtual void Update()
        {
            if (updateType == UpdateType.OnUpdate)
                DoUpdate(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            if (updateType == UpdateType.OnFixedUpdate)
                DoUpdate(Time.fixedDeltaTime);
        }

        protected virtual void DoUpdate(float deltaTime)
        {
            AbstractController top = Peek<AbstractController>();
            if (top != null)
            {
                top.ControllerStack = this;
                if (top != selectedController)
                {
                    if (selectedController != null)
                        OnControllerUnselect(selectedController);
                    OnControllerSelect(top);
                }
                OnControllerUpdate(top, deltaTime);
            }
            selectedController = top;
        }

        protected virtual void OnControllerSelect(AbstractController controller)
        {
            controller.OnControllerSelect();
        }

        protected virtual void OnControllerUpdate(AbstractController controller, float deltaTime)
        {
            controller.OnControllerUpdate(deltaTime);
        }

        protected virtual void OnControllerUnselect(AbstractController controller)
        {
            controller.OnControllerUnselect();
        }

        public enum UpdateType
        {
            OnUpdate,
            OnFixedUpdate,
            Node
        }
    }
}