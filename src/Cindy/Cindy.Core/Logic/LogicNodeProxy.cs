using Cindy.Logic.ReferenceValues;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy.Logic
{
    /// <summary>
    /// 逻辑节点代理
    /// </summary>
    [AddComponentMenu("Cindy/Logic/LogicNodeProxy")]
    public class LogicNodeProxy : LogicNode
    {
        /// <summary>
        /// 上下文
        /// </summary>
        [Header("LogicNodeProxy")]
        public Context context;
        /// <summary>
        /// 被代理的节点名称数组
        /// </summary>
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