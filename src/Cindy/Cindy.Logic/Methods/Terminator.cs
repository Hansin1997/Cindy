using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.Methods
{

    [AddComponentMenu("Cindy/Logic/Methods/Terminator")]
    public class Terminator : LogicNode
    {
        public ReferenceInt exitCode;

        protected override void Run()
        {
            Application.Quit(exitCode.Value);
        }
    }
}
