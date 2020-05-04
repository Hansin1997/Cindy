using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    /// <summary>
    /// 抽象字符串条件
    /// </summary>
    public abstract class StringCondition : Condition
    {
        public StringObject stringObject;
    }

    /// <summary>
    /// 字符串非空条件
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Conditions/StringNotEmptyCondition")]
    public class StringNotEmptyCondition : StringCondition
    {
        /// <summary>
        /// 是否剔除首尾空字符
        /// </summary>
        public bool trim;

        public override bool Check()
        {
            return stringObject != null && stringObject.Value != null &&
                (trim ? stringObject.Value.Trim() : stringObject.Value).Length > 0;
        }
    }

    /// <summary>
    /// 字符串比较条件
    /// </summary>
    [AddComponentMenu("Cindy/Logic/Conditions/StringCompareCondition")]
    public class StringCompareCondition : StringCondition
    {
        /// <summary>
        /// 操作符
        /// </summary>
        public Operator @operator;
        /// <summary>
        /// 比较对象
        /// </summary>
        public ReferenceString target;

        public override bool Check()
        {
            if (stringObject == null || stringObject.Value == null)
                return false;
            switch (@operator)
            {
                default:
                case Operator.Equals:
                    return stringObject.Value.Equals(target.Value);
                case Operator.NotEquals:
                    return !stringObject.Value.Equals(target.Value);
                case Operator.IsMatch:
                    return Regex.IsMatch(stringObject.Value, target.Value);
                case Operator.NotMatch:
                    return !Regex.IsMatch(stringObject.Value, target.Value);
            }
        }

        public enum Operator
        {
            /// <summary>
            /// 等于
            /// </summary>
            Equals,
            /// <summary>
            /// 不等于
            /// </summary>
            NotEquals,
            /// <summary>
            /// 匹配
            /// </summary>
            IsMatch,
            /// <summary>
            /// 不匹配
            /// </summary>
            NotMatch
        }
    }
}
