using Cindy.Logic.ReferenceValues;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Logic
{
    [AddComponentMenu("Cindy/Logic/LogicNodeProxy")]
    public class LogicNodeProxy : LogicNode
    {
        [Header("LogicNodeProxy")]
        public Context context;
        public ReferenceString[] targets;

        protected override void Run()
        {
            if (targets == null || targets.Length == 0 || context == null)
                return;
            LogicNode[] nodes = context.GetLogicNodes<LogicNode>();
            HashSet<string> names = new HashSet<string>();
            foreach(ReferenceString target in targets)
            {
                names.Add(target.Value);
            }
            foreach(LogicNode node in nodes)
            {
                if (names.Contains(node.nodeName.Value))
                    node.Execute();
            }
        }

    }
}
