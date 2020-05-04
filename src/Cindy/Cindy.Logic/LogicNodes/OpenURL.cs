using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes
{
    /// <summary>
    /// 打开网页
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNodes/OpenURL")]
    public class OpenURL : LogicNode
    {
        public ReferenceString URL;

        protected override void Run()
        {
            Application.OpenURL(URL.Value);
        }
    }
}
