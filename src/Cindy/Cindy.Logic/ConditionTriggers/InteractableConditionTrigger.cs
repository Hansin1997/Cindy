using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.ConditionTriggers
{
    [AddComponentMenu("Cindy/Logic/Triggers/InteractableConditionTrigger")]
    public class InteractableConditionTrigger : ConditionTrigger
    {
        public Selectable[] selectables;

        protected override void OnConditionValueChanged(bool val)
        {
            foreach (Selectable selectable in selectables)
                selectable.interactable = val;
        }
    }
}
