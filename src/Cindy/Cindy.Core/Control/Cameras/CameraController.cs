using Cindy.Util;
using System;
using System.Collections.Generic;

namespace Cindy.Control.Cameras
{
    public abstract class CameraController : Attachment
    {
        public CameraBehaviour Behaviour
        {
            get
            {
                return GetCameraBehaviour();
            }

        }

        private Dictionary<string, object> temp;
        public Dictionary<string,object> Temp
        {
            get
            {
                if (temp == null)
                    temp = new Dictionary<string, object>();
                return temp;
            }
        }
        protected override Type GetAttachableType()
        {
            return typeof(CameraControllerStack);
        }

        protected abstract CameraBehaviour GetCameraBehaviour();
    }
}
