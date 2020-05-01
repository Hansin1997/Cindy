using Cindy.Logic.ReferenceValues;

namespace Cindy.Logic
{
    public abstract class Condition : NamedObject
    {
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
