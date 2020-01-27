using UnityEngine;

namespace Cindy.Logic.Methods
{

    [AddComponentMenu("Cindy/Logic/Methods/Terminator")]
    public class Terminator : LogicNode
    {
        public int exitCode;

        protected override void Run()
        {
            Application.Quit(exitCode);
        }
    }
}
