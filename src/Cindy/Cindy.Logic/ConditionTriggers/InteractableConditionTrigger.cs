using UnityEngine;
using UnityEngine.UI;

namespace Cindy.Logic.ConditionTriggers
{
    [AddComponentMenu("Cindy/Logic/ConditionTriggers/InteractableConditionTrigger")]
    public class InteractableConditionTrigger : ConditionTrigger
    {
        public Selectable[] selectables;

        protected override void Handle(bool val)
        {
            foreach (Selectable selectable in selectables)
                selectable.interactable = val;
        }
    }
}
