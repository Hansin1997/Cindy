using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes
{
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
