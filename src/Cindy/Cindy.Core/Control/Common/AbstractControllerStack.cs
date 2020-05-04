using Cindy.Util;
using UnityEngine;

namespace Cindy.Control
{
    /// <summary>
    /// 抽象控制器栈
    /// </summary>
    public abstract class AbstractControllerStack : AttachmentContainer
    {
        /// <summary>
        /// 控制器更新类型
        /// </summary>
        [Header("Controller Stack")]
        public UpdateType updateType = UpdateType.OnFixedUpdate;

        protected AbstractController selectedController; // 当前选中的控制器

        protected virtual void Update()
        {
            if (updateType == UpdateType.OnUpdate)
                DoUpdate(Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            if (updateType == UpdateType.OnFixedUpdate)
                DoUpdate(Time.fixedDeltaTime);
        }

        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="deltaTime"></param>
        protected virtual void DoUpdate(float deltaTime)
        {
            AbstractController top = Peek<AbstractController>();
            if (top != null)
            {
                top.ControllerStack = this;
                if (top != selectedController)
                {
                    if (selectedController != null)
                        OnControllerUnselect(selectedController);
                    OnControllerSelect(top);
                }
                OnControllerUpdate(top, deltaTime);
            }
            selectedController = top;
        }

        /// <summary>
        /// 控制器选中
        /// </summary>
        /// <param name="controller"></param>
        protected virtual void OnControllerSelect(AbstractController controller)
        {
            controller.OnControllerSelect();
        }

        /// <summary>
        /// 控制器更新
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="deltaTime"></param>
        protected virtual void OnControllerUpdate(AbstractController controller, float deltaTime)
        {
            controller.OnControllerUpdate(deltaTime);
        }

        /// <summary>
        /// 控制器取消选中
        /// </summary>
        /// <param name="controller"></param>
        protected virtual void OnControllerUnselect(AbstractController controller)
        {
            controller.OnControllerUnselect();
        }

        /// <summary>
        /// 更新类型
        /// </summary>
        public enum UpdateType
        {
            OnUpdate,
            OnFixedUpdate,
            Node
        }
    }
}