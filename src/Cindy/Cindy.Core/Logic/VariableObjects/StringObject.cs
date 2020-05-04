using Cindy.Logic.ReferenceValues;
using System;
using System.Text;
using UnityEngine;

namespace Cindy.Logic.VariableObjects
{
    /// <summary>
    /// 字符串变量
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/StringObject (String)")]
    public class StringObject : VariableObject<string>
    {

    }

    /// <summary>
    /// 组合字符串
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/CombinedStringObject (String)")]
    public class CombinedStringObject : StringObject
    {
        public ReferenceString[] before, after;
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
            value = StringBuilder.ToString();
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }

    /// <summary>
    /// 格式化字符串
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/StringFormat (String)")]
    public class StringFormat : StringObject
    {
        /// <summary>
        /// 格式
        /// </summary>
        public ReferenceString format;
        /// <summary>
        /// 参数
        /// </summary>
        public Behaviour[] objects;

        public OnException onException = OnException.LogWarning;

        public override string GetValue()
        {
            try
            {
                value = string.Format(format.Value, objects);
            }catch(Exception e)
            {
                switch (onException)
                {
                    case OnException.LogError:
                        Debug.LogError(e, this);
                        break;
                    case OnException.LogWarning:
                        Debug.LogWarning(e, this);
                        break;
                    case OnException.Log:
                        Debug.Log(e, this);
                        break;
                    case OnException.SetExceptionStringToValue:
                        value = e.Message;
                        break;
                    default:
                    case OnException.None:
                        break;

                }
            }
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }

        public enum OnException
        {
            Log,
            LogWarning,
            LogError,
            SetExceptionStringToValue,
            None
        }
    }

    /// <summary>
    /// 物体名称字符串
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/Name (String)")]
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

    /// <summary>
    /// 整型转字符串
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/IntToString (String)")]
    public class IntToString : StringObject
    {
        [Header("IntToString")]
        public ReferenceInt target;

        public override string GetValue()
        {
            value = target == null ? "" : target.Value.ToString();
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }

    /// <summary>
    /// 浮点型转字符串
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/FloatToString (String)")]
    public class FloatToString : StringObject
    {
        [Header("FloatToString")]
        public ReferenceFloat target;

        public override string GetValue()
        {
            value = target == null ? "" : target.Value.ToString();
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }

    /// <summary>
    /// 双精度浮点型转字符串
    /// </summary>
    [AddComponentMenu("Cindy/Logic/VariableObject/DoubleToString (String)")]
    public class DoubleToString : StringObject
    {
        [Header("DoubleToString")]
        public ReferenceDouble target;

        public override string GetValue()
        {
            value = target == null ? "" : target.Value.ToString();
            return base.GetValue();
        }

        public override void SetValue(string value)
        {

        }
    }
}
