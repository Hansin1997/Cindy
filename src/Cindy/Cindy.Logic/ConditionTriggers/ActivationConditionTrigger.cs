using UnityEngine;

namespace Cindy.Logic.ConditionTriggers
{
    [AddComponentMenu("Cindy/Logic/ConditionTriggers/ActivationConditionTrigger")]
    public class ActivationConditionTrigger : ConditionTrigger
    {
        [Header("Targets")]
        public GameObject[] gameObjects;
        public Behaviour[] behaviours;

        protected override void Handle(bool val)
        {
            foreach (GameObject gameObject in gameObjects)
                gameObject.SetActive(val);
            foreach (Behaviour behaviour in behaviours)
                behaviour.enabled = val;
        }
    }
}
