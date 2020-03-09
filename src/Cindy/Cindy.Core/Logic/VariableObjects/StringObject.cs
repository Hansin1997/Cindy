using Cindy.Logic.ReferenceValues;
using Cindy.Storages;
using Cindy.Strings;
using System.Text;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    [AddComponentMenu("Cindy/Logic/VariableObject/StringObject")]
    public class StringObject : VariableObject<string>
    {
        protected override string TransformFrom(string value)
        {
            return value;
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/CombinedStringObject")]
    public class CombinedStringObject : StringObject
    {
        public ReferenceString[] before,after;
        public ReferenceString split;

        protected StringBuilder stringBuilder;
        protected StringBuilder StringBuilder { get { return stringBuilder != null ? stringBuilder : (stringBuilder = new StringBuilder()); } }

        public override string GetValue()
        {
            StringBuilder.Clear();
            foreach (ReferenceString referenceString in before)
                StringBuilder.Append(referenceString.Value + split.Value);
            StringBuilder.Append(value + split.Value);
            foreach (ReferenceString referenceString in after)
                StringBuilder.Append(referenceString.Value + split.Value);
            return StringBuilder.ToString();
        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/StringObjectFromStringSource")]
    public class StringObjectFromStringSource : StringObject
    {
        public ReferenceString stringKey;
        public StringSource stringSource;

        public override string GetValue()
        {
            value = stringSource != null ? stringSource.GetString(stringKey.Value, stringKey.Value) : stringKey.Value;
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }

    [AddComponentMenu("Cindy/Logic/VariableObject/Strings/Name")]
    public class Name : StringObject
    {
        public GameObject target;

        public override void SetValue(string value)
        {

        }

        public override string GetValue()
        {
            if (target == null)
                value = gameObject.name;
            else
                value = target.name;
            return base.GetValue();
        }
    }
}
