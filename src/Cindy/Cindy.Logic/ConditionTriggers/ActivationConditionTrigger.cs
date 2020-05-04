using UnityEngine;

namespace Cindy.Logic.ConditionTriggers
{
    /// <summary>
    /// 条件激活触发器
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Triggers/ActivationConditionTrigger")]
    public class ActivationConditionTrigger : ConditionTrigger
    {
        /// <summary>
        /// 被管理的目标物体
        /// </summary>
        [Header("Targets")]
        public GameObject[] gameObjects;
        /// <summary>
        /// 被管理的目标对象
        /// </summary>
        public Behaviour[] behaviours;

        protected override void OnConditionValueChanged(bool val)
        {
            foreach (GameObject gameObject in gameObjects)
                gameObject.SetActive(val);
            foreach (Behaviour behaviour in behaviours)
                behaviour.enabled = val;
        }
    }
}
