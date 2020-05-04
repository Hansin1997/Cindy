using Cindy.Util;

namespace Cindy.Control
{
    /// <summary>
    /// 抽象控制器
    /// </summary>
    public abstract class AbstractController : Attachment
    {
        /// <summary>
        /// 所属控制器栈
        /// </summary>
        public AbstractControllerStack ControllerStack { get; internal set; }
        /// <summary>
        /// 当控制器被选中
        /// </summary>
        public abstract void OnControllerSelect();
        /// <summary>
        /// 控制器更新
        /// </summary>
        /// <param name="deltaTime">变化时间</param>
        public abstract void OnControllerUpdate(float deltaTime);
        /// <summary>
        /// 当控制器取消选中
        /// </summary>
        public abstract void OnControllerUnselect();
    }
}