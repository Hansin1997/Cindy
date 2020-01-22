using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Cindy.UI.Components.UIAttachments
{
    public class SimpleUIAttachment : UIAttachment
    {
        public RectTransform[] templates;

        public override IList<RectTransform> GenerateComponents(GameObject root)
        {

        }

        public override bool IsActived()
        {
            throw new NotImplementedException();
        }
    }
}
