using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Control.CameraBehaviours
{
    public abstract class BaseCameraBehaviour : CameraBehaviour
    {
        [Header("Base Config")]
        public AnimationCurve cameraSpeed;
        public bool raycast = true;
        public bool lookAtTarget = true;

        public override void OnCameraBlur(Camera camera, Transform target, IDictionary<string, object> parameters)
        {

        }

        public override void OnCameraFocus(Camera camera, Transform target, IDictionary<string, object> parameters)
        {

        }

        public override void OnCameraUpdate(Camera camera, Transform target, float deltaTime, IDictionary<string, object> parameters)
        {
            Vector3 newPosition = GetPosition(camera, target, deltaTime, parameters);
            if (raycast)
                newPosition = ProcessRaycast(camera, target, newPosition, deltaTime, parameters);

            Vector3 dir = camera.transform.position - newPosition;
            float t = cameraSpeed.Evaluate(dir.magnitude);
            camera.transform.position = Vector3.Lerp(camera.transform.position, newPosition, t);
            if (lookAtTarget)
                camera.transform.LookAt(target);
        }

        protected abstract Vector3 GetPosition(Camera camera, Transform target, float deltaTime, IDictionary<string, object> parameters);

        protected virtual Vector3 ProcessRaycast(Camera camera, Transform target, Vector3 newPosition, float deltaTime, IDictionary<string, object> parameters)
        {
            Vector3 direction = newPosition - target.transform.position;
            RaycastHit[] hits = Physics.RaycastAll(target.transform.position, direction, direction.magnitude);
            RaycastHit HIT = default;
            bool flag = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.isTrigger || hit.collider.gameObject == target)
                    continue;
                if (!flag || HIT.distance > hit.distance)
                {
                    HIT = hit;
                    flag = true;
                }
            }
            if (flag)
            {
                if (HIT.distance > 1)
                    newPosition = HIT.point - direction.normalized;
                else
                    newPosition = HIT.point;
            }
            return newPosition;
        }
    }
}
