using UnityEngine;

namespace Cindy.Logic.Conditions
{
    [AddComponentMenu("Cindy/Logic/Conditions/IsMobilePlatform")]
    public class IsMobilePlatform : Condition
    {
        public override bool Check()
        {
            return Application.isMobilePlatform;
        }
    }
}
