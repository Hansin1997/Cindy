using UnityEngine;

namespace Cindy.Control.Cameras
{
    public abstract class BaseCameraBehaviour : CameraBehaviour
    {
        [Header("Base Config")]
        public AnimationCurve cameraMovingSpeed;
        public AnimationCurve cameraLookSpeed;
        public bool raycast = true;
        public bool lookAtTarget = true;

        public override void OnCameraBlur(Camera camera, CameraBehaviourAttachment attachment)
        {

        }

        public override void OnCameraFocus(Camera camera, CameraBehaviourAttachment attachment)
        {

        }

        public override void OnCameraUpdate(Camera camera, CameraBehaviourAttachment attachment, float deltaTime)
        {
            Vector3 newPosition = GetPosition(camera, attachment, deltaTime);
            if (raycast)
                newPosition = ProcessRaycast(camera, attachment, newPosition, deltaTime);

            Vector3 dir = camera.transform.position - newPosition;
            float t = cameraMovingSpeed.Evaluate(dir.magnitude);
            camera.transform.position = Vector3.Lerp(camera.transform.position, newPosition, t);
            if (lookAtTarget)
            {
                dir = attachment.transform.position - camera.transform.position;
                float angle = Vector3.Angle(camera.transform.forward, dir);
                t = cameraLookSpeed.Evaluate(angle);
                Vector3 tmp = Vector3.Lerp(camera.transform.forward, dir, t);
                camera.transform.forward = tmp;
            }
        }

        protected abstract Vector3 GetPosition(Camera camera, CameraBehaviourAttachment attachment, float deltaTime);

        protected virtual Vector3 ProcessRaycast(Camera camera, CameraBehaviourAttachment attachment, Vector3 newPosition, float deltaTime)
        {
            Vector3 direction = newPosition - attachment.transform.position;
            RaycastHit[] hits = Physics.RaycastAll(attachment.transform.position, direction, direction.magnitude);
            RaycastHit HIT = default;
            bool flag = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.isTrigger || hit.collider.gameObject == attachment.transform)
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
