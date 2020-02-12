using Cindy.Logic.ReferenceValues;
using UnityEngine;
using static UnityEngine.RectTransform;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/RelativedAxis")]
    public class RelativedAxis : FloatObject
    {
        [Header("Axis")]
        public ReferenceString horizontalAxis = new ReferenceString() { value = "Horizontal" };
        public ReferenceString verticalAxis = new ReferenceString() { value = "Vertical" };
        public Axis valueType;

        [Header("Camera")]
        public ReferenceString cameraName = new ReferenceString() { value = "Main Camera" };
        public ReferenceString objectName;

        protected Camera targetCam;
        protected GameObject targetObj;
        private string lastNameCam, lastNameObj;

        protected override void Start()
        {
            FindCam();
            FindObj();
        }

        protected virtual void FindCam()
        {
            lastNameCam = cameraName.Value;
            targetCam = FindCameraByName(lastNameCam, Camera.main);
        }

        protected virtual void FindObj()
        {
            lastNameObj = objectName.Value;
            targetObj = FindObjectByName(lastNameObj);
        }

        protected bool ValueExist()
        {
            return horizontalAxis.Value != null && verticalAxis.Value != null
                && horizontalAxis.Value.Length > 0 && verticalAxis.Value.Length > 0;
        }

        protected virtual void ComputeValue()
        {
            if (ValueExist() && targetCam != null && targetObj != null)
            {
                Vector3 input = new Vector3(VirtualInput.GetAxis(horizontalAxis.Value), 0, VirtualInput.GetAxis(verticalAxis.Value));
                Vector3 vector;
                
                float angle = targetCam.transform.rotation.eulerAngles.y;
                input = Quaternion.Euler(Vector3.up * angle) * input;

                switch (valueType)
                {
                    case Axis.Horizontal:
                        vector = targetObj.transform.right;
                        break;
                    case Axis.Vertical:
                        vector = targetObj.transform.forward;
                        break;
                    default:
                        value = 0;
                        return;
                }

                vector.y = 0;
                vector.Normalize();

                Vector3 p = Vector3.Project(input, vector);
                if (Vector3.Angle(p, vector) <= 90)
                    value = p.magnitude;
                else
                    value = -p.magnitude;
            }
            else
                value = 0;

        }

        public  static GameObject FindObjectByName(string name,GameObject defaultObject = null)
        {
            if (name == null)
                return defaultObject;
            GameObject result = GameObject.Find(name);
            if (result == null)
                return defaultObject;
            return result;
        }

        public static Camera FindCameraByName(string name, Camera defaultCamera = null)
        {
            if (name == null)
                return defaultCamera;
            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
                if (camera.gameObject.name.Equals(name))
                    return camera;
            return defaultCamera;
        }

        public override void SetValue(float value)
        {

        }

        public override float GetValue()
        {
            if (cameraName.Value != null && !cameraName.Value.Equals(lastNameCam))
                FindCam();
            if (objectName.Value != null && !objectName.Value.Equals(lastNameObj))
                FindObj();
            ComputeValue();
            return base.GetValue();
        }
    }
}
