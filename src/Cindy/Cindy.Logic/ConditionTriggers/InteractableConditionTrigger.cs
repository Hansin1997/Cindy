using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.ConditionTriggers
{
    /// <summary>
    /// 条件激活触发器
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Triggers/InteractableConditionTrigger")]
    public class InteractableConditionTrigger : ConditionTrigger
    {
        /// <summary>
        /// 管理对象
        /// </summary>
        public Selectable[] selectables;

        protected override void OnConditionValueChanged(bool val)
        {
            foreach (Selectable selectable in selectables)
                selectable.interactable = val;
        }
    }
}
