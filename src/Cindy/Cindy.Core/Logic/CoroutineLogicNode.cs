using System.Collections;

namespace Cindy.Logic
{
    /// <summary>
    /// 协程逻辑节点，执行此节点时以协程启动此类的DoRun()方法。
    /// </summary>
    public abstract class CoroutineLogicNode : LogicNode
    {
        protected override void Run()
        {
            base.Run();
            StartCoroutine(DoRun());
        }

        /// <summary>
        /// 执行节点
        /// </summary>
        /// <returns>协程迭代器</returns>
        protected abstract IEnumerator DoRun();
    }
}
