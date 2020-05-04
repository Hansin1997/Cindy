using UnityEngine;

namespace Cindy.Control.Cameras
{
    /// <summary>
    /// 摄像机行为
    /// </summary>
    public abstract class CameraBehaviour : ScriptableObject
    {
        /// <summary>
        /// 当摄像机聚焦时
        /// </summary>
        /// <param name="camera">摄像机</param>
        /// <param name="attachment">摄像机控制器</param>
        public abstract void OnCameraFocus(Camera camera, CameraController attachment);
        /// <summary>
        /// 摄像机更新
        /// </summary>
        /// <param name="camera">摄像机</param>
        /// <param name="attachment">摄像机控制器</param>
        /// <param name="deltaTime">变化时间</param>
        public abstract void OnCameraUpdate(Camera camera, CameraController attachment, float deltaTime);
        /// <summary>
        /// 当摄像机失焦时
        /// </summary>
        /// <param name="camera">摄像机</param>
        /// <param name="attachment">摄像机控制器</param>
        public abstract void OnCameraBlur(Camera camera, CameraController attachment);
    }
}
