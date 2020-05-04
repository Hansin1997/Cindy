using Cindy.Logic.ReferenceValues;

namespace Cindy.Logic
{
    /// <summary>
    /// 抽象条件类
    /// </summary>
    public abstract class Condition : NamedBehaviour
    {
        /// <summary>
        /// 条件名称
        /// </summary>
        public ReferenceString conditionName;

        /// <summary>
        /// 检查条件是否成立
        /// </summary>
        /// <returns>条件是否成立</returns>
        public abstract bool Check();

        protected override string GetName()
        {
            return conditionName.Value;
        }

    }
}
