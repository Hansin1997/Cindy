using UnityEngine;

namespace Cindy.Logic.Conditions
{
    /// <summary>
    /// 是否移动设备条件
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Conditions/IsMobilePlatform")]
    public class IsMobilePlatform : Condition
    {
        public override bool Check()
        {
            return Application.isMobilePlatform;
        }
    }
}
