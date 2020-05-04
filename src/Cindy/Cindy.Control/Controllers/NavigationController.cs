using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Control.Controllers
{
    /// <summary>
    /// 导航控制器
    /// </summary>
    [AddComponentMenu("Cindy/Control/Controllers/NavigationController")]
    public class NavigationController : Controller
    {
        /// <summary>
        /// 扫描距离
        /// </summary>
        [Header("Parameters")]
        public float scanRadius = 10;
        /// <summary>
        /// 行为数组
        /// </summary>
        public NavigationControllerBehaviour[] behaviours;
        
        /// <summary>
        /// 新位置输出
        /// </summary>
        [Header("Output")]
        public ReferenceVector3 targetPosition;

        protected NavigationControllerBehaviour a;
        protected GameObject t;

        public override void OnControllerSelect()
        {

        }

        public override void OnControllerUnselect()
        {

        }

        public override void OnControllerUpdate(float deltaTime)
        {
            if (a != null)
            {
                if (t == null)
                    a = null;
                else if (!a.IsMatch(t))
                {
                    a = null;
                    t = null;
                }
            }
            if (t == null || a == null)
            {
                t = null;
                a = null;
                Collider[] colliders = Physics.OverlapSphere(transform.position, scanRadius);
                foreach (Collider collider in colliders)
                {
                    foreach (NavigationControllerBehaviour action in behaviours)
                    {
                        if (action.IsMatch(collider.gameObject))
                        {
                            a = action;
                            t = collider.gameObject;
                            break;
                        }
                    }
                }
            }
            if (t == null)
            {
                targetPosition.Value = transform.position + transform.forward * scanRadius;
            }
            else
            {
                Vector3 d = transform.position - t.transform.position;
                if (d.magnitude <= a.actionRadius)
                {
                    a.DoAction(t);
                    a = null;
                    t = null;
                }
                else
                {
                    targetPosition.Value = t.transform.position;
                }
            }
        }

        /// <summary>
        /// 导航控制器行为
        /// </summary>
        public abstract class NavigationControllerBehaviour : ScriptableObject
        {
            /// <summary>
            /// 执行距离
            /// </summary>
            public float actionRadius = 1;
            
            /// <summary>
            /// 判断对象是否匹配
            /// </summary>
            /// <param name="gameObject">对象</param>
            /// <returns>是否匹配</returns>
            public abstract bool IsMatch(GameObject gameObject);

            /// <summary>
            /// 执行行为
            /// </summary>
            /// <param name="gameObject">对象</param>
            public abstract void DoAction(GameObject gameObject);
        }
    }
}
