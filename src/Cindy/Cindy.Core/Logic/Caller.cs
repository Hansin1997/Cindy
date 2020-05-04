using UnityEngine;

namespace Cindy.Logic
{
    /// <summary>
    /// 通用触发器
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Triggers/Caller")]
    public class Caller : AbstractMonoMethodTrigger
    {
        public bool justOnce = false;

        protected bool done = false;

        protected override bool Handle()
        {
            return !(justOnce && done);
        }
    }
}
