using Cindy.Util;
using UnityEngine;

namespace Cindy.Control
{
    [AddComponentMenu("Cindy/Control/ControllerStack", 1)]
    [DisallowMultipleComponent]
    public class ControllerStack : AbstractControllerStack
    {
        protected override bool CheckAttachment(Attachment attachment)
        {
            return attachment is Controller;
        }

        protected override bool IsAvailable(Attachment attachment)
        {
            if (attachment != null && attachment.gameObject.activeSelf && attachment is Controller controllerBehaviour
                && controllerBehaviour.enabled)
                return true;
            return false;
        }
    }
}