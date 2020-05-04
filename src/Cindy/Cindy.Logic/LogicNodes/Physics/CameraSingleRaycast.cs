using Cindy.Logic.ReferenceValues;
using Cindy.Util;
using UnityEngine;

namespace Cindy.Logic.LogicNodes.Physics
{
    /// <summary>
    /// 摄像机射线碰撞检测
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNodes/Physics/CameraSingleRaycast")]
    public class CameraSingleRaycast : AbstractRaycast
    {
        [Header("Camera And ScreenPoint")]
        public ReferenceString cameraName;
        public ReferenceVector2 screenPoint;
        public Camera.MonoOrStereoscopicEye monoOrStereoscopicEye = Camera.MonoOrStereoscopicEye.Mono;
        public ReferenceFloat maxDistance = new ReferenceFloat() { value = -1f };

        protected Camera _camera;
        public Camera _Camera
        {
            get
            {
                return _camera != null && _camera.name.Equals(cameraName.Value) ?
                    _camera :
                    _camera = Finder.Find<Camera>(cameraName.Value, false);
            }
        }

        protected override bool Raycast(out RaycastHit hit, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction)
        {
            Camera cam = _Camera;
            if (cam == null)
            {
                Debug.LogWarning("Camera Not Found!");
                hit = default;
                return false;
            }
            Ray ray = cam.ScreenPointToRay(screenPoint.Value, monoOrStereoscopicEye);
            if (maxDistance.Value >= 0)
            {
                return UnityEngine.Physics.Raycast(ray, out hit, maxDistance.Value, layerMask, queryTriggerInteraction);
            }
            else
            {
                return UnityEngine.Physics.Raycast(ray, out hit);
            }

        }
    }
}
