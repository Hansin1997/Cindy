using System.Collections;

namespace Cindy.Logic
{
    public abstract class CoroutineLogicNode : LogicNode
    {
        protected override void Run()
        {
            base.Run();
            StartCoroutine(DoRun());
        }

        protected abstract IEnumerator DoRun();
    }
}
