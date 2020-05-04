using UnityEngine;

namespace Cindy.Control.Cameras
{
    /// <summary>
    /// 基础摄像机行为
    /// </summary>
    public abstract class BaseCameraBehaviour : CameraBehaviour
    {
        /// <summary>
        /// 相机移动速度函数，相对于新位置的距离。
        /// </summary>
        [Header("Base Config")]
        public AnimationCurve cameraMovingSpeed;
        /// <summary>
        /// 相机注视速度，相对于角度偏差。
        /// </summary>
        public AnimationCurve cameraLookSpeed;
        /// <summary>
        /// 是否进行射线检测遮挡
        /// </summary>
        public bool raycast = true;
        /// <summary>
        /// 是否观察目标
        /// </summary>
        public bool lookAtTarget = true;

        public override void OnCameraBlur(Camera camera, CameraController attachment)
        {

        }

        public override void OnCameraFocus(Camera camera, CameraController attachment)
        {

        }

        public override void OnCameraUpdate(Camera camera, CameraController attachment, float deltaTime)
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

        /// <summary>
        /// 计算新的位置
        /// </summary>
        /// <param name="camera">摄像机</param>
        /// <param name="attachment">摄像机控制器</param>
        /// <param name="deltaTime">变化时间</param>
        /// <returns>新的相机位置</returns>
        protected abstract Vector3 GetPosition(Camera camera, CameraController attachment, float deltaTime);

        /// <summary>
        /// 处理射线检测
        /// </summary>
        /// <param name="camera">摄像机</param>
        /// <param name="attachment">摄像机控制器</param>
        /// <param name="newPosition">新的位置</param>
        /// <param name="deltaTime">变化时间</param>
        /// <returns>处理后的新位置</returns>
        protected virtual Vector3 ProcessRaycast(Camera camera, CameraController attachment, Vector3 newPosition, float deltaTime)
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
