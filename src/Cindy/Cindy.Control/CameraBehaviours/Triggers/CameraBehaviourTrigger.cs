using UnityEngine;

namespace Cindy.Control.CameraBehaviours.Triggers
{

    [AddComponentMenu("Cindy/Control/Camera/CameraBehaviourTrigger", 99)]
    [RequireComponent(typeof(Collider))]
    public class CameraBehaviourTrigger : MonoBehaviour
    {

        public string targetName = "";
        public CameraBehaviour cameraBehaviour;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals(targetName) && cameraBehaviour)
            {
                cameraBehaviour.target = other.gameObject.transform;
                cameraBehaviour.Attach();
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (cameraBehaviour)
            {
                cameraBehaviour.Detach();
            }
        }
    }
}
