using Cindy.Util;
using UnityEngine;

namespace Cindy.Control
{
    [AddComponentMenu("Cindy/Control/ControllerStack", 1)]
    public class ControllerStack : Attachable
    {
        private Controller selectedAttachment;

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is Controller;
        }

        protected virtual void FixedUpdate()
        {
            Controller top = Peek<Controller>();
            if (top != null)
            {
                if (top != selectedAttachment)
                {
                    if (selectedAttachment != null)
                        selectedAttachment.OnControllerUnselect();
                    top.OnControllerSelect();
                }
                top.OnControllerUpdate(Time.fixedDeltaTime);
            }
            selectedAttachment = top;
        }

        protected override bool IsPeek(Attachment attachment)
        {
            if (attachment != null && attachment.gameObject.activeSelf && attachment is Controller controllerBehaviour
                && controllerBehaviour.enabled)
                return true;
            return false;
        }
    }
}