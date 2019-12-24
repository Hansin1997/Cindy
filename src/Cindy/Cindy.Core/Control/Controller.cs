using Cindy.Util;
using UnityEngine;

namespace Cindy.Control
{
    [AddComponentMenu("Cindy/Control/Controller", 1)]
    public class Controller : Attachable
    {
        private ControllerAttachment selectedAttachment;

        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is ControllerAttachment;
        }

        protected virtual void FixedUpdate()
        {
            ControllerAttachment top = Peek<ControllerAttachment>();
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
            if (attachment != null && attachment.gameObject.activeSelf && attachment is ControllerAttachment controllerBehaviour
                && controllerBehaviour.enabled)
                return true;
            return false;
        }
    }
}