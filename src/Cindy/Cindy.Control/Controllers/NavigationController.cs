using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Control.Controllers
{
    [AddComponentMenu("Cindy/Control/Controllers/NavigationController")]
    public class NavigationController : Controller
    {
        [Header("Parameters")]
        public float scanRadius = 10;
        public NavigationControllerAction[] actions;

        [Header("Output")]
        public ReferenceVector3 targetPosition;

        protected NavigationControllerAction a;
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
                else if (!a.IsActived(t))
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
                    foreach (NavigationControllerAction action in actions)
                    {
                        if (action.IsActived(collider.gameObject))
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

        public abstract class NavigationControllerAction : ScriptableObject
        {
            public float actionRadius = 1;

            public abstract bool IsActived(GameObject gameObject);

            public abstract void DoAction(GameObject gameObject);
        }
    }
}
