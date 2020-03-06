using UnityEngine;

namespace Cindy.UI.PositionBinders
{
    [AddComponentMenu("Cindy/UI/PositionBinders/BlockedPositionBinder")]
    public class BlockedPositionBinder : AbstractPositionBinder
    {
        public override RectTransform[] GenerateComponents(GameObject root)
        {
            return new RectTransform[0];
        }

        public override bool IsActived()
        {
            return false;
        }
    }

}
