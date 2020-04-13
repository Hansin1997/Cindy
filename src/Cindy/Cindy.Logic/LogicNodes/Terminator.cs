using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes
{
    [AddComponentMenu("Cindy/Logic/LogicNodes/Terminator")]
    public class Terminator : LogicNode
    {
        [Header("Terminator")]
        public ReferenceInt exitCode;

        protected override void Run()
        {
            Application.Quit(exitCode.Value);
        }
    }
}
