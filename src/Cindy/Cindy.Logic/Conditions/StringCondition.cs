using Cindy.Logic.ReferenceValues;
using Cindy.Logic.VariableObjects;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Cindy.Logic.Conditions
{
    public abstract class StringCondition : Condition
    {
        public StringObject stringObject;
    }

    [AddComponentMenu("Cindy/Logic/Conditions/StringNotEmptyCondition")]
    public class StringNotEmptyCondition : StringCondition
    {
        public bool trim;

        public override bool Check()
        {
            return stringObject != null && stringObject.Value != null &&
                (trim ? stringObject.Value.Trim() : stringObject.Value).Length > 0;
        }
    }

    [AddComponentMenu("Cindy/Logic/Conditions/StringCompareCondition")]
    public class StringCompareCondition : StringCondition
    {
        public Operator @operator;
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
            Equals,
            NotEquals,
            IsMatch,
            NotMatch
        }
    }
}
