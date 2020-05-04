using Cindy.Logic.ReferenceValues;
using UnityEngine;

namespace Cindy.Logic.LogicNodes
{
    /// <summary>
    /// 终止器，用于退出游戏
    /// </summary>
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
